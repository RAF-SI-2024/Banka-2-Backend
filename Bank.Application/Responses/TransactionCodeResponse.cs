namespace Bank.Application.Responses;

public class TransactionCodeResponse
{
    public required Guid     Id         { set; get; }
    public required string   Code       { set; get; }
    public required string   Name       { set; get; }
    public required DateTime CreatedAt  { set; get; }
    public required DateTime ModifiedAt { set; get; }
}
