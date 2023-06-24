using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Delivery.Data.Entities;

[Table("user_datas")]
public class User
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public long ChatId { get; set; }
    public int Step { get; set; }
    public string? Name { get; set; }
    public string? Username { get; set; }
    public string? Phone { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public bool IsAdmin { get; set; }
    public List<Order> Orders { get; set; }
}