namespace Bank.Application.Responses;

public class CardResponse
{
    public Guid             Id         { get; set; }
    public string           Number     { get; set; }
    public CardTypeResponse Type       { get; set; }
    public string           Name       { get; set; }
    public DateOnly         ExpiresAt  { get; set; }
    public AccountResponse  Account    { get; set; }
    public string           CVV        { get; set; }
    public decimal          Limit      { get; set; }
    public bool             Status     { get; set; }
    public DateTime         CreatedAt  { get; set; }
    public DateTime         ModifiedAt { get; set; }
}
