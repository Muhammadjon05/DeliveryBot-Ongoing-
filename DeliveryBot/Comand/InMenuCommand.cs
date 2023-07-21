using Delivery.Data.Context;
using Delivery.Data.Entities;
using DeliveryBot.Common;
using DeliveryBot.Enums;
using DeliveryBot.JsonSerialization;
using DeliveryBot.Services;
using JFA.Telegram;
using Microsoft.EntityFrameworkCore;

namespace DeliveryBot.Comand;

[Command((int)UStep.Menu)]
public class InMenuCommand : CommandHandler
{
    
    private readonly ProductJson _json;
    public InMenuCommand(DeliveryDbContext context, TelegramBotService telegramBotService, ProductJson json)
        : base(context, telegramBotService)
    {
        _json = json;
    }

    [Method("🍽️📝 Menu")]
    public async Task SendMenu(MessageContext context)
    {
        if (!string.IsNullOrEmpty(context.Message))
        {
            await TelegramBotService.SendMessage(context.User.ChatId, "Menuni tanlang", TelegramBotService.GetKeyboard(
                new List<string>()
                {
                    "🍔🍟 FastFood",
                    "🍨 Muzqaymoqlar",
                    "⬅️ Ortga"
                }));
            context.User.Step = (int)UStep.FastFoodMenu;
            await Context.SaveChangesAsync();
        }
    }

    [Method("🛒 Buyurtmalar")]
    public async Task SendOrder(MessageContext context)
    {
        var orders  = await Context.OrderItem.Where
            (i => i.UserId == context.User.Id).ToListAsync();
       
        var groupedOrders = orders
            .GroupBy(order => new { order.UserId, order.ProductId })
            .Select(group => new
            {
                UserId = group.Key.UserId,
                ProductId = group.Key.ProductId,
                TotalQuantity = group.Sum(order => order.Quantity)
            });

        var messages = new List<string>();
        foreach (var orderGroup in groupedOrders)
        {
            var productName = _json.Products.FirstOrDefault(i => i.Id == orderGroup.ProductId).Name;
            var productPrice = _json.Products.FirstOrDefault(i => i.Id == orderGroup.ProductId).Price;
            var productQuantity = orderGroup.TotalQuantity; 
            var habar = $"Mahsulot: {productName}\n{productPrice} x {productQuantity} = {productPrice*productQuantity}\n";
            messages.Add(habar);
        }
        string empty = String.Empty;
        foreach (var message in messages)
        {
            empty += message;
        }
        await TelegramBotService.SendMessage(context.User.ChatId, empty,TelegramBotService.GetKeyboard(new List<string>()
        {
            "⬅️ Ortga"
        }));
        
        context.User.Step = (int)UStep.OrdersMenu;
        await Context.SaveChangesAsync();
    }

    [Method]
    public async Task NotButton(MessageContext context)
    {
        if (!string.IsNullOrEmpty(context.Message))
        {
            await TelegramBotService.SendMessage(context.User.ChatId,
                "Notog'ri buyruq kiritildi iltimos menudan birini kiriting", TelegramBotService.GetKeyboard(
                    new List<string>()
                    {
                        "🍽️📝 Menu",
                        "🛒 Buyurtmalar",
                        "🔧 Sozlamalar"
                    }));
            context.User!.Step = (int)UStep.Menu;
            await Context.SaveChangesAsync();
        }
        
    }
}