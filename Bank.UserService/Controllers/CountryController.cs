using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.Permissions.Core;
using Bank.UserService.Services;

using Microsoft.AspNetCore.Mvc;

namespace Bank.UserService.Controllers;

[ApiController]
public class CountryController(ICountryService countryService) : ControllerBase
{
    private readonly ICountryService m_CountryService = countryService;

    [Authorize]
    [HttpGet(Endpoints.Country.GetAll)]
    public async Task<ActionResult<Page<CountryResponse>>> GetAll([FromQuery] CountryFilterQuery countryFilterQuery, [FromQuery] Pageable pageable)
    {
        var result = await m_CountryService.FindAll(countryFilterQuery, pageable);
        return result.ActionResult;
    }

    [Authorize]
    [HttpGet(Endpoints.Country.GetOne)]
    public async Task<ActionResult<CountryResponse>> GetOne([FromRoute] Guid id)
    {
        var result = await m_CountryService.FindById(id);
        return result.ActionResult;
    }
}
