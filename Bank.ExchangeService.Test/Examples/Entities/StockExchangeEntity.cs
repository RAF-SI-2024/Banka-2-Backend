using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.ExchangeService.Database.Seeders;

namespace Bank.ExchangeService.Test.Examples.Entities;

public static partial class Example
{
    public static partial class Entity
    {
        public static class StockExchange
        {
            public static readonly StockExchangeFilterQuery FilterQuery = new StockExchangeFilterQuery();

            public static readonly Guid Id = Seeder.StockExchange.Nasdaq.Id;

            public static readonly String Acronym = Seeder.StockExchange.Nasdaq.Acronym;

            public static readonly ExchangeCreateRequest CreateRequest = Database.Examples.Example.StockExchange.CreateRequest;
        }
    }
}
