using Delivery.Data.Context;
using JFA.Telegram;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddTelegramCommands();


AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var app = builder.Build();


app.UseHttpsRedirection();
app.MapControllers();
app.Run();