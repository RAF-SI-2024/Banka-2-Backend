using Bank.UserService.Models;

namespace Bank.UserService.Database.Seeders;

using CountryModel = Country;

public static partial class Seeder
{
    public static class Country
    {
        public static readonly CountryModel Germany = new()
                                                      {
                                                          Id         = Guid.Parse("ca9cdcb6-db85-4f2c-bf8c-0666083cbcb6"),
                                                          Name       = "Germany",
                                                          CurrencyId = Currency.Euro.Id,
                                                          CreatedAt  = DateTime.UtcNow,
                                                          ModifiedAt = DateTime.UtcNow
                                                      };

        public static readonly CountryModel Japan = new()
                                                    {
                                                        Id         = Guid.Parse("d718fa0b-07d4-4ff4-b7c6-a4de4cc8e72a"),
                                                        Name       = "Japan",
                                                        CurrencyId = Currency.JapaneseYen.Id,
                                                        CreatedAt  = DateTime.UtcNow,
                                                        ModifiedAt = DateTime.UtcNow
                                                    };

        public static readonly CountryModel Serbia = new()
                                                     {
                                                         Id         = Guid.Parse("ebc201e0-83fc-4a14-859d-e387773eb992"),
                                                         Name       = "Serbia",
                                                         CurrencyId = Currency.SerbianDinar.Id,
                                                         CreatedAt  = DateTime.UtcNow,
                                                         ModifiedAt = DateTime.UtcNow
                                                     };

        public static readonly CountryModel Switzerland = new()
                                                          {
                                                              Id         = Guid.Parse("87c5dc7f-38a9-4440-be01-fe7805553158"),
                                                              Name       = "Switzerland",
                                                              CurrencyId = Currency.SwissFranc.Id,
                                                              CreatedAt  = DateTime.UtcNow,
                                                              ModifiedAt = DateTime.UtcNow
                                                          };

        public static readonly CountryModel UnitedKingdom = new()
                                                            {
                                                                Id         = Guid.Parse("c743100b-e041-4c51-8943-949e0b4bf9fd"),
                                                                Name       = "United Kingdom",
                                                                CurrencyId = Currency.BritishPound.Id,
                                                                CreatedAt  = DateTime.UtcNow,
                                                                ModifiedAt = DateTime.UtcNow
                                                            };

        public static readonly CountryModel UnitedStates = new()
                                                           {
                                                               Id         = Guid.Parse("f382099b-40a7-4e29-9fe0-bcf2f500d842"),
                                                               Name       = "United States",
                                                               CurrencyId = Currency.USDollar.Id,
                                                               CreatedAt  = DateTime.UtcNow,
                                                               ModifiedAt = DateTime.UtcNow
                                                           };
    }
}

public static class CountrySeederExtension
{
    public static async Task SeedCountry(this ApplicationContext context)
    {
        if (context.Countries.Any())
            return;

        await context.Countries.AddRangeAsync([
                                                  Seeder.Country.Germany, Seeder.Country.Japan, Seeder.Country.Serbia, Seeder.Country.Switzerland,
                                                  Seeder.Country.UnitedKingdom, Seeder.Country.UnitedStates
                                              ]);

        await context.SaveChangesAsync();
    }
}
