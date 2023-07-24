using Delivery.Data.Context;
using Delivery.Data.Entities;
using DeliveryBot.Common;
using DeliveryBot.Services;
using JFA.DependencyInjection;
using JFA.Telegram;
using Microsoft.EntityFrameworkCore;

namespace DeliveryBot.Comand;

[Scoped]
public class AuthCommand : CommandHandler
{
    public AuthCommand(DeliveryDbContext context, 
    TelegramBotService telegramBotService) 
        : base(context, telegramBotService)
    {
    }

    [Method]
    public async Task Auth(MessageContext context)
    {
        var user = await Context.Users
            .FirstOrDefaultAsync(u => u.ChatId == context.ChatId);
        if (user is null)
        {
            user = new User 
            {
                ChatId = context.ChatId,
                Username = context.Username,
                Step = 0,

            };
            await Context.AddAsync(user);
            await Context.SaveChangesAsync();
        }

        context.Step = user.Step;
        context.User = user;
    }
}