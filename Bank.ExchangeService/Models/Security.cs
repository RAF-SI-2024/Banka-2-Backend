using Bank.Application.Domain;
using Bank.ExchangeService.Model;

namespace Bank.ExchangeService.Models;

public class Security
{
    public required Guid Id { get; set; }

    public required SecurityType SecurityType { get; set; }

    //All
    public required string         Name            { get; set; }
    public required string         Ticker          { get; set; }
    public required Guid           StockExchangeId { get; set; }
    public          List<Quote>    Quotes          { get; set; } = [];
    public          StockExchange? StockExchange   { get; set; }

    //ForexPair 
    public Guid       BaseCurrencyId  { get; set; }
    public Guid       QuoteCurrencyId { get; set; }
    public decimal    ExchangeRate    { get; set; }
    public Liquidity? Liquidity       { get; set; }

    //Option
    public decimal  StrikePrice    { get; set; }
    public int      OpenInterest   { get; set; }
    public DateOnly SettlementDate { get; set; }

    public OptionType? OptionType { get; set; }

    //Future
    public int           ContractSize { get; set; }
    public ContractUnit? ContractUnit { get; set; }

    public List<DailyQuote> DailyQuotes = [];
    public decimal          HighPrice          => SecurityUtils.GetHighPrice(Quotes, DailyQuotes);
    public decimal          LowPrice           => SecurityUtils.GetLowPrice(Quotes, DailyQuotes);
    public long             Volume             => SecurityUtils.GetVolume(Quotes, DailyQuotes);
    public decimal          PriceChange        => SecurityUtils.GetPriceChange(Quotes, DailyQuotes);
    public decimal          PriceChangePercent => SecurityUtils.GetPriceChangePercent(Quotes, DailyQuotes);
    public decimal          AskPrice           => Quotes.Count      > 0 ? Quotes[0].AskPrice : 0;
    public decimal          BidPrice           => Quotes.Count      > 0 ? Quotes[0].BidPrice : 0;
    public int              AskSize            => Quotes.Count      > 0 ? Quotes[0].AskSize : 0;
    public int              BidSize            => Quotes.Count      > 0 ? Quotes[0].BidSize : 0;
    public int              ContractCount      => Quotes.Count      > 0 ? Quotes[0].ContractCount : 1;
    public decimal          ClosePrice         => DailyQuotes.Count > 0 ? DailyQuotes[0].ClosePrice : 0;
    public decimal          OpeningPrice       => SecurityUtils.GetOpeningPrice(Quotes, DailyQuotes);
    public DateTime         CreatedAt          => SecurityUtils.GetCreatedAtDate(Quotes, DailyQuotes);
    public DateTime         ModifiedAt         => SecurityUtils.GetModifiedAtDate(Quotes, DailyQuotes);
}

public class Stock
{
    public required Guid           Id              { get; set; }
    public required string         Name            { get; set; }
    public required string         Ticker          { get; set; }
    public required Guid           StockExchangeId { get; set; }
    public          List<Quote>    Quotes          { get; set; } = [];
    public          StockExchange? StockExchange   { get; set; }

    public List<DailyQuote> DailyQuotes = [];
    public decimal          HighPrice          => SecurityUtils.GetHighPrice(Quotes, DailyQuotes);
    public decimal          LowPrice           => SecurityUtils.GetLowPrice(Quotes, DailyQuotes);
    public long             Volume             => SecurityUtils.GetVolume(Quotes, DailyQuotes);
    public decimal          PriceChange        => SecurityUtils.GetPriceChange(Quotes, DailyQuotes);
    public decimal          PriceChangePercent => SecurityUtils.GetPriceChangePercent(Quotes, DailyQuotes);
    public decimal          AskPrice           => Quotes.Count      > 0 ? Quotes[0].AskPrice : 0;
    public decimal          BidPrice           => Quotes.Count      > 0 ? Quotes[0].BidPrice : 0;
    public int              AskSize            => Quotes.Count      > 0 ? Quotes[0].AskSize : 0;
    public int              BidSize            => Quotes.Count      > 0 ? Quotes[0].BidSize : 0;
    public int              ContractCount      => Quotes.Count      > 0 ? Quotes[0].ContractCount : 1;
    public decimal          ClosePrice         => DailyQuotes.Count > 0 ? DailyQuotes[0].ClosePrice : 0;
    public decimal          OpeningPrice       => SecurityUtils.GetOpeningPrice(Quotes, DailyQuotes);
    public DateTime         CreatedAt          => SecurityUtils.GetCreatedAtDate(Quotes, DailyQuotes);
    public DateTime         ModifiedAt         => SecurityUtils.GetModifiedAtDate(Quotes, DailyQuotes);
}

