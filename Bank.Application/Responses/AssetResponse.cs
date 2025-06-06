namespace Bank.Application.Responses;

public class AssetResponse
{
    public required Guid         Id      { set; get; }
    public required UserResponse Actuary { set; get; }

    //public required SecuritySimpleResponse Security     { set; get; }
    public required int      Quantity     { set; get; }
    public required decimal  AveragePrice { set; get; }
    public required DateTime CreatedAt    { set; get; }
    public required DateTime ModifiedAt   { set; get; }
}
