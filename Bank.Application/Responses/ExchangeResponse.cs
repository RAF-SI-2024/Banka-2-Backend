namespace Bank.Application.Responses;

public class ExchangeResponse
{
    public required Guid                   Id           { set; get; }
    public required CurrencySimpleResponse CurrencyFrom { set; get; }
    public required CurrencySimpleResponse CurrencyTo   { set; get; }
    public required decimal                Commission   { set; get; }
    public required decimal                Rate         { set; get; }
    public required decimal                InverseRate  { set; get; }
    public required DateTime               CreatedAt    { set; get; }
    public required DateTime               ModifiedAt   { set; get; }
}

public class ExchangeFetchResponse
{
    public required string  Code { set; get; }
    public required decimal Rate { set; get; }
}
