using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.UserService.Services;
using Bank.UserService.Test.Examples.Entities;

using Microsoft.AspNetCore.Mvc;

using Shouldly;

namespace Bank.UserService.Test.Steps;

[Binding]
public class CurrencySteps(ScenarioContext scenarioContext, ICurrencyService currencyService)
{
    private readonly ICurrencyService m_CurrencyService = currencyService;
    private readonly ScenarioContext  m_ScenarioContext = scenarioContext;

    [Given(@"currency get request with name filter parameter")]
    public void GivenCurrencyGetRequestWithNameFilterParameter()
    {
        m_ScenarioContext[Constant.FilterParam] = Example.Entity.Currency.FilterQueryWithName;
    }

    [Given(@"currency get request with code filter parameter")]
    public void GivenCurrencyGetRequestWithCodeFilterParameter()
    {
        m_ScenarioContext[Constant.FilterParam] = Example.Entity.Currency.FilterQueryWithCode;
    }

    [Given(@"currency get request with Id")]
    public void GivenCurrencyGetRequestWithId()
    {
        m_ScenarioContext[Constant.Id] = Example.Entity.Currency.GetById;
    }

    [When(@"currencies are fetched from the database")]
    public async Task WhenCurrenciesAreFetchedFromTheDatabase()
    {
        var filterQuery = m_ScenarioContext.Get<CurrencyFilterQuery>(Constant.FilterParam);
        var result      = await m_CurrencyService.FindAll(filterQuery);
        m_ScenarioContext[Constant.GetResult] = result;
    }

    [When(@"currency is fetched by Id from the database")]
    public async Task WhenCurrencyIsFetchedByIdFromTheDatabase()
    {
        var id     = m_ScenarioContext.Get<Guid>(Constant.Id);
        var result = await m_CurrencyService.FindById(id);
        m_ScenarioContext[Constant.GetResult] = result;
    }

    [Then(@"response should contain a list of currencies matching the name filter")]
    public void ThenResponseShouldContainAListOfCurrenciesMatchingTheNameFilter()
    {
        var name = m_ScenarioContext.Get<CurrencyFilterQuery>(Constant.FilterParam)
                                    .Name;

        var result = m_ScenarioContext.Get<Result<List<CurrencyResponse>>>(Constant.GetResult);
        result.ActionResult.ShouldBeOfType<OkObjectResult>();
        result.Value.ShouldNotBeNull();
        name.ShouldNotBeNull();
        result.Value.ShouldAllBe(c => c.Name.Contains(name));
    }

    [Then(@"response should contain a list of currencies matching the code filter")]
    public void ThenResponseShouldContainAListOfCurrenciesMatchingTheCodeFilter()
    {
        var code = m_ScenarioContext.Get<CurrencyFilterQuery>(Constant.FilterParam)
                                    .Code;

        var result = m_ScenarioContext.Get<Result<List<CurrencyResponse>>>(Constant.GetResult);
        result.ActionResult.ShouldBeOfType<OkObjectResult>();
        result.Value.ShouldNotBeNull();
        code.ShouldNotBeNull();
        result.Value.ShouldAllBe(c => c.Code == code);
    }

    [Then(@"response should contain the currency with the given Id")]
    public void ThenResponseShouldContainTheCurrencyWithTheGivenId()
    {
        var result = m_ScenarioContext.Get<Result<CurrencyResponse>>(Constant.GetResult);
        result.ActionResult.ShouldBeOfType<OkObjectResult>();
        result.Value.ShouldNotBeNull();
        result.Value.Id.ShouldBe(m_ScenarioContext.Get<Guid>(Constant.Id));
    }
}

file static class Constant
{
    public const string FilterParam = "CurrencyFilterQuery";
    public const string Id          = "CurrencyId";
    public const string GetResult   = "CurrancyGetResult";
}
