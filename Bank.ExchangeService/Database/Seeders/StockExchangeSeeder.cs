using Bank.ExchangeService.Model;

namespace Bank.ExchangeService.Database.Seeders.Resource;

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
                                                               TimeZone   = TimeSpan.Zero,
                                                               CreatedAt  = DateTime.UtcNow,
                                                               ModifiedAt = DateTime.UtcNow
                                                           };

        public static readonly StockExchangeModel ASX = new()
                                                        {
                                                            Id         = Guid.Parse("f92a6507-e8d5-4cb2-bac5-3b40fdc3bbaf"),
                                                            Name       = "Asx - Trade24",
                                                            Acronym    = "SFE",
                                                            MIC        = "XSFE",
                                                            Polity     = "Australia",
                                                            CurrencyId = Guid.Parse("895ab6f9-8a9a-4532-bea7-b3361d0dc936"), // AUD
                                                            TimeZone   = TimeSpan.Zero,
                                                            CreatedAt  = DateTime.UtcNow,
                                                            ModifiedAt = DateTime.UtcNow
                                                        };

        public static readonly StockExchangeModel EDGADark = new()
                                                             {
                                                                 Id         = Guid.Parse("e0dc796a-c8d8-44f7-9716-407782e870df"),
                                                                 Name       = "Cboe Edga U.s. Equities Exchange Dark",
                                                                 Acronym    = "EDGADARK",
                                                                 MIC        = "EDGD",
                                                                 Polity     = "United States",
                                                                 CurrencyId = Guid.Parse("81bf331a-0a35-4716-ad12-d1d1bcf66627"), // USD
                                                                 TimeZone   = TimeSpan.Zero,
                                                                 CreatedAt  = DateTime.UtcNow,
                                                                 ModifiedAt = DateTime.UtcNow
                                                             };

        public static readonly StockExchangeModel ClearStreet = new()
                                                                {
                                                                    Id         = Guid.Parse("d5041fff-189f-4aea-bd52-fdc91c0a0483"),
                                                                    Name       = "Clear Street",
                                                                    Acronym    = "CLST",
                                                                    MIC        = "CLST",
                                                                    Polity     = "United States",
                                                                    CurrencyId = Guid.Parse("81bf331a-0a35-4716-ad12-d1d1bcf66627"), // USD
                                                                    TimeZone   = TimeSpan.Zero,
                                                                    CreatedAt  = DateTime.UtcNow,
                                                                    ModifiedAt = DateTime.UtcNow
                                                                };

        public static readonly StockExchangeModel MarexIreland = new()
                                                                 {
                                                                     Id         = Guid.Parse("d1a9ea53-e940-49f0-b9d6-4145b3902eaf"),
                                                                     Name       = "Marex Spectron Europe Limited - Otf",
                                                                     Acronym    = "MSEL OTF",
                                                                     MIC        = "MSEL",
                                                                     Polity     = "Ireland",
                                                                     CurrencyId = Guid.Parse("6842a5fa-eee4-4438-bcff-5217b6ac6ace"), // Euro
                                                                     TimeZone   = TimeSpan.Zero,
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
                                                                      TimeZone   = TimeSpan.Zero,
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

        await context.StockExchanges.AddRangeAsync(Seeder.StockExchange.Nasdaq, Seeder.StockExchange.ASX, Seeder.StockExchange.EDGADark, Seeder.StockExchange.ClearStreet,
                                                   Seeder.StockExchange.MarexIreland, Seeder.StockExchange.BorsaItaliana);

        await context.SaveChangesAsync();
    }
}
// public static class StockExchangeSeeder
// {
//     public static async Task SeedStockExchanges(this DatabaseContext context)
//     {
//         if (context.StockExchanges.Any())
//             return;
//
//         var exchanges = ReadExchangesFromCsv();
//         await context.StockExchanges.AddRangeAsync(exchanges);
//         await context.SaveChangesAsync();
//     }
//
//     private static List<StockExchangeModel> ReadExchangesFromCsv()
//     {
//         var exchanges = new List<StockExchangeModel>();
//         var now = DateTime.UtcNow;
//
//         var currencyMap = new Dictionary<string, Guid>(StringComparer.OrdinalIgnoreCase)
//         {
//             { "Euro", Guid.Parse("6842a5fa-eee4-4438-bcff-5217b6ac6ace") },
//             { "Swiss Franc", Guid.Parse("6665627e-be4d-48f2-9dd3-a01cbbd1dfa3") },
//             { "US Dollar", Guid.Parse("81bf331a-0a35-4716-ad12-d1d1bcf66627") },
//             { "British Pound", Guid.Parse("8e8e9283-4ced-4d9e-aa4a-1036d0174c8c") },
//             { "Japanese Yen", Guid.Parse("1a77ed84-d984-4410-85ec-ffde69508625") },
//             { "Canadian Dollar", Guid.Parse("3834d8f4-1703-4091-859c-07225b9ce2a6") },
//             { "Australian Dollar", Guid.Parse("895ab6f9-8a9a-4532-bea7-b3361d0dc936") },
//             { "Serbian Dinar", Guid.Parse("88bfe7f0-8f74-42f7-b6ba-07b3145da989") }
//         };
//
//         var baseDirectory    = AppContext.BaseDirectory;
//         var projectDirectory = Directory.GetParent(baseDirectory)!.Parent!.Parent!.Parent!.FullName;
//         var filePath         = Path.Combine(projectDirectory, "Database", "Seeders", "resource", "exchanges.csv");
//
//         string[] lines = File.ReadAllLines(filePath);
//
//         // Skip header
//         for (var i = 1; i < lines.Length; i++)
//         {
//             var line = lines[i];
//             string[] fields = ParseCsvLine(line);
//
//             if (fields.Length >= 5)
//             {
//                 var name     = fields[0];
//                 var acronym  = fields[1];
//                 var mic      = string.IsNullOrWhiteSpace(fields[2]) ? "XXXX" : fields[2];
//                 var polity   = string.IsNullOrWhiteSpace(fields[3]) ? "Unknown" : fields[3];
//                 var currency = fields[4];
//
//                 if (!currencyMap.TryGetValue(currency, out var currencyId))
//                     continue;
//
//                 var exchange = new StockExchangeModel
//                 {
//                     Id         = Guid.NewGuid(),
//                     Name       = name,
//                     Acronym    = acronym,
//                     MIC        = mic,
//                     Polity     = polity,
//                     CurrencyId = currencyId,
//                     TimeZone   = TimeSpan.Zero,
//                     CreatedAt  = now,
//                     ModifiedAt = now
//                 };
//
//                 exchanges.Add(exchange);
//             }
//         }
//
//         return exchanges;
//     }
//
//     private static string[] ParseCsvLine(string line)
//     {
//         var result = new List<string>();
//         var inQuotes = false;
//         var field = "";
//
//         for (var i = 0; i < line.Length; i++)
//         {
//             var c = line[i];
//
//             if (c == '"')
//             {
//                 inQuotes = !inQuotes;
//             }
//             else if (c == ',' && !inQuotes)
//             {
//                 result.Add(field);
//                 field = "";
//             }
//             else
//             {
//                 field += c;
//             }
//         }
//
//         result.Add(field);
//         return result.ToArray();
//     }
// }
