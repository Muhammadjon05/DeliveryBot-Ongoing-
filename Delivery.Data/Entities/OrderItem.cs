using System.ComponentModel.DataAnnotations.Schema;

namespace Delivery.Data.Entities;

public class OrderItem
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    
}