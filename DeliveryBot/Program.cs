using Delivery.Data.Context;
using JFA.Telegram;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddTelegramCommands();
builder.Services.AddDbContext<DeliveryDbContext>(optionsBuilder =>
{
    optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("DeliveryDb"));
});

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();