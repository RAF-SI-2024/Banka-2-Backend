using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Services;

using Microsoft.AspNetCore.Mvc;

namespace Bank.UserService.Controllers;

[ApiController]
public class ExchangeRateController(IExchangeRateService exchangeRateService) : ControllerBase
{
    private readonly IExchangeRateService m_ExchangeRateService = exchangeRateService;

    [HttpGet(Endpoints.ExchangeRate.GetAll)]
    public async Task<ActionResult<List<ExchangeRateResponse>>> GetAll([FromQuery] ExchangeRateFilterQuery exchangeRateFilterQuery)
    {
        var result = await m_ExchangeRateService.GetAll(exchangeRateFilterQuery);

        return result.ActionResult;
    }

    [HttpGet(Endpoints.ExchangeRate.GetOne)]
    public async Task<ActionResult<ExchangeRateResponse>> GetOne([FromRoute] Guid id)
    {
        var result = await m_ExchangeRateService.GetById(id);

        return result.ActionResult;
    }

    [HttpGet(Endpoints.ExchangeRate.GetByCurrencies)]
    public async Task<ActionResult<ExchangeRateResponse>> GetByCurrencies([FromBody] ExchangeRateBetweenRequest exchangeRateBetweenRequest)
    {
        var result = await m_ExchangeRateService.GetByCurrencies(exchangeRateBetweenRequest);

        return result.ActionResult;
    }

    [HttpPut(Endpoints.ExchangeRate.Exchange)]
    public async Task<ActionResult<ExchangeRateResponse>> MakeExchange([FromBody] ExchangeRateMakeExchangeRequest exchangeRateMakeExchangeRequest)
    {
        var result = await m_ExchangeRateService.MakeExchange(exchangeRateMakeExchangeRequest);

        return result.ActionResult;
    }

    [HttpPut(Endpoints.ExchangeRate.Update)]
    public async Task<ActionResult<ExchangeRateResponse>> Update([FromRoute] Guid id, [FromBody] ExchangeRateUpdateRequest exchangeRateUpdateRequest)
    {
        var result = await m_ExchangeRateService.Update(exchangeRateUpdateRequest, id);

        return result.ActionResult;
    }
}
