using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.ExchangeService.HttpClients;
using Bank.ExchangeService.Mappers;
using Bank.ExchangeService.Model;
using Bank.ExchangeService.Repositorties;

namespace Bank.ExchangeService.Services;

public interface IListingHistoricalService
{
    Task<Result<Page<ListingHistoricalResponse>>> GetAll(ListingHistoricalFilterQuery filter, Pageable pageable);

    Task<Result<ListingHistoricalResponse>> GetOne(Guid id);

    Task<Result<ListingHistoricalResponse>> Create(ListingHistoricalCreateRequest request);
}

public class ListingHistoricalService(
    IListingHistoricalRepository historicalRepository,
    IListingRepository           listingRepository,
    IStockExchangeRepository     exchangeRepository,
    ICurrencyClient              currencyClient
) : IListingHistoricalService
{
    private readonly IListingHistoricalRepository m_HistoricalRepo = historicalRepository;
    private readonly IListingRepository           m_ListingRepo    = listingRepository;
    private readonly IStockExchangeRepository     m_ExchangeRepo   = exchangeRepository;
    private readonly ICurrencyClient              m_CurrencyClient = currencyClient;

    public async Task<Result<Page<ListingHistoricalResponse>>> GetAll(ListingHistoricalFilterQuery filter, Pageable pageable)
    {
        var page = await m_HistoricalRepo.FindAll(filter, pageable);

        var listingIds = page.Items.Select(h => h.ListingId)
                             .Distinct();

        var listingMap  = new Dictionary<Guid, Listing>();
        var exchangeMap = new Dictionary<Guid, StockExchange>();
        var currencyMap = new Dictionary<Guid, CurrencyResponse>();

        foreach (var listingId in listingIds)
        {
            var listing = await m_ListingRepo.FindById(listingId);

            if (listing != null)
            {
                listingMap[listingId] = listing;

                var exchangeId = listing.StockExchangeId;

                if (!exchangeMap.ContainsKey(exchangeId))
                {
                    var exchange = await m_ExchangeRepo.FindById(exchangeId);

                    if (exchange != null)
                    {
                        exchangeMap[exchangeId] = exchange;

                        var currencyId = exchange.CurrencyId;

                        if (!currencyMap.ContainsKey(currencyId))
                        {
                            var currency = await m_CurrencyClient.GetCurrencyById(currencyId);

                            if (currency != null)
                            {
                                currencyMap[currencyId] = currency;
                            }
                        }
                    }
                }
            }
        }

        var responses = page.Items.Where(h => listingMap.ContainsKey(h.ListingId))
                            .Select(h =>
                                    {
                                        var listing  = listingMap[h.ListingId];
                                        var exchange = exchangeMap[listing.StockExchangeId];
                                        var currency = currencyMap[exchange.CurrencyId];
                                        return h.ToResponse(listing.ToResponse(exchange.ToResponse(currency)));
                                    })
                            .ToList();

        return Result.Ok(new Page<ListingHistoricalResponse>(responses, page.PageNumber, page.PageSize, page.TotalElements));
    }

    public async Task<Result<ListingHistoricalResponse>> GetOne(Guid id)
    {
        var historical = await m_HistoricalRepo.FindById(id);

        if (historical is null)
            return Result.NotFound<ListingHistoricalResponse>($"No historical record found with ID: {id}");

        var listing = await m_ListingRepo.FindById(historical.ListingId);

        if (listing is null)
            return Result.NotFound<ListingHistoricalResponse>($"No listing found with ID: {historical.ListingId}");

        var exchange = await m_ExchangeRepo.FindById(listing.StockExchangeId);

        if (exchange is null)
            return Result.NotFound<ListingHistoricalResponse>($"No exchange found with ID: {listing.StockExchangeId}");

        var currency = await m_CurrencyClient.GetCurrencyById(exchange.CurrencyId);

        if (currency is null)
            return Result.NotFound<ListingHistoricalResponse>($"No currency found with ID: {exchange.CurrencyId}");

        return Result.Ok(historical.ToResponse(listing.ToResponse(exchange.ToResponse(currency))));
    }

    public async Task<Result<ListingHistoricalResponse>> Create(ListingHistoricalCreateRequest request)
    {
        var listing = await m_ListingRepo.FindById(request.Listing);

        if (listing is null)
            return Result.NotFound<ListingHistoricalResponse>($"Listing not found: {request.Listing}");

        var exchange = await m_ExchangeRepo.FindById(listing.StockExchangeId);

        if (exchange is null)
            return Result.NotFound<ListingHistoricalResponse>($"Exchange not found: {listing.StockExchangeId}");

        var currency = await m_CurrencyClient.GetCurrencyById(exchange.CurrencyId);

        if (currency is null)
            return Result.NotFound<ListingHistoricalResponse>($"Currency not found: {exchange.CurrencyId}");

        var entity  = request.ToListingHistorical();
        var created = await m_HistoricalRepo.Add(entity);

        return Result.Ok(created.ToResponse(listing.ToResponse(exchange.ToResponse(currency))));
    }
}
