using Bank.Application.Requests;
using Bank.UserService.Database.Sample;
using Bank.UserService.Database.Seeders;
using Bank.UserService.Models;

namespace Bank.UserService.Test.Examples.Entities;

public static partial class Example
{
    public static partial class Entity
    {
        public static class ExchangeEntity
        {
            public static readonly Exchange GetExchange = new()
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

            public static readonly ExchangeUpdateRequest ExchangeUpdateRequest = Sample.Exchange.UpdateRequest;

            public static readonly Exchange UpdatedExchange = new()
                                                              {
                                                                  Id             = Seeder.Exchange.RsdAndUsd.Id,
                                                                  CurrencyFromId = Seeder.Exchange.RsdAndUsd.CurrencyFromId,
                                                                  CurrencyToId   = Seeder.Exchange.RsdAndUsd.CurrencyToId,
                                                                  Commission     = ExchangeUpdateRequest.Commission,
                                                                  Rate           = Seeder.Exchange.RsdAndUsd.Rate,
                                                                  CreatedAt      = Seeder.Exchange.RsdAndUsd.CreatedAt,
                                                                  ModifiedAt     = Seeder.Exchange.RsdAndUsd.ModifiedAt
                                                              };
        }
    }
}
