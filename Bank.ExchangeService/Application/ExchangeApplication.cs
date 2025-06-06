using System.IdentityModel.Tokens.Jwt;

using Bank.Application;
using Bank.Database;
using Bank.ExchangeService.BackgroundServices;
using Bank.ExchangeService.Configurations;
using Bank.ExchangeService.Database;
using Bank.ExchangeService.Database.Examples;
using Bank.ExchangeService.Database.Processors;
using Bank.ExchangeService.Database.WebSockets;
using Bank.ExchangeService.HostedServices;
using Bank.ExchangeService.Repositories;
using Bank.ExchangeService.Services;
using Bank.Http;
using Bank.OpenApi;
using Bank.Permissions;

using DotNetEnv;

using FluentValidation;
using FluentValidation.AspNetCore;

namespace Bank.ExchangeService.Application;

public class ExchangeApplication
{
    public static void Run(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        Env.Load();

        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        builder.Services.AddSignalR();
        builder.Services.AddValidation();
        builder.Services.AddServices();
        builder.Services.AddDatabaseServices<DatabaseContext>();
        builder.Services.AddInMemoryDatabaseServices();
        builder.Services.AddHostedServices();
        builder.Services.AddBackgroundServices();
        builder.Services.AddRealtimeProcessors();
        builder.Services.AddHttpServices();

        builder.Services.AddCors();
        builder.Services.AddAuthenticationServices();
        builder.Services.AddAuthorizationServices();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApiServices();
        builder.Services.AddOpenApiExamples();

        var app = builder.Build();

        app.UseCors(Configuration.Policy.FrontendApplication);

        app.MapHub<SecurityHub>("security-hub");

        app.MapOpenApiServices();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IStockExchangeRepository, StockExchangeRepository>();
        services.AddSingleton<IStockExchangeService, StockExchangeService>();
        services.AddSingleton<IStockService, StockService>();
        services.AddSingleton<IOptionService, OptionService>();
        services.AddSingleton<IForexPairService, ForexPairService>();
        services.AddSingleton<IFutureContractService, FutureContractService>();
        services.AddSingleton<IQuoteRepository, QuoteRepository>();
        services.AddSingleton<ISecurityRepository, SecurityRepository>();
        services.AddSingleton<IOrderRepository, OrderRepository>();
        services.AddSingleton<IOrderService, OrderService>();
        services.AddSingleton<IRedisRepository, RedisRepository>();
        services.AddSingleton<ISecurityService, SecurityService>();
        services.AddSingleton<IAssetRepository, AssetRepository>();
        services.AddSingleton<IAssetService, AssetService>();

        return services;
    }

    public static IServiceCollection AddBackgroundServices(this IServiceCollection services)
    {
        services.AddSingleton<DatabaseBackgroundService>();
        services.AddSingleton<ForexPairBackgroundService>();
        services.AddSingleton<OptionBackgroundService>();
        services.AddSingleton<StockBackgroundService>();

        return services;
    }

    public static IServiceCollection AddRealtimeProcessors(this IServiceCollection services)
    {
        services.AddSingleton<IRealtimeProcessor, InMemoryRealtimeProcessor>();
        services.AddSingleton<IRealtimeProcessor, PersistentRealtimeProcessor>();
        services.AddSingleton<IRealtimeProcessor, WebSocketRealtimeProcessor>();
        services.AddSingleton<IRealtimeProcessor, OrderRealtimeProcessor>();
        services.AddSingleton<FakeRealtimeSecurityBackgroundService>();

        return services;
    }

    public static IServiceCollection AddHostedServices(this IServiceCollection services)
    {
        services.AddHostedService<ApplicationHostedService>();

        return services;
    }

    public static IServiceCollection AddHttpServices(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddHttpContextAccessor();
        services.AddUserServiceHttpClient();

        // services.AddHttpClient(Configuration.HttpClient.GetLatestStocks, httpClient =>
        //                                                                  {
        //                                                                      httpClient.BaseAddress = new Uri(Configuration.Security.Stock.GetLatest);
        //
        //                                                                      httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames
        //                                                                                                                                                      .Application.Json));
        //                                                                  })
        //         .AddHttpMessageHandler<AlpacaKeyMessageHandler>();

        return services;
    }

    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Continue;
        ValidatorOptions.Global.DefaultRuleLevelCascadeMode  = CascadeMode.Stop;

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<AssemblyInfo>();

        return services;
    }

    public static IServiceCollection AddCors(this IServiceCollection services)
    {
        services.AddCors(options => options.AddPolicy(Configuration.Policy.FrontendApplication, policy => policy.WithOrigins(Configuration.Frontend.BaseUrl)
                                                                                                                .AllowAnyHeader()
                                                                                                                .AllowAnyMethod()
                                                                                                                .AllowCredentials()));

        return services;
    }

    public static IServiceCollection AddOpenApiExamples(this IServiceCollection services)
    {
        services.AddOpenApiExample(Example.Account.Response);
        services.AddOpenApiExample(Example.Account.SimpleResponse);
        services.AddOpenApiExample(Example.AccountCurrency.Response);
        services.AddOpenApiExample(Example.AccountType.Response);
        services.AddOpenApiExample(Example.Client.SimpleResponse);
        services.AddOpenApiExample(Example.Country.SimpleResponse);
        services.AddOpenApiExample(Example.Currency.SimpleResponse);
        services.AddOpenApiExample(Example.Currency.Response);
        services.AddOpenApiExample(Example.Employee.SimpleResponse);
        services.AddOpenApiExample(Example.StockExchange.CreateRequest);
        services.AddOpenApiExample(Example.StockExchange.Response);
        services.AddOpenApiExample(Example.ForexPair.Response);
        services.AddOpenApiExample(Example.ForexPair.SimpleResponse);
        services.AddOpenApiExample(Example.ForexPair.DailyResponse);
        services.AddOpenApiExample(Example.FutureContract.Response);
        services.AddOpenApiExample(Example.FutureContract.SimpleResponse);
        services.AddOpenApiExample(Example.FutureContract.DailyResponse);
        services.AddOpenApiExample(Example.Option.Response);
        services.AddOpenApiExample(Example.Option.SimpleResponse);
        services.AddOpenApiExample(Example.Option.DailyResponse);
        services.AddOpenApiExample(Example.Order.CreateRequest);
        services.AddOpenApiExample(Example.Order.UpdateRequest);
        services.AddOpenApiExample(Example.Order.Response);
        services.AddOpenApiExample(Example.Quote.SimpleResponse);
        services.AddOpenApiExample(Example.Quote.DailySimpleResponse);
        services.AddOpenApiExample(Example.Quote.LatestSimpleResponse);
        services.AddOpenApiExample(Example.Stock.Response);
        services.AddOpenApiExample(Example.Stock.SimpleResponse);
        services.AddOpenApiExample(Example.Stock.DailyResponse);
        services.AddOpenApiExample(Example.User.Response);

        return services;
    }
}
