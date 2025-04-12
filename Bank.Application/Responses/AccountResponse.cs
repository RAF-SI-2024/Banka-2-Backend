namespace Bank.Application.Responses;

public class AccountResponse
{
    public required Guid                          Id                { set;  get; }
    public required string                        AccountNumber     { set;  get; }
    public required string                        Name              { set;  get; }
    public required ClientSimpleResponse          Client            { set;  get; }
    public required decimal                       Balance           { set;  get; }
    public required decimal                       AvailableBalance  { set;  get; }
    public required EmployeeSimpleResponse        Employee          { set;  get; }
    public required CurrencyResponse              Currency          { set;  get; }
    public required AccountTypeResponse           Type              { set;  get; }
    public required List<AccountCurrencyResponse> AccountCurrencies { init; get; } = [];
    public required decimal                       DailyLimit        { set;  get; }
    public required decimal                       MonthlyLimit      { set;  get; }
    public required DateOnly                      CreationDate      { set;  get; }
    public required DateOnly                      ExpirationDate    { set;  get; }
    public required bool                          Status            { set;  get; }
    public required DateTime                      CreatedAt         { set;  get; }
    public required DateTime                      ModifiedAt        { set;  get; }
}

public class AccountSimpleResponse
{
    public required Guid   Id            { set; get; }
    public required string AccountNumber { set; get; }
}
