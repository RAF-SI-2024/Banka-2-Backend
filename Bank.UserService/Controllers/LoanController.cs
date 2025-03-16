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
public class LoanController : ControllerBase
{
    private readonly ILoanService m_LoanService;

    public LoanController(ILoanService loanService)
    {
        m_LoanService = loanService;
    }

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
    [HttpGet(Endpoints.Loan.GetByAccount)]
    public async Task<ActionResult<Page<LoanResponse>>> GetByAccount([FromRoute] Guid accountId, [FromQuery] Pageable pageable)
    {
        var loanFilterQuery = new LoanFilterQuery { AccountNumber = accountId.ToString() };
        var result          = await m_LoanService.GetAll(loanFilterQuery, pageable);

        return result.ActionResult;
    }

    [HttpPost(Endpoints.Loan.Create)]
    [Authorize(Roles = $"{Role.Admin}, {Role.Employee}")]
    public async Task<ActionResult<LoanResponse>> Create([FromBody] LoanRequest loanRequest)
    {
        var result = await m_LoanService.Create(loanRequest);

        return result.ActionResult;
    }

    [HttpPut(Endpoints.Loan.Update)]
    [Authorize(Roles = $"{Role.Admin}, {Role.Employee}")]
    public async Task<ActionResult<LoanResponse>> Update([FromBody] LoanRequest loanRequest, [FromRoute] Guid id)
    {
        var result = await m_LoanService.Update(loanRequest, id);

        return result.ActionResult;
    }

    [Authorize]
    [HttpGet(Endpoints.Loan.GetInstallments)]
    public async Task<ActionResult<Page<InstallmentResponse>>> GetInstallments([FromRoute] Guid loanId, [FromQuery] Pageable pageable)
    {
        var result = await m_LoanService.GetAllInstallments(loanId, pageable);

        return result.ActionResult;
    }
}
