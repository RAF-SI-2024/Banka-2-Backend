using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.ExchangeService.Services;
using Bank.ExchangeService.Test.Examples.Entities;

using Microsoft.AspNetCore.Mvc;

using Shouldly;

namespace Bank.ExchangeService.Test.Steps;

[Binding]
public class StockExchangeSteps(ScenarioContext context, IStockExchangeService stockExchangeService)
{
    private readonly ScenarioContext       m_ScenarioContext      = context;
    private readonly IStockExchangeService m_StockExchangeService = stockExchangeService;

    [Given(@"a valid stock exchange filter query and pageable")]
    public void GivenAValidStockExchangeFilterQueryAndPageable()
    {
        m_ScenarioContext[Constant.StockExchangeFilterQuery] = Example.Entity.StockExchange.FilterQuery;
        m_ScenarioContext[Constant.StockExchangePageable]    = new Pageable();
    }

    [When(@"all stock exchanges are fetched")]
    public async Task WhenAllStockExchangesAreFetched()
    {
        var filter   = m_ScenarioContext.Get<StockExchangeFilterQuery>(Constant.StockExchangeFilterQuery);
        var pageable = m_ScenarioContext.Get<Pageable>(Constant.StockExchangePageable);

        var result = await m_StockExchangeService.GetAll(filter, pageable);
        m_ScenarioContext[Constant.StockExchangesResult] = result;
    }

    [Then(@"a non-empty list of stock exchanges should be returned")]
    public void ThenANonEmptyListOfStockExchangesShouldBeReturned()
    {
        var result = m_ScenarioContext.Get<Result<Page<StockExchangeResponse>>>(Constant.StockExchangesResult);

        result.ActionResult.ShouldBeOfType<OkObjectResult>();
        result.Value.ShouldNotBeNull();
        result.Value.Items.ShouldNotBeEmpty();
    }

    [Given(@"a valid stock exchange Id")]
    public void GivenAValidStockExchangeId()
    {
        m_ScenarioContext[Constant.StockExchangeId] = Example.Entity.StockExchange.Id;
    }

    [When(@"the stock exchange is fetched by Id")]
    public async Task WhenTheStockExchangeIsFetchedById()
    {
        var id = m_ScenarioContext.Get<Guid>(Constant.StockExchangeId);

        var result = await m_StockExchangeService.GetOne(id);
        m_ScenarioContext[Constant.StockExchangeResult] = result;
    }

    [Then(@"the stock exchange details should be returned")]
    public void ThenTheStockExchangeDetailsShouldBeReturned()
    {
        var result = m_ScenarioContext.Get<Result<StockExchangeResponse>>(Constant.StockExchangeResult);

        result.ActionResult.ShouldBeOfType<OkObjectResult>();
        result.Value.ShouldNotBeNull();
        result.Value.Id.ShouldBe(m_ScenarioContext.Get<Guid>(Constant.StockExchangeId));
    }

    [Given(@"a valid stock exchange acronym")]
    public void GivenAValidStockExchangeAcronym()
    {
        m_ScenarioContext[Constant.StockExchangeAcronym] = Example.Entity.StockExchange.Acronym;
    }

    [When(@"the stock exchange is fetched by acronym")]
    public async Task WhenTheStockExchangeIsFetchedByAcronym()
    {
        var acronym = m_ScenarioContext.Get<string>(Constant.StockExchangeAcronym);

        var result = await m_StockExchangeService.GetOne(acronym);
        m_ScenarioContext[Constant.StockExchangeResult] = result;
    }

    [Given(@"a valid stock exchange create request")]
    public void GivenAValidStockExchangeCreateRequest()
    {
        m_ScenarioContext[Constant.StockExchangeCreateRequest] = Example.Entity.StockExchange.CreateRequest;
    }

    [When(@"the stock exchange is created")]
    public async Task WhenTheStockExchangeIsCreated()
    {
        var request = m_ScenarioContext.Get<ExchangeCreateRequest>(Constant.StockExchangeCreateRequest);

        var result = await m_StockExchangeService.Create(request);
        m_ScenarioContext[Constant.StockExchangeCreateResult] = result;
    }

    [Then(@"the created stock exchange details should be returned")]
    public void ThenTheCreatedStockExchangeDetailsShouldBeReturned()
    {
        var result = m_ScenarioContext.Get<Result<StockExchangeResponse>>(Constant.StockExchangeCreateResult);

        result.ActionResult.ShouldBeOfType<OkObjectResult>();
        result.Value.ShouldNotBeNull();
        result.Value.Acronym.ShouldBe(Example.Entity.StockExchange.CreateRequest.Acronym);
        result.Value.Currency.Id.ShouldBe(Example.Entity.StockExchange.CreateRequest.CurrencyId);
    }

    [Then(@"the stock exchange details should be returned with right acronym")]
    public void ThenTheStockExchangeDetailsShouldBeReturnedWithRightAcronym()
    {
        var result = m_ScenarioContext.Get<Result<StockExchangeResponse>>(Constant.StockExchangeResult);

        result.ActionResult.ShouldBeOfType<OkObjectResult>();
        result.Value.ShouldNotBeNull();
        result.Value.Acronym.ShouldBe(Example.Entity.StockExchange.Acronym);
    }
}

file static class Constant
{
    public const string StockExchangeFilterQuery = "StockExchangeFilterQuery";
    public const string StockExchangePageable    = "StockExchangePageable";
    public const string StockExchangesResult     = "StockExchangesResult";

    public const string StockExchangeId     = "StockExchangeId";
    public const string StockExchangeResult = "StockExchangeResult";

    public const string StockExchangeAcronym = "StockExchangeAcronym";

    public const string StockExchangeCreateRequest = "StockExchangeCreateRequest";
    public const string StockExchangeCreateResult  = "StockExchangeCreateResult";
}
