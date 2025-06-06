using Bank.Application.Domain;
using Bank.Application.Responses;

namespace Bank.OpenApi.Examples;

public static partial class Example
{
    public static class Security
    {
        public static readonly SecurityResponse DefaultResponse = new() { };

        public static readonly SecuritySimpleResponse DefaultSimpleResponse = new()
                                                                              {
                                                                                  Id             = Constant.Id,
                                                                                  SecurityType   = SecurityType.Stock,
                                                                                  Name           = Constant.SecurityName,
                                                                                  Ticker         = Constant.Ticker,
                                                                                  StockExchange  = StockExchange.DefaultResponse,
                                                                                  BaseCurrency   = Currency.DefaultSimpleResponse,
                                                                                  QuoteCurrency  = Currency.DefaultSimpleResponse,
                                                                                  ExchangeRate   = Constant.Rate,
                                                                                  Liquidity      = Constant.Liquidity,
                                                                                  StrikePrice    = Constant.StrikePrice,
                                                                                  OpenInterest   = 0,
                                                                                  SettlementDate = Constant.CreationDate,
                                                                                  OptionType     = OptionType.Call,
                                                                                  ContractSize   = Constant.ContractSize,
                                                                                  ContractUnit   = ContractUnit.Kilogram
                                                                              };
    }
}
