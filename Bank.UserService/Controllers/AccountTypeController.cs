using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.Permissions.Core;
using Bank.UserService.Services;

using Microsoft.AspNetCore.Mvc;

namespace Bank.UserService.Controllers;

[ApiController]
public class AccountTypeController(IAccountTypeService accountTypeService) : ControllerBase
{
    private readonly IAccountTypeService m_AccountTypeService = accountTypeService;

    [Authorize(Permission.Admin, Permission.Employee)]
    [HttpGet(Endpoints.AccountType.GetAll)]
    public async Task<ActionResult<Page<AccountTypeResponse>>> GetAll([FromQuery] AccountTypeFilterQuery accountTypeFilterQuery, [FromQuery] Pageable pageable)
    {
        var result = await m_AccountTypeService.GetAll(accountTypeFilterQuery, pageable);

        return result.ActionResult;
    }

    [Authorize(Permission.Admin, Permission.Employee)]
    [HttpGet(Endpoints.AccountType.GetOne)]
    public async Task<ActionResult<AccountTypeResponse>> GetOne([FromQuery] Guid id)
    {
        var result = await m_AccountTypeService.GetOne(id);

        return result.ActionResult;
    }
}
