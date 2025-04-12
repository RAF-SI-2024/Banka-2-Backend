using Bank.Application.Domain;

namespace Bank.Application.Responses;

public class OrderResponse
{
    public required Guid                    Id      { set; get; }
    public required EmployeeSimpleResponse? Actuary { set; get; }

    //TODO asset
    public required OrderType               OrderType         { set; get; }
    public required int                     Quantity          { set; get; }
    public required int                     ContractCount     { set; get; }
    public required decimal                 PricePerUnit      { set; get; }
    public required Direction               Direction         { set; get; }
    public required OrderStatus             Status            { set; get; }
    public required EmployeeSimpleResponse? Supervisor        { set; get; } // Approved By  
    public required bool                    Done              { set; get; }
    public required int                     RemainingPortions { set; get; }
    public required bool                    AfterHours        { set; get; }
    public required DateTime                CreatedAt         { set; get; }
    public required DateTime                ModifiedAt        { set; get; }
}
