namespace Bank.Application.Responses;

public class CardTypeResponse
{
    public required Guid     Id         { get; set; }
    public required string   Name       { get; set; }
    public required DateTime CreatedAt  { get; set; }
    public required DateTime ModifiedAt { get; set; }
}