public class FutureContract
{
    public required Guid           Id              { get; set; }
    public required int            ContractSize    { get; set; }
    public required ContractUnit   ContractUnit    { get; set; }
    public required DateOnly       SettlementDate  { get; set; }
    public required string         Name            { get; set; }
    public required string         Ticker          { get; set; }
    public required Guid           StockExchangeId { get; set; }
    public          StockExchange? StockExchange   { get; set; }
    public          List<Quote>    Quotes          { get; set; } = [];

    public List<DailyQuote> DailyQuotes = [];
    public decimal          HighPrice          => Quotes.Count > 0 ? Quotes[0].HighPrice : 0;
    public decimal          LowPrice           => Quotes.Count > 0 ? Quotes[0].LowPrice : 0;
    public long             Volume             => Quotes.Count > 0 ? Quotes[0].Volume : 0;
    public decimal          PriceChange        => SecurityUtils.GetPriceChange(Quotes, DailyQuotes);
    public decimal          PriceChangePercent => SecurityUtils.GetPriceChangePercent(Quotes, DailyQuotes);
    public decimal          AskPrice           => Quotes.Count      > 0 ? Quotes[0].AskPrice : 0;
    public decimal          BidPrice           => Quotes.Count      > 0 ? Quotes[0].BidPrice : 0;
    public int              AskSize            => Quotes.Count      > 0 ? Quotes[0].AskSize : 0;
    public int              BidSize            => Quotes.Count      > 0 ? Quotes[0].BidSize : 0;
    public int              ContractCount      => Quotes.Count      > 0 ? Quotes[0].ContractCount : 1;
    public decimal          ClosePrice         => DailyQuotes.Count > 0 ? DailyQuotes[0].ClosePrice : 0;
    public decimal          OpeningPrice       => SecurityUtils.GetOpeningPrice(Quotes, DailyQuotes);
    public DateTime         CreatedAt          => SecurityUtils.GetCreatedAtDate(Quotes, DailyQuotes);
    public DateTime         ModifiedAt         => SecurityUtils.GetModifiedAtDate(Quotes, DailyQuotes);
}

public class Option
{
    public required Guid           Id              { get; set; }
    public required OptionType     OptionType      { get; set; }
    public required decimal        StrikePrice     { get; set; }
    public required DateOnly       SettlementDate  { get; set; }
    public required string         Name            { get; set; }
    public required string         Ticker          { get; set; }
    public required Guid           StockExchangeId { get; set; }
    public          StockExchange? StockExchange   { get; set; }
    public          List<Quote>    Quotes          { get; set; } = [];

    public List<DailyQuote> DailyQuotes = [];

    public decimal  ImpliedVolatility = 0m;
    public decimal  HighPrice          => SecurityUtils.GetHighPrice(Quotes, DailyQuotes);
    public decimal  LowPrice           => SecurityUtils.GetLowPrice(Quotes, DailyQuotes);
    public long     Volume             => SecurityUtils.GetVolume(Quotes, DailyQuotes);
    public decimal  PriceChange        => SecurityUtils.GetPriceChange(Quotes, DailyQuotes);
    public decimal  PriceChangePercent => SecurityUtils.GetPriceChangePercent(Quotes, DailyQuotes);
    public decimal  AskPrice           => Quotes.Count      > 0 ? Quotes[0].AskPrice : 0;
    public decimal  BidPrice           => Quotes.Count      > 0 ? Quotes[0].BidPrice : 0;
    public int      AskSize            => Quotes.Count      > 0 ? Quotes[0].AskSize : 0;
    public int      BidSize            => Quotes.Count      > 0 ? Quotes[0].BidSize : 0;
    public int      ContractCount      => Quotes.Count      > 0 ? Quotes[0].ContractCount : 1;
    public decimal  ClosePrice         => DailyQuotes.Count > 0 ? DailyQuotes[0].ClosePrice : 0;
    public decimal  OpeningPrice       => SecurityUtils.GetOpeningPrice(Quotes, DailyQuotes);
    public DateTime CreatedAt          => SecurityUtils.GetCreatedAtDate(Quotes, DailyQuotes);
    public DateTime ModifiedAt         => SecurityUtils.GetModifiedAtDate(Quotes, DailyQuotes);
}

public class ForexPair
{
    public required Guid           Id              { get; set; }
    public required Guid           BaseCurrencyId  { get; set; }
    public required Guid           QuoteCurrencyId { get; set; }
    public required decimal        ExchangeRate    { get; set; }
    public required Liquidity      Liquidity       { get; set; }
    public required string         Name            { get; set; }
    public required string         Ticker          { get; set; }
    public required Guid           StockExchangeId { get; set; }
    public          StockExchange? StockExchange   { get; set; }
    public          List<Quote>    Quotes          { get; set; } = [];

