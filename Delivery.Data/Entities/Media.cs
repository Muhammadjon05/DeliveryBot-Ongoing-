using System.ComponentModel.DataAnnotations.Schema;

namespace Delivery.Data.Entities;

public class Media
{
    public bool Exist { get; set; }
    public string Name  { get; set; }
}