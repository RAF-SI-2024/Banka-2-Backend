using Bank.Application.Domain;
using Bank.ExchangeService.Models;

namespace Bank.ExchangeService.Database.Seeders;

using OrderModel = Order;

public static partial class Seeder
{
    public static class Order
    {
        public static readonly OrderModel Order01 = new()
                                                    {
                                                        Id            = Guid.Parse("fde50c25-8515-4c83-a89b-fa6a6c01c254"),
                                                        ActuaryId     = Guid.Parse("b503387d-b9b5-41a2-9621-ee205c48a9cf"), //GUID USER
                                                        AccountId     = Guid.Parse("633419a2-21d5-420c-a951-a4a1b9b351c0"), //GUID USER
                                                        SupervisorId  = Guid.Parse("f38ac169-0865-4baa-afb7-56e422b5cf82"),
                                                        SecurityId    = Stock.Apple.Id,
                                                        OrderType     = OrderType.Market,
                                                        Quantity      = 8,
                                                        ContractCount = 0,
                                                        LimitPrice    = 0,
                                                        StopPrice     = 0,
                                                        Direction     = Direction.Buy,
                                                        Status        = OrderStatus.Canceled,
                                                        CreatedAt     = DateTime.UtcNow,
                                                        ModifiedAt    = DateTime.UtcNow,
                                                        AllOrNone     = true
                                                    };

        public static readonly OrderModel Order02 = new()
                                                    {
                                                        Id            = Guid.Parse("40962c3c-f8da-48c8-bc43-a94e5534276f"),
                                                        ActuaryId     = Guid.Parse("f38ac169-0865-4baa-afb7-56e422b5cf82"),
                                                        AccountId     = Guid.Parse("e4df2e9b-a57f-460e-a79e-c6b1e47ef4ab"),
                                                        SecurityId    = Stock.Microsoft.Id,
                                                        OrderType     = OrderType.Market,
                                                        Quantity      = 5,
                                                        ContractCount = 0,
                                                        LimitPrice    = 0,
                                                        StopPrice     = 0,
                                                        Direction     = Direction.Sell,
                                                        Status        = OrderStatus.Active,
                                                        CreatedAt     = DateTime.UtcNow,
                                                        ModifiedAt    = DateTime.UtcNow,
                                                        AllOrNone     = true
                                                    };
    }
}

public static class OrderSeederExtension
{
    public static async Task SeedOrdersHardcoded(this DatabaseContext context)
    {
        if (context.Orders.Any())
            return;

        await context.Orders.AddRangeAsync(Seeder.Order.Order01, Seeder.Order.Order02);

        await context.SaveChangesAsync();
    }
}
