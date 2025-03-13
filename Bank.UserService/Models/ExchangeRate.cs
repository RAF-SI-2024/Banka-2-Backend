namespace Bank.UserService.Models;

public class ExchangeRate
{
    public required Guid     Id             { set; get; }
    public required Guid     CurrencyFromId { set; get; }
    public required Currency CurrencyFrom   { set; get; }
    public required Guid     CurrencyToId   { set; get; }
    public required Currency CurrencyTo     { set; get; }
    public required decimal  Commission     { set; get; }
    public required decimal  Rate           { set; get; }
    public required DateTime CreatedAt      { set; get; }
    public required DateTime ModifiedAt     { set; get; }

    public decimal InverseRate => 1           / Rate;
    public decimal BidRate     => InverseRate * (1 + Commission);
    public decimal AskRate     => InverseRate * (1 - Commission);
}
