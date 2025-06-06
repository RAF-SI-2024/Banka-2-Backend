using Bank.Application.Extensions;
using Bank.Database;
using Bank.Permissions.Services;
using Bank.UserService.Application;
using Bank.UserService.BackgroundServices;
using Bank.UserService.Controllers;
using Bank.UserService.Database;
using Bank.UserService.Test.Integration.Services;

using DotNetEnv;

using Microsoft.Extensions.DependencyInjection;

using Reqnroll.BoDi;
using Reqnroll.Microsoft.Extensions.DependencyInjection;

namespace Bank.UserService.Test.Hooks;

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

        services.AddSingleton<IAuthorizationServiceFactory, TestAuthorizationServiceFactory>();
        services.AddServices();
        services.AddBackgroundServices();
        services.AddHttpServices();
        services.AddDatabaseServices<ApplicationContext>();
        services.AddHostedServices();
        services.AddOpenApiExamples();
        services.AddBankLinkServices();
        services.AddTransient<AccountController>();
        services.AddTransient<AccountCurrencyController>();
        services.AddTransient<AccountTypeController>();
        services.AddTransient<CardController>();
        services.AddTransient<CardTypeController>();
        services.AddTransient<ClientController>();
        services.AddTransient<CompanyController>();
        services.AddTransient<CurrencyController>();
        services.AddTransient<CountryController>();
        services.AddTransient<EmployeeController>();
        services.AddTransient<ExchangeController>();
        services.AddTransient<InstallmentController>();
        services.AddTransient<LoanController>();
        services.AddTransient<LoanTypeController>();
        services.AddTransient<TransactionController>();
        services.AddTransient<TransactionCodeController>();
        services.AddTransient<TransactionTemplateController>();
        services.AddTransient<UserController>();

        var serviceProvider = services.BuildServiceProvider();

        serviceProvider.GetRequiredService<DatabaseBackgroundService>()
                       .OnApplicationStarted(CancellationToken.None)
                       .Wait();

        serviceProvider.GetRequiredService<TransactionBackgroundService>()
                       .OnApplicationStarted()
                       .Wait();

        serviceProvider.GetRequiredService<CurrencyExchangeBackgroundService>()
                       .OnApplicationStarted(CancellationToken.None)
                       .Wait();

        return services;
    }
}
