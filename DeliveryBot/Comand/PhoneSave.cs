using Delivery.Data.Context;
using DeliveryBot.Common;
using DeliveryBot.Enums;
using DeliveryBot.Services;
using JFA.Telegram;
using Telegram.Bot.Types;

namespace DeliveryBot.Comand;

[Command((int)UStep.PhoneNumber)]
public class PhoneSave : CommandHandler
{
    public PhoneSave(DeliveryDbContext context, TelegramBotService telegramBotService) : base(context, telegramBotService)
    {
    }
    [Method("📞 Raqamni jonatish")]
    public async Task SavePhone(MessageContext context)
    {
        context.User!.Phone = context.PhoneNumber;
        await Context.SaveChangesAsync();
        await TelegramBotService.SendMessage(context.User.ChatId,"Raqamingiz tizimda ro'yhatga olindi",TelegramBotService.GetKeyboard(
            new List<string>()
            {
                "🍽️📝 Menu",
                "🛒 Buyurtmalar",
            }));
        context.User!.Step = (int)UStep.Menu;
        await Context.SaveChangesAsync();
    }
}