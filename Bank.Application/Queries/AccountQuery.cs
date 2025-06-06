namespace Bank.Application.Queries;

public class AccountFilterQuery
{
    public string?    Number          { set; get; }
    public string?    ClientEmail     { set; get; }
    public string?    ClientLastName  { set; get; }
    public string?    ClientFirstName { set; get; }
    public string?    EmployeeEmail   { set; get; }
    public string?    CurrencyName    { set; get; }
    public string?    AccountTypeName { set; get; }
    public bool?      Status          { set; get; }
    public List<Guid> Ids             { set; get; } = [];
}
