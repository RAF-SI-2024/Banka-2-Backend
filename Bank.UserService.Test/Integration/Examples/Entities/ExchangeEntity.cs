using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.UserService.Database.Seeders;

namespace Bank.UserService.Test.Examples.Entities;

public static partial class Example
{
    public static partial class Entity
    {
        public static class Exchange
        {
            public static readonly Models.Exchange GetExchange = new()
                                                                 {
                                                                     Id             = Seeder.Exchange.RsdAndEur.Id,
                                                                     CurrencyFromId = Seeder.Exchange.RsdAndEur.CurrencyFromId,
                                                                     CurrencyToId   = Seeder.Exchange.RsdAndEur.CurrencyToId,
                                                                     Commission     = Seeder.Exchange.RsdAndEur.Commission,
                                                                     Rate           = Seeder.Exchange.RsdAndEur.Rate,
                                                                     CreatedAt      = Seeder.Exchange.RsdAndEur.CreatedAt,
                                                                     ModifiedAt     = Seeder.Exchange.RsdAndEur.ModifiedAt
                                                                 };

            public static readonly Guid UpdateId = Seeder.Exchange.RsdAndUsd.Id;

            public static readonly ExchangeUpdateRequest ExchangeUpdateRequest = Database.Examples.Example.Exchange.UpdateRequest;

            public static readonly Models.Exchange UpdatedExchange = new()
                                                                     {
                                                                         Id             = Seeder.Exchange.RsdAndUsd.Id,
                                                                         CurrencyFromId = Seeder.Exchange.RsdAndUsd.CurrencyFromId,
                                                                         CurrencyToId   = Seeder.Exchange.RsdAndUsd.CurrencyToId,
                                                                         Commission     = ExchangeUpdateRequest.Commission,
                                                                         Rate           = Seeder.Exchange.RsdAndUsd.Rate,
                                                                         CreatedAt      = Seeder.Exchange.RsdAndUsd.CreatedAt,
                                                                         ModifiedAt     = Seeder.Exchange.RsdAndUsd.ModifiedAt
                                                                     };

            public static readonly Guid ExchangeId = Seeder.Exchange.RsdAndEur.Id;

            public static readonly ExchangeBetweenQuery ExchangeBetweenQuery = new()
                                                                               {
                                                                                   CurrencyFromCode = Currency.SerbianDinar.Code,
                                                                                   CurrencyToCode   = Currency.Euro.Code
                                                                               };

            public static readonly ExchangeMakeExchangeRequest ExchangeMakeExchangeRequest = new()
                                                                                             {
                                                                                                 CurrencyFromId = Seeder.Exchange.RsdAndEur.CurrencyFromId,
                                                                                                 CurrencyToId   = Seeder.Exchange.RsdAndEur.CurrencyToId,
                                                                                                 Amount         = 1000,
                                                                                                 AccountId      = Seeder.Account.DomesticAccount02.Id
                                                                                             };
        }
    }
}
