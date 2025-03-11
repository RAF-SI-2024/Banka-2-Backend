using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Mappers;
using Bank.UserService.Repositories;

namespace Bank.UserService.Services;

public interface IAccountCurrencyService
{
    Task<Result<AccountCurrencyResponse>> GetOne(Guid id);

    Task<Result<Page<AccountCurrencyResponse>>> GetAll(Pageable pageable);

    Task<Result<AccountCurrencyResponse>> Create(AccountCurrencyCreateRequest accountCurrencyCreateRequest);

    Task<Result<AccountCurrencyResponse>> Update(AccountCurrencyClientUpdateRequest accountClientUpdateRequest, Guid id);
}

public class AccountCurrencyService(
    IAccountCurrencyRepository accountCurrencyRepository,
    IAccountRepository         accountRepository,
    ICurrencyRepository        currencyRepository,
    IUserRepository            userRepository,
    IAuthorizationService      authorizationService
) : IAccountCurrencyService
{
    private readonly IAccountCurrencyRepository m_AccountCurrencyRepository = accountCurrencyRepository;
    private readonly IAccountRepository         m_AccountRepository         = accountRepository;
    private readonly ICurrencyRepository        m_CurrencyRepository        = currencyRepository;
    private readonly IAuthorizationService      m_AuthorizationService      = authorizationService;
    private readonly IUserRepository            m_UserRepository            = userRepository;

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

    public async Task<Result<AccountCurrencyResponse>> Create(AccountCurrencyCreateRequest accountCurrencyCreateRequest)
    {
        var account  = await m_AccountRepository.FindById(accountCurrencyCreateRequest.AccountId);
        var currency = await m_CurrencyRepository.FindById(accountCurrencyCreateRequest.CurrencyId);
        var employee = await m_UserRepository.FindById(m_AuthorizationService.UserId);

        if (account == null || currency == null || employee == null)
            return Result.BadRequest<AccountCurrencyResponse>("Invalid data.");

        var accountCurrency = await m_AccountCurrencyRepository.Add(accountCurrencyCreateRequest.ToAccountCurrency(employee, currency, account));

        return Result.Ok(accountCurrency.ToResponse());
    }

    public async Task<Result<AccountCurrencyResponse>> Update(AccountCurrencyClientUpdateRequest accountClientUpdateRequest, Guid id)
    {
        var oldAccountCurrency = await m_AccountCurrencyRepository.FindById(id);

        if (oldAccountCurrency is null)
            return Result.NotFound<AccountCurrencyResponse>($"No AccountCurrency found with Id: {id}");

        var accountCurrency = await m_AccountCurrencyRepository.Update(oldAccountCurrency, accountClientUpdateRequest.ToAccountCurrency(oldAccountCurrency));

        return Result.Ok(accountCurrency.ToResponse());
    }
}
