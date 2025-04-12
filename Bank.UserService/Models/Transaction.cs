using Bank.Application.Domain;

namespace Bank.UserService.Models;

public class Transaction
{
    public required Guid              Id              { set; get; }
    public          Account?          FromAccount     { set; get; }
    public          Guid?             FromAccountId   { set; get; }
    public          Currency?         FromCurrency    { set; get; }
    public          Guid?             FromCurrencyId  { set; get; }
    public          decimal           FromAmount      { set; get; }
    public          Account?          ToAccount       { set; get; }
    public          Guid?             ToAccountId     { set; get; }
    public          Currency?         ToCurrency      { set; get; }
    public          Guid?             ToCurrencyId    { set; get; }
    public          decimal           ToAmount        { set; get; }
    public required Guid              CodeId          { set; get; }
    public          TransactionCode?  Code            { set; get; }
    public          string?           ReferenceNumber { set; get; }
    public          string?           Purpose         { set; get; }
    public required TransactionStatus Status          { set; get; }
    public required DateTime          CreatedAt       { set; get; }
    public required DateTime          ModifiedAt      { set; get; }
}

public class TempyTransaction
{
    public          string? FromAccountNumber { set; get; }
    public          Guid    FromCurrencyId    { set; get; }
    public          string? ToAccountNumber   { set; get; }
    public          Guid    ToCurrencyId      { set; get; }
    public required decimal Amount            { set; get; }
    public required Guid    CodeId            { set; get; }
    public          string? ReferenceNumber   { set; get; }
    public          string? Purpose           { set; get; }
}

public class PrepareDepositTransaction
{
    public required Account Account    { set; get; }
    public required Guid    CurrencyId { set; get; }
    public required decimal Amount     { set; get; }
}

public class PrepareWithdrawTransaction
{
    public required Account Account    { set; get; }
    public required Guid    CurrencyId { set; get; }
    public required decimal Amount     { set; get; }
}

public class PrepareInternalTransaction
{
    public required Account         FromAccount       { set; get; }
    public required Guid            FromCurrencyId    { set; get; }
    public required Account         ToAccount         { set; get; }
    public required Guid            ToCurrencyId      { set; get; }
    public required decimal         Amount            { set; get; }
    public required ExchangeDetails ExchangeDetails   { set; get; } = null!;
    public required Guid            TransactionCodeId { set; get; }
    public          string?         ReferenceNumber   { set; get; }
    public          string?         Purpose           { set; get; }
}

public class PrepareExternalTransaction { }

public class ProcessTransaction
{
    public required Guid    TransactionId  { set; get; }
    public required Guid    FromAccountId  { set; get; }
    public required Guid    FromCurrencyId { set; get; }
    public required decimal FromAmount     { set; get; }
    public required Guid    ToAccountId    { set; get; }
    public required Guid    ToCurrencyId   { set; get; }
    public required decimal ToAmount       { set; get; }
    public required decimal FromBankAmount { set; get; }
}
