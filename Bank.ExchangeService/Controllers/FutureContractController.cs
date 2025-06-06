using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.ExchangeService.Services;

using Microsoft.AspNetCore.Mvc;

namespace Bank.ExchangeService.Controllers;

[ApiController]
public class FutureContractController(IFutureContractService futureContractService) : ControllerBase
{
    private readonly IFutureContractService m_FutureContractService = futureContractService;

    [HttpGet(Endpoints.FutureContract.GetAll)]
    public async Task<ActionResult<Page<FutureContractSimpleResponse>>> GetAll([FromQuery] QuoteFilterQuery filter, [FromQuery] Pageable pageable)
    {
        var result = await m_FutureContractService.GetAll(filter, pageable);
        return result.ActionResult;
    }

    [HttpGet(Endpoints.FutureContract.GetOne)]
    public async Task<ActionResult<FutureContractResponse>> GetOne([FromRoute] Guid id, [FromQuery] QuoteFilterIntervalQuery filter)
    {
        var result = await m_FutureContractService.GetOne(id, filter);
        return result.ActionResult;
    }

    [HttpGet(Endpoints.FutureContract.GetOneDaily)]
    public async Task<ActionResult<FutureContractResponse>> GetOneDaily([FromRoute] Guid id, [FromQuery] QuoteFilterIntervalQuery filter)
    {
        var result = await m_FutureContractService.GetOneDaily(id, filter);
        return result.ActionResult;
    }
}
