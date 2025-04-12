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
public class TransactionController(ITransactionService transactionService) : ControllerBase
{
    private readonly ITransactionService m_TransactionService = transactionService;

    [Authorize]
    [HttpGet(Endpoints.Transaction.GetAll)]
    public async Task<ActionResult<Page<TransactionResponse>>> GetAll([FromQuery] TransactionFilterQuery transactionFilterQuery, [FromQuery] Pageable pageable)
    {
        var result = await m_TransactionService.GetAll(transactionFilterQuery, pageable);

        return result.ActionResult;
    }

    [Authorize]
    [HttpGet(Endpoints.Transaction.GetAllForAccount)]
    public async Task<ActionResult<Page<TransactionResponse>>> GetAllByAccountId([FromRoute] Guid     accountId, [FromQuery] TransactionFilterQuery transactionFilterQuery,
                                                                                 [FromQuery] Pageable pageable)
    {
        var result = await m_TransactionService.GetAllByAccountId(accountId, transactionFilterQuery, pageable);

        return result.ActionResult;
    }

    [Authorize]
    [HttpGet(Endpoints.Transaction.GetOne)]
    public async Task<ActionResult<TransactionResponse>> GetOne([FromRoute] Guid id)
    {
        var result = await m_TransactionService.GetOne(id);

        return result.ActionResult;
    }

    [Authorize]
    [HttpPost(Endpoints.Transaction.Create)]
    public async Task<ActionResult<TransactionCreateResponse>> Create([FromBody] TransactionCreateRequest transactionCreateRequest)
    {
        var result = await m_TransactionService.Create(transactionCreateRequest);

        return result.ActionResult;
    }

    [Authorize(Roles = $"{Role.Admin}")]
    [HttpPut(Endpoints.Transaction.Update)]
    public async Task<ActionResult<TransactionResponse>> Update([FromBody] TransactionUpdateRequest transactionUpdateRequest, [FromRoute] Guid id)
    {
        var result = await m_TransactionService.Update(transactionUpdateRequest, id);

        return result.ActionResult;
    }
}
