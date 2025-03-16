using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.UserService.Configurations;
using Bank.UserService.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Bank.Application.Requests;
using Bank.Application.Responses;

namespace Bank.UserService.Controllers;

using Role = Configuration.Policy.Role;

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

    [HttpPost(Endpoints.LoanType.Create)]
    [Authorize(Roles = $"{Role.Admin}, {Role.Employee}")]
    public async Task<ActionResult<LoanTypeResponse>> Create([FromBody] LoanTypeRequest request)
    {
        var result = await m_LoanTypeService.Create(request);

        return result.ActionResult;
    }

    [HttpPut(Endpoints.LoanType.Update)]
    [Authorize(Roles = $"{Role.Admin}, {Role.Employee}")]
    public async Task<ActionResult<LoanTypeResponse>> Update([FromBody] LoanTypeRequest request, [FromRoute] Guid id)
    {
        var result = await m_LoanTypeService.Update(request, id);

        return Ok(result.Value);
    }
}
