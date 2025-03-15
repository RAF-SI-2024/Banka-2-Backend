using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Services;

using Microsoft.AspNetCore.Mvc;

namespace Bank.UserService.Controllers;

[ApiController]
public class ExchangeController(IExchangeService exchangeService) : ControllerBase
{
    private readonly IExchangeService m_ExchangeService = exchangeService;

    [HttpGet(Endpoints.Exchange.GetAll)]
    public async Task<ActionResult<List<ExchangeResponse>>> GetAll([FromQuery] ExchangeFilterQuery exchangeFilterQuery)
    {
        var result = await m_ExchangeService.GetAll(exchangeFilterQuery);

        return result.ActionResult;
    }

    [HttpGet(Endpoints.Exchange.GetOne)]
    public async Task<ActionResult<ExchangeResponse>> GetOne([FromRoute] Guid id)
    {
        var result = await m_ExchangeService.GetById(id);

        return result.ActionResult;
    }

    [HttpGet(Endpoints.Exchange.GetByCurrencies)]
    public async Task<ActionResult<ExchangeResponse>> GetByCurrencies([FromBody] ExchangeBetweenRequest exchangeBetweenRequest, [FromQuery] ExchangeFilterQuery exchangeFilterQuery)
    {
        var result = await m_ExchangeService.GetByCurrencies(exchangeBetweenRequest, exchangeFilterQuery);

        return result.ActionResult;
    }

    [HttpPost(Endpoints.Exchange.MakeExchange)]
    public async Task<ActionResult<ExchangeResponse>> MakeExchange([FromBody]  ExchangeMakeExchangeRequest exchangeMakeExchangeRequest,
                                                                   [FromQuery] ExchangeFilterQuery         exchangeFilterQuery)
    {
        var result = await m_ExchangeService.MakeExchange(exchangeMakeExchangeRequest, exchangeFilterQuery);

        return result.ActionResult;
    }

    [HttpPut(Endpoints.Exchange.Update)]
    public async Task<ActionResult<ExchangeResponse>> Update([FromRoute] Guid id, [FromBody] ExchangeUpdateRequest exchangeUpdateRequest)
    {
        var result = await m_ExchangeService.Update(exchangeUpdateRequest, id);

        return result.ActionResult;
    }
}
