using Delivery.Data.Context;
using Delivery.Data.Entities;
using DeliveryBot.Common;
using DeliveryBot.Enums;
using DeliveryBot.JsonSerialization;
using DeliveryBot.Services;
using JFA.Telegram;
using Microsoft.AspNetCore.Components.Forms;
using Telegram.Bot.Types.ReplyMarkups;

namespace DeliveryBot.Comand;

[Command((int)UStep.FastFoodChoice)]
public class FastFoodChoice : CommandHandler
{
    private readonly ProductJson Json;
    public FastFoodChoice(DeliveryDbContext context, TelegramBotService telegramBotService, ProductJson json) : base(context, telegramBotService)
    {
        Json = json;
    }
    
    [Method("Lavash")]
    public async Task Lavash(MessageContext context)
    {
        if (!string.IsNullOrEmpty(context.Message))
        {
            if (Json.Products[0].Media.Exist)
            {
                var rows = new List<List<InlineKeyboardButton>>();
                var row = new List<InlineKeyboardButton>()
                {
                    InlineKeyboardButton.WithCallbackData("-"),
                    InlineKeyboardButton.WithCallbackData($"{context.User.NumberOfOrder}"),
                    InlineKeyboardButton.WithCallbackData("+"),
                };
                var extra = new List<InlineKeyboardButton>()
                {
                    InlineKeyboardButton.WithCallbackData("📥 Savatga qo'shish")
                };
                rows.Add(row);
                rows.Add(extra);
                var fileBytes = File.ReadAllBytes("C://Users//Muhammadjon//MarketPlace//DeliveryBot//DeliveryBot//wwwroot//Rasmlar//1.png");
                var ms = new MemoryStream(fileBytes);
                Stream s = ms;
                context.User.Step = (int)UStep.Save;
                context.User.CurrentProductsId = Json.Products[0].Id;
                await Context.SaveChangesAsync();
                await TelegramBotService.SendMessage(chatId: context.User.ChatId,
                    message: Json.Products[0].Description, image: s , new InlineKeyboardMarkup(rows));
                
            }
        }
    }

    [Method]
    public async Task NotButton(MessageContext context)
    {
        await TelegramBotService.SendMessage(context.User.ChatId, "Berilgan menudan tanlang iltimos",
            TelegramBotService.GetKeyboard(new List<string>()
            {
                "Lavash",
                "Nonburger",
                "Hotdog",
                "Donar",
                "⬅️ Ortga"
            }));
        context.User.Step = (int)UStep.FastFoodMenu;
        await Context.SaveChangesAsync();
    }
}