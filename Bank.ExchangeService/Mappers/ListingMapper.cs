using Bank.Application.Responses;
using Bank.ExchangeService.Model;

namespace Bank.ExchangeService.Mappers;

public static class ListingMapper
{
    public static ListingResponse ToResponse(this Listing listing, StockExchangeResponse exchange)
    {
        return new ListingResponse
               {
                   Id         = listing.Id,
                   Name       = listing.Name,
                   Ticker     = listing.Ticker,
                   Exchange   = exchange,
                   CreatedAt  = listing.CreatedAt,
                   ModifiedAt = listing.ModifiedAt
               };
    }

    public static Listing ToListing(this ListingCreateRequest request)
    {
        return new Listing
               {
                   Id              = Guid.NewGuid(),
                   Name            = request.Name,
                   Ticker          = request.Ticker,
                   StockExchangeId = request.Exchange,
                   CreatedAt       = DateTime.UtcNow,
                   ModifiedAt      = DateTime.UtcNow
               };
    }
}
