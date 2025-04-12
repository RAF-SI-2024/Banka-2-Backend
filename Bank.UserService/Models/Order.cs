using Bank.Application.Domain;

namespace Bank.UserService.Models;

public class Order
{
    public required Guid  Id      { set; get; }
    public          User? Actuary { set; get; }

    public required Guid ActuaryId { set; get; }

    //TODO asset
    public required OrderType   OrderType         { set; get; }
    public required int         Quantity          { set; get; }
    public required int         ContractCount     { set; get; }
    public required decimal     PricePerUnit      { set; get; }
    public required Direction   Direction         { set; get; }
    public required OrderStatus Status            { set; get; }
    public          User?       Supervisor        { set; get; } // Approved By
    public          Guid?       SupervisorId      { set; get; }
    public required bool        Done              { set; get; }
    public required int         RemainingPortions { set; get; }
    public required bool        AfterHours        { set; get; }
    public required DateTime    CreatedAt         { set; get; }
    public required DateTime    ModifiedAt        { set; get; }
}
