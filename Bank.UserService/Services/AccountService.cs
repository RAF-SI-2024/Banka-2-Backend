using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.Permissions.Services;
using Bank.UserService.Mappers;
using Bank.UserService.Repositories;

namespace Bank.UserService.Services;

public interface IAccountService
{
    Task<Result<Page<AccountResponse>>> GetAll(AccountFilterQuery accountFilterQuery, Pageable pageable);

    Task<Result<Page<AccountResponse>>> GetAllForClient(Guid clientId, AccountFilterQuery filter, Pageable pageable);

    Task<Result<AccountResponse>> GetOne(Guid id);

    Task<Result<AccountResponse>> GetOneByAccountNumber(string number);

    Task<Result<AccountResponse>> Create(AccountCreateRequest accountCreateRequest);

    Task<Result<AccountResponse>> Update(AccountUpdateClientRequest accountUpdateRequest, Guid id);

    Task<Result<AccountResponse>> Update(AccountUpdateEmployeeRequest accountUpdateRequest, Guid id);
}

public class AccountService(
    IAccountRepository           accountRepository,
    IAccountTypeRepository       accountTypeRepository,
    ICurrencyRepository          currencyRepository,
    IUserRepository              userRepository,
    IAuthorizationServiceFactory authorizationServiceFactory
) : IAccountService
{
    private readonly IAccountRepository           m_AccountRepository           = accountRepository;
    private readonly IAccountTypeRepository       m_AccountTypeRepository       = accountTypeRepository;
    private readonly IUserRepository              m_UserRepository              = userRepository;
    private readonly ICurrencyRepository          m_CurrencyRepository          = currencyRepository;
    private readonly IAuthorizationServiceFactory m_AuthorizationServiceFactory = authorizationServiceFactory;

    public async Task<Result<Page<AccountResponse>>> GetAll(AccountFilterQuery accountFilterQuery, Pageable pageable)
    {
        var page = await m_AccountRepository.FindAll(accountFilterQuery, pageable);

        var accountResponses = page.Items.Select(account => account.ToResponse())
                                   .ToList();

        return Result.Ok(new Page<AccountResponse>(accountResponses, page.PageNumber, page.PageSize, page.TotalElements));
    }

    public async Task<Result<Page<AccountResponse>>> GetAllForClient(Guid clientId, AccountFilterQuery filter, Pageable pageable)
    {
        var page = await m_AccountRepository.FindAllByClientId(clientId, pageable);

        var accountResponses = page.Items.Select(account => account.ToResponse())
                                   .ToList();

        return Result.Ok(new Page<AccountResponse>(accountResponses, page.PageNumber, page.PageSize, page.TotalElements));
    }

    public async Task<Result<AccountResponse>> GetOne(Guid id)
    {
        var account = await m_AccountRepository.FindById(id);

        if (account is null)
            return Result.NotFound<AccountResponse>($"No Account found with Id: {id}");

        return Result.Ok(account.ToResponse());
    }

    public async Task<Result<AccountResponse>> GetOneByAccountNumber(string number)
    {
        var account = await m_AccountRepository.FindByAccountNumber(number);

        if (account is null)
            return Result.NotFound<AccountResponse>($"No Account found with Number: {number}");

        return Result.Ok(account.ToResponse());
    }

    public async Task<Result<AccountResponse>> Create(AccountCreateRequest accountCreateRequest)
    {
        var authorizationService = m_AuthorizationServiceFactory.AuthorizationService;

        var accountType = await m_AccountTypeRepository.FindById(accountCreateRequest.AccountTypeId);
        var client      = await m_UserRepository.FindById(accountCreateRequest.ClientId);
        var currency    = await m_CurrencyRepository.FindById(accountCreateRequest.CurrencyId);
        var employee    = await m_UserRepository.FindById(authorizationService.UserId);

        if (accountType == null || client == null || currency == null || employee == null)
            return Result.BadRequest<AccountResponse>("Invalid data.");

        var account = await m_AccountRepository.Add(accountCreateRequest.ToAccount(authorizationService.UserId));

        account.Type     = accountType;
        account.Client   = client;
        account.Currency = currency;
        account.Employee = employee;

        return Result.Ok(account.ToResponse());
    }

    public async Task<Result<AccountResponse>> Update(AccountUpdateClientRequest request, Guid id)
    {
        var dbAccount = await m_AccountRepository.FindById(id);

        if (dbAccount is null)
            return Result.NotFound<AccountResponse>($"No Account found with Id: {id}");

        var account = await m_AccountRepository.Update(dbAccount.ToAccount(request));

        return Result.Ok(account.ToResponse());
    }

    public async Task<Result<AccountResponse>> Update(AccountUpdateEmployeeRequest request, Guid id)
    {
        var dbAccount = await m_AccountRepository.FindById(id);

        if (dbAccount is null)
            return Result.NotFound<AccountResponse>($"No Account found with Id: {id}");

        var account = await m_AccountRepository.Update(dbAccount.ToAccount(request));

        return Result.Ok(account.ToResponse());
    }
}
