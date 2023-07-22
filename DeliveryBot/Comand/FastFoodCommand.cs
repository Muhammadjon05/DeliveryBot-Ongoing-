using Delivery.Data.Context;
using DeliveryBot.Common;
using DeliveryBot.Enums;
using DeliveryBot.Services;
using JFA.Telegram;

namespace DeliveryBot.Comand;
[Command((int)UStep.FastFoodMenu)]
public class FastFoodCommand : CommandHandler
{

    public FastFoodCommand(DeliveryDbContext context, TelegramBotService telegramBotService) : base(context, telegramBotService)
    {
    }

    [Method("🍔🍟 FastFood")]
    public async Task FastFood(MessageContext context)
    {
        if (context.Message == "🍔🍟 FastFood")
        {
            await TelegramBotService.SendMessage(context.User.ChatId, "Menuni tanlang", TelegramBotService.GetKeyboard(
                new List<string>()
                {
                    "Lavash",
                    "Nonburger",
                    "Hotdog",
                    "Donar",
                    "⬅️ Ortga"
                }));
            context.User.Step = (int)UStep.FastFoodChoice; 
            await Context.SaveChangesAsync();
        }
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

}
