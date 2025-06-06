using Bank.Application.Domain;
using Bank.Application.Responses;
using Bank.ExchangeService.Database.Seeders;

namespace Bank.ExchangeService.Database.Examples;

public static partial class Example
{
    public static class ForexPair
    {
        public static readonly ForexPairResponse Response = new()
                                                            {
                                                                Id            = Seeder.ForexPair.GbpAud.Id,
                                                                Liquidity     = (Liquidity)Seeder.ForexPair.GbpAud.Liquidity!,
                                                                BaseCurrency  = null!,
                                                                QuoteCurrency = null!,
                                                                ExchangeRate  = Seeder.ForexPair.GbpAud.ExchangeRate,
                                                                MaintenanceDecimal = (decimal)0.1 * Seeder.ForexPair.GbpAud.ContractSize *
                                                                                     Seeder.ForexPair.GbpAud.ExchangeRate,
                                                                Name                         = Seeder.ForexPair.GbpAud.Name,
                                                                Ticker                       = Seeder.ForexPair.GbpAud.Ticker,
                                                                HighPrice                    = Seeder.ForexPair.GbpAud.HighPrice,
                                                                LowPrice                     = Seeder.ForexPair.GbpAud.LowPrice,
                                                                AskPrice                     = Seeder.ForexPair.GbpAud.AskPrice,
                                                                BidPrice                     = Seeder.ForexPair.GbpAud.BidPrice,
                                                                PriceChangeInInterval        = Seeder.ForexPair.GbpAud.PriceChange,
                                                                PriceChangePercentInInterval = Seeder.ForexPair.GbpAud.PriceChangePercent,
                                                                CreatedAt                    = DateTime.UtcNow,
                                                                ModifiedAt                   = DateTime.UtcNow,
                                                                StockExchange                = null!,
                                                                Quotes                       = [],
                                                                AskSize                      = 20,
                                                                BidSize                      = 12,
                                                                ContractCount                = 1
                                                            };

        public static readonly ForexPairSimpleResponse SimpleResponse = new()
                                                                        {
                                                                            Id            = Seeder.ForexPair.GbpAud.Id,
                                                                            Liquidity     = (Liquidity)Seeder.ForexPair.GbpAud.Liquidity,
                                                                            BaseCurrency  = null!,
                                                                            QuoteCurrency = null!,
                                                                            ExchangeRate  = Seeder.ForexPair.GbpAud.ExchangeRate,
                                                                            MaintenanceDecimal = (decimal)0.1 * Seeder.ForexPair.GbpAud.ContractSize *
                                                                                                 Seeder.ForexPair.GbpAud.ExchangeRate,
                                                                            Name                         = Seeder.ForexPair.GbpAud.Name,
                                                                            Ticker                       = Seeder.ForexPair.GbpAud.Ticker,
                                                                            HighPrice                    = Seeder.ForexPair.GbpAud.HighPrice,
                                                                            LowPrice                     = Seeder.ForexPair.GbpAud.LowPrice,
                                                                            AskPrice                     = Seeder.ForexPair.GbpAud.AskPrice,
                                                                            BidPrice                     = Seeder.ForexPair.GbpAud.BidPrice,
                                                                            PriceChangeInInterval        = Seeder.ForexPair.GbpAud.PriceChange,
                                                                            PriceChangePercentInInterval = Seeder.ForexPair.GbpAud.PriceChangePercent,
                                                                            Price                        = Seeder.ForexPair.GbpAud.AskPrice,
                                                                            CreatedAt                    = DateTime.UtcNow,
                                                                            ModifiedAt                   = DateTime.UtcNow,
                                                                            AskSize                      = 45,
                                                                            BidSize                      = 40,
                                                                            ContractCount                = 1
                                                                        };

        public static readonly ForexPairDailyResponse DailyResponse = new()
                                                                      {
                                                                          Id            = Seeder.ForexPair.GbpAud.Id,
                                                                          Liquidity     = (Liquidity)Seeder.ForexPair.GbpAud.Liquidity,
                                                                          BaseCurrency  = null!,
                                                                          QuoteCurrency = null!,
                                                                          ExchangeRate  = Seeder.ForexPair.GbpAud.ExchangeRate,
                                                                          MaintenanceDecimal = (decimal)0.1 * Seeder.ForexPair.GbpAud.ContractSize *
                                                                                               Seeder.ForexPair.GbpAud.ExchangeRate,
                                                                          Name                         = Seeder.ForexPair.GbpAud.Name,
                                                                          Ticker                       = Seeder.ForexPair.GbpAud.Ticker,
                                                                          HighPrice                    = Seeder.ForexPair.GbpAud.HighPrice,
                                                                          LowPrice                     = Seeder.ForexPair.GbpAud.LowPrice,
                                                                          OpeningPrice                 = Seeder.ForexPair.GbpAud.OpeningPrice,
                                                                          ClosePrice                   = Seeder.ForexPair.GbpAud.ClosePrice,
                                                                          PriceChangeInInterval        = Seeder.ForexPair.GbpAud.PriceChange,
                                                                          PriceChangePercentInInterval = Seeder.ForexPair.GbpAud.PriceChangePercent,
                                                                          CreatedAt                    = DateTime.UtcNow,
                                                                          ModifiedAt                   = DateTime.UtcNow,
                                                                          StockExchange                = null!,
                                                                          Quotes                       = []
                                                                      };
    }
}
