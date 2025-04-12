using Bank.Application.Domain;

namespace Bank.Application.Requests;

public class OrderCreateRequest
{
    public required Guid ActuaryId { set; get; }
    //TODO asset
    public required OrderType OrderType         { set; get; }
    public required int       Quantity          { set; get; }
    public required int       ContractCount     { set; get; }
    public required decimal   PricePerUnit      { set; get; }
    public required Direction Direction         { set; get; }
    public required Guid      SupervisorId      { set; get; }
    public required int       RemainingPortions { set; get; }
    public required bool      AfterHours        { set; get; }
}

public class OrderUpdateRequest
{
    public required OrderStatus Status { set; get; }
}
