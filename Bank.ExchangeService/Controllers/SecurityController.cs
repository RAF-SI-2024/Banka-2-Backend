using Bank.Application.Endpoints;
using Bank.Application.Responses;
using Bank.ExchangeService.Services;

using Microsoft.AspNetCore.Mvc;

namespace Bank.ExchangeService.Controllers;

[ApiController]
public class SecurityController(ISecurityService securityService) : ControllerBase
{
    private readonly ISecurityService m_SecurityService = securityService;

    [HttpGet(Endpoints.Security.GetOneSimple)]
    public async Task<ActionResult<SecuritySimpleResponse>> GetOneSimple([FromRoute] Guid id)
    {
        var result = await m_SecurityService.GetOneSimple(id);
        return result.ActionResult;
    }
}
