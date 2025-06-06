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
public class CompanyController(ICompanyService companyService) : ControllerBase
{
    private readonly ICompanyService m_CompanyService = companyService;

    [Authorize]
    [HttpGet(Endpoints.Company.GetAll)]
    public async Task<ActionResult<Page<CompanyResponse>>> GetAll([FromQuery] CompanyFilterQuery companyFilterQuery, [FromQuery] Pageable pageable)
    {
        var result = await m_CompanyService.GetAll(companyFilterQuery, pageable);

        return result.ActionResult;
    }

    [Authorize]
    [HttpGet(Endpoints.Company.GetOne)]
    public async Task<ActionResult<CompanyResponse>> GetOne([FromRoute] Guid id)
    {
        var result = await m_CompanyService.GetOne(id);

        return result.ActionResult;
    }

    [Authorize]
    [HttpPost(Endpoints.Company.Create)]
    public async Task<ActionResult<CompanyResponse>> Create([FromBody] CompanyCreateRequest companyCreateRequest)
    {
        var result = await m_CompanyService.Create(companyCreateRequest);

        return result.ActionResult;
    }

    [Authorize]
    [HttpPut(Endpoints.Company.Update)]
    public async Task<ActionResult<CompanyResponse>> Update([FromBody] CompanyUpdateRequest companyUpdateRequest, [FromRoute] Guid id)
    {
        var result = await m_CompanyService.Update(companyUpdateRequest, id);

        return result.ActionResult;
    }
}
