namespace Bank.Application.Domain;

public class Pageable
{
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 10;
}

public class Page<T>
{
    public List<T> Items         { get; set; } = [];
    public int     PageNumber    { get; set; }
    public int     PageSize      { get; set; }
    public int     TotalElements { get; set; }
    public int     TotalPages    => (int)Math.Ceiling((double)TotalElements / PageSize);

    public Page(List<T> items, int pageNumber, int pageSize, int totalElements)
    {
        Items         = items;
        PageNumber    = pageNumber;
        PageSize      = pageSize;
        TotalElements = totalElements;
    }
}
