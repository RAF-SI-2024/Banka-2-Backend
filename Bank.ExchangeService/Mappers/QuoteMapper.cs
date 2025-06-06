using Bank.Application.Domain;
using Bank.Application.Extensions;
using Bank.Application.Responses;
using Bank.ExchangeService.Models;

namespace Bank.ExchangeService.Mappers;

public static class QuoteMapper
{
    public static QuoteSimpleResponse ToSimpleResponse(this Quote quote)
    {
        return new QuoteSimpleResponse
               {
                   Id            = quote.Id,
                   HighPrice     = quote.HighPrice,
                   LowPrice      = quote.LowPrice,
                   Volume        = quote.Volume,
                   CreatedAt     = quote.CreatedAt,
                   ModifiedAt    = quote.ModifiedAt,
                   AskPrice      = quote.AskPrice,
                   BidPrice      = quote.BidPrice,
                   ContractCount = quote.ContractCount,
               };
    }

    public static QuoteLatestSimpleResponse ToLatestSimpleResponse(this Quote quote)
    {
        return new QuoteLatestSimpleResponse
               {
                   SecurityTicker = quote.Security!.Ticker,
                   AskPrice       = quote.AskPrice,
                   BidPrice       = quote.BidPrice,
                   HighPrice      = quote.HighPrice,
                   LowPrice       = quote.LowPrice,
                   Volume         = quote.Volume,
                   CreatedAt      = quote.CreatedAt,
                   ModifiedAt     = quote.ModifiedAt,
               };
    }

    public static Quote ToQuote(this RedisQuote redisQuote, Guid securityId)
    {
        return new Quote
               {
                   Id            = redisQuote.Id,
                   SecurityId    = securityId,
                   AskPrice      = redisQuote.AskPrice,
                   BidPrice      = redisQuote.BidPrice,
                   AskSize       = redisQuote.AskSize,
                   BidSize       = redisQuote.BidSize,
                   HighPrice     = redisQuote.HighPrice,
                   LowPrice      = redisQuote.LowPrice,
                   ClosePrice    = redisQuote.ClosePrice,
                   OpeningPrice  = redisQuote.OpeningPrice,
                   Volume        = redisQuote.Volume,
                   ContractCount = redisQuote.ContractCount,
                   CreatedAt     = redisQuote.Time,
                   ModifiedAt    = redisQuote.Time
               };
    }

    public static QuoteDailySimpleResponse ToDailySimpleResponse(this DailyQuote quote)
    {
        return new QuoteDailySimpleResponse
               {
                   HighPrice  = quote.HighPrice,
                   LowPrice   = quote.LowPrice,
                   Volume     = quote.Volume,
                   CreatedAt  = DateOnly.FromDateTime(quote.Date),
                   ModifiedAt = DateOnly.FromDateTime(quote.Date),
                   OpenPrice  = quote.OpeningPrice,
                   ClosePrice = quote.ClosePrice,
               };
    }

    public static QuoteSimpleResponse ToChartSimpleResponse(this Quote quote)
    {
        return new QuoteSimpleResponse
               {
                   Id            = quote.Id,
                   HighPrice     = quote.HighPrice,
                   LowPrice      = quote.LowPrice,
                   Volume        = quote.Volume,
                   CreatedAt     = quote.CreatedAt,
                   ModifiedAt    = quote.ModifiedAt,
                   AskPrice      = quote.AskPrice,
                   BidPrice      = quote.BidPrice,
                   ContractCount = quote.ContractCount,
               };
    }

    public static Quote ToQuote(this FetchStockBarOneResponse stockBarOneResponse, Guid stockId, int askSize, int bidSize)
    {
        return new Quote
               {
                   Id            = Guid.NewGuid(),
                   AskPrice      = stockBarOneResponse.ClosePrice,
                   BidPrice      = 0,
                   SecurityId    = stockId,
                   HighPrice     = stockBarOneResponse.HighPrice,
                   LowPrice      = stockBarOneResponse.LowPrice,
                   Volume        = stockBarOneResponse.NumberOfTradesInInterval,
                   CreatedAt     = stockBarOneResponse.Date,
                   ModifiedAt    = stockBarOneResponse.Date,
                   ClosePrice    = stockBarOneResponse.ClosePrice,
                   OpeningPrice  = stockBarOneResponse.OpeningPrice,
                   AskSize       = askSize,
                   BidSize       = bidSize,
                   ContractCount = 1
               };
    }

