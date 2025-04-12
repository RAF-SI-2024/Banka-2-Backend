using Bank.Application.Domain;

namespace Bank.UserService.Models;

public class Loan
{
    public required Guid         Id           { get; set; }
    public required Guid         TypeId       { get; set; }
    public required Guid         AccountId    { get; set; }
    public required decimal      Amount       { get; set; }
    public required int          Period       { get; set; }
    public required DateTime     CreationDate { get; set; }
    public required DateTime     MaturityDate { get; set; }
    public required Guid         CurrencyId   { get; set; }
    public required LoanStatus   Status       { get; set; }
    public required InterestType InterestType { get; set; }
    public required DateTime     CreatedAt    { get; set; }
    public required DateTime     ModifiedAt   { get; set; }

    // Navigation properties
    public LoanType?         LoanType     { get;  set; }
    public Account?          Account      { get;  set; }
    public Currency?         Currency     { get;  set; }
    public List<Installment> Installments { init; get; } = [];
}
