using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.Permissions.Core;
using Bank.UserService.Services;

using Microsoft.AspNetCore.Mvc;

namespace Bank.UserService.Controllers;

[ApiController]
public class TransactionCodeController(ITransactionCodeService transactionCodeService) : ControllerBase
{
    private readonly ITransactionCodeService m_TransactionCodeService = transactionCodeService;

    [Authorize]
    [HttpGet(Endpoints.TransactionCode.GetAll)]
    public async Task<ActionResult<Page<TransactionCodeResponse>>> GetAll([FromQuery] TransactionCodeFilterQuery filter, [FromQuery] Pageable pageable)
    {
        var result = await m_TransactionCodeService.GetAll(filter, pageable);

        return result.ActionResult;
    }

    [Authorize]
    [HttpGet(Endpoints.TransactionCode.GetOne)]
    public async Task<ActionResult<TransactionCodeResponse>> GetOne([FromRoute] Guid id)
    {
        var result = await m_TransactionCodeService.GetOne(id);

        return result.ActionResult;
    }
}
