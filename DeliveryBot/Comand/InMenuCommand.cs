using Delivery.Data.Context;
using DeliveryBot.Common;
using DeliveryBot.Enums;
using DeliveryBot.Services;
using JFA.Telegram;

namespace DeliveryBot.Comand;

[Command((int)UStep.Menu)]
public class InMenuCommand : CommandHandler
{
    public InMenuCommand(DeliveryDbContext context, TelegramBotService telegramBotService)
        : base(context, telegramBotService)
    {
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
        }
    }
}