using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.ExchangeService.HttpClients;
using Bank.ExchangeService.Mappers;
using Bank.ExchangeService.Repositorties;

namespace Bank.ExchangeService.Services;

public interface IStockExchangeService
{
    Task<Result<Page<StockExchangeResponse>>> GetAll(StockExchangeFilterQuery filter, Pageable pageable);

    Task<Result<StockExchangeResponse>> GetOne(Guid id);

    Task<Result<StockExchangeResponse>> Create(ExchangeCreateRequest request);
}

public class StockExchangeService(IStockExchangeRepository stockExchangeRepository, ICurrencyClient currencyClient) : IStockExchangeService
{
    private readonly IStockExchangeRepository m_Repo   = stockExchangeRepository;
    private readonly ICurrencyClient          m_Client = currencyClient;

    public async Task<Result<Page<StockExchangeResponse>>> GetAll(StockExchangeFilterQuery filter, Pageable pageable)
    {
        var page = await m_Repo.FindAll(filter, pageable);

        var currencyIds = page.Items.Select(se => se.CurrencyId)
                              .Distinct();

        var currencyTasks = currencyIds.Select(id => m_Client.GetCurrencyById(id))
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
        var currency      = await m_Client.GetCurrencyById(stockExchange.CurrencyId);
        return stockExchange is null ? Result.NotFound<StockExchangeResponse>($"No StockExchange found with ID: {id}") : Result.Ok(stockExchange.ToResponse(currency));
    }

    public async Task<Result<StockExchangeResponse>> Create(ExchangeCreateRequest request)
    {
        var currency = await m_Client.GetCurrencyById(request.CurrencyId);

        if (currency == null)
            return Result.NotFound<StockExchangeResponse>($"Currency not found: {request.CurrencyId}");

        var entity  = request.ToStockExchange();
        var created = await m_Repo.Add(entity);

        return Result.Ok(created.ToResponse(currency));
    }
}
