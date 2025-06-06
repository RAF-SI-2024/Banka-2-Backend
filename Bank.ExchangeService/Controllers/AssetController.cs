using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.ExchangeService.Services;
using Bank.Permissions.Core;

using Microsoft.AspNetCore.Mvc;

namespace Bank.ExchangeService.Controllers;

[ApiController]
public class AssetController(IAssetService assetService) : ControllerBase
{
    private readonly IAssetService m_AssetService = assetService;

    [Authorize]
    [HttpGet(Endpoints.Asset.GetAll)]
    public async Task<ActionResult<Page<AssetResponse>>> GetAll([FromQuery] AssetFilterQuery filter, [FromQuery] Pageable pageable)
    {
        var result = await m_AssetService.GetAll(filter, pageable);

        return result.ActionResult;
    }

    [Authorize]
    [HttpGet(Endpoints.Asset.GetAllForActuary)]
    public async Task<ActionResult<Page<AssetResponse>>> GetAllForActuary([FromRoute] Guid id, [FromQuery] Pageable pageable)
    {
        var result = await m_AssetService.GetAllByActuaryId(id, pageable);

        return result.ActionResult;
    }

    [Authorize]
    [HttpGet(Endpoints.Asset.GetOne)]
    public async Task<ActionResult<AssetResponse>> GetOne([FromRoute] Guid id)
    {
        var result = await m_AssetService.GetOne(id);

        return result.ActionResult;
    }
}
