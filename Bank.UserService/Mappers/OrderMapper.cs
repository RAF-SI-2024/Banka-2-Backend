using Bank.Application.Domain;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Models;

namespace Bank.UserService.Mappers;

public static class OrderMapper
{
    public static OrderResponse ToResponse(this Order order)
    {
        return new OrderResponse
               {
                   Id = order.Id,
                   //TODO asset
                   Actuary = order.Actuary?.ToEmployee()
                                  .ToSimpleResponse(),
                   OrderType     = order.OrderType,
                   Quantity      = order.Quantity,
                   ContractCount = order.ContractCount,
                   PricePerUnit  = order.PricePerUnit,
                   Direction     = order.Direction,
                   Status        = order.Status,
                   Supervisor = order.Supervisor?.ToEmployee()
                                     .ToSimpleResponse(),
                   Done              = order.Done,
                   RemainingPortions = order.RemainingPortions,
                   AfterHours        = order.AfterHours,
                   CreatedAt         = order.CreatedAt,
                   ModifiedAt        = order.ModifiedAt
               };
    }

    public static Order ToOrder(this Order order, OrderUpdateRequest updateRequest)
    {
        order.Status     = updateRequest.Status;
        order.ModifiedAt = DateTime.UtcNow;
        return order;
    }
    
    public static Order ToOrder(this OrderCreateRequest createRequest)
    {
        return new Order
               {
                   Id                = Guid.NewGuid(),
                   ActuaryId         = createRequest.ActuaryId,
                   OrderType         = createRequest.OrderType,
                   Quantity          = createRequest.Quantity,
                   ContractCount     = createRequest.ContractCount,
                   PricePerUnit      = createRequest.PricePerUnit,
                   Direction         = createRequest.Direction,
                   Status            = OrderStatus.Pending,
                   Done              = false,
                   RemainingPortions = createRequest.RemainingPortions,
                   AfterHours        = createRequest.AfterHours,
                   CreatedAt         = DateTime.UtcNow,
                   ModifiedAt        = DateTime.UtcNow
               };

    }
}
