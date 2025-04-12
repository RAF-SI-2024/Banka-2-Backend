using Bank.Application.Domain;
using Bank.Application.Queries;

using Seeder = Bank.UserService.Database.Seeders.Seeder;

namespace Bank.UserService.Test.Examples.Entities;

public static partial class Example
{
    public static partial class Entity
    {
        public static class Country
        {
            public static readonly CountryFilterQuery FilterQueryWithName = new()
                                                                            {
                                                                                Name = "Germany",
                                                                            };

            public static readonly CountryFilterQuery FilterQueryWithCurrencyCode = new()
                                                                                    {
                                                                                        CurrencyCode = "EUR",
                                                                                    };

            public static readonly CountryFilterQuery FilterQueryWithCurrencyName = new()
                                                                                    {
                                                                                        CurrencyName = "Euro",
                                                                                    };

            public static readonly Pageable Pageable = new()
                                                       {
                                                           Page = 1,
                                                           Size = 10
                                                       };

            public static readonly Guid GetById = Seeder.Country.Switzerland.Id;
        }
    }
}
