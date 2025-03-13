using Bank.Application.Domain;

namespace Bank.UserService.Models;

public class Transaction
{
    public required Guid              Id              { set; get; }
    public          Account?          FromAccount     { set; get; }
    public required Guid              FromAccountId   { set; get; }
    public          Account?          ToAccount       { set; get; }
    public required Guid?             ToAccountId     { set; get; }
    public required decimal           FromAmount      { set; get; }
    public          Currency?         FromCurrency    { set; get; }
    public required Guid              FromCurrencyId  { set; get; }
    public required decimal           ToAmount        { set; get; }
    public          Currency?         ToCurrency      { set; get; }
    public required Guid              ToCurrencyId    { set; get; }
    public required Guid              CodeId          { set; get; }
    public          TransactionCode?  Code            { set; get; }
    public required string?           ReferenceNumber { set; get; }
    public required string            Purpose         { set; get; }
    public required TransactionStatus Status          { set; get; }
    public required DateTime          CreatedAt       { set; get; }
    public required DateTime          ModifiedAt      { set; get; }
}
