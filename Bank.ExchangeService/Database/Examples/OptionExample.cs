using Bank.Application.Domain;
using Bank.Application.Responses;
using Bank.ExchangeService.Database.Seeders;

namespace Bank.ExchangeService.Database.Examples;

public static partial class Example
{
    public static class Option
    {
        public static readonly OptionResponse Response = new()
                                                         {
                                                             Id                           = Seeder.Option.MicrosoftCallOption.Id,
                                                             StrikePrice                  = Seeder.Option.MicrosoftCallOption.StrikePrice,
                                                             ImpliedVolatility            = 0m,
                                                             SettlementDate               = Seeder.Option.MicrosoftCallOption.SettlementDate,
                                                             Name                         = Seeder.Option.MicrosoftCallOption.Name,
                                                             Ticker                       = Seeder.Option.MicrosoftCallOption.Ticker,
                                                             OptionType                   = (OptionType)Seeder.Option.MicrosoftCallOption.OptionType!,
                                                             HighPrice                    = Seeder.Option.MicrosoftCallOption.HighPrice,
                                                             LowPrice                     = Seeder.Option.MicrosoftCallOption.LowPrice,
                                                             Volume                       = Seeder.Option.MicrosoftCallOption.Volume,
                                                             PriceChangeInInterval        = Seeder.Option.MicrosoftCallOption.PriceChange,
                                                             PriceChangePercentInInterval = Seeder.Option.MicrosoftCallOption.PriceChangePercent,
                                                             AskPrice                     = Seeder.Option.MicrosoftCallOption.AskPrice,
                                                             BidPrice                     = Seeder.Option.MicrosoftCallOption.BidPrice,
                                                             CreatedAt                    = DateTime.UtcNow,
                                                             ModifiedAt                   = DateTime.UtcNow,
                                                             StockExchange                = null!,
                                                             Quotes                       = [],
                                                             AskSize                      = 50,
                                                             BidSize                      = 150,
                                                             ContractCount                = 1
                                                         };

        public static readonly OptionSimpleResponse SimpleResponse = new()
                                                                     {
                                                                         Id                           = Seeder.Option.MicrosoftCallOption.Id,
                                                                         StrikePrice                  = Seeder.Option.MicrosoftCallOption.StrikePrice,
                                                                         ImpliedVolatility            = 0m,
                                                                         SettlementDate               = Seeder.Option.MicrosoftCallOption.SettlementDate,
                                                                         Name                         = Seeder.Option.MicrosoftCallOption.Name,
                                                                         Ticker                       = Seeder.Option.MicrosoftCallOption.Ticker,
                                                                         OptionType                   = (OptionType)Seeder.Option.MicrosoftCallOption.OptionType,
                                                                         HighPrice                    = Seeder.Option.MicrosoftCallOption.HighPrice,
                                                                         LowPrice                     = Seeder.Option.MicrosoftCallOption.LowPrice,
                                                                         Volume                       = Seeder.Option.MicrosoftCallOption.Volume,
                                                                         PriceChange                  = Seeder.Option.MicrosoftCallOption.PriceChange,
                                                                         PriceChangeInInterval        = Seeder.Option.MicrosoftCallOption.PriceChange,
                                                                         PriceChangePercentInInterval = Seeder.Option.MicrosoftCallOption.PriceChangePercent,
                                                                         AskPrice                     = Seeder.Option.MicrosoftCallOption.AskPrice,
                                                                         BidPrice                     = Seeder.Option.MicrosoftCallOption.BidPrice,
                                                                         CreatedAt                    = DateTime.UtcNow,
                                                                         ModifiedAt                   = DateTime.UtcNow,
                                                                         AskSize                      = 40,
                                                                         BidSize                      = 90,
                                                                         ContractCount                = 1
                                                                     };

        public static readonly OptionDailyResponse DailyResponse = new()
                                                                   {
                                                                       Id                           = Seeder.Option.MicrosoftCallOption.Id,
                                                                       StrikePrice                  = Seeder.Option.MicrosoftCallOption.StrikePrice,
                                                                       ImpliedVolatility            = 0m,
                                                                       SettlementDate               = Seeder.Option.MicrosoftCallOption.SettlementDate,
                                                                       Name                         = Seeder.Option.MicrosoftCallOption.Name,
                                                                       Ticker                       = Seeder.Option.MicrosoftCallOption.Ticker,
                                                                       OptionType                   = (OptionType)Seeder.Option.MicrosoftCallOption.OptionType,
                                                                       HighPrice                    = Seeder.Option.MicrosoftCallOption.HighPrice,
                                                                       LowPrice                     = Seeder.Option.MicrosoftCallOption.LowPrice,
                                                                       OpeningPrice                 = Seeder.Option.MicrosoftCallOption.OpeningPrice,
                                                                       ClosePrice                   = Seeder.Option.MicrosoftCallOption.ClosePrice,
                                                                       Volume                       = Seeder.Option.MicrosoftCallOption.Volume,
                                                                       PriceChangeInInterval        = Seeder.Option.MicrosoftCallOption.PriceChange,
                                                                       PriceChangePercentInInterval = Seeder.Option.MicrosoftCallOption.PriceChangePercent,
                                                                       CreatedAt                    = DateTime.UtcNow,
                                                                       ModifiedAt                   = DateTime.UtcNow,
                                                                       StockExchange                = null!,
                                                                       Quotes                       = [],
                                                                   };
    }
}
