using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.ExchangeService.Database.Seeders;

namespace Bank.ExchangeService.Test.Examples.Entities;

public static partial class Example
{
    public static partial class Entity
    {
        public static class ForexPair
        {
            public static readonly Guid Id = Seeder.ForexPair.EurJpy.Id;

            public static readonly QuoteFilterQuery QuoteFilterQuery = new()
                                                                       {
                                                                           StockExchangeId = Seeder.StockExchange.Nasdaq.Id,
                                                                           Interval        = QuoteIntervalType.Max,
                                                                           Name            = Seeder.ForexPair.EurJpy.Name,
                                                                           Ticker          = Seeder.ForexPair.EurJpy.Ticker
                                                                       };

            public static readonly QuoteFilterIntervalQuery QuoteFilterIntervalQuery = new()
                                                                                       {
                                                                                           Interval = QuoteIntervalType.Day
                                                                                       };
        }
    }
}
