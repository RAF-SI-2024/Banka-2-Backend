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
public class LoanController(ILoanService loanService) : ControllerBase
{
    private readonly ILoanService m_LoanService = loanService;

    [Authorize]
    [HttpGet(Endpoints.Loan.GetAll)]
    public async Task<ActionResult<Page<LoanResponse>>> GetAll([FromQuery] LoanFilterQuery loanFilterQuery, [FromQuery] Pageable pageable)
    {
        var result = await m_LoanService.GetAll(loanFilterQuery, pageable);

        return result.ActionResult;
    }

    [Authorize]
    [HttpGet(Endpoints.Loan.GetOne)]
    public async Task<ActionResult<LoanResponse>> GetOne([FromRoute] Guid id)
    {
        var result = await m_LoanService.GetOne(id);

        return result.ActionResult;
    }

    [Authorize]
    [HttpGet(Endpoints.Loan.GetAllForClient)]
    public async Task<ActionResult<Page<LoanResponse>>> GetByClient([FromRoute] Guid clientId, [FromQuery] Pageable pageable)
    {
        var result = await m_LoanService.GetByClient(clientId, pageable);

        return result.ActionResult;
    }

    [Authorize]
    [HttpPost(Endpoints.Loan.Create)]
    public async Task<ActionResult<LoanResponse>> Create([FromBody] LoanCreateRequest loanCreateRequest)
    {
        var result = await m_LoanService.Create(loanCreateRequest);

        return result.ActionResult;
    }

    [Authorize(Permission.Admin, Permission.Employee)]
    [HttpPut(Endpoints.Loan.Update)]
    public async Task<ActionResult<LoanResponse>> Update([FromBody] LoanUpdateRequest loanRequest, [FromRoute] Guid id)
    {
        var result = await m_LoanService.Update(loanRequest, id);

        return result.ActionResult;
    }
}
