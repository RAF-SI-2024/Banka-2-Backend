using Bank.Application.Domain;

namespace Bank.Application.Queries;

public class UserFilterQuery
{
    public string? Email     { set; get; }
    public string? FirstName { set; get; }
    public string? LastName  { set; get; }
    public Role    Role      { set; get; }
}
