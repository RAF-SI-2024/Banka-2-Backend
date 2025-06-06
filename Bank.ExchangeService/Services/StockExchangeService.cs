using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.ExchangeService.Mappers;
using Bank.ExchangeService.Repositories;
using Bank.Http.Clients.User;

namespace Bank.ExchangeService.Services;

public interface IStockExchangeService
{
    Task<Result<Page<StockExchangeResponse>>> GetAll(StockExchangeFilterQuery filter, Pageable pageable);

    Task<Result<StockExchangeResponse>> GetOne(Guid id);

    Task<Result<StockExchangeResponse>> GetOne(string acronym);

    Task<Result<StockExchangeResponse>> Create(ExchangeCreateRequest request);
}

public class StockExchangeService(IStockExchangeRepository stockExchangeRepository, IUserServiceHttpClient userServiceHttpClient) : IStockExchangeService
{
    private readonly IStockExchangeRepository m_Repo                  = stockExchangeRepository;
    private readonly IUserServiceHttpClient   m_UserServiceHttpClient = userServiceHttpClient;

    public async Task<Result<Page<StockExchangeResponse>>> GetAll(StockExchangeFilterQuery filter, Pageable pageable)
    {
        var page = await m_Repo.FindAll(filter, pageable);

        var currencyIds = page.Items.Select(se => se.CurrencyId)
                              .Distinct();

        var currencyTasks = currencyIds.Select(id => m_UserServiceHttpClient.GetOneSimpleCurrency(id))
                                       .ToList();

        var currencies = await Task.WhenAll(currencyTasks);

        var currencyMap = currencies.Where(c => c != null)
                                    .ToDictionary(c => c!.Id, c => c!);

        var responses = page.Items.Where(se => currencyMap.ContainsKey(se.CurrencyId))
                            .Select(se => se.ToResponse(currencyMap[se.CurrencyId]))
                            .ToList();

        return Result.Ok(new Page<StockExchangeResponse>(responses, page.PageNumber, page.PageSize, page.TotalElements));
    }

    public async Task<Result<StockExchangeResponse>> GetOne(Guid id)
    {
        var stockExchange = await m_Repo.FindById(id);

        if (stockExchange is null)
            return Result.NotFound<StockExchangeResponse>($"No StockExchange found with ID: {id}");

        var currency = await m_UserServiceHttpClient.GetOneSimpleCurrency(stockExchange.CurrencyId);

        if (currency is null)
            throw new Exception($"StockExchange currency doesn't exist with id: {stockExchange.CurrencyId}");

        return Result.Ok(stockExchange.ToResponse(currency));
    }

    public async Task<Result<StockExchangeResponse>> GetOne(string acronym)
    {
        var stockExchange = await m_Repo.FindByAcronym(acronym);

        if (stockExchange is null)
            Result.NotFound<StockExchangeResponse>($"No StockExchange found with acronym: {acronym}");

        var currency = await m_UserServiceHttpClient.GetOneSimpleCurrency(stockExchange!.CurrencyId);

        if (currency is null)
            throw new Exception($"StockExchange currency doesn't exist with id: {stockExchange.CurrencyId}");

        return Result.Ok(stockExchange.ToResponse(currency));
    }

    public async Task<Result<StockExchangeResponse>> Create(ExchangeCreateRequest request)
    {
        var currency = await m_UserServiceHttpClient.GetOneSimpleCurrency(request.CurrencyId);

        if (currency == null)
            return Result.NotFound<StockExchangeResponse>($"Currency not found: {request.CurrencyId}");

        var entity  = request.ToStockExchange();
        var created = await m_Repo.Add(entity);

        return Result.Ok(created.ToResponse(currency));
    }
}
