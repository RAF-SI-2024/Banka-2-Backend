using Bank.ExchangeService.Model;

namespace Bank.ExchangeService.Database.Seeders;

using StockExchangeModel = StockExchange;

public static partial class Seeder
{
    public static class StockExchange
    {
        public static readonly StockExchangeModel Nasdaq = new()
                                                           {
                                                               Id         = Guid.Parse("00b0538d-4c7e-4e3c-b8f8-e101654e57fe"),
                                                               Name       = "Nasdaq",
                                                               Acronym    = "NASDAQ",
                                                               MIC        = "XNAS",
                                                               Polity     = "USA",
                                                               CurrencyId = Guid.Parse("81bf331a-0a35-4716-ad12-d1d1bcf66627"), // USD
                                                               TimeZone   = TimeSpan.FromHours(-5),                             // EST/EDT
                                                               MarketOpen = TimeSpan.FromHours(13)
                                                                                    .Add(TimeSpan.FromMinutes(30)), // 13:30 UTC (EDT) / 14:30 UTC (EST)
                                                               MarketClose = TimeSpan.FromHours(20),                // 20:00 UTC (EDT) / 21:00 UTC (EST)
                                                               CreatedAt   = DateTime.UtcNow,
                                                               ModifiedAt  = DateTime.UtcNow
                                                           };

        public static readonly StockExchangeModel IEX = new()
                                                        {
                                                            Id         = Guid.Parse("1f2e3d4c-5b6a-4978-8c9d-0e1f2a3b4c5d"),
                                                            Name       = "Investors Exchange",
                                                            Acronym    = "IEX",
                                                            MIC        = "IEXG",
                                                            Polity     = "USA",
                                                            CurrencyId = Guid.Parse("81bf331a-0a35-4716-ad12-d1d1bcf66627"), // USD
                                                            TimeZone   = TimeSpan.FromHours(-5),                             // EST/EDT
                                                            MarketOpen = TimeSpan.FromHours(13)
                                                                                 .Add(TimeSpan.FromMinutes(30)), // 13:30 UTC (EDT) / 14:30 UTC (EST)
                                                            MarketClose = TimeSpan.FromHours(20),                // 20:00 UTC (EDT) / 21:00 UTC (EST)
                                                            CreatedAt   = DateTime.UtcNow,
                                                            ModifiedAt  = DateTime.UtcNow
                                                        };

        public static readonly StockExchangeModel ASX = new()
                                                        {
                                                            Id          = Guid.Parse("f92a6507-e8d5-4cb2-bac5-3b40fdc3bbaf"),
                                                            Name        = "Asx - Trade24",
                                                            Acronym     = "SFE",
                                                            MIC         = "XSFE",
                                                            Polity      = "Australia",
                                                            CurrencyId  = Guid.Parse("895ab6f9-8a9a-4532-bea7-b3361d0dc936"), // AUD
                                                            TimeZone    = TimeSpan.FromHours(10),                             // AEST/AEDT
                                                            MarketOpen  = TimeSpan.FromHours(23),                             // 23:00 UTC (AEDT) / 00:00 UTC (AEST)
                                                            MarketClose = TimeSpan.FromHours(5),                              // 05:00 UTC (AEDT) / 06:00 UTC (AEST)
                                                            CreatedAt   = DateTime.UtcNow,
                                                            ModifiedAt  = DateTime.UtcNow
                                                        };

        public static readonly StockExchangeModel EDGADark = new()
                                                             {
                                                                 Id         = Guid.Parse("e0dc796a-c8d8-44f7-9716-407782e870df"),
                                                                 Name       = "Cboe Edga U.s. Equities Exchange Dark",
                                                                 Acronym    = "EDGADARK",
                                                                 MIC        = "EDGD",
                                                                 Polity     = "United States",
                                                                 CurrencyId = Guid.Parse("81bf331a-0a35-4716-ad12-d1d1bcf66627"), // USD
                                                                 TimeZone   = TimeSpan.FromHours(-5),                             // EST/EDT
                                                                 MarketOpen = TimeSpan.FromHours(13)
                                                                                      .Add(TimeSpan.FromMinutes(30)), // 13:30 UTC (EDT) / 14:30 UTC (EST)
                                                                 MarketClose = TimeSpan.FromHours(20),                // 20:00 UTC (EDT) / 21:00 UTC (EST)
                                                                 CreatedAt   = DateTime.UtcNow,
                                                                 ModifiedAt  = DateTime.UtcNow
                                                             };

