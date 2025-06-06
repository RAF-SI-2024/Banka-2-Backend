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
public class ForexPairSteps(ScenarioContext context, IForexPairService forexPairService)
{
    private readonly ScenarioContext   m_ScenarioContext  = context;
    private readonly IForexPairService m_ForexPairService = forexPairService;

    [Given(@"a valid quote filter query and pageable")]
    public void GivenAValidQuoteFilterQueryAndPageable()
    {
        m_ScenarioContext[Constant.ForexPairFilterQuery] = Example.Entity.ForexPair.QuoteFilterQuery;
        m_ScenarioContext[Constant.ForexPairPageable]    = new Pageable();
    }

    [When(@"all forex pairs are fetched")]
    public async Task WhenAllForexPairsAreFetched()
    {
        var query    = m_ScenarioContext.Get<QuoteFilterQuery>(Constant.ForexPairFilterQuery);
        var pageable = m_ScenarioContext.Get<Pageable>(Constant.ForexPairPageable);

        var result = await m_ForexPairService.GetAll(query, pageable);
        m_ScenarioContext[Constant.ForexPairsResult] = result;
    }

    [Then(@"a non-empty list of forex pairs should be returned")]
    public void ThenANonEmptyListOfForexPairsShouldBeReturned()
    {
        var result = m_ScenarioContext.Get<Result<Page<ForexPairSimpleResponse>>>(Constant.ForexPairsResult);

        result.ActionResult.ShouldBeOfType<OkObjectResult>();
        result.Value.ShouldNotBeNull();
    }

    [Given(@"a valid forex pair Id and filter query")]
    public void GivenAValidForexPairIdAndFilterQuery()
    {
        m_ScenarioContext[Constant.ForexPairId]             = Example.Entity.ForexPair.Id;
        m_ScenarioContext[Constant.ForexPairIntervalFilter] = Example.Entity.ForexPair.QuoteFilterIntervalQuery;
    }

    [When(@"the forex pair is fetched")]
    public async Task WhenTheForexPairIsFetched()
    {
        var id     = m_ScenarioContext.Get<Guid>(Constant.ForexPairId);
        var filter = m_ScenarioContext.Get<QuoteFilterIntervalQuery>(Constant.ForexPairIntervalFilter);

        var result = await m_ForexPairService.GetOne(id, filter);
        m_ScenarioContext[Constant.ForexPairResult] = result;
    }

    [Then(@"the forex pair details should be returned")]
    public void ThenTheForexPairDetailsShouldBeReturned()
    {
        var result = m_ScenarioContext.Get<Result<ForexPairResponse>>(Constant.ForexPairResult);

        result.ActionResult.ShouldBeOfType<OkObjectResult>();
        result.Value.ShouldNotBeNull();
        result.Value.Id.ShouldBe(m_ScenarioContext.Get<Guid>(Constant.ForexPairId));
    }

    [Given(@"a valid forex pair Id and daily filter query")]
    public void GivenAValidForexPairIdAndDailyFilterQuery()
    {
        m_ScenarioContext[Constant.ForexPairId]             = Example.Entity.ForexPair.Id;
        m_ScenarioContext[Constant.ForexPairIntervalFilter] = Example.Entity.ForexPair.QuoteFilterIntervalQuery;
    }

    [When(@"the daily forex pair data is fetched")]
    public async Task WhenTheDailyForexPairDataIsFetched()
    {
        var id     = m_ScenarioContext.Get<Guid>(Constant.ForexPairId);
        var filter = m_ScenarioContext.Get<QuoteFilterIntervalQuery>(Constant.ForexPairIntervalFilter);

        var result = await m_ForexPairService.GetOneDaily(id, filter);
        m_ScenarioContext[Constant.ForexPairDailyResult] = result;
    }

    [Then(@"the daily forex pair candle data should be returned")]
    public void ThenTheDailyForexPairCandleDataShouldBeReturned()
    {
        var result = m_ScenarioContext.Get<Result<ForexPairDailyResponse>>(Constant.ForexPairDailyResult);

        result.ActionResult.ShouldBeOfType<OkObjectResult>();
        result.Value.ShouldNotBeNull();
        result.Value.Id.ShouldBe(m_ScenarioContext.Get<Guid>(Constant.ForexPairId));
    }
}

file static class Constant
{
    public const string ForexPairFilterQuery    = "ForexPairFilterQuery";
    public const string ForexPairPageable       = "ForexPairPageable";
    public const string ForexPairsResult        = "ForexPairsResult";
    public const string ForexPairId             = "ForexPairId";
    public const string ForexPairIntervalFilter = "ForexPairIntervalFilter";
    public const string ForexPairResult         = "ForexPairResult";
    public const string ForexPairDailyResult    = "ForexPairDailyResult";
}
