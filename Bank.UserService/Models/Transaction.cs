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
    public          decimal           TaxAmount       { set; get; }
    public required Guid              CodeId          { set; get; }
    public          TransactionCode?  Code            { set; get; }
    public          string?           ReferenceNumber { set; get; }
    public          string?           Purpose         { set; get; }
    public required TransactionStatus Status          { set; get; }
    public required DateTime          CreatedAt       { set; get; }
    public required DateTime          ModifiedAt      { set; get; }
}

public class PrepareToAccountTransaction
{
    public required Account?        Account         { set; get; }
    public required Currency?       Currency        { set; get; }
    public required TransactionCode TransactionCode { set; get; }
    public required decimal         Amount          { set; get; }
    public required decimal         Profit          { set; get; }
    public          string?         Purpose         { set; get; }
}

public class PrepareDirectToAccountTransaction
{
    public required Account?        Account         { set; get; }
    public required Currency?       Currency        { set; get; }
    public required TransactionCode TransactionCode { set; get; }
    public required decimal         Amount          { set; get; }
    public          string?         Purpose         { set; get; }
}

public class PrepareFromAccountTransaction
{
    public required Account?        Account         { set; get; }
    public required Currency?       Currency        { set; get; }
    public required TransactionCode TransactionCode { set; get; }
    public required decimal         Amount          { set; get; }
    public          string?         Purpose         { set; get; }
}

public class PrepareSecurityAccountTransaction
{
    public required Account?         Account         { set; get; }
    public required Currency?        FromCurrency    { set; get; }
    public required Currency?        ToCurrency      { set; get; }
    public required TransactionCode  TransactionCode { set; get; }
    public required ExchangeDetails? ExchangeDetails { set; get; }
    public required decimal          Amount          { set; get; }
    public required decimal          Profit          { set; get; }
    public          string?          Purpose         { set; get; }
}

public class PrepareDirectFromAccountTransaction
{
    public required Account?        Account         { set; get; }
    public required Currency?       Currency        { set; get; }
    public required TransactionCode TransactionCode { set; get; }
    public required decimal         Amount          { set; get; }
    public          string?         Purpose         { set; get; }
}

public class PrepareInternalTransaction
{
    public required Account?         FromAccount     { set; get; }
    public required Currency?        FromCurrency    { set; get; }
    public required Account?         ToAccount       { set; get; }
    public required Currency?        ToCurrency      { set; get; }
    public required decimal          Amount          { set; get; }
    public required TransactionCode  TransactionCode { set; get; }
    public required ExchangeDetails? ExchangeDetails { set; get; }
    public          string?          ReferenceNumber { set; get; }
    public          string?          Purpose         { set; get; }
}

public class PrepareExternalTransaction
{
    public required string?          FromAccountNumber     { set; get; }
    public required Account?         FromAccount           { set; get; }
    public required Currency?        FromCurrency          { set; get; }
    public required string?          ToAccountNumber       { set; get; }
    public required Account?         ToAccount             { set; get; }
    public required Currency?        ToCurrency            { set; get; }
    public required decimal          Amount                { set; get; }
    public required TransactionCode  TransactionCode       { set; get; }
    public required ExchangeDetails? ExchangeDetails       { set; get; }
    public          string?          ReferenceNumber       { set; get; }
    public          string?          Purpose               { set; get; }
    public          object?          ExternalTransactionId { set; get; }
}

public class ProcessTransaction
{
    public required Guid    TransactionId         { set; get; }
    public          object? ExternalTransactionId { set; get; }
    public required Guid    FromAccountId         { set; get; }
    public required Guid    FromCurrencyId        { set; get; }
    public required decimal FromAmount            { set; get; }
    public required Guid    ToAccountId           { set; get; }
    public required Guid    ToCurrencyId          { set; get; }
    public required decimal ToAmount              { set; get; }
    public required decimal FromBankAmount        { set; get; }
    public          decimal Profit                { set; get; } = 0;
    public required bool    IsDirect              { set; get; }
}
