using MarshallActon2;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var hostBuilder = Host.CreateDefaultBuilder(args);

hostBuilder.ConfigureLogging(logging => logging.ClearProviders());
hostBuilder.ConfigureServices(services => services.AddHostedService<BackgroundTask>());
hostBuilder.UseWindowsService();
hostBuilder.Build().Run();
