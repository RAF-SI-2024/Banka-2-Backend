using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Services;

using Microsoft.AspNetCore.Mvc;

namespace Bank.UserService.Controllers;

public class CardController(ICardService service) : ControllerBase
{
    private readonly ICardService m_CardService = service;

    [HttpGet(Endpoints.Card.GetOne)]
    public async Task<ActionResult<CardResponse>> GetOne([FromRoute] Guid id)
    {
        var cards = await m_CardService.GetOne(id);

        return cards.ActionResult;
    }

    [HttpGet(Endpoints.Card.GetAll)]
    public async Task<ActionResult<Page<CardResponse>>> GetAll([FromQuery] CardFilterQuery filterQuery, [FromQuery] Pageable pageable)
    {
        var cardType = await m_CardService.GetAll(filterQuery, pageable);
        return cardType.ActionResult;
    }

    [HttpPost(Endpoints.Card.Create)]
    //[Authorize(Roles = $"{Role.Employee}")]
    public async Task<ActionResult<CardResponse>> Create([FromBody] CardCreateRequest cardRequest)
    {
        var card = await m_CardService.Create(cardRequest);

        return card.ActionResult;
    }

    [HttpPut(Endpoints.Card.UpdateEmployee)]
    //[Authorize(Roles = $"{Role.Employee}")]
    public async Task<ActionResult<CardResponse>> UpdateStatus([FromBody] CardStatusUpdateRequest cardStatusUpdateRequest, [FromRoute] Guid id)
    {
        var result = await m_CardService.Update(cardStatusUpdateRequest, id);

        return result.ActionResult;
    }

    [HttpPut(Endpoints.Card.UpdateClient)]
    //[Authorize(Roles = $"{Role.Client}")]
    public async Task<ActionResult<CardResponse>> UpdateLimit([FromBody] CardLimitUpdateRequest cardLimitUpdateRequest, [FromRoute] Guid id)
    {
        var result = await m_CardService.Update(cardLimitUpdateRequest, id);

        return result.ActionResult;
    }
}
