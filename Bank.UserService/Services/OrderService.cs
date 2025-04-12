using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.UserService.Repositories;
using Bank.Application.Requests;
using Bank.UserService.Mappers;

namespace Bank.UserService.Services;

public interface IOrderService
{
    Task<Result<Page<OrderResponse>>> GetAll(OrderFilterQuery orderFilterQuery, Pageable pageable);

    Task<Result<OrderResponse>> GetOne(Guid id);

    Task<Result<OrderResponse>> Create(OrderCreateRequest request);

    Task<Result<OrderResponse>> Update(OrderUpdateRequest request, Guid id);
}

public class OrderService(IOrderRepository orderRepository) : IOrderService
{
    private readonly IOrderRepository m_OrderRepository = orderRepository;
    
    public async Task<Result<Page<OrderResponse>>> GetAll(OrderFilterQuery orderFilterQuery, Pageable pageable)
    {
        var orders = await m_OrderRepository.FindAll(orderFilterQuery, pageable);

        var result = orders.Items.Select(order => order.ToResponse())
                           .ToList();

        return Result.Ok(new Page<OrderResponse>(result, orders.PageNumber, orders.PageSize, orders.TotalElements));
    }

    public async Task<Result<OrderResponse>> GetOne(Guid id)
    {
        var order = await m_OrderRepository.FindById(id);

        if (order == null)
            return Result.NotFound<OrderResponse>();

        return Result.Ok(order.ToResponse());
    }

    public async Task<Result<OrderResponse>> Create(OrderCreateRequest request)
    {
        var order = await m_OrderRepository.Add(request.ToOrder()); 
        
        return Result.Ok(order.ToResponse());
    }

    public async Task<Result<OrderResponse>> Update(OrderUpdateRequest request, Guid id)
    {
        var dbOrder = await m_OrderRepository.FindById(id);
    
        if (dbOrder is null)
            return Result.NotFound<OrderResponse>($"No Order found with Id: {id}");
    
        var order = await m_OrderRepository.Update(dbOrder.ToOrder(request));
    
        return Result.Ok(order.ToResponse());
     
    }
}
