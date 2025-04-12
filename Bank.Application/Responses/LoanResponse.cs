using Bank.Application.Domain;

namespace Bank.Application.Responses;

public class LoanResponse
{
    public Guid             Id                     { get; set; }
    public LoanTypeResponse Type                   { get; set; }
    public AccountResponse  Account                { get; set; }
    public decimal          Amount                 { get; set; }
    public int              Period                 { get; set; }
    public DateOnly         CreationDate           { get; set; }
    public DateOnly         MaturityDate           { get; set; }
    public CurrencyResponse Currency               { get; set; }
    public LoanStatus       Status                 { get; set; }
    public InterestType     InterestType           { get; set; }
    public DateTime         CreatedAt              { get; set; }
    public DateTime         ModifiedAt             { get; set; }
    public decimal          NominalInstallmentRate { get; set; }
    public decimal          RemainingAmount        { get; set; }
}
