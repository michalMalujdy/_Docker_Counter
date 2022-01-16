var builder = WebApplication.CreateBuilder(args);
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "redis-server";
    options.InstanceName = "Counter";
});

builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();