using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Role = Bank.UserService.Configurations.Configuration.Policy.Role;

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
    [HttpGet(Endpoints.Account.GetOne)]
    public async Task<ActionResult<AccountResponse>> GetOne([FromQuery] Guid id)
    {
        var result = await m_AccountService.GetOne(id);

        return result.ActionResult;
    }

    [HttpPost(Endpoints.Account.Create)]
    [Authorize(Roles = $"{Role.Admin}, {Role.Employee}")]
    public async Task<ActionResult<AccountResponse>> Create([FromBody] AccountCreateRequest accountCreateRequest)
    {
        var result = await m_AccountService.Create(accountCreateRequest);

        return result.ActionResult;
    }

    [HttpPut(Endpoints.Account.UpdateClient)]
    [Authorize(Roles = $"{Role.Client}")]
    public async Task<ActionResult<AccountResponse>> Update([FromBody] AccountUpdateClientRequest accountUpdateClientRequest, [FromRoute] Guid id)
    {
        var result = await m_AccountService.Update(accountUpdateClientRequest, id);

        return result.ActionResult;
    }

    [HttpPut(Endpoints.Account.UpdateEmployee)]
    [Authorize(Roles = $"{Role.Admin}, {Role.Employee}")]
    public async Task<ActionResult<AccountResponse>> Update([FromBody] AccountUpdateEmployeeRequest accountUpdateEmployeeRequest, [FromRoute] Guid id)
    {
        var result = await m_AccountService.Update(accountUpdateEmployeeRequest, id);

        return result.ActionResult;
    }
}
