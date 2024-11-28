using lab3;
using lab3.api;
using lab3.service;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddScoped<TCPServer>();

builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<BusService>();
builder.Services.AddScoped<TripService>();
builder.Services.AddScoped<RatingService>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();

using (var scope = host.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}



await host.RunAsync();