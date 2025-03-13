using Bank.Application.Domain;

namespace Bank.Application.Requests;

public class TransactionCreateRequest
{
    public required Guid    FromAccountId   { set; get; }
    public required Guid    FromCurrencyId  { set; get; }
    public required string  ToAccountNumber { set; get; }
    public required Guid    ToCurrencyId    { set; get; }
    public required decimal Amount          { set; get; }
    public required Guid    CodeId          { set; get; }
    public required string? ReferenceNumber { set; get; }
    public required string  Purpose         { set; get; }
}

public class TransactionUpdateRequest
{
    public required TransactionStatus Status { set; get; }
}
