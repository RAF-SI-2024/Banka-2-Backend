namespace Bank.Application.Requests;

public class AccountTypeCreateRequest
{
    public required string Name { set; get; }
    public required string Code { set; get; }
}

public class AccountTypeUpdateRequest
{
    public required string Name { set; get; }
    public required string Code { set; get; }
}
