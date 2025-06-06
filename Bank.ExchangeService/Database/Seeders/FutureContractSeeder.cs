using Bank.Application.Domain;
using Bank.ExchangeService.Models;
using Bank.ExchangeService.Repositories;

using Microsoft.EntityFrameworkCore;

namespace Bank.ExchangeService.Database.Seeders;

using SecurityModel = Security;

public static partial class Seeder
{
    public static class FutureContract
    {
        public static readonly SecurityModel CrudeOilFuture = new()
                                                              {
                                                                  Id              = Guid.Parse("f17ac10b-58cc-4372-a567-0e02b2c3d484"),
                                                                  ContractSize    = 1000,
                                                                  ContractUnit    = ContractUnit.Barrel,
                                                                  SettlementDate  = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(3)),
                                                                  Name            = "Crude Oil Future March 2024",
                                                                  Ticker          = "CLH24",
                                                                  StockExchangeId = StockExchange.Nasdaq.Id,
                                                                  SecurityType    = SecurityType.FutureContract
                                                              };

        public static readonly SecurityModel GoldFuture = new()
                                                          {
                                                              Id              = Guid.Parse("f27ac10b-58cc-4372-a567-0e02b2c3d484"),
                                                              ContractSize    = 100,
                                                              ContractUnit    = ContractUnit.Kilogram,
                                                              SettlementDate  = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(6)),
                                                              Name            = "Gold Future June 2024",
                                                              Ticker          = "GCM24",
                                                              StockExchangeId = StockExchange.ClearStreet.Id,
                                                              SecurityType    = SecurityType.FutureContract
                                                          };

        public static readonly SecurityModel CornFuture = new()
                                                          {
                                                              Id              = Guid.Parse("f37ac10b-58cc-4372-a567-0e02b2c3d484"),
                                                              ContractSize    = 5000,
                                                              ContractUnit    = ContractUnit.Kilogram,
                                                              SettlementDate  = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(4)),
                                                              Name            = "Corn Future May 2024",
                                                              Ticker          = "ZCK24",
                                                              StockExchangeId = StockExchange.BorsaItaliana.Id,
                                                              SecurityType    = SecurityType.FutureContract
                                                          };

        public static readonly SecurityModel NaturalGasFuture = new()
                                                                {
                                                                    Id              = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d484"),
                                                                    ContractSize    = 10000,
                                                                    ContractUnit    = ContractUnit.Barrel,
                                                                    SettlementDate  = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(2)),
                                                                    Name            = "Natural Gas Future February 2024",
                                                                    Ticker          = "NGG24",
                                                                    StockExchangeId = StockExchange.EDGADark.Id,
                                                                    SecurityType    = SecurityType.FutureContract
                                                                };
    }
}

public static class FutureContractSeederExtension
{
    public static async Task SeedFutureContractHardcoded(this DatabaseContext context)
    {
        if (context.Securities.Any(security => security.SecurityType == SecurityType.FutureContract))
            return;

        await context.Securities.AddRangeAsync(Seeder.FutureContract.CornFuture, Seeder.FutureContract.CrudeOilFuture, Seeder.FutureContract.GoldFuture,
                                               Seeder.FutureContract.NaturalGasFuture);

        await context.SaveChangesAsync();
    }

    public static async Task SeedFutureContractsAndQuotes(this DatabaseContext context, ISecurityRepository securityRepository, IQuoteRepository quoteRepository)
    {
        if (context.Securities.Any(security => security.SecurityType == SecurityType.FutureContract))
            return;

        var acronymAndStockExchange = await context.StockExchanges.ToDictionaryAsync(stockExchange => stockExchange.Acronym, stockExchange => stockExchange);
        var futureContractPath      = Path.Combine("Database", "Seeders", "resource", "futures_data.csv");
        var quotesPath              = Path.Combine("Database", "Seeders", "resource", "quotes_30min_2days.csv");
        var futures                 = new List<SecurityModel>();

        using (var reader = new StreamReader(futureContractPath))
        {
            await reader.ReadLineAsync();

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();

                if (line is null)
                    continue;

                var parts = line.Split(',');

                var ticker = parts[4]
                .Trim();

                var stockExchangeAcronym = parts[5]
                .Trim();

                if (!acronymAndStockExchange.TryGetValue(stockExchangeAcronym, out var stockExchange))
                    continue;

                var security = new SecurityModel
                               {
                                   Id = Guid.NewGuid(),
                                   Name = parts[3]
                                   .Trim(),
                                   Ticker          = ticker,
                                   StockExchangeId = stockExchange.Id,
                                   SecurityType = Enum.Parse<SecurityType>(parts[6]
                                                                           .Trim()),
                                   ContractSize = int.Parse(parts[0]
                                                            .Trim()),
                                   ContractUnit = Enum.Parse<ContractUnit>(parts[1]
                                                                           .Trim()),
                                   SettlementDate = DateOnly.ParseExact(parts[2]
                                                                        .Trim(), "dd.MM.yyyy"),
                               };

                futures.Add(security);
            }
        }

        await securityRepository.CreateSecurities(futures);

        var tickerAndFutureContract = (await securityRepository.FindAll(SecurityType.FutureContract)).ToDictionary(security => security.Ticker, security => security);
        var quotes                  = new List<Quote>();

        using (var reader = new StreamReader(quotesPath))
        {
            await reader.ReadLineAsync();

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();

                if (line is null)
                    continue;

                var parts = line.Split(',');

                string ticker = parts[0]
                .Trim();

                if (!tickerAndFutureContract.TryGetValue(ticker, out var security))
                    continue;

                var quote = new Quote
                            {
                                Id                = Guid.NewGuid(),
                                SecurityId        = security.Id,
                                AskPrice          = decimal.Parse(parts[1]),
                                BidPrice          = decimal.Parse(parts[2]),
                                HighPrice         = decimal.Parse(parts[3]),
                                LowPrice          = decimal.Parse(parts[4]),
                                ClosePrice        = decimal.Parse(parts[5]),
                                OpeningPrice      = decimal.Parse(parts[6]),
                                ImpliedVolatility = decimal.Parse(parts[7]),
                                Volume            = long.Parse(parts[8]),
                                CreatedAt = DateTime.Parse(parts[9])
                                                    .ToUniversalTime(),
                                ModifiedAt = DateTime.Parse(parts[10])
                                                     .ToUniversalTime(),
                                AskSize       = int.Parse(parts[11]),
                                BidSize       = int.Parse(parts[12]),
                                ContractCount = 1,
                            };

                quotes.Add(quote);
            }
        }

        await quoteRepository.CreateQuotes(quotes);
    }
}
