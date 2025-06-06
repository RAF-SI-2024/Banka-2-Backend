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
public class FutureContractSteps(ScenarioContext context, IFutureContractService futureContractService)
{
    private readonly ScenarioContext        m_ScenarioContext       = context;
    private readonly IFutureContractService m_FutureContractService = futureContractService;

    [Given(@"a valid future contract filter query and pageable")]
    public void GivenAValidFutureContractFilterQueryAndPageable()
    {
        m_ScenarioContext[Constant.FutureContractFilterQuery] = Example.Entity.FutureContract.QuoteFilterQuery;
        m_ScenarioContext[Constant.FutureContractPageable]    = new Pageable();
    }

    [When(@"all future contracts are fetched")]
    public async Task WhenAllFutureContractsAreFetched()
    {
        var query    = m_ScenarioContext.Get<QuoteFilterQuery>(Constant.FutureContractFilterQuery);
        var pageable = m_ScenarioContext.Get<Pageable>(Constant.FutureContractPageable);

        var result = await m_FutureContractService.GetAll(query, pageable);
        m_ScenarioContext[Constant.FutureContractsResult] = result;
    }

    [Then(@"a non-empty list of future contracts should be returned")]
    public void ThenANonEmptyListOfFutureContractsShouldBeReturned()
    {
        var result = m_ScenarioContext.Get<Result<Page<FutureContractSimpleResponse>>>(Constant.FutureContractsResult);

        result.ActionResult.ShouldBeOfType<OkObjectResult>();
        result.Value.ShouldNotBeNull();
    }

    [Given(@"a valid future contract Id and filter query")]
    public void GivenAValidFutureContractIdAndFilterQuery()
    {
        m_ScenarioContext[Constant.FutureContractId]             = Example.Entity.FutureContract.Id;
        m_ScenarioContext[Constant.FutureContractIntervalFilter] = Example.Entity.FutureContract.QuoteFilterIntervalQuery;
    }

    [When(@"the future contract is fetched")]
    public async Task WhenTheFutureContractIsFetched()
    {
        var id     = m_ScenarioContext.Get<Guid>(Constant.FutureContractId);
        var filter = m_ScenarioContext.Get<QuoteFilterIntervalQuery>(Constant.FutureContractIntervalFilter);

        var result = await m_FutureContractService.GetOne(id, filter);
        m_ScenarioContext[Constant.FutureContractResult] = result;
    }

    [Then(@"the future contract details should be returned")]
    public void ThenTheFutureContractDetailsShouldBeReturned()
    {
        var result = m_ScenarioContext.Get<Result<FutureContractResponse>>(Constant.FutureContractResult);

        result.ActionResult.ShouldBeOfType<OkObjectResult>();
        result.Value.ShouldNotBeNull();
        result.Value.Id.ShouldBe(m_ScenarioContext.Get<Guid>(Constant.FutureContractId));
    }

    [Given(@"a valid future contract Id and daily filter query")]
    public void GivenAValidFutureContractIdAndDailyFilterQuery()
    {
        m_ScenarioContext[Constant.FutureContractId]             = Example.Entity.FutureContract.Id;
        m_ScenarioContext[Constant.FutureContractIntervalFilter] = Example.Entity.FutureContract.QuoteFilterIntervalQuery;
    }

    [When(@"the daily future contract data is fetched")]
    public async Task WhenTheDailyFutureContractDataIsFetched()
    {
        var id     = m_ScenarioContext.Get<Guid>(Constant.FutureContractId);
        var filter = m_ScenarioContext.Get<QuoteFilterIntervalQuery>(Constant.FutureContractIntervalFilter);

        var result = await m_FutureContractService.GetOneDaily(id, filter);
        m_ScenarioContext[Constant.FutureContractDailyResult] = result;
    }

    [Then(@"the daily future contract candle data should be returned")]
    public void ThenTheDailyFutureContractCandleDataShouldBeReturned()
    {
        var result = m_ScenarioContext.Get<Result<FutureContractDailyResponse>>(Constant.FutureContractDailyResult);

        result.ActionResult.ShouldBeOfType<OkObjectResult>();
        result.Value.ShouldNotBeNull();
        result.Value.Id.ShouldBe(m_ScenarioContext.Get<Guid>(Constant.FutureContractId));
    }
}

file static class Constant
{
    public const string FutureContractFilterQuery = "FutureContractFilterQuery";
    public const string FutureContractPageable    = "FutureContractPageable";
    public const string FutureContractsResult     = "FutureContractsResult";

    public const string FutureContractId             = "FutureContractId";
    public const string FutureContractIntervalFilter = "FutureContractIntervalFilter";
    public const string FutureContractResult         = "FutureContractResult";

    public const string FutureContractDailyResult = "FutureContractDailyResult";
}
