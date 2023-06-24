
using Delivery.Data.Entities;

namespace DeliveryBot.Common;
public class MessageContext : JFA.Telegram.MessageContextBase<User>
{
    public string? Username { get; set; }
    public string? Name { get; set; }
    public int MessageId { get; set; }
}