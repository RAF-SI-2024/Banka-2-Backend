using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Configurations;
using Bank.UserService.Mappers;
using Bank.UserService.Repositories;

namespace Bank.UserService.Services;

public interface IExchangeService
{
    Task<Result<List<ExchangeResponse>>> GetAll(ExchangeFilterQuery exchangeFilterQuery);

    Task<Result<ExchangeResponse>> GetById(Guid id);

    Task<Result<ExchangeResponse>> GetByCurrencies(ExchangeBetweenRequest exchangeBetweenRequest, ExchangeFilterQuery exchangeFilterQuery);

    Task<Result<ExchangeResponse>> MakeExchange(ExchangeMakeExchangeRequest exchangeMakeExchangeRequest, ExchangeFilterQuery exchangeFilterQuery);

    Task<Result<ExchangeResponse>> Update(ExchangeUpdateRequest exchangeUpdateRequest, Guid id);
}

public class ExchangeService(
    IExchangeRepository        exchangeRepository,
    ICurrencyRepository        currencyRepository,
    IAccountRepository         accountRepository,
    IAccountCurrencyRepository accountCurrencyRepository
) : IExchangeService
{
    private readonly IExchangeRepository        m_ExchangeRepository        = exchangeRepository;
    private readonly ICurrencyRepository        m_CurrencyRepository        = currencyRepository;
    private readonly IAccountRepository         m_AccountRepository         = accountRepository;
    private readonly IAccountCurrencyRepository m_AccountCurrencyRepository = accountCurrencyRepository;

    public async Task<Result<List<ExchangeResponse>>> GetAll(ExchangeFilterQuery exchangeFilterQuery)
    {
        var exchanges = await m_ExchangeRepository.FindAll(exchangeFilterQuery);

        List<ExchangeResponse> exchangeResponses;

        if (exchangeFilterQuery.CurrencyId != Guid.Empty)
        {
            exchangeResponses = exchanges.Select(exchange => exchange.CurrencyFromId == exchangeFilterQuery.CurrencyId
                                                             ? exchange.ToResponse()
                                                             : exchange.Inverse()
                                                                       .ToResponse())
                                         .ToList();
        }
        else if (!string.IsNullOrEmpty(exchangeFilterQuery.CurrencyCode))
        {
            exchangeResponses = exchanges.Select(exchange => exchange.CurrencyFrom.Code == exchangeFilterQuery.CurrencyCode
                                                             ? exchange.ToResponse()
                                                             : exchange.Inverse()
                                                                       .ToResponse())
                                         .ToList();
        }
        else
            exchangeResponses = exchanges.Select(exchange => exchange.ToResponse())
                                         .ToList();

        return Result.Ok(exchangeResponses);
    }

    public async Task<Result<ExchangeResponse>> GetById(Guid id)
    {
        var exchange = await m_ExchangeRepository.FindById(id);

        if (exchange is null)
            return Result.NotFound<ExchangeResponse>($"No Exchange with Id '{id}'");

        return Result.Ok(exchange.ToResponse());
    }

    public async Task<Result<ExchangeResponse>> GetByCurrencies(ExchangeBetweenRequest exchangeBetweenRequest, ExchangeFilterQuery exchangeFilterQuery)
    {
        var firstCurrency = await m_CurrencyRepository.FindByCode(exchangeBetweenRequest.CurrencyFromCode);

        if (firstCurrency is null)
            return Result.NotFound<ExchangeResponse>($"No Currency with code '{exchangeBetweenRequest.CurrencyFromCode}'");

        var secondCurrency = await m_CurrencyRepository.FindByCode(exchangeBetweenRequest.CurrencyToCode);

        if (secondCurrency is null)
            return Result.NotFound<ExchangeResponse>($"No Currency with code '{exchangeBetweenRequest.CurrencyToCode}'");

        var exchange = await m_ExchangeRepository.FindByCurrencyFromAndCurrencyTo(firstCurrency, secondCurrency, exchangeFilterQuery);

        if (exchange is null)
            return Result.NotFound<ExchangeResponse>($"No Exchange with currencies '{firstCurrency.Code}' and '{secondCurrency.Code}'");

        return Result.Ok(exchange.CurrencyFromId == firstCurrency.Id
                         ? exchange.ToResponse()
                         : exchange.Inverse()
                                   .ToResponse());
    }

    public async Task<Result<ExchangeResponse>> MakeExchange(ExchangeMakeExchangeRequest exchangeMakeExchangeRequest, ExchangeFilterQuery exchangeFilterQuery)
    {
        var account = await m_AccountRepository.FindById(exchangeMakeExchangeRequest.AccountId);

        if (account is null)
            return Result.NotFound<ExchangeResponse>($"No Account with Id '{exchangeMakeExchangeRequest.AccountId}'");

        var currencyFrom = await m_CurrencyRepository.FindById(exchangeMakeExchangeRequest.CurrencyFromId);

        if (currencyFrom is null)
            return Result.NotFound<ExchangeResponse>($"No Currency with Id '{exchangeMakeExchangeRequest.CurrencyFromId}'");

        var currencyTo = await m_CurrencyRepository.FindById(exchangeMakeExchangeRequest.CurrencyToId);

        if (currencyTo is null)
            return Result.NotFound<ExchangeResponse>($"No Currency with Id '{exchangeMakeExchangeRequest.CurrencyToId}'");

        var defaultCurrency = await m_CurrencyRepository.FindByCode(Configuration.Exchange.DefaultCurrencyCode);

        if (defaultCurrency is null)
            throw new Exception("No Domestic Currency");

        var exchangeFrom = await m_ExchangeRepository.FindByCurrencyFromAndCurrencyTo(defaultCurrency, currencyFrom, exchangeFilterQuery);
        var exchangeTo   = await m_ExchangeRepository.FindByCurrencyFromAndCurrencyTo(defaultCurrency, currencyTo,   exchangeFilterQuery);

        if (exchangeFrom is null && exchangeTo is null)
            return Result.BadRequest<ExchangeResponse>($"No Exchange with currencies '{currencyFrom.Code}' and '{currencyTo.Code}'");

        decimal rateFrom = exchangeFrom?.AskRate ?? 1;
        decimal rateTo   = exchangeTo?.BidRate   ?? 1;

        decimal resultRate  = rateFrom                           / rateTo;
        decimal finalAmount = exchangeMakeExchangeRequest.Amount * resultRate;

        // TODO: make transaction

        return Result.Ok<ExchangeResponse>();
    }

    public async Task<Result<ExchangeResponse>> Update(ExchangeUpdateRequest exchangeUpdateRequest, Guid id)
    {
        var oldExchange = await m_ExchangeRepository.FindById(id);

        if (oldExchange == null)
            return Result.NotFound<ExchangeResponse>($"No Exchange found with Id '{id}'");

        var updatedExchange = await m_ExchangeRepository.Update(oldExchange, exchangeUpdateRequest.ToExchange(oldExchange));

        return Result.Ok(updatedExchange.ToResponse());
    }
}
