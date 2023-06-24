using Delivery.Data.Context;
using DeliveryBot.Services;
using JFA.Telegram;

namespace DeliveryBot.Common;

public abstract class CommandHandler : CommandHandlerBase<MessageContext>
{
    protected CommandHandler(DeliveryDbContext context, TelegramBotService telegramBotService)
    {
        Context = context;
        TelegramBotService = telegramBotService;
    }

    protected DeliveryDbContext Context { get; set; }
    protected TelegramBotService TelegramBotService { get; set; }
}