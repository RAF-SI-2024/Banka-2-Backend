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
public class ClientController(IClientService clientService) : ControllerBase
{
    private readonly IClientService m_ClientService = clientService;

    [HttpGet(Endpoints.Client.GetAll)]
    [Authorize(Roles = $"{Role.Admin}, {Role.Employee}")]
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

    [HttpPost(Endpoints.Client.Create)]
    [Authorize(Roles = $"{Role.Admin}, {Role.Employee}")]
    public async Task<ActionResult<ClientResponse>> Create([FromBody] ClientCreateRequest clientCreateRequest)
    {
        var result = await m_ClientService.Create(clientCreateRequest);

        return result.ActionResult;
    }

    [HttpPut(Endpoints.Client.Update)]
    [Authorize(Roles = $"{Role.Admin}, {Role.Employee}")]
    public async Task<ActionResult<ClientResponse>> Update([FromBody] ClientUpdateRequest clientUpdateRequest, [FromRoute] Guid id)
    {
        var result = await m_ClientService.Update(clientUpdateRequest, id);

        return result.ActionResult;
    }
}
