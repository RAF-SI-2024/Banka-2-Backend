namespace Bank.Application.Queries;

public class CountryFilterQuery
{
    public string? Name         { get; set; }
    public string? CurrencyName { get; set; }
    public string? CurrencyCode { get; set; }
}
