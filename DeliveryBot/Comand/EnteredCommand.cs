using Delivery.Data.Context;
using DeliveryBot.Common;
using DeliveryBot.Enums;
using DeliveryBot.Services;
using JFA.Telegram;

namespace DeliveryBot.Comand;

[Command((int)UStep.Created)]
public class EnteredCommand : CommandHandler
{
    public EnteredCommand(DeliveryDbContext context, TelegramBotService telegramBotService)
        : base(context, telegramBotService)
    {
    }
    [Method("/start")]
    public async Task SendMessage(MessageContext context)
    {
        
        await TelegramBotService.SendMessage(context.User.ChatId, "Iltimos ismingizni kiriting? ");
        context.User.Step = (int)UStep.Name;
        await Context.SaveChangesAsync();
    }
    
    
    [Method]
    public async Task SendMenu(MessageContext context)
    {
        if (!string.IsNullOrEmpty(context.Message))
        {
            await TelegramBotService.SendMessage(context.User.ChatId, "Assalomu alekum yana bir bor menudan tanlang", TelegramBotService.GetKeyboard(
                new List<string>()
                {
                    "🍽️📝 Menu",
                    "🛒 Buyurtmalar",
                }));
            context.User!.Step = (int)UStep.Menu;
        }
        await Context.SaveChangesAsync();
    }
    
}