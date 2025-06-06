using Bank.Application.Domain;

namespace Bank.Application.Responses;

public class SecurityResponse { }

public class SecuritySimpleResponse
{
    public required Guid                    Id             { get; set; }
    public required SecurityType            SecurityType   { get; set; }
    public required string                  Name           { get; set; }
    public required string                  Ticker         { get; set; }
    public required StockExchangeResponse?  StockExchange  { get; set; }
    public required CurrencySimpleResponse? BaseCurrency   { get; set; }
    public required CurrencySimpleResponse? QuoteCurrency  { get; set; }
    public required decimal                 ExchangeRate   { get; set; }
    public required Liquidity?              Liquidity      { get; set; }
    public required decimal                 StrikePrice    { get; set; }
    public required int                     OpenInterest   { get; set; }
    public required DateOnly                SettlementDate { get; set; }
    public required OptionType?             OptionType     { get; set; }
    public required int                     ContractSize   { get; set; }
    public required ContractUnit?           ContractUnit   { get; set; }
}
