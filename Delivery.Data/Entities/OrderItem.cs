using System.ComponentModel.DataAnnotations.Schema;

namespace Delivery.Data.Entities;

public class OrderItem
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Order Order {get; set; }
    [NotMapped]
    public Product Product { get; set; }
    public int Quantity { get; set; }
    
}