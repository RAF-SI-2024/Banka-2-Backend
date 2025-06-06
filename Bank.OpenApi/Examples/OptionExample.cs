using Bank.Application.Responses;

namespace Bank.OpenApi.Examples;

public static partial class Example
{
    public static class Option
    {
        public static readonly OptionResponse DefaultResponse = new()
                                                                {
                                                                    Id                           = Constant.Id,
                                                                    StrikePrice                  = Constant.StrikePrice,
                                                                    ImpliedVolatility            = Constant.ImpliedVolatility,
                                                                    SettlementDate               = Constant.CreationDate,
                                                                    Name                         = Constant.OptionName,
                                                                    Ticker                       = Constant.Ticker,
                                                                    OptionType                   = Constant.OptionType,
                                                                    HighPrice                    = Constant.HighPrice,
                                                                    LowPrice                     = Constant.LowPrice,
                                                                    Volume                       = Constant.Volume,
                                                                    PriceChangeInInterval        = Constant.PriceChangeInInterval,
                                                                    PriceChangePercentInInterval = Constant.PriceChangePercentInInterval,
                                                                    AskPrice                     = Constant.AskPrice,
                                                                    AskSize                      = Constant.AskSize,
                                                                    BidPrice                     = Constant.BidPrice,
                                                                    BidSize                      = Constant.BidSize,
                                                                    CreatedAt                    = Constant.CreatedAt,
                                                                    ModifiedAt                   = Constant.ModifiedAt,
                                                                    StockExchange                = StockExchange.DefaultResponse,
                                                                    Quotes =
                                                                    [
                                                                        Quote.DefaultSimpleResponse
                                                                    ],
                                                                    ContractCount = Constant.ContractCount
                                                                };

        public static readonly OptionSimpleResponse DefaultSimpleResponse = new()
                                                                            {
                                                                                Id                           = Constant.Id,
                                                                                StrikePrice                  = Constant.StrikePrice,
                                                                                ImpliedVolatility            = Constant.ImpliedVolatility,
                                                                                SettlementDate               = Constant.CreationDate,
                                                                                Name                         = Constant.OptionName,
                                                                                Ticker                       = Constant.Ticker,
                                                                                OptionType                   = Constant.OptionType,
                                                                                HighPrice                    = Constant.HighPrice,
                                                                                LowPrice                     = Constant.LowPrice,
                                                                                Volume                       = Constant.Volume,
                                                                                PriceChange                  = Constant.PriceChangeInInterval,
                                                                                PriceChangeInInterval        = Constant.PriceChangeInInterval,
                                                                                PriceChangePercentInInterval = Constant.PriceChangePercentInInterval,
                                                                                AskPrice                     = Constant.AskPrice,
                                                                                AskSize                      = Constant.AskSize,
                                                                                BidPrice                     = Constant.BidPrice,
                                                                                BidSize                      = Constant.BidSize,
                                                                                CreatedAt                    = Constant.CreatedAt,
                                                                                ModifiedAt                   = Constant.ModifiedAt,
                                                                                ContractCount                = Constant.ContractCount
                                                                            };

        public static readonly OptionDailyResponse DefaultDailyResponse = new()
                                                                          {
                                                                              Id                           = Constant.Id,
                                                                              StrikePrice                  = Constant.StrikePrice,
                                                                              ImpliedVolatility            = Constant.ImpliedVolatility,
                                                                              SettlementDate               = Constant.CreationDate,
                                                                              Name                         = Constant.OptionName,
                                                                              Ticker                       = Constant.Ticker,
                                                                              OptionType                   = Constant.OptionType,
                                                                              HighPrice                    = Constant.HighPrice,
                                                                              LowPrice                     = Constant.LowPrice,
                                                                              OpeningPrice                 = Constant.OpeningPrice,
                                                                              ClosePrice                   = Constant.ClosePrice,
                                                                              Volume                       = Constant.Volume,
                                                                              PriceChangeInInterval        = Constant.PriceChangeInInterval,
                                                                              PriceChangePercentInInterval = Constant.PriceChangePercentInInterval,
                                                                              CreatedAt                    = Constant.CreatedAt,
                                                                              ModifiedAt                   = Constant.ModifiedAt,
                                                                              StockExchange                = StockExchange.DefaultResponse,
                                                                              Quotes                       = [Quote.DefaultDailySimpleResponse]
                                                                          };
    }
}
