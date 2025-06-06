using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.ExchangeService.Database.Seeders;

namespace Bank.ExchangeService.Test.Examples.Entities;

public static partial class Example
{
    public static partial class Entity
    {
        public static class Order
        {
            public static readonly OrderFilterQuery FilterQuery = new()
                                                                  {
                                                                      Status = OrderStatus.Active
                                                                  };

            public static readonly Guid Id = Seeder.Order.Order01.Id;

            public static readonly OrderCreateRequest CreateRequest = Database.Examples.Example.Order.CreateRequest;

            public static readonly OrderUpdateRequest UpdateRequest = Database.Examples.Example.Order.UpdateRequest;
        }
    }
}
