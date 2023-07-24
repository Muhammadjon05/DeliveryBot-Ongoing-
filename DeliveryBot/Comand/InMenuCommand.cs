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
            await TelegramBotService.SendMessage(context.User.ChatId, "Menuni tanlang", TelegramBotService.GenerateKeyboard(
                new List<string>()
                {
                    "🍔🍟 FastFood",
                }));
            context.User.Step = (int)UStep.FastFoodMenu;
            await Context.SaveChangesAsync();
        }
    }

    [Method("🛒 Buyurtmalar")]
    public async Task SendOrder(MessageContext context)
    {
        var orders  = await Context.OrderItem.Where
            (i => i.UserId == context.User!.Id).ToListAsync();
        if (orders.Count > 0)
        {
            var groupedOrders = orders
                .GroupBy(order => new { order.UserId, order.ProductId })
                .Select(group => new
                {
                    UserId = group.Key.UserId,
                    ProductId = group.Key.ProductId,
                    TotalQuantity = group.Sum(order => order.Quantity)
                });
            var messages = new List<string>();
            decimal totalPrice = Decimal.Zero;
            var msg = "Sizning buyurtmangiz:\n\nTo'lov usuli: Naqt\n";
            foreach (var orderGroup in groupedOrders)
            {
                var productName = _json.Products.FirstOrDefault(i => i.Id == orderGroup.ProductId)!.Name;
                var productPrice = _json.Products.FirstOrDefault(i => i.Id == orderGroup.ProductId)!.Price;
                var productQuantity = orderGroup.TotalQuantity;
                totalPrice += productPrice * productQuantity;
                msg += $"Mahsulot: {productName}\n{productPrice} x {productQuantity} = {productPrice*productQuantity}\n";
             
            }
            var empty = msg + $"Umumiy: {totalPrice}\n";
            await TelegramBotService.SendMessage(context.User.ChatId, empty,TelegramBotService.GenerateKeyboard(new List<string>()
            {
                "❎ Buyurmalarni bekor qilish",
            }));
        }
        else
        {
            await TelegramBotService.SendMessage(context.User.ChatId, "Sizda hech qanday buyurtmalaringiz yoq",TelegramBotService.GenerateKeyboard(new List<string>()
            {
            }));
        }
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
                    }));
            context.User!.Step = (int)UStep.Menu;
            await Context.SaveChangesAsync();
        }
        
    }
}