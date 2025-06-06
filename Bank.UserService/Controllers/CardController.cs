using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.Permissions.Core;
using Bank.UserService.Services;

using Microsoft.AspNetCore.Mvc;

namespace Bank.UserService.Controllers;

public class CardController(ICardService service) : ControllerBase
{
    private readonly ICardService m_CardService = service;

    [Authorize]
    [HttpGet(Endpoints.Card.GetAll)]
    public async Task<ActionResult<Page<CardResponse>>> GetAll([FromQuery] CardFilterQuery filterQuery, [FromQuery] Pageable pageable)
    {
        var cardType = await m_CardService.GetAll(filterQuery, pageable);

        return cardType.ActionResult;
    }

    [Authorize]
    [HttpGet(Endpoints.Card.GetAllForAccount)]
    public async Task<ActionResult<Page<CardResponse>>> GetAllForAccount([FromRoute] Guid accountId, [FromQuery] CardFilterQuery filter, [FromQuery] Pageable pageable)
    {
        var result = await m_CardService.GetAllForAccount(accountId, filter, pageable);

        return result.ActionResult;
    }

    [Authorize]
    [HttpGet(Endpoints.Card.GetAllForClient)]
    public async Task<ActionResult<CardResponse>> GetAllForClient([FromRoute] Guid clientId, [FromQuery] CardFilterQuery filter, [FromQuery] Pageable pageable)
    {
        var result = await m_CardService.GetAllForClient(clientId, filter, pageable);

        return result.ActionResult;
    }

    [Authorize]
    [HttpGet(Endpoints.Card.GetOne)]
    public async Task<ActionResult<CardResponse>> GetOne([FromRoute] Guid id)
    {
        var cards = await m_CardService.GetOne(id);

        return cards.ActionResult;
    }

    [Authorize]
    [HttpPost(Endpoints.Card.Create)]
    public async Task<ActionResult<CardResponse>> Create([FromBody] CardCreateRequest cardRequest)
    {
        var card = await m_CardService.Create(cardRequest);

        return card.ActionResult;
    }

    [Authorize(Permission.Client, Permission.Employee)]
    [HttpPut(Endpoints.Card.UpdateStatus)]
    public async Task<ActionResult<CardResponse>> UpdateStatus([FromBody] CardUpdateStatusRequest cardUpdateStatusRequest, [FromRoute] Guid id)
    {
        var result = await m_CardService.Update(cardUpdateStatusRequest, id);

        return result.ActionResult;
    }

    [Authorize(Permission.Client)]
    [HttpPut(Endpoints.Card.UpdateLimit)]
    public async Task<ActionResult<CardResponse>> UpdateLimit([FromBody] CardUpdateLimitRequest cardUpdateLimitRequest, [FromRoute] Guid id)
    {
        var result = await m_CardService.Update(cardUpdateLimitRequest, id);

        return result.ActionResult;
    }
}
