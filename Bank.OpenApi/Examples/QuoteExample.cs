using Bank.Application.Responses;

namespace Bank.OpenApi.Examples;

public static partial class Example
{
    public static class Quote
    {
        public static readonly QuoteSimpleResponse DefaultSimpleResponse = new()
                                                                           {
                                                                               Id            = Constant.Id,
                                                                               HighPrice     = Constant.HighPrice,
                                                                               LowPrice      = Constant.LowPrice,
                                                                               AskPrice      = Constant.AskPrice,
                                                                               BidPrice      = Constant.BidPrice,
                                                                               Volume        = Constant.Volume,
                                                                               CreatedAt     = Constant.CreatedAt,
                                                                               ModifiedAt    = Constant.ModifiedAt,
                                                                               ContractCount = Constant.ContractCount
                                                                           };

        public static readonly QuoteLatestSimpleResponse DefaultLatestSimpleResponse = new()
                                                                                       {
                                                                                           SecurityTicker = Constant.Ticker,
                                                                                           AskPrice       = Constant.AskPrice,
                                                                                           BidPrice       = Constant.BidPrice,
                                                                                           HighPrice      = Constant.HighPrice,
                                                                                           LowPrice       = Constant.LowPrice,
                                                                                           Volume         = Constant.Volume,
                                                                                           CreatedAt      = Constant.CreatedAt,
                                                                                           ModifiedAt     = Constant.ModifiedAt
                                                                                       };

        public static readonly QuoteDailySimpleResponse DefaultDailySimpleResponse = new()
                                                                                     {
                                                                                         HighPrice  = Constant.HighPrice,
                                                                                         LowPrice   = Constant.LowPrice,
                                                                                         OpenPrice  = Constant.OpeningPrice,
                                                                                         ClosePrice = Constant.ClosePrice,
                                                                                         Volume     = Constant.Volume,
                                                                                         CreatedAt  = Constant.CreationDate,
                                                                                         ModifiedAt = Constant.CreationDate
                                                                                     };
    }
}
