using Bank.UserService.Models;

namespace Bank.UserService.Database.Seeders;

using CurrencyModel = Currency;

public static partial class Seeder
{
    public static class Currency
    {
        public static readonly CurrencyModel Euro = new()
                                                    {
                                                        Id          = Guid.Parse("6842a5fa-eee4-4438-bcff-5217b6ac6ace"),
                                                        Name        = "Euro",
                                                        Code        = "EUR",
                                                        Symbol      = "€",
                                                        Countries   = [],
                                                        Description = "The official currency of the Eurozone.",
                                                        Status      = true,
                                                        CreatedAt   = DateTime.UtcNow,
                                                        ModifiedAt  = DateTime.UtcNow
                                                    };

        public static readonly CurrencyModel SwissFranc = new()
                                                          {
                                                              Id          = Guid.Parse("6665627e-be4d-48f2-9dd3-a01cbbd1dfa3"),
                                                              Name        = "Swiss Franc",
                                                              Code        = "CHF",
                                                              Symbol      = "CHF",
                                                              Countries   = [],
                                                              Description = "The official currency of Switzerland.",
                                                              Status      = true,
                                                              CreatedAt   = DateTime.UtcNow,
                                                              ModifiedAt  = DateTime.UtcNow
                                                          };

        public static readonly CurrencyModel USDollar = new()
                                                        {
                                                            Id          = Guid.Parse("81bf331a-0a35-4716-ad12-d1d1bcf66627"),
                                                            Name        = "US Dollar",
                                                            Code        = "USD",
                                                            Symbol      = "$",
                                                            Countries   = [],
                                                            Description = "The official currency of the United States of America.",
                                                            Status      = true,
                                                            CreatedAt   = DateTime.UtcNow,
                                                            ModifiedAt  = DateTime.UtcNow
                                                        };

        public static readonly CurrencyModel BritishPound = new()
                                                            {
                                                                Id          = Guid.Parse("8e8e9283-4ced-4d9e-aa4a-1036d0174c8c"),
                                                                Name        = "British Pound",
                                                                Code        = "GBP",
                                                                Symbol      = "£",
                                                                Countries   = [],
                                                                Description = "The official currency of the United Kingdom.",
                                                                Status      = true,
                                                                CreatedAt   = DateTime.UtcNow,
                                                                ModifiedAt  = DateTime.UtcNow
                                                            };

        public static readonly CurrencyModel JapaneseYen = new()
                                                           {
                                                               Id          = Guid.Parse("1a77ed84-d984-4410-85ec-ffde69508625"),
                                                               Name        = "Japanese Yen",
                                                               Code        = "JPY",
                                                               Symbol      = "¥",
                                                               Countries   = [],
                                                               Description = "The official currency of Japan.",
                                                               Status      = true,
                                                               CreatedAt   = DateTime.UtcNow,
                                                               ModifiedAt  = DateTime.UtcNow
                                                           };

        public static readonly CurrencyModel CanadianDollar = new()
                                                              {
                                                                  Id          = Guid.Parse("3834d8f4-1703-4091-859c-07225b9ce2a6"),
                                                                  Name        = "Canadian Dollar",
                                                                  Code        = "CAD",
                                                                  Symbol      = "$",
                                                                  Countries   = [],
                                                                  Description = "The official currency of Canada.",
                                                                  Status      = true,
                                                                  CreatedAt   = DateTime.UtcNow,
                                                                  ModifiedAt  = DateTime.UtcNow
                                                              };

        public static readonly CurrencyModel AustralianDollar = new()
                                                                {
                                                                    Id          = Guid.Parse("895ab6f9-8a9a-4532-bea7-b3361d0dc936"),
                                                                    Name        = "Australian Dollar",
                                                                    Code        = "AUD",
                                                                    Symbol      = "$",
                                                                    Countries   = [],
                                                                    Description = "The official currency of Australia.",
                                                                    Status      = true,
                                                                    CreatedAt   = DateTime.UtcNow,
                                                                    ModifiedAt  = DateTime.UtcNow
                                                                };

        public static readonly CurrencyModel SerbianDinar = new()
                                                            {
                                                                Id          = Guid.Parse("88bfe7f0-8f74-42f7-b6ba-07b3145da989"),
                                                                Name        = "Serbian Dinar",
                                                                Code        = "RSD",
                                                                Symbol      = "RSD",
                                                                Countries   = [],
                                                                Description = "The official currency of Serbia.",
                                                                Status      = true,
                                                                CreatedAt   = DateTime.UtcNow,
                                                                ModifiedAt  = DateTime.UtcNow
                                                            };
    }
}

public static class CurrencySeederExtension
{
    public static async Task SeedCurrency(this ApplicationContext context)
    {
        if (context.Currencies.Any())
            return;

        await context.Currencies.AddRangeAsync([
                                                   Seeder.Currency.Euro, Seeder.Currency.SwissFranc, Seeder.Currency.USDollar, Seeder.Currency.BritishPound,
                                                   Seeder.Currency.JapaneseYen, Seeder.Currency.CanadianDollar, Seeder.Currency.AustralianDollar, Seeder.Currency.SerbianDinar
                                               ]);

        await context.SaveChangesAsync();
    }
}
