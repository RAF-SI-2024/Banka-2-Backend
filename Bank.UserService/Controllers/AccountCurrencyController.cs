using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.Permissions.Core;
using Bank.UserService.Services;

using Microsoft.AspNetCore.Mvc;

namespace Bank.UserService.Controllers;

[ApiController]
public class AccountCurrencyController(IAccountCurrencyService accountCurrencyService) : ControllerBase
{
    private readonly IAccountCurrencyService m_AccountCurrencyService = accountCurrencyService;

    [Authorize]
    [HttpGet(Endpoints.AccountCurrency.GetAll)]
    public async Task<ActionResult<Page<AccountCurrencyResponse>>> GetAll([FromQuery] Pageable pageable)
    {
        var result = await m_AccountCurrencyService.GetAll(pageable);

        return result.ActionResult;
    }

    [Authorize]
    [HttpGet(Endpoints.AccountCurrency.GetOne)]
    public async Task<ActionResult<AccountCurrencyResponse>> GetOne([FromQuery] Guid id)
    {
        var result = await m_AccountCurrencyService.GetOne(id);

        return result.ActionResult;
    }

    [Authorize(Permission.Admin, Permission.Employee)]
    [HttpPost(Endpoints.AccountCurrency.Create)]
    public async Task<ActionResult<AccountCurrencyResponse>> Create([FromBody] AccountCurrencyCreateRequest accountCurrencyCreateRequest)
    {
        var result = await m_AccountCurrencyService.Create(accountCurrencyCreateRequest);

        return result.ActionResult;
    }

    [Authorize(Permission.Client)]
    [HttpPut(Endpoints.AccountCurrency.UpdateClient)]
    public async Task<ActionResult<AccountCurrencyResponse>> Update([FromBody] AccountCurrencyClientUpdateRequest accountCurrencyClientUpdateRequest, [FromRoute] Guid id)
    {
        var result = await m_AccountCurrencyService.Update(accountCurrencyClientUpdateRequest, id);

        return result.ActionResult;
    }
}
