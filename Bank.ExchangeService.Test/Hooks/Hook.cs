using Bank.Application.Extensions;
using Bank.Database;
using Bank.ExchangeService.Application;
using Bank.ExchangeService.BackgroundServices;
using Bank.ExchangeService.Database;
using Bank.ExchangeService.Test.Services;
using Bank.OpenApi;
using Bank.Permissions;
using Bank.Permissions.Services;

using DotNetEnv;

using Microsoft.Extensions.DependencyInjection;

using Reqnroll.BoDi;
using Reqnroll.Microsoft.Extensions.DependencyInjection;

namespace Bank.ExchangeService.Test.Hooks;

[Binding]
public class Hooks
{
    [BeforeTestRun]
    public static void IncreaseResolutionTimeout()
    {
        ObjectContainer.DefaultConcurrentObjectResolutionTimeout = TimeSpan.FromSeconds(10);
    }

    [ScenarioDependencies]
    public static IServiceCollection CreateServices()
    {
        Env.Load(Directory.GetCurrentDirectory()
                          .UpDirectory(3));

        var services = new ServiceCollection();

        services.AddSingleton<IAuthorizationService, TestAuthorizationService>();
        services.AddServices();
        services.AddBackgroundServices();
        services.AddRealtimeProcessors();
        services.AddHttpServices();
        services.AddInMemoryDatabaseServices();
        services.AddDatabaseServices<DatabaseContext>();
        services.AddHostedServices();
        services.AddOpenApiExamples();
        services.AddSignalR();
        services.AddOpenApiServices();
        services.AddOpenApiExamples();
        services.AddAuthorizationServices();
        services.AddAuthenticationServices();

        var serviceProvider = services.BuildServiceProvider();

        serviceProvider.GetRequiredService<DatabaseBackgroundService>()
                       .OnApplicationStarted();

        return services;
    }
}
