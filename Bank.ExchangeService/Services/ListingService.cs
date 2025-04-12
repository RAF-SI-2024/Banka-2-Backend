using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.ExchangeService.HttpClients;
using Bank.ExchangeService.Mappers;
using Bank.ExchangeService.Model;
using Bank.ExchangeService.Repositorties;

namespace Bank.ExchangeService.Services;

public interface IListingService
{
    Task<Result<Page<ListingResponse>>> GetAll(ListingFilterQuery filter, Pageable pageable);

    Task<Result<ListingResponse>> GetOne(Guid id);

    Task<Result<ListingResponse>> Create(ListingCreateRequest request);
}

public class ListingService(IListingRepository listingRepository, IStockExchangeRepository exchangeRepository, ICurrencyClient currencyClient) : IListingService
{
    private readonly IListingRepository       m_Repo           = listingRepository;
    private readonly IStockExchangeRepository m_ExchangeRepo   = exchangeRepository;
    private readonly ICurrencyClient          m_CurrencyClient = currencyClient;

    public async Task<Result<Page<ListingResponse>>> GetAll(ListingFilterQuery filter, Pageable pageable)
    {
        var page = await m_Repo.FindAll(filter, pageable);

        var exchangeIds = page.Items.Select(l => l.StockExchangeId)
                              .Distinct();

        var exchangeMap = new Dictionary<Guid, StockExchange>();
        var currencyMap = new Dictionary<Guid, CurrencyResponse>();

        foreach (var exchangeId in exchangeIds)
        {
            var exchange = await m_ExchangeRepo.FindById(exchangeId);

            if (exchange != null)
            {
                var currency = await m_CurrencyClient.GetCurrencyById(exchange.CurrencyId);

                if (currency != null)
                {
                    exchangeMap[exchangeId]          = exchange;
                    currencyMap[exchange.CurrencyId] = currency;
                }
            }
        }

        var responses = page.Items.Where(l => exchangeMap.ContainsKey(l.StockExchangeId))
                            .Select(l =>
                                    {
                                        var exchange = exchangeMap[l.StockExchangeId];
                                        var currency = currencyMap[exchange.CurrencyId];
                                        return l.ToResponse(exchange.ToResponse(currency));
                                    })
                            .ToList();

        return Result.Ok(new Page<ListingResponse>(responses, page.PageNumber, page.PageSize, page.TotalElements));
    }

    public async Task<Result<ListingResponse>> GetOne(Guid id)
    {
        var listing = await m_Repo.FindById(id);

        if (listing is null)
            return Result.NotFound<ListingResponse>($"No listing found with ID: {id}");

        var exchange = await m_ExchangeRepo.FindById(listing.StockExchangeId);

        if (exchange is null)
            return Result.NotFound<ListingResponse>($"No exchange found with ID: {listing.StockExchangeId}");

        var currency = await m_CurrencyClient.GetCurrencyById(exchange.CurrencyId);

        if (currency is null)
            return Result.NotFound<ListingResponse>($"No currency found with ID: {exchange.CurrencyId}");

        return Result.Ok(listing.ToResponse(exchange.ToResponse(currency)));
    }

    public async Task<Result<ListingResponse>> Create(ListingCreateRequest request)
    {
        var exchange = await m_ExchangeRepo.FindById(request.Exchange);

        if (exchange == null)
            return Result.NotFound<ListingResponse>($"Exchange not found: {request.Exchange}");

        var currency = await m_CurrencyClient.GetCurrencyById(exchange.CurrencyId);

        if (currency == null)
            return Result.NotFound<ListingResponse>($"Currency not found: {exchange.CurrencyId}");

        var entity  = request.ToListing();
        var created = await m_Repo.Add(entity);

        return Result.Ok(created.ToResponse(exchange.ToResponse(currency)));
    }
}
