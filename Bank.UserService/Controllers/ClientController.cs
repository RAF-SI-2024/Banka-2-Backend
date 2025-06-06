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
public class ClientController(IClientService clientService) : ControllerBase
{
    private readonly IClientService m_ClientService = clientService;

    [Authorize(Permission.Admin, Permission.Employee)]
    [HttpGet(Endpoints.Client.GetAll)]
    public async Task<ActionResult<Page<ClientResponse>>> GetAll([FromQuery] UserFilterQuery filterQuery, [FromQuery] Pageable pageable)
    {
        var result = await m_ClientService.FindAll(filterQuery, pageable);

        return result.ActionResult;
    }

    [Authorize]
    [HttpGet(Endpoints.Client.GetOne)]
    public async Task<ActionResult<ClientResponse>> GetOne([FromRoute] Guid id)
    {
        var result = await m_ClientService.GetOne(id);

        return result.ActionResult;
    }

    [Authorize(Permission.Admin, Permission.Employee)]
    [HttpPost(Endpoints.Client.Create)]
    public async Task<ActionResult<ClientResponse>> Create([FromBody] ClientCreateRequest clientCreateRequest)
    {
        var result = await m_ClientService.Create(clientCreateRequest);

        return result.ActionResult;
    }

    [Authorize(Permission.Admin, Permission.Employee)]
    [HttpPut(Endpoints.Client.Update)]
    public async Task<ActionResult<ClientResponse>> Update([FromBody] ClientUpdateRequest clientUpdateRequest, [FromRoute] Guid id)
    {
        var result = await m_ClientService.Update(clientUpdateRequest, id);

        return result.ActionResult;
    }
}
