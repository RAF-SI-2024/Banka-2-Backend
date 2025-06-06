using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.ExchangeService.Services;
using Bank.ExchangeService.Test.Examples.Entities;

using Microsoft.AspNetCore.Mvc;

using Shouldly;

namespace Bank.ExchangeService.Test.Steps;

[Binding]
public class OrderSteps(ScenarioContext context, IOrderService orderService)
{
    private readonly ScenarioContext m_ScenarioContext = context;
    private readonly IOrderService   m_OrderService    = orderService;

    [Given(@"a valid order filter query and pageable")]
    public void GivenAValidOrderFilterQueryAndPageable()
    {
        m_ScenarioContext[Constant.OrderFilterQuery] = Example.Entity.Order.FilterQuery;
        m_ScenarioContext[Constant.OrderPageable]    = new Pageable();
    }

    [When(@"all orders are fetched")]
    public async Task WhenAllOrdersAreFetched()
    {
        var filter   = m_ScenarioContext.Get<OrderFilterQuery>(Constant.OrderFilterQuery);
        var pageable = m_ScenarioContext.Get<Pageable>(Constant.OrderPageable);

        var result = await m_OrderService.GetAll(filter, pageable);
        m_ScenarioContext[Constant.OrdersResult] = result;
    }

    [Then(@"a non-empty list of orders should be returned")]
    public void ThenANonEmptyListOfOrdersShouldBeReturned()
    {
        var result = m_ScenarioContext.Get<Result<Page<OrderResponse>>>(Constant.OrdersResult);

        result.ActionResult.ShouldBeOfType<OkObjectResult>();
        result.Value.ShouldNotBeNull();
    }

    [Given(@"a valid order Id")]
    public void GivenAValidOrderId()
    {
        m_ScenarioContext[Constant.OrderId] = Example.Entity.Order.Id;
    }

    [When(@"the order is fetched")]
    public async Task WhenTheOrderIsFetched()
    {
        var id     = m_ScenarioContext.Get<Guid>(Constant.OrderId);
        var result = await m_OrderService.GetOne(id);

        m_ScenarioContext[Constant.OrderResult] = result;
    }

    [Then(@"the order details should be returned")]
    public void ThenTheOrderDetailsShouldBeReturned()
    {
        var result = m_ScenarioContext.Get<Result<OrderResponse>>(Constant.OrderResult);

        result.ActionResult.ShouldBeOfType<OkObjectResult>();
        result.Value.ShouldNotBeNull();
        result.Value.Id.ShouldBe(m_ScenarioContext.Get<Guid>(Constant.OrderId));
    }

    [Given(@"a valid order create request")]
    public void GivenAValidOrderCreateRequest()
    {
        m_ScenarioContext[Constant.OrderCreateRequest] = Example.Entity.Order.CreateRequest;
    }

    [When(@"the order is created")]
    public async Task WhenTheOrderIsCreated()
    {
        var request = m_ScenarioContext.Get<OrderCreateRequest>(Constant.OrderCreateRequest);

        var result = await m_OrderService.Create(request);
        m_ScenarioContext[Constant.OrderCreateResult] = result;
    }

    [Then(@"the created order details should be returned")]
    public void ThenTheCreatedOrderDetailsShouldBeReturned()
    {
        var result = m_ScenarioContext.Get<Result<OrderResponse>>(Constant.OrderCreateResult);

        result.ActionResult.ShouldBeOfType<OkObjectResult>();
        result.Value.ShouldNotBeNull();
        result.Value.Actuary.Id.ShouldBe(Example.Entity.Order.CreateRequest.ActuaryId);
        result.Value.OrderType.ShouldBe(Example.Entity.Order.CreateRequest.OrderType);
        result.Value.Quantity.ShouldBe(Example.Entity.Order.CreateRequest.Quantity);
        result.Value.ContractCount.ShouldBe(Example.Entity.Order.CreateRequest.ContractCount);
        result.Value.StopPrice.ShouldBe(Example.Entity.Order.CreateRequest.StopPrice);
        result.Value.LimitPrice.ShouldBe(Example.Entity.Order.CreateRequest.LimitPrice);
        result.Value.Direction.ShouldBe(Example.Entity.Order.CreateRequest.Direction);
        result.Value.Status.ShouldBe(OrderStatus.NeedsApproval);
        result.Value.Account.AccountNumber.ShouldBe(Example.Entity.Order.CreateRequest.AccountNumber);
    }

    [Given(@"a valid order update request and order Id")]
    public void GivenAValidOrderUpdateRequestAndOrderId()
    {
        m_ScenarioContext[Constant.OrderUpdateRequest] = Example.Entity.Order.UpdateRequest;
        m_ScenarioContext[Constant.OrderId]            = Example.Entity.Order.Id;
    }

    [When(@"the order is updated")]
    public async Task WhenTheOrderIsUpdated()
    {
        var request = m_ScenarioContext.Get<OrderUpdateRequest>(Constant.OrderUpdateRequest);
        var id      = m_ScenarioContext.Get<Guid>(Constant.OrderId);

        var result = await m_OrderService.Update(request, id);
        m_ScenarioContext[Constant.OrderUpdateResult] = result;
    }

    [Then(@"the updated order details should be returned")]
    public void ThenTheUpdatedOrderDetailsShouldBeReturned()
    {
        var result = m_ScenarioContext.Get<Result<OrderResponse>>(Constant.OrderUpdateResult);

        result.ActionResult.ShouldBeOfType<OkObjectResult>();
        result.Value.ShouldNotBeNull();
        result.Value.Status.ShouldBe(Example.Entity.Order.UpdateRequest.Status);
    }
}

file static class Constant
{
    public const string OrderFilterQuery = "OrderFilterQuery";
    public const string OrderPageable    = "OrderPageable";
    public const string OrdersResult     = "OrdersResult";

    public const string OrderId     = "OrderId";
    public const string OrderResult = "OrderResult";

    public const string OrderCreateRequest = "OrderCreateRequest";
    public const string OrderCreateResult  = "OrderCreateResult";

    public const string OrderUpdateRequest = "OrderUpdateRequest";
    public const string OrderUpdateResult  = "OrderUpdateResult";
}
