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
    [Method("Nonburger")]
    public async Task Nonburger(MessageContext context)
    {
        if (!string.IsNullOrEmpty(context.Message))
        {
            if (Json.Products[1].Media.Exist)
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
                var fileBytes = File.ReadAllBytes("C://Users//Muhammadjon//MarketPlace//DeliveryBot//DeliveryBot//wwwroot//Rasmlar//2.png");
                var ms = new MemoryStream(fileBytes);
                Stream s = ms;
                context.User.Step = (int)UStep.Save;
                context.User.CurrentProductsId = Json.Products[1].Id;
                await Context.SaveChangesAsync();
                await TelegramBotService.SendMessage(chatId: context.User.ChatId,
                    message: Json.Products[1].Description, image: s , new InlineKeyboardMarkup(rows));
            }
        }
    }   [Method("Donar")]
    public async Task Donar(MessageContext context)
    {
        if (!string.IsNullOrEmpty(context.Message))
        {
            if (Json.Products[2].Media.Exist)
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
                var fileBytes = File.ReadAllBytes("C://Users//Muhammadjon//MarketPlace//DeliveryBot//DeliveryBot//wwwroot//Rasmlar//3.png");
                var ms = new MemoryStream(fileBytes);
                Stream s = ms;
                context.User.Step = (int)UStep.Save;
                context.User.CurrentProductsId = Json.Products[2].Id;
                await Context.SaveChangesAsync();
                await TelegramBotService.SendMessage(chatId: context.User.ChatId,
                    message: Json.Products[2].Description, image: s , new InlineKeyboardMarkup(rows));
            }
        }
    }
 [Method("Donar")]
    public async Task HotDog(MessageContext context)
    {
        if (!string.IsNullOrEmpty(context.Message))
        {
            if (Json.Products[3].Media.Exist)
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
                var fileBytes = File.ReadAllBytes("C://Users//Muhammadjon//MarketPlace//DeliveryBot//DeliveryBot//wwwroot//Rasmlar//4.png");
                var ms = new MemoryStream(fileBytes);
                Stream s = ms;
                context.User.Step = (int)UStep.Save;
                context.User.CurrentProductsId = Json.Products[3].Id;
                await Context.SaveChangesAsync();
                await TelegramBotService.SendMessage(chatId: context.User.ChatId,
                    message: Json.Products[3].Description, image: s , new InlineKeyboardMarkup(rows));
            }
        }
    }
    [Method("⬅️ Ortga")]
    public async Task SendMenu(MessageContext context)
    {
        if (!string.IsNullOrEmpty(context.Message))
        {
            await TelegramBotService.SendMessage(context.User.ChatId, "Menuni tanlang", TelegramBotService.GenerateKeyboard(
                new List<string>()
                {
                    "🍔🍟 FastFood",
                    "🍨 Muzqaymoqlar",
                }));
            context.User.Step = (int)UStep.FastFoodMenu;
            await Context.SaveChangesAsync();
        }
    }
    [Method]
    public async Task NotButton(MessageContext context)
    {
        await TelegramBotService.SendMessage(context.User.ChatId, "Berilgan menudan tanlang iltimos",
            TelegramBotService.GenerateKeyboard(new List<string>()
            {
                "Lavash",
                "Nonburger",
                "Hotdog",
                "Donar",
            }));
        context.User.Step = (int)UStep.FastFoodChoice;
        await Context.SaveChangesAsync();
    }
}