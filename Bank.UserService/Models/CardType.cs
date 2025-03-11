namespace Bank.UserService.Models;

public class CardType
{
    public required Guid     Id         { get; set; }
    public required string   Name       { get; set; }
    public required DateTime CreatedAt  { get; set; }
    public required DateTime ModifiedAt { get; set; }
}
