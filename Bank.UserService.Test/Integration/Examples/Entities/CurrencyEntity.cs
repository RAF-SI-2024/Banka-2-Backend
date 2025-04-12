using Bank.Application.Queries;
using Bank.UserService.Database.Seeders;

using CurrencyModel = Bank.UserService.Models.Currency;

namespace Bank.UserService.Test.Examples.Entities;

public static partial class Example
{
    public static partial class Entity
    {
        public static class Currency
        {
            public static readonly CurrencyFilterQuery FilterQueryWithName = new()
                                                                             {
                                                                                 Name = "Euro",
                                                                             };

            public static readonly CurrencyFilterQuery FilterQueryWithCode = new()
                                                                             {
                                                                                 Code = "EUR",
                                                                             };

            public static readonly Guid GetById = Seeder.Currency.AustralianDollar.Id;

            public static readonly CurrencyModel SerbianDinar = new()
                                                                {
                                                                    Id          = Seeder.Currency.SerbianDinar.Id,
                                                                    Name        = Seeder.Currency.SerbianDinar.Name,
                                                                    Code        = Seeder.Currency.SerbianDinar.Code,
                                                                    Symbol      = Seeder.Currency.SerbianDinar.Symbol,
                                                                    Countries   = Seeder.Currency.SerbianDinar.Countries,
                                                                    Description = Seeder.Currency.SerbianDinar.Description,
                                                                    Status      = Seeder.Currency.SerbianDinar.Status,
                                                                    CreatedAt   = Seeder.Currency.SerbianDinar.CreatedAt,
                                                                    ModifiedAt  = Seeder.Currency.SerbianDinar.ModifiedAt
                                                                };

            public static readonly CurrencyModel Euro = new()
                                                        {
                                                            Id          = Seeder.Currency.Euro.Id,
                                                            Name        = Seeder.Currency.Euro.Name,
                                                            Code        = Seeder.Currency.Euro.Code,
                                                            Symbol      = Seeder.Currency.Euro.Symbol,
                                                            Countries   = Seeder.Currency.Euro.Countries,
                                                            Description = Seeder.Currency.Euro.Description,
                                                            Status      = Seeder.Currency.Euro.Status,
                                                            CreatedAt   = Seeder.Currency.Euro.CreatedAt,
                                                            ModifiedAt  = Seeder.Currency.Euro.ModifiedAt
                                                        };
        }
    }
}
