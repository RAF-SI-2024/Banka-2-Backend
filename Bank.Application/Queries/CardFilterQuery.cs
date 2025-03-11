namespace Bank.Application.Queries;

public class CardFilterQuery
{
    public string? Number { get; set; }
    public string? Name   { get; set; }
}

public class CardTypeFilterQuery
{
    public string? Name { get; set; }
}
