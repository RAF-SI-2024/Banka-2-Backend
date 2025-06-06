using Bank.Application.Domain;

namespace Bank.Application.Responses;

public class FutureContractResponse
{
    public required Guid                      Id                           { get; set; }
    public required int                       ContractSize                 { get; set; }
    public required ContractUnit              ContractUnit                 { get; set; }
    public required DateOnly                  SettlementDate               { get; set; }
    public required string                    Name                         { get; set; }
    public required string                    Ticker                       { get; set; }
    public required decimal                   HighPrice                    { get; set; }
    public required decimal                   LowPrice                     { get; set; }
    public required decimal                   AskPrice                     { get; set; }
    public required decimal                   BidPrice                     { get; set; }
    public required int                       AskSize                      { get; set; }
    public required int                       BidSize                      { get; set; }
    public required int                       ContractCount                { get; set; }
    public required long                      Volume                       { get; set; }
    public required decimal                   PriceChangeInInterval        { get; set; }
    public required decimal                   PriceChangePercentInInterval { get; set; }
    public required DateTime                  CreatedAt                    { get; set; }
    public required DateTime                  ModifiedAt                   { get; set; }
    public required StockExchangeResponse     StockExchange                { get; set; }
    public required List<QuoteSimpleResponse> Quotes                       { get; set; } = [];
}

public class FutureContractDailyResponse
{
    public required Guid                           Id                           { get; set; }
    public required int                            ContractSize                 { get; set; }
    public required ContractUnit                   ContractUnit                 { get; set; }
    public required DateOnly                       SettlementDate               { get; set; }
    public required string                         Name                         { get; set; }
    public required string                         Ticker                       { get; set; }
    public required decimal                        HighPrice                    { get; set; }
    public required decimal                        LowPrice                     { get; set; }
    public required decimal                        OpenPrice                    { get; set; }
    public required decimal                        ClosePrice                   { get; set; }
    public required long                           Volume                       { get; set; }
    public required decimal                        PriceChangeInInterval        { get; set; }
    public required decimal                        PriceChangePercentInInterval { get; set; }
    public required DateTime                       CreatedAt                    { get; set; }
    public required DateTime                       ModifiedAt                   { get; set; }
    public required StockExchangeResponse          StockExchange                { get; set; }
    public required List<QuoteDailySimpleResponse> Quotes                       { get; set; } = [];
}

public class FutureContractSimpleResponse
{
    public required Guid         Id                           { get; set; }
    public required int          ContractSize                 { get; set; }
    public required ContractUnit ContractUnit                 { get; set; }
    public required DateOnly     SettlementDate               { get; set; }
    public required string       Name                         { get; set; }
    public required string       Ticker                       { get; set; }
    public required decimal      HighPrice                    { get; set; }
    public required decimal      LowPrice                     { get; set; }
    public required long         Volume                       { get; set; }
    public required decimal      PriceChangeInInterval        { get; set; }
    public required decimal      PriceChangePercentInInterval { get; set; }
    public required decimal      AskPrice                     { get; set; }
    public required decimal      BidPrice                     { get; set; }
    public required int          AskSize                      { get; set; }
    public required int          BidSize                      { get; set; }
    public required int          ContractCount                { get; set; }
    public required DateTime     CreatedAt                    { get; set; }
    public required DateTime     ModifiedAt                   { get; set; }
}
