namespace Bank.UserService.Models;

public class Country
{
    public required Guid      Id         { set; get; }
    public required string    Name       { set; get; }
    public          Currency? Currency   { set; get; }
    public required Guid      CurrencyId { set; get; }
    public required DateTime  CreatedAt  { set; get; }
    public required DateTime  ModifiedAt { set; get; }
}
