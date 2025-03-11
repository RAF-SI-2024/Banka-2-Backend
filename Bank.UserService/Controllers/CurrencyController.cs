﻿using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.UserService.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.UserService.Controllers;

[ApiController]
public class CurrencyController(ICurrencyService currencyService) : ControllerBase
{
    private readonly ICurrencyService m_CurrencyService = currencyService;

    [Authorize]
    [HttpGet(Endpoints.Currency.GetAll)]
    public async Task<ActionResult<Page<CurrencyResponse>>> GetAll([FromQuery] CurrencyFilterQuery currencyFilterQuery, [FromQuery] Pageable pageable)
    {
        var result = await m_CurrencyService.FindAll(currencyFilterQuery, pageable);
        return result.ActionResult;
    }

    [Authorize]
    [HttpGet(Endpoints.Currency.GetOne)]
    public async Task<ActionResult<CurrencyResponse>> GetOne([FromRoute] Guid id)
    {
        var result = await m_CurrencyService.FindById(id);
        return result.ActionResult;
    }
}
