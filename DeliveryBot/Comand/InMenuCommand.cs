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

        var list = new List<Product?>();
        string message = String.Empty;
        foreach (var order in orders)
        {
            if (_json.Products.Any(i => i.Id == order.ProductId))
            {
                var product = _json.Products.FirstOrDefault(i => i.Id == order.ProductId);
                message = "Bizning hizmatlarimizdan foydalanganigiz uchun rahmat" +
                          $"Siznning buyurmalaringiz soni : {orders.Count}" +
                          $"Buyurtma mahsulot nomi:{product.Name}." +
                          $"Mahsulot soni:{order.Quantity}." +
                          $"Mahsulot narxi: {product.Price}" +
                          $"Umumiy narxi:{product.Price * order.Quantity}";
            }
            else
            {
                message = "Siz hali mahsulot buyurtma qilmadingiz";
            }
        }
        await TelegramBotService.SendMessage(context.User.ChatId, message,TelegramBotService.GetKeyboard(new List<string>()
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