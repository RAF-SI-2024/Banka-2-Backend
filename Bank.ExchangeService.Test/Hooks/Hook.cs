using Bank.Application.Extensions;
using Bank.ExchangeService.Application;

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

        services.AddServices();
        services.AddBackgroundServices();
        services.AddHttpServices();
        services.AddDatabase();
        services.AddHostedServices();
        services.AddSwagger();

        return services;
    }
}
