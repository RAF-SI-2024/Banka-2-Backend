﻿using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Permissions.Core;
using Bank.UserService.Services;

using Microsoft.AspNetCore.Mvc;

namespace Bank.UserService.Controllers;

[ApiController]
public class CardTypeController(ICardTypeService service) : ControllerBase
{
    private readonly ICardTypeService m_CardTypeService = service;

    [Authorize]
    [HttpGet(Endpoints.CardType.GetOne)]
    public async Task<IActionResult> GetOne([FromRoute] Guid id)
    {
        var cards = await m_CardTypeService.GetCardTypeById(id);

        return cards.ActionResult;
    }

    [Authorize]
    [HttpGet(Endpoints.CardType.GetAll)]
    public async Task<IActionResult> GetAll([FromQuery] CardTypeFilterQuery filterQuery, [FromQuery] Pageable pageable)
    {
        var cardType = await m_CardTypeService.FindAll(filterQuery, pageable);
        return cardType.ActionResult;
    }
}
