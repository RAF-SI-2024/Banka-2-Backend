using Bank.Application.Domain;

namespace Bank.Application.Requests;

public class OrderCreateRequest
{
    public required Guid      ActuaryId     { set; get; }
    public required string    AccountNumber { set; get; }
    public required OrderType OrderType     { set; get; }
    public required int       Quantity      { set; get; }
    public          int       ContractCount { set; get; }
    public          decimal   LimitPrice    { set; get; }
    public          decimal   StopPrice     { set; get; }
    public required Direction Direction     { set; get; }
    public          Guid      SupervisorId  { set; get; }
    public required Guid      SecurityId    { set; get; }
}

public class OrderUpdateRequest
{
    public required OrderStatus Status { set; get; }
}
