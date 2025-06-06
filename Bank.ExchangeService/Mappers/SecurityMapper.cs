using Bank.Application.Domain;
using Bank.Application.Responses;
using Bank.ExchangeService.Models;

namespace Bank.ExchangeService.Mappers;

public static class SecurityMapper
{
    public static Stock ToStock(this Security security)
    {
        return new Stock()
               {
                   Id              = security.Id,
                   Name            = security.Name,
                   Ticker          = security.Ticker,
                   StockExchange   = security.StockExchange,
                   StockExchangeId = security.StockExchangeId,
                   Quotes          = security.Quotes,
                   DailyQuotes     = security.DailyQuotes
               };
    }

    public static ForexPair ToForexPair(this Security security)
    {
        return new ForexPair()
               {
                   Id              = security.Id,
                   Liquidity       = (Liquidity)security.Liquidity!,
                   ExchangeRate    = security.ExchangeRate,
                   BaseCurrencyId  = security.BaseCurrencyId,
                   QuoteCurrencyId = security.QuoteCurrencyId,
                   Name            = security.Name,
                   Ticker          = security.Ticker,
                   StockExchange   = security.StockExchange,
                   Quotes          = security.Quotes,
                   StockExchangeId = security.StockExchangeId,
                   DailyQuotes     = security.DailyQuotes
               };
    }

    public static FutureContract ToFutureContract(this Security security)
    {
        return new FutureContract()
               {
                   Id              = security.Id,
                   ContractSize    = security.ContractSize,
                   ContractUnit    = (ContractUnit)security.ContractUnit!,
                   SettlementDate  = security.SettlementDate,
                   Name            = security.Name,
                   Ticker          = security.Ticker,
                   StockExchange   = security.StockExchange,
                   Quotes          = security.Quotes,
                   DailyQuotes     = security.DailyQuotes,
                   StockExchangeId = security.StockExchangeId
               };
    }

    public static Option ToOption(this Security security)
    {
        return new Option()
               {
                   Id              = security.Id,
                   StrikePrice     = security.StrikePrice,
                   SettlementDate  = security.SettlementDate,
                   Name            = security.Name,
                   Ticker          = security.Ticker,
                   StockExchange   = security.StockExchange,
                   StockExchangeId = security.StockExchangeId,
                   OptionType      = (OptionType)security.OptionType!,
                   Quotes          = security.Quotes,
                   DailyQuotes     = security.DailyQuotes
               };
    }

    public static Security ToSecurity(this ForexPair forexPair)
    {
        return new Security
               {
                   Id              = forexPair.Id,
                   Liquidity       = forexPair.Liquidity,
                   ExchangeRate    = forexPair.ExchangeRate,
                   BaseCurrencyId  = forexPair.BaseCurrencyId,
                   QuoteCurrencyId = forexPair.QuoteCurrencyId,
                   Name            = forexPair.Name,
                   Ticker          = forexPair.Ticker,
                   StockExchange   = forexPair.StockExchange,
                   Quotes          = forexPair.Quotes,
                   StockExchangeId = forexPair.StockExchangeId,
                   SecurityType    = SecurityType.ForexPair,
               };
    }

    public static Security ToSecurity(this FutureContract futureContract)
    {
        return new Security
               {
                   Id              = futureContract.Id,
                   ContractSize    = futureContract.ContractSize,
                   ContractUnit    = futureContract.ContractUnit,
                   SettlementDate  = futureContract.SettlementDate,
                   Name            = futureContract.Name,
                   Ticker          = futureContract.Ticker,
                   StockExchange   = futureContract.StockExchange,
                   Quotes          = futureContract.Quotes,
                   StockExchangeId = futureContract.StockExchangeId,
                   SecurityType    = SecurityType.FutureContract
               };
    }

    public static Security ToSecurity(this Option option)
    {
        return new Security
               {
                   Id              = option.Id,
                   StrikePrice     = option.StrikePrice,
                   SettlementDate  = option.SettlementDate,
                   Name            = option.Name,
                   Ticker          = option.Ticker,
                   StockExchange   = option.StockExchange,
                   StockExchangeId = option.StockExchangeId,
                   OptionType      = option.OptionType,
                   Quotes          = option.Quotes,
                   SecurityType    = SecurityType.Option,
               };
    }

    public static Security ToSecurity(this Stock stock)
    {
        return new Security
               {
                   Id              = stock.Id,
                   Name            = stock.Name,
                   Ticker          = stock.Ticker,
                   StockExchange   = stock.StockExchange,
                   StockExchangeId = stock.StockExchangeId,
                   Quotes          = stock.Quotes,
                   SecurityType    = SecurityType.Stock,
               };
    }

    public static SecuritySimpleResponse ToSecuritySimpleResponse(this Security           security,            CurrencySimpleResponse? stockExchangeCurrency = null,
                                                                  CurrencySimpleResponse? baseCurrency = null, CurrencySimpleResponse? quoteCurrency         = null)
    {
        return new SecuritySimpleResponse
               {
                   Id             = security.Id,
                   SecurityType   = security.SecurityType,
                   Name           = security.Name,
                   Ticker         = security.Ticker,
                   StockExchange  = stockExchangeCurrency == null ? null : security.StockExchange!.ToResponse(stockExchangeCurrency),
                   BaseCurrency   = baseCurrency,
                   QuoteCurrency  = quoteCurrency,
                   ExchangeRate   = security.ExchangeRate,
                   Liquidity      = security.Liquidity,
                   StrikePrice    = security.StrikePrice,
                   OpenInterest   = security.OpenInterest,
                   SettlementDate = security.SettlementDate,
                   OptionType     = security.OptionType,
                   ContractSize   = security.ContractSize,
                   ContractUnit   = security.ContractUnit,
               };
    }
}
