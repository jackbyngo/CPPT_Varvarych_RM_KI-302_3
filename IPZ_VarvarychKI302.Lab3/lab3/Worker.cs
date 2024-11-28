using lab3;
using lab3.service;

namespace lab3;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider _serviceProvider;

    public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }
    
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var userService = scope.ServiceProvider.GetRequiredService<UserService>();
            var busService = scope.ServiceProvider.GetRequiredService<BusService>();
            var tripService = scope.ServiceProvider.GetRequiredService<TripService>();
            var ratingService = scope.ServiceProvider.GetRequiredService<RatingService>();
            var tcpServer = scope.ServiceProvider.GetRequiredService<TCPServer>();
            await tcpServer.StartAsync(stoppingToken);
        }
    }

}