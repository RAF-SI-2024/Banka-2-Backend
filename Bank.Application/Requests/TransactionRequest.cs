using Bank.Application.Domain;

namespace Bank.Application.Requests;

public class TransactionCreateRequest
{
    public          string? FromAccountNumber     { set; get; }
    public          Guid    FromCurrencyId        { set; get; }
    public          string? ToAccountNumber       { set; get; }
    public          Guid    ToCurrencyId          { set; get; }
    public required decimal Amount                { set; get; }
    public          decimal Profit                { set; get; }
    public required Guid    CodeId                { set; get; }
    public          string? ReferenceNumber       { set; get; }
    public required string  Purpose               { set; get; }
    public          string? ConfirmationCode      { set; get; }
    public          object? ExternalTransactionId { set; get; }
}

public class TransactionUpdateRequest
{
    public required TransactionStatus Status { set; get; }
}

public class TransactionNotifyStatusRequest
{
    public required object? TransactionId     { set; get; }
    public required bool    TransferSucceeded { set; get; }
    public required string? AccountNumber     { set; get; }
}
