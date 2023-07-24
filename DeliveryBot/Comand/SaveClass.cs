using Delivery.Data.Context;
using Delivery.Data.Entities;
using DeliveryBot.Common;
using DeliveryBot.Enums;
using DeliveryBot.JsonSerialization;
using DeliveryBot.Services;
using JFA.Telegram;
using Telegram.Bot.Types.ReplyMarkups;

namespace DeliveryBot.Comand;

[Command((int)UStep.Save)]
public class SaveClass: CommandHandler
{
    private readonly ProductJson _json;
    public SaveClass(DeliveryDbContext context,
        TelegramBotService telegramBotService, 
        ProductJson json) : base(context, telegramBotService)
    {
        _json = json;
    }

    [Method("+")]
    public async Task PlusSign(MessageContext context)
    {
        context.User.NumberOfOrder++;
        await Context.SaveChangesAsync();
        if (!string.IsNullOrEmpty(context.Message))
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
           await TelegramBotService.EditMessageButtons(context.User.ChatId,
                context.MessageId, reply: new InlineKeyboardMarkup(rows));
        }
    }
    [Method("-")]
    public async Task MinusSign(MessageContext context)
    {
        if (context.User.NumberOfOrder <= 1)
        {
            if (!string.IsNullOrEmpty(context.Message))
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
                await TelegramBotService.EditMessageButtons(context.User.ChatId,
                    context.MessageId, reply: new InlineKeyboardMarkup(rows));
            }
        }
        else
        {
            context.User.NumberOfOrder--;
            await Context.SaveChangesAsync();
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
            await TelegramBotService.EditMessageButtons(context.User.ChatId,
                context.MessageId, reply: new InlineKeyboardMarkup(rows));
        }
    }
    [Method("📥 Savatga qo'shish")]
    public async Task SaveToCart(MessageContext context)
    {
        var productJson = _json.Products[context.User.CurrentProductsId - 1];
        var orderItem = new OrderItem()
        {
            UserId = context.User.Id,
            ProductId = productJson.Id,
            Quantity = context.User.NumberOfOrder,
        };
         Context.OrderItem.Add(orderItem);
         context.User.NumberOfOrder = 1;
         context.User.CurrentProductsId = 0;
         context.User.Step = (int)UStep.Menu;
         await TelegramBotService.Delete(context.User.ChatId, context.MessageId);
         await TelegramBotService.SendMessage(context.User.ChatId, "Buyurmangiz qabul qilindi sizga ozimiz aloqaga chiqamiz!!!",TelegramBotService.GetKeyboard(
             new List<string>()
             {
                 "🍽️📝 Menu",
                 "🛒 Buyurtmalar",
             }));
         await Context.SaveChangesAsync();
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
                }));
            context.User.Step = (int)UStep.FastFoodMenu;
            await Context.SaveChangesAsync();
        }
    }
}