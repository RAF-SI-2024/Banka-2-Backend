using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.ExchangeService.Services;

using Microsoft.AspNetCore.Mvc;

namespace Bank.ExchangeService.Controllers;

[ApiController]
public class ListingController : ControllerBase
{
    private readonly IListingService m_Service;

    public ListingController(IListingService service)
    {
        m_Service = service;
    }

    [HttpGet(Endpoints.Listing.GetAll)]
    public async Task<ActionResult<Page<ListingResponse>>> GetAll([FromQuery] ListingFilterQuery filter, [FromQuery] Pageable pageable)
    {
        var result = await m_Service.GetAll(filter, pageable);
        return result.ActionResult;
    }

    [HttpGet(Endpoints.Listing.GetOne)]
    public async Task<ActionResult<ListingResponse>> GetOne([FromRoute] Guid id)
    {
        var result = await m_Service.GetOne(id);
        return result.ActionResult;
    }

    [HttpPost(Endpoints.Listing.Create)]
    public async Task<ActionResult<ListingResponse>> Create([FromBody] ListingCreateRequest request)
    {
        var result = await m_Service.Create(request);
        return result.ActionResult;
    }
}
