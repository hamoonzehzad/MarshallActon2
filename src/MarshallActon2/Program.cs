using MarshallActon2;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var hostBuilder = Host.CreateDefaultBuilder(args);

hostBuilder.ConfigureServices(services => services.AddHostedService<BackgroundTask>());
hostBuilder.UseWindowsService();
hostBuilder.Build().Run();
