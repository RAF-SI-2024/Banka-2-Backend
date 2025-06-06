using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.ExchangeService.Services;
using Bank.Permissions.Core;

using Microsoft.AspNetCore.Mvc;

namespace Bank.ExchangeService.Controllers;

[ApiController]
public class OrderController(IOrderService orderService) : ControllerBase
{
    private readonly IOrderService m_OrderService = orderService;

    [HttpGet(Endpoints.Order.GetAll)]
    [Authorize(Permission.Admin, Permission.Trade)]
    public async Task<ActionResult<List<OrderResponse>>> GetAll([FromQuery] OrderFilterQuery orderFilterQuery, [FromQuery] Pageable pageable)
    {
        var result = await m_OrderService.GetAll(orderFilterQuery, pageable);

        return result.ActionResult;
    }

    [HttpGet(Endpoints.Order.GetOne)]
    [Authorize(Permission.Admin, Permission.Trade)]
    public async Task<ActionResult<OrderResponse>> GetOne([FromRoute] Guid id)
    {
        var result = await m_OrderService.GetOne(id);

        return result.ActionResult;
    }

    [HttpPost(Endpoints.Order.Create)]
    [Authorize(Permission.Admin, Permission.Trade)]
    public async Task<ActionResult<OrderResponse>> Create([FromBody] OrderCreateRequest orderCreateRequest)
    {
        var result = await m_OrderService.Create(orderCreateRequest);

        return result.ActionResult;
    }

    [HttpPut(Endpoints.Order.Update)]
    public async Task<ActionResult<CompanyResponse>> Update([FromBody] OrderUpdateRequest orderUpdateRequest, [FromRoute] Guid id)
    {
        var result = await m_OrderService.Update(orderUpdateRequest, id);

        return result.ActionResult;
    }

    [HttpPut(Endpoints.Order.Approve)]
    [Authorize(Permission.Admin, Permission.ApproveTrade)]
    public async Task<ActionResult<CompanyResponse>> Approve([FromRoute] Guid id)
    {
        var result = await m_OrderService.Approve(id);

        return result.ActionResult;
    }

    [HttpPut(Endpoints.Order.Decline)]
    [Authorize(Permission.Admin, Permission.ApproveTrade)]
    public async Task<ActionResult<CompanyResponse>> Decline([FromRoute] Guid id)
    {
        var result = await m_OrderService.Decline(id);

        return result.ActionResult;
    }
}
