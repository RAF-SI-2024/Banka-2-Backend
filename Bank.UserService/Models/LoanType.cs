namespace Bank.UserService.Models;

public class LoanType
{
    public required Guid     Id         { get; set; }
    public required string   Name       { get; set; }
    public required decimal  Margin     { get; set; }
    public required DateTime CreatedAt  { get; set; }
    public required DateTime ModifiedAt { get; set; }
}