    public List<DailyQuote> DailyQuotes = [];
    public decimal          MaintenanceDecimal => (decimal)0.1 * ContractCount * ExchangeRate;
    public decimal          HighPrice          => SecurityUtils.GetHighPrice(Quotes, DailyQuotes);
    public decimal          LowPrice           => SecurityUtils.GetLowPrice(Quotes, DailyQuotes);
    public decimal          PriceChange        => SecurityUtils.GetPriceChange(Quotes, DailyQuotes);
    public decimal          PriceChangePercent => SecurityUtils.GetPriceChangePercent(Quotes, DailyQuotes);
    public decimal          AskPrice           => Quotes.Count      > 0 ? Quotes[0].AskPrice : 0;
    public decimal          BidPrice           => Quotes.Count      > 0 ? Quotes[0].BidPrice : 0;
    public int              AskSize            => Quotes.Count      > 0 ? Quotes[0].AskSize : 0;
    public int              BidSize            => Quotes.Count      > 0 ? Quotes[0].BidSize : 0;
    public int              ContractCount      => Quotes.Count      > 0 ? Quotes[0].ContractCount : 1;
    public decimal          ClosePrice         => DailyQuotes.Count > 0 ? DailyQuotes[0].ClosePrice : 0;
    public decimal          OpeningPrice       => SecurityUtils.GetOpeningPrice(Quotes, DailyQuotes);
    public DateTime         CreatedAt          => SecurityUtils.GetCreatedAtDate(Quotes, DailyQuotes);
    public DateTime         ModifiedAt         => SecurityUtils.GetModifiedAtDate(Quotes, DailyQuotes);
}

file static class SecurityUtils
{
    public static decimal GetPriceChange(List<Quote> quotes, List<DailyQuote> dailyQuotes)
    {
        if (quotes.Count == 0 && dailyQuotes.Count == 0)
            return 0;

        var lastPrice = quotes.Count > 0
                        ? quotes.First()
                                .AskPrice
                        : dailyQuotes.First()
                                     .ClosePrice;

        var openingPrice = quotes.Count > 0
                           ? quotes.Last()
                                   .OpeningPrice
                           : dailyQuotes.Last()
                                        .OpeningPrice;

        return lastPrice - openingPrice;
    }

    public static decimal GetPriceChangePercent(List<Quote> quotes, List<DailyQuote> dailyQuotes)
    {
        if (quotes.Count == 0 && dailyQuotes.Count == 0)
            return 0;

        var lastPrice = quotes.Count > 0
                        ? quotes.First()
                                .AskPrice
                        : dailyQuotes.First()
                                     .ClosePrice;

        var openingPrice = quotes.Count > 0
                           ? quotes.Last()
                                   .OpeningPrice
                           : dailyQuotes.Last()
                                        .OpeningPrice;

        return openingPrice == 0m ? 0m : (lastPrice - openingPrice) / openingPrice;
    }

    public static decimal GetHighPrice(List<Quote> quotes, List<DailyQuote> dailyQuotes)
    {
        if (quotes.Count == 0 && dailyQuotes.Count == 0)
            return 0;

        return quotes.Count > 0
               ? quotes.TakeWhile(quote => quote.CreatedAt.Date == quotes[0].CreatedAt.Date)
                       .Max(quote => quote.HighPrice)
               : dailyQuotes[0].HighPrice;
    }

    public static decimal GetLowPrice(List<Quote> quotes, List<DailyQuote> dailyQuotes)
    {
        if (quotes.Count == 0 && dailyQuotes.Count == 0)
            return 0;

        return quotes.Count > 0
               ? quotes.TakeWhile(quote => quote.CreatedAt.Date == quotes[0].CreatedAt.Date)
                       .Min(quote => quote.LowPrice)
               : dailyQuotes[0].LowPrice;
    }

    public static long GetVolume(List<Quote> quotes, List<DailyQuote> dailyQuotes)
    {
        if (quotes.Count == 0 && dailyQuotes.Count == 0)
            return 0;

        return quotes.Count > 0 ? quotes[0].Volume : dailyQuotes[0].Volume;
    }

    public static decimal GetOpeningPrice(List<Quote> quotes, List<DailyQuote> dailyQuotes)
    {
        if (quotes.Count == 0 && dailyQuotes.Count == 0)
            return 0;

        return quotes.Count > 0
               ? quotes.TakeWhile(quote => quote.CreatedAt.Date == quotes[0].CreatedAt.Date)
                       .Last()
                       .OpeningPrice
               : dailyQuotes[0].OpeningPrice;
    }

    public static DateTime GetCreatedAtDate(List<Quote> quotes, List<DailyQuote> dailyQuotes)
    {
        if (quotes.Count == 0 && dailyQuotes.Count == 0)
            return DateTime.MinValue;

        return quotes.Count > 0 ? quotes[0].CreatedAt : dailyQuotes[0].Date;
    }

    public static DateTime GetModifiedAtDate(List<Quote> quotes, List<DailyQuote> dailyQuotes)
    {
        if (quotes.Count == 0 && dailyQuotes.Count == 0)
            return DateTime.MinValue;

        return quotes.Count > 0 ? quotes[0].ModifiedAt : dailyQuotes[0].Date;
    }
}
