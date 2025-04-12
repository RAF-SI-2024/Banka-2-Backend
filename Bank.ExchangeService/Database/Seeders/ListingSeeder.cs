using Bank.ExchangeService.Model;

using Microsoft.EntityFrameworkCore;

namespace Bank.ExchangeService.Database.Seeders;

using ListingModel = Listing;

public static partial class Seeder
{
    public static class Listing
    {
        public static readonly ListingModel MicrosoftStock = new()
                                                             {
                                                                 Id              = Guid.Parse("a7e15ab3-4efb-44c9-a338-1ce3f8ac52f9"),
                                                                 Name            = "Microsoft Corporation",
                                                                 Ticker          = "MSFT",
                                                                 StockExchangeId = Guid.Parse("00b0538d-4c7e-4e3c-b8f8-e101654e57fe"), // NASDAQ
                                                                 CreatedAt       = DateTime.UtcNow,
                                                                 ModifiedAt      = DateTime.UtcNow
                                                             };

        public static readonly ListingModel AppleStock = new()
                                                         {
                                                             Id              = Guid.Parse("b9d87e0c-8c25-4f7a-95b4-143985d7211f"),
                                                             Name            = "Apple Inc.",
                                                             Ticker          = "AAPL",
                                                             StockExchangeId = Guid.Parse("ee3171c9-c290-41e0-adba-d9331ce76172"), // NASDAQ
                                                             CreatedAt       = DateTime.UtcNow,
                                                             ModifiedAt      = DateTime.UtcNow
                                                         };

        public static readonly ListingModel GoogleStock = new()
                                                          {
                                                              Id              = Guid.Parse("c5e83d2f-91b6-4f1c-a5e7-26d4c89a74b2"),
                                                              Name            = "Alphabet Inc.",
                                                              Ticker          = "GOOGL",
                                                              StockExchangeId = Guid.Parse("e0dc796a-c8d8-44f7-9716-407782e870df"), // NASDAQ
                                                              CreatedAt       = DateTime.UtcNow,
                                                              ModifiedAt      = DateTime.UtcNow
                                                          };

        public static readonly ListingModel AmazonStock = new()
                                                          {
                                                              Id              = Guid.Parse("d4f21e3b-8a7c-42d5-93b8-149c7581d73a"),
                                                              Name            = "Amazon.com, Inc.",
                                                              Ticker          = "AMZN",
                                                              StockExchangeId = Guid.Parse("d5041fff-189f-4aea-bd52-fdc91c0a0483"), // NASDAQ
                                                              CreatedAt       = DateTime.UtcNow,
                                                              ModifiedAt      = DateTime.UtcNow
                                                          };

        public static readonly ListingModel JPMorganStock = new()
                                                            {
                                                                Id              = Guid.Parse("e2c54a9d-1b47-40cf-85d1-8732fc875b58"),
                                                                Name            = "JPMorgan Chase & Co.",
                                                                Ticker          = "JPM",
                                                                StockExchangeId = Guid.Parse("d1a9ea53-e940-49f0-b9d6-4145b3902eaf"), // NYSE
                                                                CreatedAt       = DateTime.UtcNow,
                                                                ModifiedAt      = DateTime.UtcNow
                                                            };
    }
}

public static class ListingSeederExtension
{
    public static async Task SeedListings(this DatabaseContext context)
    {
        if (context.Listings.Any())
            return;

        var exchanges = await context.StockExchanges.ToListAsync();

        var exchangeMap = exchanges.GroupBy(e => e.Acronym)
                                   .ToDictionary(g => g.Key, g => g.First()
                                                                   .Id);

        // Dodeli tačne StockExchangeId vrednosti iz baze
        Seeder.Listing.MicrosoftStock.StockExchangeId = exchangeMap["NASDAQ"];
        Seeder.Listing.AppleStock.StockExchangeId     = exchangeMap["NASDAQ"];
        Seeder.Listing.GoogleStock.StockExchangeId    = exchangeMap["NASDAQ"];
        Seeder.Listing.AmazonStock.StockExchangeId    = exchangeMap["NASDAQ"];
        Seeder.Listing.JPMorganStock.StockExchangeId  = exchangeMap["NASDAQ"];

        await context.Listings.AddRangeAsync(Seeder.Listing.MicrosoftStock, Seeder.Listing.AppleStock, Seeder.Listing.GoogleStock, Seeder.Listing.AmazonStock,
                                             Seeder.Listing.JPMorganStock);

        await context.SaveChangesAsync();
    }
}
