using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.Permissions.Core;
using Bank.UserService.Services;

using Microsoft.AspNetCore.Mvc;

namespace Bank.UserService.Controllers;

[ApiController]
public class InstallmentController(IInstallmentService installmentService) : ControllerBase
{
    private readonly IInstallmentService m_InstallmentService = installmentService;

    [Authorize]
    [HttpGet(Endpoints.Installment.GetAllByLoan)]
    public async Task<ActionResult<Page<InstallmentResponse>>> GetAllByLoan([FromRoute] Guid loanId, [FromQuery] Pageable pageable)
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

    [Authorize(Permission.Admin, Permission.Employee)]
    [HttpPost(Endpoints.Installment.Create)]
    public async Task<ActionResult<InstallmentResponse>> Create([FromBody] InstallmentCreateRequest createRequest)
    {
        var result = await m_InstallmentService.Create(createRequest);

        return result.ActionResult;
    }

    [Authorize(Permission.Admin, Permission.Employee)]
    [HttpPut(Endpoints.Installment.Update)]
    public async Task<ActionResult<InstallmentResponse>> Update([FromBody] InstallmentUpdateRequest request, [FromRoute] Guid id)
    {
        var result = await m_InstallmentService.Update(request, id);

        return result.ActionResult;
    }
}
