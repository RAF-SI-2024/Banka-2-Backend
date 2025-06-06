using Bank.Application.Responses;
using Bank.Http;
using Bank.Link.Configurations;
using Bank.Link.Core;
using Bank.Link.Core.B3;
using Bank.Link.Core.Test;
using Bank.Link.Service;

using Microsoft.Extensions.DependencyInjection;

namespace Bank.Link;

public static class LinkExtensions
{
    public static IServiceCollection AddLinkServices(this IServiceCollection services, DefaultData? defaultData = null)
    {
        services.AddSingleton<IDataService, DataService>();
        services.AddSingleton<IExternalUserData, ExternalUserData>();
        services.AddSingleton<IBankExchangeData, BankExchangeData>();
        services.AddUserServiceHttpClient();

        if (defaultData is null)
            return services;

        services.AddSingleton<IEnumerable<CurrencyResponse>>(defaultData.Currencies);
        services.AddSingleton<IEnumerable<TransactionCodeResponse>>(defaultData.TransactionCodes);
        services.AddSingleton<IEnumerable<AccountTypeResponse>>(defaultData.AccountTypes);

        return services;
    }

    public static IServiceCollection AddTestLink(this IServiceCollection services, List<BankData> bankData)
    {
        foreach (var data in bankData)
            services.AddSingleton<IExternalUserDataLink>(serviceProvider => new TestExternalUserDataLink(data, serviceProvider.GetRequiredService<IDataService>()))
                    .AddSingleton<IBankExchangeDataLink>(_ => new TestBankExchangeDataLink());

        return services;
    }

    public static IServiceCollection AddB3Link(this IServiceCollection services, BankData bankData)
    {
        return services.AddSingleton<IExternalUserDataLink>(serviceProvider =>
                                                            new B3UserDataLink(bankData, serviceProvider.GetRequiredService<IHttpClientFactory>(),
                                                                               serviceProvider.GetRequiredService<IDataService>()))
                       .AddSingleton<IBankExchangeDataLink>(serviceProvider => new B3ExchangeDataLink(bankData, serviceProvider.GetRequiredService<IHttpClientFactory>()))
                       .CreateHttpClient(bankData);
    }

    internal static IServiceCollection CreateHttpClient(this IServiceCollection services, BankData data)
    {
        services.AddHttpClient(data.Code, httpClient =>
                                          {
                                              httpClient.BaseAddress = new Uri(data.BaseUrl);
                                              httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Configuration.Jwt.B3Token}");
                                          });

        return services;
    }
}
