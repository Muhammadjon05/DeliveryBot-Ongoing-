using Delivery.Data.Entities;
using JFA.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Data.Context;

[Scoped]
public class DeliveryDbContext : DbContext
{
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlServer(@"Server=sql.bsite.net\MSSQL2016;Database=muhammadjon005_sql;User Id=muhammadjon005_sql;Password=sql;TrustServerCertificate=True");
  }
  public DbSet<OrderItem> OrderItem { get; set; }
  public DbSet<User> Users { get; set; }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<Media>().HasNoKey();
   
  }
}