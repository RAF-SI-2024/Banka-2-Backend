using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.ExchangeService.Services;

using Microsoft.AspNetCore.Mvc;

namespace Bank.ExchangeService.Controllers;

[ApiController]
public class OptionController(IOptionService optionService) : ControllerBase
{
    private readonly IOptionService m_OptionService = optionService;

    [HttpGet(Endpoints.Option.GetAll)]
    public async Task<ActionResult<Page<OptionSimpleResponse>>> GetAll([FromQuery] QuoteFilterQuery filter, [FromQuery] Pageable pageable)
    {
        var result = await m_OptionService.GetAll(filter, pageable);
        return result.ActionResult;
    }

    [HttpGet(Endpoints.Option.GetOneDaily)]
    public async Task<ActionResult<OptionResponse>> GetOneDaily([FromRoute] Guid id, [FromQuery] QuoteFilterIntervalQuery filter)
    {
        var result = await m_OptionService.GetOneDaily(id, filter);
        return result.ActionResult;
    }

    [HttpGet(Endpoints.Option.GetOne)]
    public async Task<ActionResult<OptionResponse>> GetOne([FromRoute] Guid id, [FromQuery] QuoteFilterIntervalQuery filter)
    {
        var result = await m_OptionService.GetOne(id, filter);
        return result.ActionResult;
    }
}
