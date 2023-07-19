using Delivery.Data.Entities;
using JFA.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Data.Context;

[Scoped]
public class DeliveryDbContext : DbContext
{

  public DeliveryDbContext(DbContextOptions<DeliveryDbContext> options): base(options)
  {
    
  }
  public DbSet<OrderItem> OrderItem { get; set; }
  public DbSet<User> Users { get; set; }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<Media>().HasNoKey();
   
  }
}