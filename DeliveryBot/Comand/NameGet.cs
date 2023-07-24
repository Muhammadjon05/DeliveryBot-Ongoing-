using Delivery.Data.Context;
using DeliveryBot.Common;
using DeliveryBot.Enums;
using DeliveryBot.Services;
using JFA.Telegram;
using Telegram.Bot.Types.ReplyMarkups;


namespace DeliveryBot.Comand;
[Command((int)UStep.Name)]
public class NameGet : CommandHandler
{
    public NameGet(DeliveryDbContext context, TelegramBotService telegramBotService) : base(context, telegramBotService)
    {
    }

    [Method]
    public async Task GetName(MessageContext context)
    {
        context.User.Name = context.Message;
        KeyboardButton button = KeyboardButton.WithRequestContact("📞 Raqamni jonatish");
        ReplyKeyboardMarkup keyboardMarkup = new ReplyKeyboardMarkup(button);
        keyboardMarkup.ResizeKeyboard = true;
        context.User.Step = (int)UStep.PhoneNumber;
        await Context.SaveChangesAsync();
        await TelegramBotService.SendMessage(context.User.ChatId, "Raqamingizni jonating", keyboardMarkup);
    }
}