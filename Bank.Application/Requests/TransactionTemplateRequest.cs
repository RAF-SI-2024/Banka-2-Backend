namespace Bank.Application.Requests;

public class TransactionTemplateCreateRequest
{
    public required string Name          { set; get; }
    public required string AccountNumber { set; get; }
}

public class TransactionTemplateUpdateRequest
{
    public required string Name          { set; get; }
    public required string AccountNumber { set; get; }
    public required bool   Deleted       { set; get; }
}
