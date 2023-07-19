﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Delivery.Data.Entities;
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public virtual List<OrderItem> OrderItems { get; set; }
    [NotMapped]
    public Media Media { get; set; }
}