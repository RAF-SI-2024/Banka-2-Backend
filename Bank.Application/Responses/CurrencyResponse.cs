namespace Bank.Application.Responses;

public class CurrencyResponse
{
    public required Guid                        Id          { get; set; }
    public required string                      Name        { get; set; }
    public required string                      Code        { get; set; }
    public required string                      Symbol      { get; set; }
    public required List<CountrySimpleResponse> Countries   { get; set; }
    public required string                      Description { get; set; }
    public required bool                        Status      { get; set; }
    public required DateTime                    CreatedAt   { get; set; }
    public required DateTime                    ModifiedAt  { get; set; }
}

public class CurrencySimpleResponse
{
    public required Guid     Id          { get; set; }
    public required string   Name        { get; set; }
    public required string   Code        { get; set; }
    public required string   Symbol      { get; set; }
    public required string   Description { get; set; }
    public required bool     Status      { get; set; }
    public required DateTime CreatedAt   { get; set; }
    public required DateTime ModifiedAt  { get; set; }
}
