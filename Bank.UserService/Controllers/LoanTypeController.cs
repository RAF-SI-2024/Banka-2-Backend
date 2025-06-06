using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.Permissions.Core;
using Bank.UserService.Services;

using Microsoft.AspNetCore.Mvc;

namespace Bank.UserService.Controllers;

[ApiController]
public class LoanTypeController(ILoanTypeService loanTypeService) : ControllerBase
{
    private readonly ILoanTypeService m_LoanTypeService = loanTypeService;

    [Authorize]
    [HttpGet(Endpoints.LoanType.GetAll)]
    public async Task<ActionResult<Page<LoanTypeResponse>>> GetAll([FromQuery] Pageable pageable)
    {
        var result = await m_LoanTypeService.GetAll(pageable);

        return result.ActionResult;
    }

    [Authorize]
    [HttpGet(Endpoints.LoanType.GetOne)]
    public async Task<ActionResult<LoanTypeResponse>> GetOne([FromRoute] Guid id)
    {
        var result = await m_LoanTypeService.GetOne(id);

        return result.ActionResult;
    }

    [Authorize(Permission.Admin, Permission.Employee)]
    [HttpPost(Endpoints.LoanType.Create)]
    public async Task<ActionResult<LoanTypeResponse>> Create([FromBody] LoanTypeCreateRequest createRequest)
    {
        var result = await m_LoanTypeService.Create(createRequest);

        return result.ActionResult;
    }

    [Authorize(Permission.Admin, Permission.Employee)]
    [HttpPut(Endpoints.LoanType.Update)]
    public async Task<ActionResult<LoanTypeResponse>> Update([FromBody] LoanTypeUpdateRequest request, [FromRoute] Guid id)
    {
        var result = await m_LoanTypeService.Update(request, id);

        return Ok(result.Value);
    }
}
