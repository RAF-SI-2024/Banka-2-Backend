using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Configurations;
using Bank.UserService.Database.Seeders;
using Bank.UserService.Mappers;
using Bank.UserService.Models;
using Bank.UserService.Repositories;

namespace Bank.UserService.Services;

public interface IExchangeService
{
    Task<Result<List<ExchangeResponse>>> GetAll(ExchangeFilterQuery exchangeFilterQuery);

    Task<Result<ExchangeResponse>> GetById(Guid id);

    Task<Result<ExchangeResponse>> GetByCurrencies(ExchangeBetweenQuery exchangeBetweenQuery);

    Task<Result<ExchangeResponse>> MakeExchange(ExchangeMakeExchangeRequest exchangeMakeExchangeRequest);

    Task<Result<ExchangeResponse>> Update(ExchangeUpdateRequest exchangeUpdateRequest, Guid id);

    Task<ExchangeDetails?> CalculateExchangeDetails(Guid currencyFromId, Guid currencyToId);

    ExchangeDetails CalculateExchangeDetails(Exchange? exchangeFrom, Exchange? exchangeTo);
}

public class ExchangeService(
    IExchangeRepository        exchangeRepository,
    ICurrencyRepository        currencyRepository,
    IAccountRepository         accountRepository,
    Lazy<ITransactionService>  transactionServiceLazy,
    ITransactionCodeRepository transactionCodeRepository
) : IExchangeService
{
    private readonly IExchangeRepository        m_ExchangeRepository        = exchangeRepository;
    private readonly ICurrencyRepository        m_CurrencyRepository        = currencyRepository;
    private readonly IAccountRepository         m_AccountRepository         = accountRepository;
    private readonly ITransactionCodeRepository m_TransactionCodeRepository = transactionCodeRepository;
    private readonly Lazy<ITransactionService>  m_TransactionServiceLazy    = transactionServiceLazy;

    private ITransactionService TransactionService => m_TransactionServiceLazy.Value;

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
            exchangeResponses = exchanges.Select(exchange => exchange.CurrencyFrom!.Code == exchangeFilterQuery.CurrencyCode
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

    public async Task<Result<ExchangeResponse>> GetByCurrencies(ExchangeBetweenQuery exchangeBetweenQuery)
    {
        var firstCurrency = await m_CurrencyRepository.FindByCode(exchangeBetweenQuery.CurrencyFromCode);

        if (firstCurrency is null)
            return Result.NotFound<ExchangeResponse>($"No Currency with code '{exchangeBetweenQuery.CurrencyFromCode}'");

        var secondCurrency = await m_CurrencyRepository.FindByCode(exchangeBetweenQuery.CurrencyToCode);

        if (secondCurrency is null)
            return Result.NotFound<ExchangeResponse>($"No Currency with code '{exchangeBetweenQuery.CurrencyToCode}'");

        var exchange = await m_ExchangeRepository.FindByCurrencyFromAndCurrencyTo(firstCurrency, secondCurrency);

        if (exchange is null)
            return Result.NotFound<ExchangeResponse>($"No Exchange with currencies '{firstCurrency.Code}' and '{secondCurrency.Code}'");

        return Result.Ok(exchange.CurrencyFromId == firstCurrency.Id
                         ? exchange.ToResponse()
                         : exchange.Inverse()
                                   .ToResponse());
    }

    public async Task<Result<ExchangeResponse>> MakeExchange(ExchangeMakeExchangeRequest exchangeMakeExchangeRequest)
    {
        var accountTask         = m_AccountRepository.FindById(exchangeMakeExchangeRequest.AccountId);
        var fromCurrencyTask    = m_CurrencyRepository.FindById(exchangeMakeExchangeRequest.CurrencyFromId);
        var toCurrencyTask      = m_CurrencyRepository.FindById(exchangeMakeExchangeRequest.CurrencyToId);
        var transactionCodeTask = m_TransactionCodeRepository.FindById(Seeder.TransactionCode.TransactionCode285.Id);
        var exchangeDetailsTask = CalculateExchangeDetails(exchangeMakeExchangeRequest.CurrencyFromId, exchangeMakeExchangeRequest.CurrencyToId);

        await Task.WhenAll(accountTask, fromCurrencyTask, toCurrencyTask, transactionCodeTask, exchangeDetailsTask);

        var account         = await accountTask;
        var fromCurrency    = await fromCurrencyTask;
        var toCurrency      = await toCurrencyTask;
        var transactionCode = await transactionCodeTask;
        var exchangeDetails = await exchangeDetailsTask;

        if (account is null || fromCurrency is null || toCurrency is null || transactionCode is null || exchangeDetails is null)
            return Result.BadRequest<ExchangeResponse>("Invalid data.");
        
        await TransactionService.PrepareInternalTransaction(new PrepareInternalTransaction
                                                            {
                                                                FromAccount       = account,
                                                                FromCurrency      = fromCurrency,
                                                                ToAccount         = account,
                                                                ToCurrency        = toCurrency,
                                                                Amount            = exchangeMakeExchangeRequest.Amount,
                                                                ExchangeDetails   = exchangeDetails,
                                                                TransactionCode   = transactionCode,
                                                                ReferenceNumber   = null,
                                                                Purpose           = "Currency Exchange"
                                                            });

        return Result.Ok<ExchangeResponse>();
    }

    public async Task<Result<ExchangeResponse>> Update(ExchangeUpdateRequest exchangeUpdateRequest, Guid id)
    {
        var oldExchange = await m_ExchangeRepository.FindById(id);

        if (oldExchange == null)
            return Result.NotFound<ExchangeResponse>($"No Exchange found with Id '{id}'");

        var updatedExchange = await m_ExchangeRepository.Update(oldExchange.Update(exchangeUpdateRequest));

        return Result.Ok(updatedExchange.ToResponse());
    }

    public async Task<ExchangeDetails?> CalculateExchangeDetails(Guid currencyFromId, Guid currencyToId)
    {
        var currencyFromTask    = m_CurrencyRepository.FindById(currencyFromId);
        var currencyToTask      = m_CurrencyRepository.FindById(currencyToId);
        var defaultCurrencyTask = m_CurrencyRepository.FindByCode(Configuration.Exchange.DefaultCurrencyCode);

        await Task.WhenAll(currencyFromTask, currencyToTask, defaultCurrencyTask);

        var currencyFrom    = await currencyFromTask;
        var currencyTo      = await currencyToTask;
        var defaultCurrency = await defaultCurrencyTask;

        if (currencyFrom is null || currencyTo is null)
            return null;

        if (defaultCurrency is null)
            throw new Exception("No Default Currency");

        var exchangeFromTask = m_ExchangeRepository.FindByCurrencyFromAndCurrencyTo(defaultCurrency, currencyFrom);
        var exchangeToTask   = m_ExchangeRepository.FindByCurrencyFromAndCurrencyTo(defaultCurrency, currencyTo);

        await Task.WhenAll(exchangeFromTask, exchangeToTask);

        var exchangeFrom = await exchangeFromTask;
        var exchangeTo   = await exchangeToTask;

        return CalculateExchangeDetails(exchangeFrom, exchangeTo);
    }

    public ExchangeDetails CalculateExchangeDetails(Exchange? exchangeFrom, Exchange? exchangeTo)
    {
        decimal exchangeRateFrom = exchangeFrom?.AskRate ?? 1;
        decimal exchangeRateTo   = exchangeTo?.BidRate   ?? 1;
        decimal exchangeRate     = exchangeRateFrom / exchangeRateTo;

        decimal inverseExchangeRate = exchangeRateTo / exchangeRateFrom;

        decimal averageRateFrom = exchangeFrom?.Rate ?? 1;
        decimal averageRateTo   = exchangeTo?.Rate   ?? 1;
        decimal averageRate     = averageRateFrom / averageRateTo;

        decimal inverseAverageRateFrom = exchangeFrom?.InverseRate ?? 1;
        decimal inverseAverageRateTo   = exchangeTo?.InverseRate   ?? 1;
        decimal inverseAverageRate     = inverseAverageRateFrom / inverseAverageRateTo;

        return new ExchangeDetails()
               {
                   CurrencyFrom        = exchangeFrom?.CurrencyFrom ?? null,
                   CurrencyTo          = exchangeTo?.CurrencyTo     ?? null,
                   ExchangeRate        = exchangeRate,
                   InverseExchangeRate = inverseExchangeRate,
                   AverageRate         = averageRate,
                   InverseAverageRate  = inverseAverageRate
               };
    }
}
