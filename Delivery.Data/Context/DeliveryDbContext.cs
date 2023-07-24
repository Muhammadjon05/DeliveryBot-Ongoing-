using Delivery.Data.Entities;
using JFA.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Data.Context;

[Scoped]
public class DeliveryDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseNpgsql(
        "Server=localhost; Port=1234; Database=delivery_db; User Id=postgres; Password=postgres;");
    }

    public DbSet<OrderItem> OrderItem { get; set; }
  public DbSet<User> Users { get; set; }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<Media>().HasNoKey();
   
  }
}