        public static readonly StockExchangeModel ClearStreet = new()
                                                                {
                                                                    Id         = Guid.Parse("d5041fff-189f-4aea-bd52-fdc91c0a0483"),
                                                                    Name       = "Clear Street",
                                                                    Acronym    = "CLST",
                                                                    MIC        = "CLST",
                                                                    Polity     = "United States",
                                                                    CurrencyId = Guid.Parse("81bf331a-0a35-4716-ad12-d1d1bcf66627"), // USD
                                                                    TimeZone   = TimeSpan.FromHours(-5),                             // EST/EDT
                                                                    MarketOpen = TimeSpan.FromHours(13)
                                                                                         .Add(TimeSpan.FromMinutes(30)), // 13:30 UTC (EDT) / 14:30 UTC (EST)
                                                                    MarketClose = TimeSpan.FromHours(20),                // 20:00 UTC (EDT) / 21:00 UTC (EST)
                                                                    CreatedAt   = DateTime.UtcNow,
                                                                    ModifiedAt  = DateTime.UtcNow
                                                                };

        public static readonly StockExchangeModel MarexIreland = new()
                                                                 {
                                                                     Id         = Guid.Parse("d1a9ea53-e940-49f0-b9d6-4145b3902eaf"),
                                                                     Name       = "Marex Spectron Europe Limited - Otf",
                                                                     Acronym    = "MSEL OTF",
                                                                     MIC        = "MSEL",
                                                                     Polity     = "Ireland",
                                                                     CurrencyId = Guid.Parse("6842a5fa-eee4-4438-bcff-5217b6ac6ace"), // Euro
                                                                     TimeZone   = TimeSpan.Zero,                                      // GMT/BST
                                                                     MarketOpen = TimeSpan.FromHours(8),                              // 08:00 UTC (GMT) / 07:00 UTC (BST)
                                                                     MarketClose = TimeSpan.FromHours(16)
                                                                                           .Add(TimeSpan.FromMinutes(30)), // 16:30 UTC (GMT) / 15:30 UTC (BST)
                                                                     CreatedAt  = DateTime.UtcNow,
                                                                     ModifiedAt = DateTime.UtcNow
                                                                 };

        public static readonly StockExchangeModel BorsaItaliana = new()
                                                                  {
                                                                      Id         = Guid.Parse("c2d3a01d-1ec9-4899-b1d2-bcde6a3e139e"),
                                                                      Name       = "Borsa Italiana Equity Mtf",
                                                                      Acronym    = "BITEQMTF",
                                                                      MIC        = "MTAH",
                                                                      Polity     = "Italy",
                                                                      CurrencyId = Guid.Parse("6842a5fa-eee4-4438-bcff-5217b6ac6ace"), // Euro
                                                                      TimeZone   = TimeSpan.FromHours(1),                              // CET/CEST
                                                                      MarketOpen = TimeSpan.FromHours(8),                              // 08:00 UTC (CET) / 07:00 UTC (CEST)
                                                                      MarketClose = TimeSpan.FromHours(17)
                                                                                            .Add(TimeSpan.FromMinutes(30)), // 17:30 UTC (CET) / 16:30 UTC (CEST)
                                                                      CreatedAt  = DateTime.UtcNow,
                                                                      ModifiedAt = DateTime.UtcNow
                                                                  };

        public static readonly StockExchangeModel ForexMarket = new()
                                                                {
                                                                    Id          = Guid.Parse("a1b2c3d4-e5f6-4718-9a0b-cd1e2f3a4b5c"),
                                                                    Name        = "Global Forex Market",
                                                                    Acronym     = "FOREX",
                                                                    MIC         = "XXXX",
                                                                    Polity      = "International",
                                                                    CurrencyId  = Guid.Parse("81bf331a-0a35-4716-ad12-d1d1bcf66627"), // USD
                                                                    TimeZone    = TimeSpan.Zero,
                                                                    MarketOpen  = TimeSpan.FromHours(22), // Sunday 22:00 UTC
                                                                    MarketClose = TimeSpan.FromHours(22), // Friday 22:00 UTC (24/7 except weekends)
                                                                    CreatedAt   = DateTime.UtcNow,
                                                                    ModifiedAt  = DateTime.UtcNow
                                                                };

        public static readonly StockExchangeModel CME = new()
                                                        {
                                                            Id         = Guid.Parse("c3d8d466-22c0-487c-8247-c229efda2a03"),
                                                            Name       = "Chicago Mercantile Exchange",
                                                            Acronym    = "CME",
                                                            MIC        = "XCME",
                                                            Polity     = "United States",
                                                            CurrencyId = Guid.Parse("81bf331a-0a35-4716-ad12-d1d1bcf66627"), // USD
                                                            TimeZone   = TimeSpan.FromHours(-6),                             // CST/CDT
                                                            MarketOpen = TimeSpan.FromHours(14)
                                                                                 .Add(TimeSpan.FromMinutes(30)), // 14:30 UTC (CDT) / 15:30 UTC (CST)
                                                            MarketClose = TimeSpan.FromHours(21),                // 21:00 UTC (CDT) / 22:00 UTC (CST)
                                                            CreatedAt   = DateTime.UtcNow,
                                                            ModifiedAt  = DateTime.UtcNow
                                                        };

