using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.UserService.Controllers;

[ApiController]
public class OrderController(IOrderService orderService) : ControllerBase
{
    private readonly IOrderService m_OrderService = orderService;

    [Authorize]
    [HttpGet(Endpoints.Order.GetAll)]
    public async Task<ActionResult<List<OrderResponse>>> GetAll([FromQuery] OrderFilterQuery orderFilterQuery, [FromQuery] Pageable pageable)
    {
        var result = await m_OrderService.GetAll(orderFilterQuery, pageable);
        return result.ActionResult;
    }

    [Authorize]
    [HttpGet(Endpoints.Order.GetOne)]
    public async Task<ActionResult<OrderResponse>> GetOne([FromRoute] Guid id)
    {
        var result = await m_OrderService.GetOne(id);
        return result.ActionResult;
    }

    [Authorize]
    [HttpPost(Endpoints.Order.Create)]
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
}
