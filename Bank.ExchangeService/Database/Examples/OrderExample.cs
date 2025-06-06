using Bank.Application.Domain;
using Bank.Application.Requests;
using Bank.Application.Responses;

namespace Bank.ExchangeService.Database.Examples;

public static partial class Example
{
    public static class Order
    {
        public static readonly OrderCreateRequest CreateRequest = new()
                                                                  {
                                                                      ActuaryId     = Guid.Parse("5817c260-e4a9-4dc1-87d9-2fa12af157d9"),
                                                                      OrderType     = OrderType.Market,
                                                                      Quantity      = 100,
                                                                      ContractCount = 10,
                                                                      StopPrice     = 222.22m,
                                                                      LimitPrice    = 250.75m,
                                                                      Direction     = Direction.Buy,
                                                                      AccountNumber = "222000000000000531",
                                                                      SecurityId    = Guid.Parse("c426aed1-9c27-4da1-aa51-6cf1045528d8")
                                                                  };

        public static readonly OrderUpdateRequest UpdateRequest = new()
                                                                  {
                                                                      Status = OrderStatus.NeedsApproval
                                                                  };

        public static readonly OrderResponse Response = new()
                                                        {
                                                            Id            = Guid.NewGuid(),
                                                            Actuary       = null!,
                                                            OrderType     = CreateRequest.OrderType,
                                                            Quantity      = CreateRequest.Quantity,
                                                            ContractCount = CreateRequest.ContractCount,
                                                            StopPrice     = CreateRequest.StopPrice,
                                                            LimitPrice    = CreateRequest.LimitPrice,
                                                            Direction     = CreateRequest.Direction,
                                                            Status        = UpdateRequest.Status,
                                                            Supervisor    = null!,
                                                            Account       = null!,
                                                            Security      = null!,
                                                            CreatedAt     = DateTime.UtcNow,
                                                            ModifiedAt    = DateTime.UtcNow,
                                                        };
    }
}
