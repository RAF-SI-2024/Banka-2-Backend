namespace Bank.Application.Responses;

public class CountryResponse
{
    public required Guid                    Id         { get; set; }
    public required string                  Name       { get; set; }
    public          CurrencySimpleResponse? Currency   { get; set; }
    public required DateTime                CreatedAt  { get; set; }
    public required DateTime                ModifiedAt { get; set; }
}

public class CountrySimpleResponse
{
    public required Guid     Id         { get; set; }
    public required string   Name       { get; set; }
    public required DateTime CreatedAt  { get; set; }
    public required DateTime ModifiedAt { get; set; }
}
