namespace Bank.Application.Requests;

public class AccountCreateRequest
{
    public required string  Name          { set; get; }
    public required decimal DailyLimit    { set; get; }
    public required Guid    ClientId      { set; get; }
    public required decimal Balance       { set; get; }
    public required Guid    CurrencyId    { set; get; }
    public required Guid    AccountTypeId { set; get; }
    public required decimal MonthlyLimit  { set; get; }
    public required bool    Status        { set; get; }
}

public class AccountUpdateEmployeeRequest
{
    public required bool Status { set; get; }
}

public class AccountUpdateClientRequest
{
    public required string  Name         { set; get; }
    public required decimal DailyLimit   { set; get; }
    public required decimal MonthlyLimit { set; get; }
}
