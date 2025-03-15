namespace Bank.Application.Responses;

public class TransactionTemplateResponse
{
    public required Guid                 Id            { set; get; }
    public required ClientSimpleResponse Client        { set; get; }
    public required string               Name          { set; get; }
    public required string               AccountNumber { set; get; }
    public required bool                 Deleted       { set; get; }
    public required DateTime             CreatedAt     { set; get; }
    public required DateTime             ModifiedAt    { set; get; }
}

public class TransactionTemplateSimpleResponse
{
    public required Guid   Id            { set; get; }
    public required string Name          { set; get; }
    public required string AccountNumber { set; get; }
    public required bool   Deleted       { set; get; }
}
