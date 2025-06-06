using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.ExchangeService.Mappers;
using Bank.ExchangeService.Repositories;
using Bank.Http.Clients.User;

using Result = Bank.Application.Endpoints.Result;

namespace Bank.ExchangeService.Services;

public interface IForexPairService
{
    Task<Result<Page<ForexPairSimpleResponse>>> GetAll(QuoteFilterQuery quoteFilterQuery, Pageable pageable);

    Task<Result<ForexPairResponse>> GetOne(Guid id, QuoteFilterIntervalQuery filter);

    Task<Result<ForexPairDailyResponse>> GetOneDaily(Guid id, QuoteFilterIntervalQuery filter);
}

public class ForexPairService(ISecurityRepository securityRepository, IUserServiceHttpClient userServiceHttpClient, IRedisRepository redisRepository) : IForexPairService
{
    private readonly ISecurityRepository    m_SecurityRepository    = securityRepository;
    private readonly IUserServiceHttpClient m_UserServiceHttpClient = userServiceHttpClient;
    private readonly IRedisRepository       m_RedisRepository       = redisRepository;

    public async Task<Result<Page<ForexPairSimpleResponse>>> GetAll(QuoteFilterQuery quoteFilterQuery, Pageable pageable)
    {
        var page = await m_SecurityRepository.FindAll(quoteFilterQuery, SecurityType.ForexPair, pageable);

        var list = m_UserServiceHttpClient.GetAllSimpleCurrencies(new CurrencyFilterQuery());

        if (list.Result is null)
            throw new Exception("There are no currencies in a database");

        var response = page.Items.Select(security => security.ToForexPair()
                                                             .ToSimpleResponse(list.Result.Find(curr => curr.Id == security.BaseCurrencyId)!,
                                                                               list.Result.Find(curr => curr.Id == security.QuoteCurrencyId)!))
                           .ToList();

        return Result.Ok(new Page<ForexPairSimpleResponse>(response, page.PageNumber, page.PageSize, page.TotalElements));
    }

    public async Task<Result<ForexPairResponse>> GetOne(Guid id, QuoteFilterIntervalQuery filter)
    {
        var security = await m_SecurityRepository.FindById(id, filter);

        if (security is null)
            return Result.NotFound<ForexPairResponse>($"No Forex pair found wih Id: {id}");

        var currencyBaseResponseTask  = m_UserServiceHttpClient.GetOneSimpleCurrency(security.BaseCurrencyId);
        var currencyQuoteResponseTask = m_UserServiceHttpClient.GetOneSimpleCurrency(security.QuoteCurrencyId);
        var currencyResponseTask      = m_UserServiceHttpClient.GetOneSimpleCurrency(security.StockExchange!.CurrencyId);

        Task.WaitAll(currencyBaseResponseTask, currencyQuoteResponseTask, currencyResponseTask);

        var currencyBaseResponse  = currencyBaseResponseTask.Result;
        var currencyQuoteResponse = currencyQuoteResponseTask.Result;
        var currencyResponse      = currencyResponseTask.Result;

        if (currencyBaseResponse is null)
            throw new Exception($"No Currency with Id: {security.BaseCurrencyId}");

        if (currencyQuoteResponse is null)
            throw new Exception($"No Currency with Id: {security.QuoteCurrencyId}");

        if (currencyResponse is null)
            throw new Exception($"No Currency with Id: {security.StockExchange!.CurrencyId}");

        if (filter.Interval == QuoteIntervalType.Day)
        {
            var redisQuotes = (await m_RedisRepository.FindAllForexPairQuotes(security.Ticker)).Select(redisQuote => redisQuote.ToQuote(security.Id))
                                                                                               .OrderByDescending(quote => quote.CreatedAt)
                                                                                               .ToList();

            var lastRedisQuoteDate = redisQuotes.LastOrDefault() == null
                                     ? DateTime.UtcNow
                                     : redisQuotes.Last()
                                                  .CreatedAt;

            var quotesBeforeRedisStarted = security.Quotes.SkipWhile(quote => quote.CreatedAt >= lastRedisQuoteDate)
                                                   .ToList();

            redisQuotes.AddRange(quotesBeforeRedisStarted);
            security.Quotes = redisQuotes;
        }

        return Result.Ok(security.ToForexPair()
                                 .ToResponse(currencyResponse, currencyBaseResponse, currencyQuoteResponse));
    }

    public async Task<Result<ForexPairDailyResponse>> GetOneDaily(Guid id, QuoteFilterIntervalQuery filter)
    {
        var forexPair = await m_SecurityRepository.FindByIdDaily(id, filter);

        if (forexPair is null)
            return Result.NotFound<ForexPairDailyResponse>($"No Forex pair found wih Id: {id}");

        var currencyBaseResponseTask  = m_UserServiceHttpClient.GetOneSimpleCurrency(forexPair.BaseCurrencyId);
        var currencyQuoteResponseTask = m_UserServiceHttpClient.GetOneSimpleCurrency(forexPair.QuoteCurrencyId);
        var currencyResponseTask      = m_UserServiceHttpClient.GetOneSimpleCurrency(forexPair.StockExchange!.CurrencyId);

        Task.WaitAll(currencyBaseResponseTask, currencyQuoteResponseTask, currencyResponseTask);

        var currencyBaseResponse  = currencyBaseResponseTask.Result;
        var currencyQuoteResponse = currencyQuoteResponseTask.Result;
        var currencyResponse      = currencyResponseTask.Result;

        if (currencyBaseResponse is null)
            throw new Exception($"No Currency with Id: {forexPair.BaseCurrencyId}");

        if (currencyQuoteResponse is null)
            throw new Exception($"No Currency with Id: {forexPair.QuoteCurrencyId}");

        if (currencyResponse is null)
            throw new Exception($"No Currency with Id: {forexPair.StockExchange!.CurrencyId}");

        return Result.Ok(forexPair.ToForexPair()
                                  .ToCandleResponse(currencyResponse, currencyBaseResponse, currencyQuoteResponse));
    }
}
