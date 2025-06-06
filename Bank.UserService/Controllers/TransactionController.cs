using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.Permissions.Core;
using Bank.UserService.BackgroundServices;
using Bank.UserService.Services;

using Microsoft.AspNetCore.Mvc;

namespace Bank.UserService.Controllers;

[ApiController]
public class TransactionController(ITransactionService transactionService, TransactionBackgroundService transactionBackgroundService) : ControllerBase
{
    private readonly ITransactionService          m_TransactionService           = transactionService;
    private readonly TransactionBackgroundService m_TransactionBackgroundService = transactionBackgroundService;

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

    [Authorize(Permission.Bank)]
    [HttpPut(Endpoints.Transaction.PutStatus)]
    public async Task<ActionResult<bool>> PutStatus([FromRoute] Guid id)
    {
        var result = await m_TransactionService.PutStatus(id);

        return result.ActionResult;
    }

    [Authorize]
    [HttpPost(Endpoints.Transaction.Create)]
    public async Task<ActionResult<TransactionCreateResponse>> Create([FromBody] TransactionCreateRequest transactionCreateRequest)
    {
        var result = await m_TransactionService.Create(transactionCreateRequest);

        return result.ActionResult;
    }

    [Authorize(Permission.Admin)]
    [HttpPut(Endpoints.Transaction.Update)]
    public async Task<ActionResult<TransactionResponse>> Update([FromBody] TransactionUpdateRequest transactionUpdateRequest, [FromRoute] Guid id)
    {
        var result = await m_TransactionService.Update(transactionUpdateRequest, id);

        return result.ActionResult;
    }

    [Authorize]
    [HttpGet(Endpoints.Transaction.ProcessInternal)]
    public async Task<ActionResult<Page<TransactionResponse>>> ProcessInternal()
    {
        await m_TransactionBackgroundService.ProcessInternalTransactions(m_TransactionBackgroundService);

        return Ok();
    }

    [Authorize]
    [HttpGet(Endpoints.Transaction.ProcessExternal)]
    public async Task<ActionResult<Page<TransactionResponse>>> ProcessExternal()
    {
        await m_TransactionBackgroundService.ProcessExternalTransactions(m_TransactionBackgroundService);

        return Ok();
    }
}
