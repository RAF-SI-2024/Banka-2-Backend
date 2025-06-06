using Bank.Application.Domain;
using Bank.Application.Responses;
using Bank.ExchangeService.Configurations;
using Bank.ExchangeService.Mappers;
using Bank.ExchangeService.Models;

using Microsoft.EntityFrameworkCore;

namespace Bank.ExchangeService.Database.Seeders;

using SecurityModel = Security;

public static partial class Seeder
{
    public static class Stock
    {
        public static readonly SecurityModel Apple = new()
                                                     {
                                                         Id              = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d479"),
                                                         Name            = "Apple Inc.",
                                                         Ticker          = "AAPL",
                                                         StockExchangeId = StockExchange.Nasdaq.Id,
                                                         SecurityType    = SecurityType.Stock
                                                     };

        public static readonly SecurityModel Microsoft = new()
                                                         {
                                                             Id              = Guid.Parse("b47ac10b-58cc-4372-a567-0e02b2c3d480"),
                                                             Name            = "Microsoft Corporation",
                                                             Ticker          = "MSFT",
                                                             StockExchangeId = StockExchange.Nasdaq.Id,
                                                             SecurityType    = SecurityType.Stock,
                                                         };

        public static readonly SecurityModel Tesla = new()
                                                     {
                                                         Id              = Guid.Parse("c47ac10b-58cc-4372-a567-0e02b2c3d481"),
                                                         Name            = "Tesla, Inc.",
                                                         Ticker          = "TSLA",
                                                         StockExchangeId = StockExchange.ClearStreet.Id,
                                                         SecurityType    = SecurityType.Stock
                                                     };

        public static readonly SecurityModel Amazon = new()
                                                      {
                                                          Id              = Guid.Parse("d47ac10b-58cc-4372-a567-0e02b2c3d482"),
                                                          Name            = "Amazon.com, Inc.",
                                                          Ticker          = "AMZN",
                                                          StockExchangeId = StockExchange.ClearStreet.Id,
                                                          SecurityType    = SecurityType.Stock,
                                                      };
    }
}

public static class StockSeederExtension
{
    public static async Task SeedStock(this DatabaseContext context, HttpClient httpClient)
    {
        if (context.Securities.Any(security => security.SecurityType == SecurityType.Stock))
            return;

        var (apiKey, apiSecret) = Configuration.Security.Keys.AlpacaApiKeyAndSecret;

        var request = new HttpRequestMessage
                      {
                          Method     = HttpMethod.Get,
                          RequestUri = new Uri($"{Configuration.Security.Stock.GetAllApi}?status=active&asset_class=us_equity&attributes="),
                          Headers =
                          {
                              { "accept", "application/json" },
                              { "APCA-API-KEY-ID", apiKey },
                              { "APCA-API-SECRET-KEY", apiSecret },
                          },
                      };

        using var response = await httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
            return;

        var body = await response.Content.ReadFromJsonAsync<List<FetchStockResponse>>();

        if (body is null)
            throw new Exception("List of stocks can't be null");

        var stockExchanges = await context.StockExchanges.ToListAsync();
        
        var stocks = body.Select(stockResponse => stockResponse.Tradable
                                                  ? stockResponse.ToStock(Seeder.StockExchange.IEX.Id)
                                                                 .ToSecurity()
                                                  : null)
                         .Where(security => security != null)
                         .Select(security => security!)
                         .ToList();

        await context.Securities.AddRangeAsync(stocks);

        await context.SaveChangesAsync();
    }

    public static async Task SeedStockHardcoded(this DatabaseContext context)
    {
        if (context.Securities.Any(security => security.SecurityType == SecurityType.Stock))
            return;

        await context.Securities.AddRangeAsync(Seeder.Stock.Apple, Seeder.Stock.Amazon, Seeder.Stock.Microsoft, Seeder.Stock.Tesla);

        await context.SaveChangesAsync();
    }
}
