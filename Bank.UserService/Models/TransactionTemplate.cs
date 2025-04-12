namespace Bank.UserService.Models;

public class TransactionTemplate
{
    public required Guid     Id            { set; get; }
    public required Guid     ClientId      { set; get; }
    public          User?    Client        { set; get; }
    public required string   Name          { set; get; }
    public required string   AccountNumber { set; get; }
    public required bool     Deleted       { set; get; }
    public required DateTime CreatedAt     { set; get; }
    public required DateTime ModifiedAt    { set; get; }
}
