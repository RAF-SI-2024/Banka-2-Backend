using Bank.Application.Responses;
using Bank.UserService.Configurations;
using Bank.UserService.Mappers;

using Microsoft.EntityFrameworkCore;

namespace Bank.UserService.Database.Seeders;

public static class ExchangeSeederExtension
{
    public static async Task SeedExchange(this ApplicationContext context, HttpClient httpClient, bool alwaysSeed = false)
    {
        if (!alwaysSeed && context.Exchanges.Any())
            return;

        var exchangeApiResponseMessageTask = Task.Run(() => httpClient.GetAsync(Configuration.Exchange.ApiUrlTemplate));
        var currenciesTask = Task.Run(() => context.Currencies.ToListAsync());
        var exchangeApiResponseTask = Task.Run(async () => await (await exchangeApiResponseMessageTask).Content.ReadFromJsonAsync<Dictionary<string, ExchangeFetchResponse>>());

        await Task.WhenAll(exchangeApiResponseMessageTask, currenciesTask, exchangeApiResponseTask);

        var exchangeApiResponseMessage = exchangeApiResponseMessageTask.Result;
        var exchangeApiResponse        = exchangeApiResponseTask.Result;
        var currencyDictionary         = currenciesTask.Result.ToDictionary(currency => currency.Code, currency => currency);
        var defaultCurrency            = currencyDictionary.TryGetValue(Configuration.Exchange.DefaultCurrencyCode, out var value) ? value : null;

        if (!exchangeApiResponseMessage.IsSuccessStatusCode || defaultCurrency is null || exchangeApiResponse is null)
            return;

        var dayOldExchangeDictionary = await context.Exchanges.OrderByDescending(exchange => exchange.CreatedAt)
                                                     .Take(currenciesTask.Result.Count - 1)
                                                     .ToDictionaryAsync(exchange => exchange.CurrencyTo.Code, exchange => exchange.Commission);

        var exchangeApiCurrencies = exchangeApiResponse.Values.Where(exchangeApiEntity => currencyDictionary.ContainsKey(exchangeApiEntity.Code));

        var newExchanges = exchangeApiCurrencies
                           .Select(exchangeApiCurrency => exchangeApiCurrency.ToExchange(defaultCurrency, currencyDictionary[exchangeApiCurrency.Code],
                                                                                         dayOldExchangeDictionary.GetValueOrDefault(exchangeApiCurrency.Code, 0.005m)))
                           .ToList();

        await context.Exchanges.AddRangeAsync(newExchanges);
        await context.SaveChangesAsync();
    }
}
