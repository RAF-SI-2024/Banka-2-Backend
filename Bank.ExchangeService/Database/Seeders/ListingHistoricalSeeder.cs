using Bank.ExchangeService.Model;

namespace Bank.ExchangeService.Database.Seeders;

using ListingHistoricalModel = ListingHistorical;

public static partial class Seeder
{
    public static class ListingHistorical
    {
        // Microsoft historical data
        public static readonly ListingHistoricalModel MicrosoftDay1 = new()
                                                                      {
                                                                          Id           = Guid.Parse("8cce7980-cf2b-4159-bdef-75412a5ec401"),
                                                                          ListingId    = Listing.MicrosoftStock.Id,
                                                                          ClosingPrice = 337.95m,
                                                                          HighPrice    = 340.86m,
                                                                          LowPrice     = 336.31m,
                                                                          PriceChange  = 2.47m,
                                                                          Volume       = 21458632,
                                                                          CreatedAt    = DateTime.UtcNow.AddDays(-1),
                                                                          ModifiedAt   = DateTime.UtcNow.AddDays(-1)
                                                                      };

        public static readonly ListingHistoricalModel MicrosoftDay2 = new()
                                                                      {
                                                                          Id           = Guid.Parse("723efc9d-1e5b-4fce-98e2-4e230d8a2c2e"),
                                                                          ListingId    = Listing.MicrosoftStock.Id,
                                                                          ClosingPrice = 335.48m,
                                                                          HighPrice    = 337.99m,
                                                                          LowPrice     = 333.25m,
                                                                          PriceChange  = -1.62m,
                                                                          Volume       = 18245730,
                                                                          CreatedAt    = DateTime.UtcNow.AddDays(-2),
                                                                          ModifiedAt   = DateTime.UtcNow.AddDays(-2)
                                                                      };

        // Apple historical data
        public static readonly ListingHistoricalModel AppleDay1 = new()
                                                                  {
                                                                      Id           = Guid.Parse("5e8c2d1f-3a7b-46c9-8d14-7382b9f562e9"),
                                                                      ListingId    = Listing.AppleStock.Id,
                                                                      ClosingPrice = 172.62m,
                                                                      HighPrice    = 174.30m,
                                                                      LowPrice     = 171.81m,
                                                                      PriceChange  = 0.87m,
                                                                      Volume       = 63428715,
                                                                      CreatedAt    = DateTime.UtcNow.AddDays(-1),
                                                                      ModifiedAt   = DateTime.UtcNow.AddDays(-1)
                                                                  };

        public static readonly ListingHistoricalModel AppleDay2 = new()
                                                                  {
                                                                      Id           = Guid.Parse("3f6d8b2a-7e1c-4d5f-9a0b-28c461d93e5a"),
                                                                      ListingId    = Listing.AppleStock.Id,
                                                                      ClosingPrice = 171.75m,
                                                                      HighPrice    = 173.95m,
                                                                      LowPrice     = 170.82m,
                                                                      PriceChange  = -1.23m,
                                                                      Volume       = 58762143,
                                                                      CreatedAt    = DateTime.UtcNow.AddDays(-2),
                                                                      ModifiedAt   = DateTime.UtcNow.AddDays(-2)
                                                                  };

        // Google historical data
        public static readonly ListingHistoricalModel GoogleDay1 = new()
                                                                   {
                                                                       Id           = Guid.Parse("1d2e3f4a-5b6c-7d8e-9f0a-1b2c3d4e5f6a"),
                                                                       ListingId    = Listing.GoogleStock.Id,
                                                                       ClosingPrice = 146.35m,
                                                                       HighPrice    = 148.50m,
                                                                       LowPrice     = 145.87m,
                                                                       PriceChange  = 1.25m,
                                                                       Volume       = 31254879,
                                                                       CreatedAt    = DateTime.UtcNow.AddDays(-1),
                                                                       ModifiedAt   = DateTime.UtcNow.AddDays(-1)
                                                                   };

        // Amazon historical data
        public static readonly ListingHistoricalModel AmazonDay1 = new()
                                                                   {
                                                                       Id           = Guid.Parse("a2b3c4d5-e6f7-8a9b-0c1d-2e3f4a5b6c7d"),
                                                                       ListingId    = Listing.AmazonStock.Id,
                                                                       ClosingPrice = 169.75m,
                                                                       HighPrice    = 171.80m,
                                                                       LowPrice     = 168.35m,
                                                                       PriceChange  = 2.15m,
                                                                       Volume       = 42568731,
                                                                       CreatedAt    = DateTime.UtcNow.AddDays(-1),
                                                                       ModifiedAt   = DateTime.UtcNow.AddDays(-1)
                                                                   };

        // JPMorgan historical data
        public static readonly ListingHistoricalModel JPMorganDay1 = new()
                                                                     {
                                                                         Id           = Guid.Parse("f7e8d9c0-b1a2-4c3d-5e6f-7a8b9c0d1e2f"),
                                                                         ListingId    = Listing.JPMorganStock.Id,
                                                                         ClosingPrice = 187.35m,
                                                                         HighPrice    = 188.95m,
                                                                         LowPrice     = 186.20m,
                                                                         PriceChange  = 0.95m,
                                                                         Volume       = 15783946,
                                                                         CreatedAt    = DateTime.UtcNow.AddDays(-1),
                                                                         ModifiedAt   = DateTime.UtcNow.AddDays(-1)
                                                                     };
    }
}

public static class ListingHistoricalSeederExtension
{
    public static async Task SeedListingHistoricals(this DatabaseContext context)
    {
        if (context.ListingHistoricals.Any())
            return;

        await context.ListingHistoricals.AddRangeAsync(Seeder.ListingHistorical.MicrosoftDay1, Seeder.ListingHistorical.MicrosoftDay2, Seeder.ListingHistorical.AppleDay1,
                                                       Seeder.ListingHistorical.AppleDay2, Seeder.ListingHistorical.GoogleDay1, Seeder.ListingHistorical.AmazonDay1,
                                                       Seeder.ListingHistorical.JPMorganDay1);

        await context.SaveChangesAsync();
    }
}
