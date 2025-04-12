using Bank.Application.Responses;
using Bank.ExchangeService.Model;

namespace Bank.ExchangeService.Mappers;

public static class ListingHistoricalMapper
{
    public static ListingHistoricalResponse ToResponse(this ListingHistorical entity, ListingResponse listing)
    {
        return new ListingHistoricalResponse
               {
                   Id           = entity.Id,
                   Listing      = listing,
                   ClosingPrice = entity.ClosingPrice,
                   HighPrice    = entity.HighPrice,
                   LowPrice     = entity.LowPrice,
                   PriceChange  = entity.PriceChange,
                   Volume       = entity.Volume,
                   CreatedAt    = entity.CreatedAt,
                   ModifiedAt   = entity.ModifiedAt
               };
    }

    public static ListingHistorical ToListingHistorical(this ListingHistoricalCreateRequest request)
    {
        return new ListingHistorical
               {
                   Id           = Guid.NewGuid(),
                   ListingId    = request.Listing,
                   ClosingPrice = request.ClosingPrice,
                   HighPrice    = request.HighPrice,
                   LowPrice     = request.LowPrice,
                   PriceChange  = request.HighPrice - request.LowPrice,
                   Volume       = request.Volume,
                   CreatedAt    = DateTime.UtcNow,
                   ModifiedAt   = DateTime.UtcNow
               };
    }
}
