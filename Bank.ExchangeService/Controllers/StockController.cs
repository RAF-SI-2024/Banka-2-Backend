using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.ExchangeService.Services;

using Microsoft.AspNetCore.Mvc;

namespace Bank.ExchangeService.Controllers;

[ApiController]
public class StockController(IStockService stockService) : ControllerBase
{
    private readonly IStockService m_StockService = stockService;

    [HttpGet(Endpoints.Stock.GetAll)]
    public async Task<ActionResult<Page<StockSimpleResponse>>> GetAll([FromQuery] QuoteFilterQuery filter, [FromQuery] Pageable pageable)
    {
        var result = await m_StockService.GetAll(filter, pageable);
        return result.ActionResult;
    }

    [HttpGet(Endpoints.Stock.GetOneDaily)]
    public async Task<ActionResult<StockDailyResponse>> GetOneDaily([FromRoute] Guid id, [FromQuery] QuoteFilterIntervalQuery filter)
    {
        var result = await m_StockService.GetOneDaily(id, filter);
        return result.ActionResult;
    }

    [HttpGet(Endpoints.Stock.GetOne)]
    public async Task<ActionResult<StockResponse>> GetOne([FromRoute] Guid id, [FromQuery] QuoteFilterIntervalQuery filter)
    {
        var result = await m_StockService.GetOne(id, filter);
        return result.ActionResult;
    }
}
