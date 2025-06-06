using Bank.Application.Domain;

namespace Bank.Application.Responses;

public class OrderResponse
{
    public required Guid                   Id            { set; get; }
    public required UserResponse?          Actuary       { set; get; }
    public required AccountResponse        Account       { set; get; }
    public required SecuritySimpleResponse Security      { set; get; }
    public required OrderType              OrderType     { set; get; }
    public required int                    Quantity      { set; get; }
    public required int                    ContractCount { set; get; }
    public required decimal                StopPrice     { set; get; }
    public required decimal                LimitPrice    { set; get; }
    public required Direction              Direction     { set; get; }
    public required OrderStatus            Status        { set; get; }
    public required UserResponse?          Supervisor    { set; get; }
    public required DateTime               CreatedAt     { set; get; }
    public required DateTime               ModifiedAt    { set; get; }
}
