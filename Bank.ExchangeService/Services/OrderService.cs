using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.ExchangeService.Mappers;
using Bank.ExchangeService.Repositories;
using Bank.Http.Clients.User;
using Bank.Permissions.Services;

namespace Bank.ExchangeService.Services;

public interface IOrderService
{
    Task<Result<Page<OrderResponse>>> GetAll(OrderFilterQuery orderFilterQuery, Pageable pageable);

    Task<Result<OrderResponse>> GetOne(Guid id);

    Task<Result<OrderResponse>> Create(OrderCreateRequest request);

    Task<Result> Approve(Guid id);

    Task<Result> Decline(Guid id);

    Task<Result<OrderResponse>> Update(OrderUpdateRequest request, Guid id);
}

public class OrderService(
    IOrderRepository             orderRepository,
    ISecurityRepository          securityRepository,
    IUserServiceHttpClient       userServiceHttpClient,
    IRedisRepository             redisRepository,
    IAuthorizationServiceFactory authorizationService,
    IAssetRepository             assetRepository
) : IOrderService
{
    private readonly IOrderRepository             m_OrderRepository             = orderRepository;
    private readonly IAssetRepository             m_AssetRepository             = assetRepository;
    private readonly ISecurityRepository          m_SecurityRepository          = securityRepository;
    private readonly IUserServiceHttpClient       m_UserServiceHttpClient       = userServiceHttpClient;
    private readonly IRedisRepository             m_RedisRepository             = redisRepository;
    private readonly IAuthorizationServiceFactory m_AuthorizationServiceFactory = authorizationService;

    public async Task<Result<Page<OrderResponse>>> GetAll(OrderFilterQuery orderFilterQuery, Pageable pageable)
    {
        var orders = await m_OrderRepository.FindAll(orderFilterQuery, pageable);

        var userIds = orders.Items.SelectMany(order => new[] { order.ActuaryId, order.SupervisorId })
                            .Where(userId => userId != null)
                            .Select(userId => userId!.Value)
                            .Distinct()
                            .ToList();

        var userFilter = new UserFilterQuery
                         {
                             Ids = userIds
                         };

        var accountIds = orders.Items.Select(order => order.AccountId)
                               .Select(accountId => accountId)
                               .Distinct()
                               .ToList();

        var accountFilter = new AccountFilterQuery
                            {
                                Ids = accountIds,
                            };

        var userPageTask    = m_UserServiceHttpClient.GetAllUsers(userFilter, Pageable.Create(1,       userIds.Count));
        var accountPageTask = m_UserServiceHttpClient.GetAllAccounts(accountFilter, Pageable.Create(1, accountIds.Count));

        await Task.WhenAll(userPageTask, accountPageTask);

        var userPage    = await userPageTask;
        var accountPage = await accountPageTask;

        var userDictionary    = userPage.Items.ToDictionary(userResponse => userResponse.Id, userResponse => userResponse);
        var accountDictionary = accountPage.Items.ToDictionary(accountResponse => accountResponse.Id, accountResponse => accountResponse);

        var result = orders.Items.Select(order => order.ToResponse(userDictionary[order.ActuaryId], order.SupervisorId == null ? null : userDictionary[order.SupervisorId.Value],
                                                                   accountDictionary[order.AccountId]))
                           .ToList();

        return Result.Ok(new Page<OrderResponse>(result, orders.PageNumber, orders.PageSize, orders.TotalElements));
    }

    public async Task<Result<OrderResponse>> GetOne(Guid id)
    {
        var order = await m_OrderRepository.FindById(id);

        if (order is null)
            return Result.NotFound<OrderResponse>();

        var userFilter = new UserFilterQuery
                         {
                             Ids = [order.ActuaryId, order.SupervisorId ?? Guid.Empty]
                         };

        var userPageTask        = m_UserServiceHttpClient.GetAllUsers(userFilter, Pageable.Create(1, 2));
        var accountResponseTask = m_UserServiceHttpClient.GetOneAccount(order.AccountId);

        await Task.WhenAll(userPageTask, accountResponseTask);

        var userPage        = await userPageTask;
        var accountResponse = await accountResponseTask;

        var userDictionary = userPage.Items.ToDictionary(userResponse => userResponse.Id, userResponse => userResponse);

        return Result.Ok(order.ToResponse(userDictionary[order.ActuaryId], order.SupervisorId == null ? null : userDictionary[order.SupervisorId.Value], accountResponse!));
    }

    public async Task<Result<OrderResponse>> Create(OrderCreateRequest request)
    {
        if (request.Direction == Direction.Sell && await m_AssetRepository.HasAsset(request.SecurityId, request.ActuaryId, request.Quantity) is false)
            return Result.BadRequest<OrderResponse>("No such asset available to sell.");

        var userFilter = new UserFilterQuery
                         {
                             Ids = [request.ActuaryId, request.SupervisorId]
                         };

        var accountFilter = new AccountFilterQuery
                            {
                                Number = request.AccountNumber.Substring(7, 9)
                            };

        var userPageTask    = m_UserServiceHttpClient.GetAllUsers(userFilter, Pageable.Create(1,       2));
        var accountPageTask = m_UserServiceHttpClient.GetAllAccounts(accountFilter, Pageable.Create(1, 1));

        await Task.WhenAll(userPageTask, accountPageTask);

        var userPage    = await userPageTask;
        var accountPage = await accountPageTask;

        if (request.SupervisorId == Guid.Empty && userPage.TotalElements != 1 || request.SupervisorId != Guid.Empty && userPage.TotalElements != 2 ||
            accountPage.TotalElements != 1)
            return Result.BadRequest<OrderResponse>("Bad request");

        var security = await m_SecurityRepository.FindByIdSimple(request.SecurityId);

        if (security is null)
            return Result.BadRequest<OrderResponse>("Could not find Security");

        if (security.SettlementDate != DateOnly.MinValue && security.SettlementDate < DateOnly.FromDateTime(DateTime.Now))
            return Result.BadRequest<OrderResponse>("Security settlement date has passed");

        var authorizationService = m_AuthorizationServiceFactory.AuthorizationService;
        var userResponses        = userPage.Items.ToDictionary(userResponse => userResponse.Id, userResponse => userResponse);

        var approvesTrade = authorizationService.Permissions == Permission.ApproveTrade;

        var order = await m_OrderRepository.Add(request.ToOrder(accountPage.Items[0].Id, approvesTrade));

        order.Security = security;

        if (approvesTrade)
            await m_RedisRepository.AddOrder(order);

        return Result.Ok(order.ToResponse(userResponses[order.ActuaryId], order.SupervisorId == null ? null : userResponses[order.SupervisorId.Value], accountPage.Items[0]));
    }

    public async Task<Result> Approve(Guid id)
    {
        var result = await m_OrderRepository.Approve(id);

        if (result is false)
            return Result.BadRequest("Cannot approve order.");

        var order = await m_OrderRepository.FindById(id);

        await m_RedisRepository.AddOrder(order!);

        return Result.Ok();
    }

    public async Task<Result> Decline(Guid id)
    {
        var result = await m_OrderRepository.Decline(id);

        if (result is false)
            return Result.BadRequest("Cannot decline order.");

        return Result.Ok();
    }

    public async Task<Result<OrderResponse>> Update(OrderUpdateRequest request, Guid id)
    {
        var dbOrder = await m_OrderRepository.FindById(id);

        if (dbOrder is null)
            return Result.NotFound<OrderResponse>($"No Order found with Id: {id}");

        var order = await m_OrderRepository.Update(dbOrder.ToOrder(request));

        var userFilter = new UserFilterQuery
                         {
                             Ids = [order.ActuaryId, order.SupervisorId ?? Guid.Empty]
                         };

        var userPageTask        = m_UserServiceHttpClient.GetAllUsers(userFilter, Pageable.Create(1, 2));
        var accountResponseTask = m_UserServiceHttpClient.GetOneAccount(order.AccountId);

        await Task.WhenAll(userPageTask, accountResponseTask);

        var userPage        = await userPageTask;
        var accountResponse = await accountResponseTask;

        var userDictionary = userPage.Items.ToDictionary(userResponse => userResponse.Id, userResponse => userResponse);

        return Result.Ok(order.ToResponse(userDictionary[order.ActuaryId], order.SupervisorId == null ? null : userDictionary[order.SupervisorId.Value], accountResponse!));
    }
}
