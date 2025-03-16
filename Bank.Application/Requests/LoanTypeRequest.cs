namespace Bank.Application.Requests;

public class LoanTypeRequest
{
    public required string  Name   { get; set; }
    public required decimal Margin { get; set; }
}
