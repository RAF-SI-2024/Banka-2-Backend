namespace Bank.Application.Requests;

public class AccountCurrencyCreateRequest
{
    public required Guid    EmployeeId   { set; get; }
    public required Guid    CurrencyId   { set; get; }
    public required Guid    AccountId    { set; get; }
    public required decimal DailyLimit   { set; get; }
    public required decimal MonthlyLimit { set; get; }
}

public class AccountCurrencyClientUpdateRequest
{
    public required decimal DailyLimit   { set; get; }
    public required decimal MonthlyLimit { set; get; }
}
