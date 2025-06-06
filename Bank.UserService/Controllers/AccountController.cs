using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.Permissions.Core;
using Bank.UserService.Services;

using Microsoft.AspNetCore.Mvc;

namespace Bank.UserService.Controllers;

[ApiController]
public class AccountController(IAccountService accountService) : ControllerBase
{
    private readonly IAccountService m_AccountService = accountService;

    [Authorize]
    [HttpGet(Endpoints.Account.GetAll)]
    public async Task<ActionResult<Page<AccountResponse>>> GetAll([FromQuery] AccountFilterQuery accountFilterQuery, [FromQuery] Pageable pageable)
    {
        var result = await m_AccountService.GetAll(accountFilterQuery, pageable);

        return result.ActionResult;
    }

    [Authorize]
    [HttpGet(Endpoints.Account.GetAllForClient)]
    public async Task<ActionResult<Page<AccountResponse>>> GetAllAccounts([FromRoute] Guid clientId, [FromQuery] AccountFilterQuery filter, [FromQuery] Pageable pageable)
    {
        var result = await m_AccountService.GetAllForClient(clientId, filter, pageable);

        return result.ActionResult;
    }

    [Authorize]
    [HttpGet(Endpoints.Account.GetOne)]
    public async Task<ActionResult<AccountResponse>> GetOne([FromRoute] Guid id)
    {
        var result = await m_AccountService.GetOne(id);

        return result.ActionResult;
    }

    [Authorize]
    [HttpGet(Endpoints.Account.GetOneByNumber)]
    public async Task<ActionResult<AccountResponse>> GetOneByAccountNumber([FromRoute] string number)
    {
        var result = await m_AccountService.GetOneByAccountNumber(number);

        return result.ActionResult;
    }

    [Authorize(Permission.Admin, Permission.Employee)]
    [HttpPost(Endpoints.Account.Create)]
    public async Task<ActionResult<AccountResponse>> Create([FromBody] AccountCreateRequest accountCreateRequest)
    {
        var result = await m_AccountService.Create(accountCreateRequest);

        return result.ActionResult;
    }

    [Authorize]
    [HttpPut(Endpoints.Account.UpdateClient)]
    public async Task<ActionResult<AccountResponse>> Update([FromBody] AccountUpdateClientRequest accountUpdateClientRequest, [FromRoute] Guid id)
    {
        var result = await m_AccountService.Update(accountUpdateClientRequest, id);

        return result.ActionResult;
    }

    [Authorize(Permission.Admin, Permission.Employee)]
    [HttpPut(Endpoints.Account.UpdateEmployee)]
    public async Task<ActionResult<AccountResponse>> Update([FromBody] AccountUpdateEmployeeRequest accountUpdateEmployeeRequest, [FromRoute] Guid id)
    {
        var result = await m_AccountService.Update(accountUpdateEmployeeRequest, id);

        return result.ActionResult;
    }
}
