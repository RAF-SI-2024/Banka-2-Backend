using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.ExchangeService.Database.Seeders;

namespace Bank.ExchangeService.Test.Examples.Entities;

public static partial class Example
{
    public static partial class Entity
    {
        public static class Option
        {
            public static readonly Guid Id = Seeder.Option.AppleCallOption.Id;

            public static readonly QuoteFilterQuery QuoteFilterQuery = new()
                                                                       {
                                                                           StockExchangeId = Seeder.StockExchange.Nasdaq.Id,
                                                                           Interval        = QuoteIntervalType.Max,
                                                                           Name            = Seeder.Option.AppleCallOption.Name,
                                                                           Ticker          = Seeder.Option.AppleCallOption.Ticker
                                                                       };

            public static readonly QuoteFilterIntervalQuery QuoteFilterIntervalQuery = new()
                                                                                       {
                                                                                           Interval = QuoteIntervalType.Day
                                                                                       };
        }
    }
}
