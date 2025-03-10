namespace Bank.UserService.Models;

public class Credit
{
    public required Guid         Id                   { get; set; }
    public required CreditType   CreditType           { get; set; }
    public          Guid         CreditTypeId         { get; set; }
    public required Account      Account              { get; set; }
    public required string       AccountNumber        { get; set; }
    public required int          CreditNumber         { get; set; }
    public required decimal      CreditAmount         { get; set; }
    public required int          PaymentPeriod        { get; set; }
    public required decimal      InterestRate         { get; set; }
    public required DateTime     ContractDate         { get; set; }
    public required DateTime     MaturityDate         { get; set; }
    public required decimal      MonthlyPaymentAmount { get; set; }
    public required DateTime     NextPaymentDate      { get; set; }
    public required decimal      RemainingDebt        { get; set; }
    public required Currency     Currency             { get; set; }
    public          Guid         CurrencyId           { get; set; }
    public required string       CreditStatus         { get; set; } // Mozda enum TODO: 
    public required InterestType InterestType         { get; set; }
}
