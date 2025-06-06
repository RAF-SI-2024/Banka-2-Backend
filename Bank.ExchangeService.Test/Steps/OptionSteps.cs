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
public class OptionSteps(ScenarioContext context, IOptionService optionService)
{
    private readonly ScenarioContext m_ScenarioContext = context;
    private readonly IOptionService  m_OptionService   = optionService;

    [Given(@"a valid option filter query and pageable")]
    public void GivenAValidOptionFilterQueryAndPageable()
    {
        m_ScenarioContext[Constant.OptionFilterQuery] = Example.Entity.Option.QuoteFilterQuery;
        m_ScenarioContext[Constant.OptionPageable]    = new Pageable();
    }

    [When(@"all options are fetched")]
    public async Task WhenAllOptionsAreFetched()
    {
        var query    = m_ScenarioContext.Get<QuoteFilterQuery>(Constant.OptionFilterQuery);
        var pageable = m_ScenarioContext.Get<Pageable>(Constant.OptionPageable);

        var result = await m_OptionService.GetAll(query, pageable);
        m_ScenarioContext[Constant.OptionResults] = result;
    }

    [Then(@"a non-empty list of options should be returned")]
    public void ThenANonEmptyListOfOptionsShouldBeReturned()
    {
        var result = m_ScenarioContext.Get<Result<Page<OptionSimpleResponse>>>(Constant.OptionResults);

        result.ActionResult.ShouldBeOfType<OkObjectResult>();
        result.Value.ShouldNotBeNull();
    }

    [Given(@"a valid option Id and filter query")]
    public void GivenAValidOptionIdAndFilterQuery()
    {
        m_ScenarioContext[Constant.OptionId]             = Example.Entity.Option.Id;
        m_ScenarioContext[Constant.OptionIntervalFilter] = Example.Entity.Option.QuoteFilterIntervalQuery;
    }

    [When(@"the option is fetched")]
    public async Task WhenTheOptionIsFetched()
    {
        var id     = m_ScenarioContext.Get<Guid>(Constant.OptionId);
        var filter = m_ScenarioContext.Get<QuoteFilterIntervalQuery>(Constant.OptionIntervalFilter);

        var result = await m_OptionService.GetOne(id, filter);
        m_ScenarioContext[Constant.OptionResult] = result;
    }

    [Then(@"the option details should be returned")]
    public void ThenTheOptionDetailsShouldBeReturned()
    {
        var result = m_ScenarioContext.Get<Result<OptionResponse>>(Constant.OptionResult);

        result.ActionResult.ShouldBeOfType<OkObjectResult>();
        result.Value.ShouldNotBeNull();
        result.Value.Id.ShouldBe(m_ScenarioContext.Get<Guid>(Constant.OptionId));
    }

    [Given(@"a valid option Id and daily filter query")]
    public void GivenAValidOptionIdAndDailyFilterQuery()
    {
        m_ScenarioContext[Constant.OptionId]             = Example.Entity.Option.Id;
        m_ScenarioContext[Constant.OptionIntervalFilter] = Example.Entity.Option.QuoteFilterIntervalQuery;
    }

    [When(@"the daily option data is fetched")]
    public async Task WhenTheDailyOptionDataIsFetched()
    {
        var id     = m_ScenarioContext.Get<Guid>(Constant.OptionId);
        var filter = m_ScenarioContext.Get<QuoteFilterIntervalQuery>(Constant.OptionIntervalFilter);

        var result = await m_OptionService.GetOneDaily(id, filter);
        m_ScenarioContext[Constant.OptionDailyResult] = result;
    }

    [Then(@"the daily option candle data should be returned")]
    public void ThenTheDailyOptionCandleDataShouldBeReturned()
    {
        var result = m_ScenarioContext.Get<Result<OptionDailyResponse>>(Constant.OptionDailyResult);

        result.ActionResult.ShouldBeOfType<OkObjectResult>();
        result.Value.ShouldNotBeNull();
        result.Value.Id.ShouldBe(m_ScenarioContext.Get<Guid>(Constant.OptionId));
    }
}

file static class Constant
{
    public const string OptionFilterQuery = "OptionFilterQuery";
    public const string OptionPageable    = "OptionPageable";
    public const string OptionResults     = "OptionResults";

    public const string OptionId             = "OptionId";
    public const string OptionIntervalFilter = "OptionIntervalFilter";
    public const string OptionResult         = "OptionResult";

    public const string OptionDailyResult = "OptionDailyResult";
}
