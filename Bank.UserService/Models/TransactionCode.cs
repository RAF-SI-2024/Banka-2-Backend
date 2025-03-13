namespace Bank.UserService.Models;

public class TransactionCode
{
    public required Guid     Id         { set; get; }
    public required string   Code       { set; get; }
    public required string   Name       { set; get; }
    public required DateTime CreatedAt  { set; get; }
    public required DateTime ModifiedAt { set; get; }
}
