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
public class EmployeeController(IEmployeeService employeeService) : ControllerBase
{
    private readonly IEmployeeService m_EmployeeService = employeeService;

    [HttpGet(Endpoints.Employee.GetAll)]
    [Authorize(Roles = $"{Role.Admin}, {Role.Employee}")]
    public async Task<ActionResult<Page<EmployeeResponse>>> GetAll([FromQuery] UserFilterQuery userFilterQuery, [FromQuery] Pageable pageable)
    {
        var result = await m_EmployeeService.GetAll(userFilterQuery, pageable);

        return result.ActionResult;
    }

    [HttpGet(Endpoints.Employee.GetOne)]
    [Authorize(Roles = $"{Role.Admin}, {Role.Employee}")]
    public async Task<ActionResult<EmployeeResponse>> GetOne([FromRoute] Guid id)
    {
        var result = await m_EmployeeService.GetOne(id);

        return result.ActionResult;
    }

    [HttpPost(Endpoints.Employee.Create)]
    [Authorize(Roles = $"{Role.Admin}, {Role.Employee}")]
    public async Task<ActionResult<EmployeeResponse>> Create([FromBody] EmployeeCreateRequest employeeCreateRequest)
    {
        var result = await m_EmployeeService.Create(employeeCreateRequest);

        return result.ActionResult;
    }

    [HttpPut(Endpoints.Employee.Update)]
    [Authorize(Roles = $"{Role.Admin}, {Role.Employee}")]
    public async Task<ActionResult<EmployeeResponse>> Update([FromBody] EmployeeUpdateRequest employeeUpdateRequest, [FromRoute] Guid id)
    {
        var result = await m_EmployeeService.Update(employeeUpdateRequest, id);

        return result.ActionResult;
    }
}
