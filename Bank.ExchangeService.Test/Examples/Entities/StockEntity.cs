using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.ExchangeService.Database.Seeders;

namespace Bank.ExchangeService.Test.Examples.Entities;

public static partial class Example
{
    public static partial class Entity
    {
        public static class Stock
        {
            public static readonly Guid Id = Seeder.Stock.Apple.Id;

            public static readonly QuoteFilterQuery QuoteFilterQuery = new()
                                                                       {
                                                                           StockExchangeId = Seeder.StockExchange.Nasdaq.Id,
                                                                           Interval        = QuoteIntervalType.Max,
                                                                           Name            = Seeder.Stock.Apple.Name,
                                                                           Ticker          = Seeder.Stock.Apple.Ticker
                                                                       };

            public static readonly QuoteFilterIntervalQuery QuoteFilterIntervalQuery = new()
                                                                                       {
                                                                                           Interval = QuoteIntervalType.Day
                                                                                       };
        }
    }
}
