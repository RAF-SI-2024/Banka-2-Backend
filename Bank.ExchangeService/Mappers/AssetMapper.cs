using Bank.Application.Responses;
using Bank.ExchangeService.Models;

namespace Bank.ExchangeService.Mappers;

public static class AssetMapper
{
    public static AssetResponse ToResponse(this Asset asset, UserResponse actuary)
    {
        return new AssetResponse
               {
                   Id      = asset.Id,
                   Actuary = actuary,
                   // TODO    Security = asset.Security!.ToResponse();
                   Quantity     = asset.Quantity,
                   AveragePrice = asset.AveragePrice,
                   CreatedAt    = asset.CreatedAt,
                   ModifiedAt   = asset.ModifiedAt
               };
    }
}
