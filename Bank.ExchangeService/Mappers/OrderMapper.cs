using Bank.Application.Domain;
using Bank.Application.Extensions;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.ExchangeService.Models;

namespace Bank.ExchangeService.Mappers;

public static class OrderMapper
{
    public static OrderResponse ToResponse(this Order order, UserResponse actuary, UserResponse? supervisor, AccountResponse account)
    {
        return new OrderResponse
               {
                   Id            = order.Id,
                   Actuary       = actuary,
                   OrderType     = order.OrderType,
                   Quantity      = order.Quantity,
                   ContractCount = order.ContractCount,
                   LimitPrice    = order.LimitPrice,
                   StopPrice     = order.StopPrice,
                   Direction     = order.Direction,
                   Status        = order.Status,
                   Supervisor    = supervisor,
                   CreatedAt     = order.CreatedAt,
                   ModifiedAt    = order.ModifiedAt,
                   Account       = account,
                   Security      = order.Security!.ToSecuritySimpleResponse()
               };
    }

    public static RedisOrder ToRedis(this Order order)
    {
        return new RedisOrder
               {
                   Id                = order.Id,
                   SecurityId        = order.SecurityId,
                   Ticker            = order.Security!.Ticker, //TODO: make sure its not null
                   SecurityType      = order.Security!.SecurityType,
                   Type              = order.OrderType,
                   RemainingPortions = order.Quantity,
                   LimitPrice        = order.LimitPrice,
                   StopPrice         = order.StopPrice,
                   Direction         = order.Direction,
                   AccountId         = order.AccountId,
                   AllOrNone         = order.AllOrNone
               };
    }

    public static Order ToOrder(this OrderCreateRequest createRequest, Guid accountId, bool approvesTrade)
    {
        return new Order
               {
                   Id            = Guid.NewGuid(),
                   ActuaryId     = createRequest.ActuaryId,
                   SupervisorId  = createRequest.SupervisorId == Guid.Empty ? null : createRequest.SupervisorId,
                   OrderType     = createRequest.OrderType,
                   Quantity      = createRequest.Quantity,
                   ContractCount = createRequest.ContractCount,
                   LimitPrice    = createRequest.LimitPrice,
                   StopPrice     = createRequest.StopPrice,
                   Direction     = createRequest.Direction,
                   Status        = approvesTrade ? OrderStatus.Active : OrderStatus.NeedsApproval,
                   CreatedAt     = DateTime.UtcNow,
                   ModifiedAt    = DateTime.UtcNow,
                   AccountId     = accountId,
                   SecurityId    = createRequest.SecurityId,
                   AllOrNone     = true
               };
    }

    public static Order ToOrder(this Order order, OrderUpdateRequest updateRequest)
    {
        order.Status     = updateRequest.Status;
        order.ModifiedAt = DateTime.UtcNow;
        return order;
    }

    public static RedisOrder MapKey(this RedisOrder order, string? key)
    {
        if (key is null)
            return order;

        var keyParts = key.Split(":");

        order.Id = new Guid(Convert.FromBase64String(keyParts[2]));

        order.SecurityType = (SecurityType)keyParts[1]
        .ParseIntOrDefault();

        return order;
    }

    public static string ToKey(this RedisOrder order)
    {
        return $"order:{(int)order.SecurityType}:{Convert.ToBase64String(order.Id.ToByteArray())}";
    }

    public static Asset ToAsset(this RedisOrder order, Quote quote, Guid actuaryId)
    {
        return new Asset
               {
                   Id           = Guid.NewGuid(),
                   ActuaryId    = actuaryId,
                   SecurityId   = order.SecurityId,
                   Quantity     = order.RemainingPortions,
                   AveragePrice = order.Direction == Direction.Buy ? quote.AskPrice : quote.BidPrice,
                   CreatedAt    = DateTime.UtcNow,
                   ModifiedAt   = DateTime.UtcNow
               };
    }
}
