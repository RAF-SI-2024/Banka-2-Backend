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
public class StockSteps(ScenarioContext context, IStockService stockService)
{
    private readonly ScenarioContext m_ScenarioContext = context;
    private readonly IStockService   m_StockService    = stockService;

    [Given(@"a valid stock filter query and pageable")]
    public void GivenAValidStockFilterQueryAndPageable()
    {
        m_ScenarioContext[Constant.StockFilterQuery] = Example.Entity.Stock.QuoteFilterQuery;
        m_ScenarioContext[Constant.StockPageable]    = new Pageable();
    }

    [When(@"all stocks are fetched")]
    public async Task WhenAllStocksAreFetched()
    {
        var query    = m_ScenarioContext.Get<QuoteFilterQuery>(Constant.StockFilterQuery);
        var pageable = m_ScenarioContext.Get<Pageable>(Constant.StockPageable);

        var result = await m_StockService.GetAll(query, pageable);
        m_ScenarioContext[Constant.StockResults] = result;
    }

    [Then(@"a non-empty list of stocks should be returned")]
    public void ThenANonEmptyListOfStocksShouldBeReturned()
    {
        var result = m_ScenarioContext.Get<Result<Page<StockSimpleResponse>>>(Constant.StockResults);

        result.ActionResult.ShouldBeOfType<OkObjectResult>();
        result.Value.ShouldNotBeNull();
    }

    [Given(@"a valid stock Id and filter query")]
    public void GivenAValidStockIdAndFilterQuery()
    {
        m_ScenarioContext[Constant.StockId]             = Example.Entity.Stock.Id;
        m_ScenarioContext[Constant.StockIntervalFilter] = Example.Entity.Stock.QuoteFilterIntervalQuery;
    }

    [When(@"the stock is fetched")]
    public async Task WhenTheStockIsFetched()
    {
        var id     = m_ScenarioContext.Get<Guid>(Constant.StockId);
        var filter = m_ScenarioContext.Get<QuoteFilterIntervalQuery>(Constant.StockIntervalFilter);

        var result = await m_StockService.GetOne(id, filter);
        m_ScenarioContext[Constant.StockResult] = result;
    }

    [Then(@"the stock details should be returned")]
    public void ThenTheStockDetailsShouldBeReturned()
    {
        var result = m_ScenarioContext.Get<Result<StockResponse>>(Constant.StockResult);

        result.ActionResult.ShouldBeOfType<OkObjectResult>();
        result.Value.ShouldNotBeNull();
        result.Value.Id.ShouldBe(m_ScenarioContext.Get<Guid>(Constant.StockId));
    }

    [Given(@"a valid stock Id and daily filter query")]
    public void GivenAValidStockIdAndDailyFilterQuery()
    {
        m_ScenarioContext[Constant.StockId]             = Example.Entity.Stock.Id;
        m_ScenarioContext[Constant.StockIntervalFilter] = Example.Entity.Stock.QuoteFilterIntervalQuery;
    }

    [When(@"the daily stock data is fetched")]
    public async Task WhenTheDailyStockDataIsFetched()
    {
        var id     = m_ScenarioContext.Get<Guid>(Constant.StockId);
        var filter = m_ScenarioContext.Get<QuoteFilterIntervalQuery>(Constant.StockIntervalFilter);

        var result = await m_StockService.GetOneDaily(id, filter);
        m_ScenarioContext[Constant.StockDailyResult] = result;
    }

    [Then(@"the daily stock candle data should be returned")]
    public void ThenTheDailyStockCandleDataShouldBeReturned()
    {
        var result = m_ScenarioContext.Get<Result<StockDailyResponse>>(Constant.StockDailyResult);

        result.ActionResult.ShouldBeOfType<OkObjectResult>();
        result.Value.ShouldNotBeNull();
        result.Value.Id.ShouldBe(m_ScenarioContext.Get<Guid>(Constant.StockId));
    }
}

file static class Constant
{
    public const string StockFilterQuery = "StockFilterQuery";
    public const string StockPageable    = "StockPageable";
    public const string StockResults     = "StockResults";

    public const string StockId             = "StockId";
    public const string StockIntervalFilter = "StockIntervalFilter";
    public const string StockResult         = "StockResult";

    public const string StockDailyResult = "StockDailyResult";
}
