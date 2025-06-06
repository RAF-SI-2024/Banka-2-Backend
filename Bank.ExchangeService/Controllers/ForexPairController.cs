using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.ExchangeService.Services;

using Microsoft.AspNetCore.Mvc;

namespace Bank.ExchangeService.Controllers;

[ApiController]
public class ForexPairController(IForexPairService forexPairService) : ControllerBase
{
    private readonly IForexPairService m_ForexPairService = forexPairService;

    [HttpGet(Endpoints.ForexPair.GetAll)]
    public async Task<ActionResult<Page<ForexPairSimpleResponse>>> GetAll([FromQuery] QuoteFilterQuery filter, [FromQuery] Pageable pageable)
    {
        var result = await m_ForexPairService.GetAll(filter, pageable);
        return result.ActionResult;
    }

    [HttpGet(Endpoints.ForexPair.GetOneDaily)]
    public async Task<ActionResult<ForexPairDailyResponse>> GetOneDaily([FromRoute] Guid id, [FromQuery] QuoteFilterIntervalQuery filter)
    {
        var result = await m_ForexPairService.GetOneDaily(id, filter);
        return result.ActionResult;
    }

    [HttpGet(Endpoints.ForexPair.GetOne)]
    public async Task<ActionResult<ForexPairResponse>> GetOne([FromRoute] Guid id, [FromQuery] QuoteFilterIntervalQuery filter)
    {
        var result = await m_ForexPairService.GetOne(id, filter);
        return result.ActionResult;
    }
}
