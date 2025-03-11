namespace Bank.Application.Responses;

public class AccountCurrencyResponse
{
    public required Guid                   Id               { set; get; }
    public required AccountSimpleResponse  Account          { set; get; }
    public required EmployeeSimpleResponse Employee         { set; get; }
    public required CurrencyResponse       Currency         { set; get; }
    public required decimal                Balance          { set; get; }
    public required decimal                AvailableBalance { set; get; }
    public required decimal                DailyLimit       { set; get; }
    public required decimal                MonthlyLimit     { set; get; }
    public required DateTime               CreatedAt        { set; get; }
    public required DateTime               ModifiedAt       { set; get; }
}