    //TODO add Security property for each type of latestQuote
    public static Quote ToQuote(this FetchStockSnapshotResponse stockSnapshotResponse, Security stock)
    {
        return new Quote
               {
                   Id            = Guid.NewGuid(),
                   SecurityId    = stock.Id,
                   Security      = stock,
                   AskPrice      = stockSnapshotResponse.LatestQuote!.AskPrice,
                   BidPrice      = stockSnapshotResponse.LatestQuote!.BidPrice,
                   HighPrice     = stockSnapshotResponse.DailyBar!.HighPrice,
                   LowPrice      = stockSnapshotResponse.DailyBar!.LowPrice,
                   Volume        = stockSnapshotResponse.MinuteBar!.NumberOfSharesInInterval,
                   CreatedAt     = stockSnapshotResponse.LatestQuote!.Date,
                   ModifiedAt    = stockSnapshotResponse.LatestQuote!.Date,
                   ClosePrice    = stockSnapshotResponse.DailyBar!.ClosePrice,
                   OpeningPrice  = stockSnapshotResponse.DailyBar!.OpeningPrice,
                   AskSize       = stockSnapshotResponse.LatestQuote.AskSize,
                   BidSize       = stockSnapshotResponse.LatestQuote.BidSize,
                   ContractCount = 1
               };
    }

    public static Quote ToQuote(this FetchForexPairQuoteResponse fetchForexPairQuote, Security forexPair, DateTime date)
    {
        var random = new Random();

        return new Quote
               {
                   Id            = Guid.NewGuid(),
                   SecurityId    = forexPair.Id,
                   Security      = forexPair,
                   AskPrice      = fetchForexPairQuote.Close,
                   BidPrice      = 0,
                   HighPrice     = fetchForexPairQuote.High,
                   LowPrice      = fetchForexPairQuote.Low,
                   Volume        = 0,
                   CreatedAt     = date.ToUniversalTime(),
                   ModifiedAt    = date.ToUniversalTime(),
                   ClosePrice    = fetchForexPairQuote.Close,
                   OpeningPrice  = fetchForexPairQuote.Open,
                   AskSize       = random.Next(1, 100),
                   BidSize       = random.Next(1, 100),
                   ContractCount = 1
               };
    }

    public static Quote ToQuote(this FetchForexPairLatestResponse fetchForexPairLatest, Security forexPair)
    {
        var random = new Random();

        return new Quote
               {
                   Id            = Guid.NewGuid(),
                   SecurityId    = forexPair.Id,
                   Security      = forexPair,
                   AskPrice      = fetchForexPairLatest.AskPrice,
                   BidPrice      = fetchForexPairLatest.BidPrice,
                   HighPrice     = 0,
                   LowPrice      = 0,
                   Volume        = 0,
                   CreatedAt     = fetchForexPairLatest.Date.ToUniversalTime(),
                   ModifiedAt    = fetchForexPairLatest.Date.ToUniversalTime(),
                   ClosePrice    = 0,
                   OpeningPrice  = 0,
                   AskSize       = random.Next(1, 100),
                   BidSize       = random.Next(1, 100),
                   ContractCount = 1
               };
    }

    public static Quote ToQuote(this FetchOptionOneResponse optionResponse, Security option)
    {
        return new Quote
               {
                   Id                = Guid.NewGuid(),
                   SecurityId        = option.Id,
                   Security          = option,
                   AskPrice          = optionResponse.LatestQuote!.AskPrice,
                   BidPrice          = optionResponse.LatestQuote!.BidPrice,
                   HighPrice         = optionResponse.DailyBar!.HighPrice,
                   LowPrice          = optionResponse.DailyBar!.LowPrice,
                   Volume            = optionResponse.DailyBar!.Volume,
                   CreatedAt         = optionResponse.DailyBar!.TimeStamp,
                   ModifiedAt        = optionResponse.DailyBar!.TimeStamp,
                   ImpliedVolatility = optionResponse.ImpliedVolatility,
                   OpeningPrice      = optionResponse.DailyBar.OpeningPrice,
                   ClosePrice        = optionResponse.DailyBar.ClosingPrice,
                   AskSize           = optionResponse.LatestQuote.AskSize,
                   BidSize           = optionResponse.LatestQuote.BidSize,
                   ContractCount     = 1
               };
    }

    public static RedisQuote ToRedis(this Quote quote)
    {
        return new RedisQuote
               {
                   Id                = quote.Id,
                   SecurityType      = quote.Security!.SecurityType,
                   Ticker            = quote.Security.Ticker,
                   AskPrice          = quote.AskPrice,
                   BidPrice          = quote.BidPrice,
                   AskSize           = quote.AskSize,
                   BidSize           = quote.BidSize,
                   HighPrice         = quote.HighPrice,
                   LowPrice          = quote.LowPrice,
                   ClosePrice        = quote.ClosePrice,
                   OpeningPrice      = quote.OpeningPrice,
                   ImpliedVolatility = quote.ImpliedVolatility,
                   Volume            = quote.Volume,
                   ContractCount     = quote.ContractCount
               };
    }

    public static RedisQuote MapKey(this RedisQuote quote, string? key)
    {
        if (key is null)
            return quote;

        string[] keySplit = key.Split(":");

        quote.SecurityType = keySplit[0] switch
                             {
                                 "f" => SecurityType.ForexPair,
                                 "o" => SecurityType.Option,
                                 "s" => SecurityType.Stock,
                                 _   => throw new ArgumentOutOfRangeException()
                             };

        quote.Ticker = key[1]
        .ToString();

        quote.Time = DateTime.UtcNow.Date.AddSeconds(keySplit[2]
                                                     .DecodeBase64ToInt());

        return quote;
    }
}
