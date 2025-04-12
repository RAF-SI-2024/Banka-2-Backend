using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.UserService.Database;
using Bank.UserService.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Bank.UserService.Repositories;

//TODO: Move to Exchange Service Project

public interface IOrderRepository
{
    Task<Page<Order>> FindAll(OrderFilterQuery orderFilterQuery, Pageable pageable); // filter

    Task<Order?> FindById(Guid id);

    Task<Order> Add(Order order);

    Task<Order> Update(Order order);

    Task<Order> UpdateStatus(Guid id, OrderStatus status);
}

public class OrderRepository(ApplicationContext context, IAuthorizationService authorizationService) : IOrderRepository
{
    private readonly ApplicationContext    m_Context              = context;
    private readonly IAuthorizationService m_AuthorizationService = authorizationService;

    public async Task<Page<Order>> FindAll(OrderFilterQuery filter, Pageable pageable)
    {
        var orderQuery = m_Context.Orders.Include(order => order.Actuary)
                                  .Include(order => order.Supervisor)
                                  .AsQueryable();

        if (filter.Status != OrderStatus.Invalid)
            orderQuery = orderQuery.Where(order => order.Status == filter.Status);

        var orders = await orderQuery.Skip((pageable.Page - 1) * pageable.Size)
                                     .Take(pageable.Size)
                                     .ToListAsync();

        var totalElements = await orderQuery.CountAsync();

        return new Page<Order>(orders, pageable.Page, pageable.Size, totalElements);
    }

    public async Task<Order?> FindById(Guid id)
    {
        return await m_Context.Orders.FirstOrDefaultAsync(order => order.Id == id);
    }

    public async Task<Order> Add(Order order)
    {
        var addedOrder = await m_Context.Orders.AddAsync(order);

        await m_Context.SaveChangesAsync();

        return addedOrder.Entity;
    }

    public async Task<Order> Update(Order order)
    {
        await m_Context.Orders.Where(dbOrder => dbOrder.Id == order.Id)
                       .ExecuteUpdateAsync(setters => setters.SetProperty(dbOrder => dbOrder.Status, order.Status)
                                                             .SetProperty(dbOrder => dbOrder.ModifiedAt, order.ModifiedAt));

        return order;
    }

    public async Task<Order> UpdateStatus(Guid id, OrderStatus status)
    {
        await m_Context.Orders.Where(order => order.Id == id)
                       .ExecuteUpdateAsync(setProperty => setProperty.SetProperty(order => order.Status, status));

        var order = await m_Context.Orders.AsNoTracking()
                                   .FirstOrDefaultAsync(order => order.Id == id);

        return order!;
    }
}
