using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.Permissions.Core;
using Bank.UserService.Services;

using Microsoft.AspNetCore.Mvc;

namespace Bank.UserService.Controllers;

[ApiController]
public class TransactionTemplateController(ITransactionTemplateService transactionTemplateService) : ControllerBase
{
    private readonly ITransactionTemplateService m_TransactionTemplateService = transactionTemplateService;

    [Authorize]
    [HttpGet(Endpoints.TransactionTemplate.GetAll)]
    public async Task<ActionResult<Page<TransactionTemplateResponse>>> GetAll([FromQuery] Pageable pageable)
    {
        var result = await m_TransactionTemplateService.GetAll(pageable);

        return result.ActionResult;
    }

    [Authorize]
    [HttpGet(Endpoints.TransactionTemplate.GetOne)]
    public async Task<ActionResult<TransactionTemplateResponse>> GetOne([FromRoute] Guid id)
    {
        var result = await m_TransactionTemplateService.GetOne(id);

        return result.ActionResult;
    }

    [Authorize(Permission.Client)]
    [HttpPost(Endpoints.TransactionTemplate.Create)]
    public async Task<ActionResult<TransactionTemplateResponse>> Create([FromBody] TransactionTemplateCreateRequest transactionTemplateCreateRequest)
    {
        var result = await m_TransactionTemplateService.Create(transactionTemplateCreateRequest);

        return result.ActionResult;
    }

    [Authorize(Permission.Client)]
    [HttpPut(Endpoints.TransactionTemplate.Update)]
    public async Task<ActionResult<TransactionTemplateResponse>> Update([FromBody] TransactionTemplateUpdateRequest transactionTemplateUpdateRequest, [FromRoute] Guid id)
    {
        var result = await m_TransactionTemplateService.Update(transactionTemplateUpdateRequest, id);

        return result.ActionResult;
    }
}
