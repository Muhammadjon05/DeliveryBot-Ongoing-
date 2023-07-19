using System.ComponentModel.DataAnnotations.Schema;

namespace Delivery.Data.Entities;
[NotMapped]
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    [NotMapped]
    public Media Media { get; set; }
}