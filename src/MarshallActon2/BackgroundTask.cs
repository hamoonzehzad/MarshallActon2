using Microsoft.Extensions.Hosting;

namespace MarshallActon2;

public sealed class BackgroundTask : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            MarshallService.Connect();
            
            await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
        }
    }
}