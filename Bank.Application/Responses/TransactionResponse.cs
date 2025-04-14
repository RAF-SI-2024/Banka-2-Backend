using Bank.Application.Domain;

namespace Bank.Application.Responses;

public class TransactionCreateResponse
{
    public required Guid                    Id              { set; get; }
    public required decimal                 FromAmount      { set; get; }
    public required TransactionCodeResponse Code            { set; get; }
    public required string                  ReferenceNumber { set; get; }
    public required string                  Purpose         { set; get; }
    public required TransactionStatus       Status          { set; get; }
    public required DateTime                CreatedAt       { set; get; }
    public required DateTime                ModifiedAt      { set; get; }
}

public class TransactionResponse
{
    public required Guid                    Id              { set; get; }
    public required AccountSimpleResponse   FromAccount     { set; get; }
    public required CurrencyResponse        FromCurrency    { set; get; }
    public required AccountSimpleResponse   ToAccount       { set; get; }
    public required CurrencyResponse        ToCurrency      { set; get; }
    public required decimal                 FromAmount      { set; get; }
    public required decimal                 ToAmount        { set; get; }
    public required TransactionCodeResponse Code            { set; get; }
    public required string                  ReferenceNumber { set; get; }
    public required string                  Purpose         { set; get; }
    public required TransactionStatus       Status          { set; get; }
    public required DateTime                CreatedAt       { set; get; }
    public required DateTime                ModifiedAt      { set; get; }
}