        public static readonly StockExchangeModel ICE = new()
                                                        {
                                                            Id         = Guid.Parse("bdeb3a2c-831c-44a3-8ce0-81bc7a9c3773"),
                                                            Name       = "Intercontinental Exchange",
                                                            Acronym    = "ICE",
                                                            MIC        = "IEPA",
                                                            Polity     = "United States",
                                                            CurrencyId = Guid.Parse("81bf331a-0a35-4716-ad12-d1d1bcf66627"), // USD
                                                            TimeZone   = TimeSpan.FromHours(-5),                             // EST/EDT
                                                            MarketOpen = TimeSpan.FromHours(13)
                                                                                 .Add(TimeSpan.FromMinutes(30)), // 13:30 UTC (EDT) / 14:30 UTC (EST)
                                                            MarketClose = TimeSpan.FromHours(20),                // 20:00 UTC (EDT) / 21:00 UTC (EST)
                                                            CreatedAt   = DateTime.UtcNow,
                                                            ModifiedAt  = DateTime.UtcNow
                                                        };

        public static readonly StockExchangeModel LME = new()
                                                        {
                                                            Id          = Guid.Parse("030500c4-f824-4662-a77e-799d22863381"),
                                                            Name        = "London Metal Exchange",
                                                            Acronym     = "LME",
                                                            MIC         = "XLME",
                                                            Polity      = "United Kingdom",
                                                            CurrencyId  = Guid.Parse("8e8e9283-4ced-4d9e-aa4a-1036d0174c8c"), // GBP
                                                            TimeZone    = TimeSpan.Zero,                                      // GMT/BST
                                                            MarketOpen  = TimeSpan.FromHours(8),                              // 08:00 UTC (GMT) / 07:00 UTC (BST)
                                                            MarketClose = TimeSpan.FromHours(17),                             // 17:00 UTC (GMT) / 16:00 UTC (BST)
                                                            CreatedAt   = DateTime.UtcNow,
                                                            ModifiedAt  = DateTime.UtcNow
                                                        };

        public static readonly StockExchangeModel TOCOM = new()
                                                          {
                                                              Id         = Guid.Parse("0e6b8da5-ef55-442b-af78-7bf8040980e1"),
                                                              Name       = "Tokyo Commodity Exchange",
                                                              Acronym    = "TOCOM",
                                                              MIC        = "XTKM",
                                                              Polity     = "Japan",
                                                              CurrencyId = Guid.Parse("1a77ed84-d984-4410-85ec-ffde69508625"), // JPY
                                                              TimeZone   = TimeSpan.FromHours(9),                              // JST
                                                              MarketOpen = TimeSpan.FromHours(0),                              // 00:00 UTC (Morning session)
                                                              MarketClose = TimeSpan.FromHours(22)
                                                                                    .Add(TimeSpan.FromMinutes(30)), // 22:30 UTC (Evening session)
                                                              CreatedAt  = DateTime.UtcNow,
                                                              ModifiedAt = DateTime.UtcNow
                                                          };
    }
}

public static class StockExchangeSeederExtension
{
    public static async Task SeedStockExchanges(this DatabaseContext context)
    {
        if (context.StockExchanges.Any())
            return;
        
        // Only seed IEX in production
        await context.StockExchanges.AddRangeAsync(Seeder.StockExchange.IEX, Seeder.StockExchange.ForexMarket);
        await context.SaveChangesAsync();
    }

    public static async Task SeedHardcodedStockExchanges(this DatabaseContext context)
    {
        if (context.StockExchanges.Any())
            return;
        
        // Seed all exchanges in development/staging
        await context.StockExchanges.AddRangeAsync(Seeder.StockExchange.Nasdaq, Seeder.StockExchange.IEX, Seeder.StockExchange.ASX, Seeder.StockExchange.EDGADark,
                                                   Seeder.StockExchange.ClearStreet, Seeder.StockExchange.MarexIreland, Seeder.StockExchange.BorsaItaliana,
                                                   Seeder.StockExchange.ForexMarket, Seeder.StockExchange.CME, Seeder.StockExchange.ICE, Seeder.StockExchange.LME,
                                                   Seeder.StockExchange.TOCOM);

        await context.SaveChangesAsync();
    }
}
