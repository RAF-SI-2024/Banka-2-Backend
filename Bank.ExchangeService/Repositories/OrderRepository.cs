using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.Database.Core;
using Bank.ExchangeService.Models;
using Bank.Permissions.Services;

using Microsoft.EntityFrameworkCore;

using DatabaseContext = Bank.ExchangeService.Database.DatabaseContext;

namespace Bank.ExchangeService.Repositories;

public interface IOrderRepository
{
    Task<Page<Order>> FindAll(OrderFilterQuery orderFilterQuery, Pageable pageable);

    Task<Order?> FindById(Guid id);

    Task<Order> Add(Order order);

    Task<Order> Update(Order order);

    Task<Order?> UpdateStatus(Guid id, OrderStatus status);

    Task<bool> Approve(Guid id);

    Task<bool> Decline(Guid id);

    Task<bool> UpdateStatus(List<Guid> ids, OrderStatus status);
}

public class OrderRepository(IDatabaseContextFactory<DatabaseContext> contextFactory, IAuthorizationServiceFactory authorizationServiceFactory) : IOrderRepository
{
    private readonly IDatabaseContextFactory<DatabaseContext> m_ContextFactory              = contextFactory;
    private readonly IAuthorizationServiceFactory             m_AuthorizationServiceFactory = authorizationServiceFactory;

    public async Task<Page<Order>> FindAll(OrderFilterQuery filter, Pageable pageable)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var orderQuery = context.Orders.Include(order => order.Security).AsQueryable();

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
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Orders.Include(order => order.Security)
                            .FirstOrDefaultAsync(order => order.Id == id);
    }

    public async Task<Order> Add(Order order)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var addedOrder = await context.Orders.AddAsync(order);

        await context.SaveChangesAsync();

        return addedOrder.Entity;
    }

    public async Task<Order> Update(Order order)
    {
        await using var context = await m_ContextFactory.CreateContext;

        await context.Orders.Where(dbOrder => dbOrder.Id == order.Id)
                     .ExecuteUpdateAsync(setters => setters.SetProperty(dbOrder => dbOrder.Status, order.Status)
                                                           .SetProperty(dbOrder => dbOrder.ModifiedAt, order.ModifiedAt));

        return order;
    }

    public async Task<Order?> UpdateStatus(Guid id, OrderStatus status)
    {
        await using var context              = await m_ContextFactory.CreateContext;
        var             authorizationService = m_AuthorizationServiceFactory.AuthorizationService;

        var result = await context.Orders.Where(order => order.Id == id)
                                  .ExecuteUpdateAsync(setProperty => setProperty.SetProperty(order => order.Status, status)
                                                                                .SetProperty(order => order.SupervisorId, authorizationService.UserId)
                                                                                .SetProperty(order => order.ModifiedAt,   DateTime.UtcNow));

        if (result != 1)
            return null;

        var order = await context.Orders.Include(order => order.Security)
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(order => order.Id == id);

        return order!;
    }

    public async Task<bool> Approve(Guid id)
    {
        await using var context              = await m_ContextFactory.CreateContext;
        var             authorizationService = m_AuthorizationServiceFactory.AuthorizationService;

        var result = await context.Orders.Where(order => order.Id == id && order.Status == OrderStatus.NeedsApproval)
                                  .ExecuteUpdateAsync(setProperty => setProperty.SetProperty(order => order.Status, OrderStatus.Active)
                                                                                .SetProperty(order => order.SupervisorId, authorizationService.UserId)
                                                                                .SetProperty(order => order.ModifiedAt,   DateTime.UtcNow));

        return result == 1;
    }

    public async Task<bool> Decline(Guid id)
    {
        await using var context              = await m_ContextFactory.CreateContext;
        var             authorizationService = m_AuthorizationServiceFactory.AuthorizationService;

        var result = await context.Orders.Where(order => order.Id == id && order.Status == OrderStatus.NeedsApproval)
                                  .ExecuteUpdateAsync(setProperty => setProperty.SetProperty(order => order.Status, OrderStatus.Declined)
                                                                                .SetProperty(order => order.SupervisorId, authorizationService.UserId)
                                                                                .SetProperty(order => order.ModifiedAt,   DateTime.UtcNow));

        return result == 1;
    }

    public async Task<bool> UpdateStatus(List<Guid> ids, OrderStatus status)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var result = await context.Orders.Where(order => ids.Contains(order.Id))
                                  .ExecuteUpdateAsync(setters => setters.SetProperty(order => order.Status, status)
                                                                        .SetProperty(order => order.ModifiedAt, DateTime.UtcNow));

        return result == ids.Count;
    }
}
