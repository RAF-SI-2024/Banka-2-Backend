namespace Bank.ExchangeService.Models;

public class Asset
{
    public required Guid      Id           { set; get; }
    public required Guid      ActuaryId    { set; get; } // employee -> banka, client can trade -> client_id
    public required Guid      SecurityId   { set; get; }
    public          Security? Security     { set; get; }
    public required int       Quantity     { set; get; }
    public required decimal   AveragePrice { set; get; }
    public required DateTime  CreatedAt    { set; get; }
    public required DateTime  ModifiedAt   { set; get; }
}
