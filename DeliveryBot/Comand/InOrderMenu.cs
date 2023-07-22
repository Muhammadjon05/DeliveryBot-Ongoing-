using Delivery.Data.Context;
using DeliveryBot.Common;
using DeliveryBot.Enums;
using DeliveryBot.Services;
using JFA.Telegram;

namespace DeliveryBot.Comand;


[Command((int)UStep.OrdersMenu)]
public class InOrderMenu : CommandHandler
{
    public InOrderMenu(DeliveryDbContext context, TelegramBotService telegramBotService) : base(context, telegramBotService)
    {
    }

    [Method("⬅️ Ortga")]
    public async Task Back(MessageContext context)
    {
        await TelegramBotService.SendMessage(context.User.ChatId, "Assalomu alekum yana bir bor menudan tanlang", TelegramBotService.GetKeyboard(
            new List<string>()
            {
                "🍽️📝 Menu",
                "🛒 Buyurtmalar",
            }));
        context.User!.Step = (int)UStep.Menu;
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