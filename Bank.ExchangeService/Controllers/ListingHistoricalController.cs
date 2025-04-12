using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.ExchangeService.Services;

using Microsoft.AspNetCore.Mvc;

namespace Bank.ExchangeService.Controllers;

[ApiController]
public class ListingHistoricalController : ControllerBase
{
    private readonly IListingHistoricalService m_Service;

    public ListingHistoricalController(IListingHistoricalService service)
    {
        m_Service = service;
    }

    [HttpGet(Endpoints.ListingHistorical.GetAll)]
    public async Task<ActionResult<Page<ListingHistoricalResponse>>> GetAll([FromQuery] ListingHistoricalFilterQuery filter, [FromQuery] Pageable pageable)
    {
        var result = await m_Service.GetAll(filter, pageable);
        return result.ActionResult;
    }

    [HttpGet(Endpoints.ListingHistorical.GetOne)]
    public async Task<ActionResult<ListingHistoricalResponse>> GetOne([FromRoute] Guid id)
    {
        var result = await m_Service.GetOne(id);
        return result.ActionResult;
    }

    [HttpPost(Endpoints.ListingHistorical.Create)]
    public async Task<ActionResult<ListingHistoricalResponse>> Create([FromBody] ListingHistoricalCreateRequest request)
    {
        var result = await m_Service.Create(request);
        return result.ActionResult;
    }
}
