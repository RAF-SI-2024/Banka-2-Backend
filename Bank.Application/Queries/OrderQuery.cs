using Bank.Application.Domain;

namespace Bank.Application.Queries;

public class OrderFilterQuery
{
    public OrderStatus Status { set; get; }
}
