using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Configurations;
using Bank.UserService.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.UserService.Controllers;

using Role = Configuration.Policy.Role;

[ApiController]
public class InstallmentController(IInstallmentService installmentService) : ControllerBase
{
    private readonly IInstallmentService m_InstallmentService = installmentService;

    [Authorize]
    [HttpGet(Endpoints.Installment.GetAll)]
    public async Task<ActionResult<Page<InstallmentResponse>>> GetAllByLoan([FromQuery] Guid loanId, [FromQuery] Pageable pageable)
    {
        var result = await m_InstallmentService.GetAllByLoanId(loanId, pageable);

        return result.ActionResult;
    }

    [Authorize]
    [HttpGet(Endpoints.Installment.GetOne)]
    public async Task<ActionResult<InstallmentResponse>> GetOne([FromRoute] Guid id)
    {
        var result = await m_InstallmentService.GetOne(id);

        return result.ActionResult;
    }

    [HttpPost(Endpoints.Installment.Create)]
    [Authorize(Roles = $"{Role.Admin}, {Role.Employee}")]
    public async Task<ActionResult<InstallmentResponse>> Create([FromBody] InstallmentRequest request)
    {
        var result = await m_InstallmentService.Create(request);

        return result.ActionResult;
    }

    [HttpPut(Endpoints.Installment.Update)]
    [Authorize(Roles = $"{Role.Admin}, {Role.Employee}")]
    public async Task<ActionResult<InstallmentResponse>> Update([FromBody] InstallmentRequest request)
    {
        var result = await m_InstallmentService.Update(request);

        return result.ActionResult;
    }
}
