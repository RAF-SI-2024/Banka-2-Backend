using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.ExchangeService.Services;

using Microsoft.AspNetCore.Mvc;

namespace Bank.ExchangeService.Controllers;

[ApiController]
public class StockExchangeController : ControllerBase
{
    private readonly IStockExchangeService m_Service;

    public StockExchangeController(IStockExchangeService service)
    {
        m_Service = service;
    }

    [HttpGet(Endpoints.StockExchange.GetAll)]
    public async Task<ActionResult<Page<StockExchangeResponse>>> GetAll([FromQuery] StockExchangeFilterQuery filter, [FromQuery] Pageable pageable)
    {
        var result = await m_Service.GetAll(filter, pageable);
        return result.ActionResult;
    }

    [HttpGet(Endpoints.StockExchange.GetOne)]
    public async Task<ActionResult<StockExchangeResponse>> GetOne([FromRoute] Guid id)
    {
        var result = await m_Service.GetOne(id);
        return result.ActionResult;
    }

    [HttpPost(Endpoints.StockExchange.Create)]
    public async Task<ActionResult<StockExchangeResponse>> Create([FromBody] ExchangeCreateRequest request)
    {
        var result = await m_Service.Create(request);
        return result.ActionResult;
    }
}
