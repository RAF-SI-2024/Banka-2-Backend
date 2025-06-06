using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.Permissions.Services;
using Bank.UserService.Mappers;
using Bank.UserService.Repositories;

namespace Bank.UserService.Services;

public interface IAccountCurrencyService
{
    Task<Result<AccountCurrencyResponse>> GetOne(Guid id);

    Task<Result<Page<AccountCurrencyResponse>>> GetAll(Pageable pageable);

    Task<Result<AccountCurrencyResponse>> Create(AccountCurrencyCreateRequest createRequest);

    Task<Result<AccountCurrencyResponse>> Update(AccountCurrencyClientUpdateRequest accountClientUpdateRequest, Guid id);
}

public class AccountCurrencyService(
    IAccountCurrencyRepository   accountCurrencyRepository,
    IAccountRepository           accountRepository,
    ICurrencyRepository          currencyRepository,
    IUserRepository              userRepository,
    IAuthorizationServiceFactory authorizationServiceFactory
) : IAccountCurrencyService
{
    private readonly IAccountCurrencyRepository   m_AccountCurrencyRepository   = accountCurrencyRepository;
    private readonly IAccountRepository           m_AccountRepository           = accountRepository;
    private readonly ICurrencyRepository          m_CurrencyRepository          = currencyRepository;
    private readonly IUserRepository              m_UserRepository              = userRepository;
    private readonly IAuthorizationServiceFactory m_AuthorizationServiceFactory = authorizationServiceFactory;

    public async Task<Result<AccountCurrencyResponse>> GetOne(Guid id)
    {
        var accountCurrency = await m_AccountCurrencyRepository.FindById(id);

        if (accountCurrency == null)
            return Result.NotFound<AccountCurrencyResponse>($"No AccountCurrency found with Id: {id}");

        return Result.Ok(accountCurrency.ToResponse());
    }

    public async Task<Result<Page<AccountCurrencyResponse>>> GetAll(Pageable pageable)
    {
        var page = await m_AccountCurrencyRepository.FindAll(pageable);

        var accountCurrencyResponses = page.Items.Select(accountCurrency => accountCurrency.ToResponse())
                                           .ToList();

        return Result.Ok(new Page<AccountCurrencyResponse>(accountCurrencyResponses, page.PageNumber, page.PageSize, page.TotalElements));
    }

    public async Task<Result<AccountCurrencyResponse>> Create(AccountCurrencyCreateRequest createRequest)
    {
        var authorizationService = m_AuthorizationServiceFactory.AuthorizationService;

        var account  = await m_AccountRepository.FindById(createRequest.AccountId);
        var currency = await m_CurrencyRepository.FindById(createRequest.CurrencyId);
        var employee = await m_UserRepository.FindById(authorizationService.UserId);

        if (account == null || currency == null || employee == null)
            return Result.BadRequest<AccountCurrencyResponse>("Invalid data.");

        var accountCurrency = await m_AccountCurrencyRepository.Add(createRequest.ToAccountCurrency());

        accountCurrency.Account  = account;
        accountCurrency.Currency = currency;
        accountCurrency.Employee = employee;

        return Result.Ok(accountCurrency.ToResponse());
    }

    public async Task<Result<AccountCurrencyResponse>> Update(AccountCurrencyClientUpdateRequest request, Guid id)
    {
        var dbAccountCurrency = await m_AccountCurrencyRepository.FindById(id);

        if (dbAccountCurrency is null)
            return Result.NotFound<AccountCurrencyResponse>($"No Account Currency found with Id: {id}");

        var accountCurrency = await m_AccountCurrencyRepository.Update(dbAccountCurrency.ToAccountCurrency(request));

        return Result.Ok(accountCurrency.ToResponse());
    }
}
