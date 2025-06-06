using Bank.Application.Responses;
using Bank.ExchangeService.Database.Seeders;

namespace Bank.ExchangeService.Database.Examples;

public static partial class Example
{
    public static class Stock
    {
        public static readonly StockSimpleResponse SimpleResponse = new()
                                                                    {
                                                                        Id                           = Seeder.Stock.Amazon.Id,
                                                                        Name                         = Seeder.Stock.Amazon.Name,
                                                                        Ticker                       = Seeder.Stock.Amazon.Ticker,
                                                                        HighPrice                    = Seeder.Stock.Amazon.HighPrice,
                                                                        LowPrice                     = Seeder.Stock.Amazon.LowPrice,
                                                                        AskPrice                     = Seeder.Stock.Amazon.AskPrice,
                                                                        BidPrice                     = Seeder.Stock.Amazon.BidPrice,
                                                                        Volume                       = Seeder.Stock.Amazon.Volume,
                                                                        PriceChangeInInterval        = Seeder.Stock.Amazon.PriceChange,
                                                                        PriceChangePercentInInterval = Seeder.Stock.Amazon.PriceChangePercent,
                                                                        CreatedAt                    = DateTime.UtcNow,
                                                                        ModifiedAt                   = DateTime.UtcNow,
                                                                        AskSize                      = 15,
                                                                        BidSize                      = 14,
                                                                        ContractCount                = 1
                                                                    };

        public static readonly StockResponse Response = new()
                                                        {
                                                            Id                           = Seeder.Stock.Amazon.Id,
                                                            Name                         = Seeder.Stock.Amazon.Name,
                                                            Ticker                       = Seeder.Stock.Amazon.Ticker,
                                                            HighPrice                    = Seeder.Stock.Amazon.HighPrice,
                                                            LowPrice                     = Seeder.Stock.Amazon.LowPrice,
                                                            AskPrice                     = Seeder.Stock.Amazon.AskPrice,
                                                            BidPrice                     = Seeder.Stock.Amazon.BidPrice,
                                                            Volume                       = Seeder.Stock.Amazon.Volume,
                                                            PriceChangeInInterval        = Seeder.Stock.Amazon.PriceChange,
                                                            PriceChangePercentInInterval = Seeder.Stock.Amazon.PriceChange,
                                                            CreatedAt                    = DateTime.UtcNow,
                                                            ModifiedAt                   = DateTime.UtcNow,
                                                            StockExchange                = null!,
                                                            Quotes                       = [],
                                                            AskSize                      = 24,
                                                            BidSize                      = 13,
                                                            ContractCount                = 1
                                                        };

        public static readonly StockDailyResponse DailyResponse = new()
                                                                  {
                                                                      Id                           = Seeder.Stock.Amazon.Id,
                                                                      Name                         = Seeder.Stock.Amazon.Name,
                                                                      Ticker                       = Seeder.Stock.Amazon.Ticker,
                                                                      HighPrice                    = Seeder.Stock.Amazon.HighPrice,
                                                                      LowPrice                     = Seeder.Stock.Amazon.LowPrice,
                                                                      OpenPrice                    = Seeder.Stock.Amazon.OpeningPrice,
                                                                      ClosePrice                   = Seeder.Stock.Amazon.ClosePrice,
                                                                      Volume                       = Seeder.Stock.Amazon.Volume,
                                                                      PriceChangeInInterval        = Seeder.Stock.Amazon.PriceChange,
                                                                      PriceChangePercentInInterval = Seeder.Stock.Amazon.PriceChangePercent,
                                                                      CreatedAt                    = DateTime.UtcNow,
                                                                      ModifiedAt                   = DateTime.UtcNow,
                                                                      StockExchange                = null!,
                                                                      Quotes                       = []
                                                                  };
    }
}
