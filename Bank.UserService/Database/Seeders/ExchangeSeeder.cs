using System.Collections.Immutable;

using Bank.Application.Responses;
using Bank.UserService.Configurations;
using Bank.UserService.Mappers;
using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;

namespace Bank.UserService.Database.Seeders;

using ExchangeModel = Exchange;

public static partial class Seeder
{
    public static class Exchange
    {
        public static readonly ExchangeModel RsdAndUsd = new()
                                                         {
                                                             Id             = Guid.Parse("177283bd-8041-496d-b758-b6d48661eccb"),
                                                             CurrencyFromId = Currency.SerbianDinar.Id,
                                                             CurrencyToId   = Currency.USDollar.Id,
                                                             Commission     = 0.005m,
                                                             Rate           = 0.0093128232097467m,
                                                             CreatedAt      = DateTime.UtcNow,
                                                             ModifiedAt     = DateTime.UtcNow
                                                         };

        public static readonly ExchangeModel RsdAndJpy = new()
                                                         {
                                                             Id             = Guid.Parse("1a3d3778-71a5-4e5f-8ec1-7e42edfdd23c"),
                                                             CurrencyFromId = Currency.SerbianDinar.Id,
                                                             CurrencyToId   = Currency.JapaneseYen.Id,
                                                             Commission     = 0.005m,
                                                             Rate           = 1.3860238469587m,
                                                             CreatedAt      = DateTime.UtcNow,
                                                             ModifiedAt     = DateTime.UtcNow
                                                         };

        public static readonly ExchangeModel RsdAndCad = new()
                                                         {
                                                             Id             = Guid.Parse("40451876-3a6d-407d-b726-9e038f80de43"),
                                                             CurrencyFromId = Currency.SerbianDinar.Id,
                                                             CurrencyToId   = Currency.CanadianDollar.Id,
                                                             Commission     = 0.005m,
                                                             Rate           = 0.013384594902861m,
                                                             CreatedAt      = DateTime.UtcNow,
                                                             ModifiedAt     = DateTime.UtcNow
                                                         };

        public static readonly ExchangeModel RsdAndChf = new()
                                                         {
                                                             Id             = Guid.Parse("406b410d-5adb-4117-980a-c414d391f7c0"),
                                                             CurrencyFromId = Currency.SerbianDinar.Id,
                                                             CurrencyToId   = Currency.SwissFranc.Id,
                                                             Commission     = 0.005m,
                                                             Rate           = 0.0082297516822582m,
                                                             CreatedAt      = DateTime.UtcNow,
                                                             ModifiedAt     = DateTime.UtcNow
                                                         };

        public static readonly ExchangeModel RsdAndGbp = new()
                                                         {
                                                             Id             = Guid.Parse("5a41f8e7-50cf-406b-8275-9150f78ba050"),
                                                             CurrencyFromId = Currency.SerbianDinar.Id,
                                                             CurrencyToId   = Currency.BritishPound.Id,
                                                             Commission     = 0.005m,
                                                             Rate           = 0.0071949135281971m,
                                                             CreatedAt      = DateTime.UtcNow,
                                                             ModifiedAt     = DateTime.UtcNow
                                                         };

        public static readonly ExchangeModel RsdAndEur = new()
                                                         {
                                                             Id             = Guid.Parse("a2a8dc9d-5f5f-489f-a72c-3a74c3230e81"),
                                                             CurrencyFromId = Currency.SerbianDinar.Id,
                                                             CurrencyToId   = Currency.Euro.Id,
                                                             Commission     = 0.005m,
                                                             Rate           = 0.0085518282087359m,
                                                             CreatedAt      = DateTime.UtcNow,
                                                             ModifiedAt     = DateTime.UtcNow
                                                         };

        public static readonly ExchangeModel RsdAndAud = new()
                                                         {
                                                             Id             = Guid.Parse("fd2d3020-a874-4eed-b6bc-8c48536cac43"),
                                                             CurrencyFromId = Currency.SerbianDinar.Id,
                                                             CurrencyToId   = Currency.AustralianDollar.Id,
                                                             Commission     = 0.005m,
                                                             Rate           = 0.014716681142716m,
                                                             CreatedAt      = DateTime.UtcNow,
                                                             ModifiedAt     = DateTime.UtcNow
                                                         };

        public static readonly ImmutableArray<ExchangeModel> All =
        [
            RsdAndAud, RsdAndCad, RsdAndChf, RsdAndEur, RsdAndGbp, RsdAndJpy, RsdAndUsd
        ];
    }
}

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
                                                    .ToDictionaryAsync(exchange => exchange.CurrencyTo!.Code, exchange => exchange.Commission);

        var exchangeApiCurrencies = exchangeApiResponse.Values.Where(exchangeApiEntity => currencyDictionary.ContainsKey(exchangeApiEntity.Code));

        var newExchanges = exchangeApiCurrencies
                           .Select(exchangeApiCurrency => exchangeApiCurrency.ToExchange(defaultCurrency, currencyDictionary[exchangeApiCurrency.Code],
                                                                                         dayOldExchangeDictionary.GetValueOrDefault(exchangeApiCurrency.Code, 0.005m)))
                           .ToList();

        await context.Exchanges.AddRangeAsync(newExchanges);
        await context.SaveChangesAsync();
    }

    public static async Task SeedExchangeHardcoded(this ApplicationContext context)
    {
        if (context.Exchanges.Any())
            return;

        await context.Exchanges.AddRangeAsync([
                                                  Seeder.Exchange.RsdAndAud, Seeder.Exchange.RsdAndCad, Seeder.Exchange.RsdAndChf, Seeder.Exchange.RsdAndEur,
                                                  Seeder.Exchange.RsdAndGbp, Seeder.Exchange.RsdAndJpy, Seeder.Exchange.RsdAndUsd
                                              ]);

        await context.SaveChangesAsync();
    }
}
