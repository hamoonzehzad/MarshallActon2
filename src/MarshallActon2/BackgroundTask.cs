using Microsoft.Extensions.Hosting;

namespace MarshallActon2;

public sealed class BackgroundTask : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await MarshallService.ConnectAsync();
            
            await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
        }
    }
}