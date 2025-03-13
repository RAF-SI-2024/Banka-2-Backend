namespace Bank.Application.Requests;

public class ExchangeRateBetweenRequest
{
    public required string CurrencyFromCode { set; get; }
    public required string CurrencyToCode   { set; get; }
}

public class ExchangeRateUpdateRequest
{
    public required decimal Commission { set; get; }
}

public class ExchangeRateMakeExchangeRequest
{
    public required Guid    CurrencyFromId { set; get; }
    public required Guid    CurrencyToId   { set; get; }
    public required decimal Amount         { set; get; }
    public required Guid    AccountId      { set; get; }
}
