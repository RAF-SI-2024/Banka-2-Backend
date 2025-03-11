namespace Bank.UserService.Models;

public class Currency
{
    public required Guid          Id          { set; get; }
    public required string        Name        { set; get; }
    public required string        Code        { set; get; }
    public required string        Symbol      { set; get; }
    public required List<Country> Countries   { set; get; } = [];
    public required string        Description { set; get; }
    public required bool          Status      { set; get; }
    public required DateTime      CreatedAt   { set; get; }
    public required DateTime      ModifiedAt  { set; get; }
}
