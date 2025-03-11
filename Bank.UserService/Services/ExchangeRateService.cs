using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Mappers;
using Bank.UserService.Repositories;

namespace Bank.UserService.Services;

public interface IExchangeRateService
{
    Task<Result<List<ExchangeRateResponse>>> GetAll(ExchangeRateFilterQuery exchangeRateFilterQuery);

    Task<Result<ExchangeRateResponse>> GetById(Guid id);

    Task<Result<ExchangeRateResponse>> GetByCurrencies(ExchangeRateBetweenRequest exchangeRateBetweenRequest);

    Task<Result<ExchangeRateResponse>> MakeExchange(ExchangeRateMakeExchangeRequest exchangeRateMakeExchangeRequest);

    Task<Result<ExchangeRateResponse>> Update(ExchangeRateUpdateRequest exchangeRateUpdateRequest, Guid id);
}

public class ExchangeRateService(
    IExchangeRateRepository    exchangeRateRepository,
    ICurrencyRepository        currencyRepository,
    IAccountRepository         accountRepository,
    IAccountCurrencyRepository accountCurrencyRepository
) : IExchangeRateService
{
    private readonly IExchangeRateRepository    m_ExchangeRateRepository    = exchangeRateRepository;
    private readonly ICurrencyRepository        m_CurrencyRepository        = currencyRepository;
    private readonly IAccountRepository         m_AccountRepository         = accountRepository;
    private readonly IAccountCurrencyRepository m_AccountCurrencyRepository = accountCurrencyRepository;

    public async Task<Result<List<ExchangeRateResponse>>> GetAll(ExchangeRateFilterQuery exchangeRateFilterQuery)
    {
        var exchangeRates = await m_ExchangeRateRepository.FindAll(exchangeRateFilterQuery);

        List<ExchangeRateResponse> exchangeRateResponses;

        if (exchangeRateFilterQuery.CurrencyId is not null)
        {
            exchangeRateResponses = exchangeRates.Select(exchangeRate => exchangeRate.CurrencyFromId == exchangeRateFilterQuery.CurrencyId
                                                                         ? exchangeRate.ToResponse()
                                                                         : exchangeRate.Inverse()
                                                                                       .ToResponse())
                                                 .ToList();
        }
        else if (!string.IsNullOrEmpty(exchangeRateFilterQuery.CurrencyCode))
        {
            exchangeRateResponses = exchangeRates.Select(exchangeRate => exchangeRate.CurrencyFrom.Code == exchangeRateFilterQuery.CurrencyCode
                                                                         ? exchangeRate.ToResponse()
                                                                         : exchangeRate.Inverse()
                                                                                       .ToResponse())
                                                 .ToList();
        }
        else
            exchangeRateResponses = exchangeRates.Select(exchangeRate => exchangeRate.ToResponse())
                                                 .ToList();

        return Result.Ok(exchangeRateResponses);
    }

    public async Task<Result<ExchangeRateResponse>> GetById(Guid id)
    {
        var exchangeRate = await m_ExchangeRateRepository.FindById(id);

        if (exchangeRate is null)
            return Result.NotFound<ExchangeRateResponse>($"No Exchange Rate with Id '{id}'");

        return Result.Ok(exchangeRate.ToResponse());
    }

    public async Task<Result<ExchangeRateResponse>> GetByCurrencies(ExchangeRateBetweenRequest exchangeRateBetweenRequest)
    {
        var firstCurrency = await m_CurrencyRepository.FindByCode(exchangeRateBetweenRequest.CurrencyFromCode);

        if (firstCurrency is null)
            return Result.NotFound<ExchangeRateResponse>($"No Currency with code '{exchangeRateBetweenRequest.CurrencyFromCode}'");

        var secondCurrency = await m_CurrencyRepository.FindByCode(exchangeRateBetweenRequest.CurrencyToCode);

        if (secondCurrency is null)
            return Result.NotFound<ExchangeRateResponse>($"No Currency with code '{exchangeRateBetweenRequest.CurrencyToCode}'");

        var exchangeRate = await m_ExchangeRateRepository.FindByCurrencyFromAndCurrencyTo(firstCurrency, secondCurrency);

        if (exchangeRate is null)
            return Result.NotFound<ExchangeRateResponse>($"No Exchange Rate with currencies '{firstCurrency.Code}' and '{secondCurrency.Code}'");

        return Result.Ok(exchangeRate.CurrencyFromId == firstCurrency.Id
                         ? exchangeRate.ToResponse()
                         : exchangeRate.Inverse()
                                       .ToResponse());
    }

    public async Task<Result<ExchangeRateResponse>> MakeExchange(ExchangeRateMakeExchangeRequest exchangeRateMakeExchangeRequest)
    {
        var account = await m_AccountRepository.FindById(exchangeRateMakeExchangeRequest.AccountId);

        if (account is null)
            return Result.NotFound<ExchangeRateResponse>($"No Account with Id '{exchangeRateMakeExchangeRequest.AccountId}'");

        var currencyFrom = await m_CurrencyRepository.FindById(exchangeRateMakeExchangeRequest.CurrencyFromId);

        if (currencyFrom is null)
            return Result.NotFound<ExchangeRateResponse>($"No Currency with Id '{exchangeRateMakeExchangeRequest.CurrencyFromId}'");

        var currencyTo = await m_CurrencyRepository.FindById(exchangeRateMakeExchangeRequest.CurrencyToId);

        if (currencyTo is null)
            return Result.NotFound<ExchangeRateResponse>($"No Currency with Id '{exchangeRateMakeExchangeRequest.CurrencyToId}'");

        var domesticCurrency = await m_CurrencyRepository.FindByCode("RSD");

        if (domesticCurrency is null)
            throw new Exception("No Domestic Currency");

        var exchangeRateFrom = await m_ExchangeRateRepository.FindByCurrencyFromAndCurrencyTo(domesticCurrency, currencyFrom);
        var exchangeRateTo   = await m_ExchangeRateRepository.FindByCurrencyFromAndCurrencyTo(domesticCurrency, currencyTo);

        if (exchangeRateFrom is null && exchangeRateTo is null)
            return Result.BadRequest<ExchangeRateResponse>($"No Exchange Rate with currencies '{currencyFrom.Code}' and '{currencyTo.Code}'");

        decimal rateFrom = exchangeRateFrom?.AskRate ?? 1;
        decimal rateTo   = exchangeRateTo?.BidRate   ?? 1;

        decimal resultRate  = rateFrom                               / rateTo;
        decimal finalAmount = exchangeRateMakeExchangeRequest.Amount * resultRate;

        // TODO: make transaction

        return Result.Ok<ExchangeRateResponse>();
    }

    public async Task<Result<ExchangeRateResponse>> Update(ExchangeRateUpdateRequest exchangeRateUpdateRequest, Guid id)
    {
        var oldExchangeRate = await m_ExchangeRateRepository.FindById(id);

        if (oldExchangeRate == null)
            return Result.NotFound<ExchangeRateResponse>($"No Exchange rate found with Id '{id}'");

        var updatedExchangeRate = await m_ExchangeRateRepository.Update(oldExchangeRate, exchangeRateUpdateRequest.ToExchangeRate(oldExchangeRate));

        return Result.Ok(updatedExchangeRate.ToResponse());
    }
}
