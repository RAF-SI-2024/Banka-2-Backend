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
public class EmployeeController(IEmployeeService employeeService) : ControllerBase
{
    private readonly IEmployeeService m_EmployeeService = employeeService;

    [Authorize(Permission.Admin, Permission.Employee)]
    [HttpGet(Endpoints.Employee.GetAll)]
    public async Task<ActionResult<Page<EmployeeResponse>>> GetAll([FromQuery] UserFilterQuery userFilterQuery, [FromQuery] Pageable pageable)
    {
        var result = await m_EmployeeService.GetAll(userFilterQuery, pageable);

        return result.ActionResult;
    }

    [Authorize(Permission.Admin, Permission.Employee)]
    [HttpGet(Endpoints.Employee.GetOne)]
    public async Task<ActionResult<EmployeeResponse>> GetOne([FromRoute] Guid id)
    {
        var result = await m_EmployeeService.GetOne(id);

        return result.ActionResult;
    }

    [Authorize(Permission.Admin, Permission.Employee)]
    [HttpPost(Endpoints.Employee.Create)]
    public async Task<ActionResult<EmployeeResponse>> Create([FromBody] EmployeeCreateRequest employeeCreateRequest)
    {
        var result = await m_EmployeeService.Create(employeeCreateRequest);

        return result.ActionResult;
    }

    [Authorize(Permission.Admin, Permission.Employee)]
    [HttpPut(Endpoints.Employee.Update)]
    public async Task<ActionResult<EmployeeResponse>> Update([FromBody] EmployeeUpdateRequest employeeUpdateRequest, [FromRoute] Guid id)
    {
        var result = await m_EmployeeService.Update(employeeUpdateRequest, id);

        return result.ActionResult;
    }
}
