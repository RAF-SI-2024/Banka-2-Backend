namespace Bank.Application.Responses;

public class LoanTypeResponse
{
    public required Guid    Id     { get; set; }
    public required string  Name   { get; set; }
    public required decimal Margin { get; set; }
}
