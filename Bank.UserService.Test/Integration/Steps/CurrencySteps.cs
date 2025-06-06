using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.UserService.Controllers;
using Bank.UserService.Services;
using Bank.UserService.Test.Examples.Entities;

using Microsoft.AspNetCore.Mvc;

using Shouldly;

namespace Bank.UserService.Test.Steps;

[Binding]
public class CurrencySteps(ScenarioContext scenarioContext, ICurrencyService currencyService, CurrencyController currencyController)
{
    private readonly ICurrencyService   m_CurrencyService    = currencyService;
    private readonly ScenarioContext    m_ScenarioContext    = scenarioContext;
    private readonly CurrencyController m_CurrencyController = currencyController;

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

    [When(@"simple currencies are fetched from the database")]
    public async Task WhenSimpleCurrenciesAreFetchedFromTheDatabase()
    {
        var simpleCurrencies = await m_CurrencyService.FindAllSimple(new CurrencyFilterQuery());

        m_ScenarioContext[Constant.GetResult] = simpleCurrencies;
    }

    [Then(@"response should contain a list of all simple currencies")]
    public void ThenResponseShouldContainAListOfAllSimpleCurrencies()
    {
        var simpleCurrencies = m_ScenarioContext.Get<Result<List<CurrencySimpleResponse>>>(Constant.GetResult);

        simpleCurrencies.ActionResult.ShouldBeOfType<OkObjectResult>();
        simpleCurrencies.Value.ShouldNotBeNull();
        simpleCurrencies.Value.ShouldNotBeEmpty();
    }

    [When(@"simple currency is fetched by Id from the database")]
    public async Task WhenSimpleCurrencyIsFetchedByIdFromTheDatabase()
    {
        var id = m_ScenarioContext.Get<Guid>(Constant.Id);

        var result = await m_CurrencyService.FindByIdSimple(id);

        m_ScenarioContext[Constant.GetResult] = result;
    }

    [Then(@"response should contain the simple currency with the given Id")]
    public void ThenResponseShouldContainTheSimpleCurrencyWithTheGivenId()
    {
        var result = m_ScenarioContext.Get<Result<CurrencySimpleResponse>>(Constant.GetResult);

        result.ActionResult.ShouldBeOfType<OkObjectResult>();
        result.Value.ShouldNotBeNull();
        result.Value.Id.ShouldBe(m_ScenarioContext.Get<Guid>(Constant.Id));
    }

    [Given(@"a currency filter query with name filter")]
    public void GivenACurrencyFilterQueryWithNameFilter()
    {
        m_ScenarioContext[Constant.CurrencyFilterQuery] = new CurrencyFilterQuery { Name = "TestCurrency" };
    }

    [Given(@"a currency filter query with code filter")]
    public void GivenACurrencyFilterQueryWithCodeFilter()
    {
        m_ScenarioContext[Constant.CurrencyFilterQuery] = new CurrencyFilterQuery { Code = "USD" };
    }

    [When(@"a GET request is sent to fetch currencies")]
    public async Task WhenAGetRequestIsSentToFetchCurrencies()
    {
        var filterQuery = m_ScenarioContext.Get<CurrencyFilterQuery>(Constant.CurrencyFilterQuery);

        var getCurrenciesResult = await m_CurrencyController.GetAll(filterQuery);

        m_ScenarioContext[Constant.GetCurrencies] = getCurrenciesResult;
    }

    [Then(@"the response ActionResult should indicate successful retrieval of currencies matching the name filter")]
    [Then(@"the response ActionResult should indicate successful retrieval of currencies matching the code filter")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulRetrievalOfCurrencies()
    {
        var getCurrenciesResult = m_ScenarioContext.Get<ActionResult<List<CurrencyResponse>>>(Constant.GetCurrencies);

        getCurrenciesResult.Result.ShouldBeOfType<OkObjectResult>();
        getCurrenciesResult.ShouldNotBeNull();
    }

    [Given(@"a currency Id to fetch")]
    public void GivenACurrencyIdToFetch()
    {
        m_ScenarioContext[Constant.CurrencyId] = Example.Entity.Currency.GetById;
    }

    [When(@"a GET request is sent to fetch a currency by Id")]
    public async Task WhenAGetRequestIsSentToFetchACurrencyById()
    {
        var currencyId = m_ScenarioContext.Get<Guid>(Constant.CurrencyId);

        var getCurrencyResult = await m_CurrencyController.GetOne(currencyId);

        m_ScenarioContext[Constant.GetCurrency] = getCurrencyResult;
    }

    [Then(@"the response ActionResult should indicate successful retrieval of the currency")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulRetrievalOfTheCurrency()
    {
        var getCurrencyResult = m_ScenarioContext.Get<ActionResult<CurrencyResponse>>(Constant.GetCurrency);

        getCurrencyResult.Result.ShouldBeOfType<OkObjectResult>();
        getCurrencyResult.ShouldNotBeNull();
    }

    [Given(@"a currency filter query for simple currencies")]
    public void GivenACurrencyFilterQueryForSimpleCurrencies()
    {
        m_ScenarioContext[Constant.CurrencyFilterQuery] = new CurrencyFilterQuery();
    }

    [When(@"a GET request is sent to fetch all simple currencies")]
    public async Task WhenAGetRequestIsSentToFetchAllSimpleCurrencies()
    {
        var filterQuery = m_ScenarioContext.Get<CurrencyFilterQuery>(Constant.CurrencyFilterQuery);

        var getSimpleCurrenciesResult = await m_CurrencyController.GetAllSimple(filterQuery);

        m_ScenarioContext[Constant.GetSimpleCurrencies] = getSimpleCurrenciesResult;
    }

    [Then(@"the response ActionResult should indicate successful retrieval of all simple currencies")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulRetrievalOfAllSimpleCurrencies()
    {
        var getSimpleCurrenciesResult = m_ScenarioContext.Get<ActionResult<List<CurrencyResponse>>>(Constant.GetSimpleCurrencies);

        getSimpleCurrenciesResult.Result.ShouldBeOfType<OkObjectResult>();
        getSimpleCurrenciesResult.ShouldNotBeNull();
    }

    [Given(@"a simple currency Id to fetch")]
    public void GivenASimpleCurrencyIdToFetch()
    {
        m_ScenarioContext[Constant.CurrencyId] = Example.Entity.Currency.GetById;
    }

    [When(@"a GET request is sent to fetch a simple currency by Id")]
    public async Task WhenAGetRequestIsSentToFetchASimpleCurrencyById()
    {
        var currencyId = m_ScenarioContext.Get<Guid>(Constant.CurrencyId);

        var getSimpleCurrencyResult = await m_CurrencyController.GetOneSimple(currencyId);

        m_ScenarioContext[Constant.GetSimpleCurrency] = getSimpleCurrencyResult;
    }

    [Then(@"the response ActionResult should indicate successful retrieval of the simple currency")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulRetrievalOfTheSimpleCurrency()
    {
        var getSimpleCurrencyResult = m_ScenarioContext.Get<ActionResult<CurrencyResponse>>(Constant.GetSimpleCurrency);

        getSimpleCurrencyResult.Result.ShouldBeOfType<OkObjectResult>();
        getSimpleCurrencyResult.ShouldNotBeNull();
    }
}

file static class Constant
{
    public const string FilterParam         = "CurrencyFilterQuery";
    public const string Id                  = "CurrencyId";
    public const string GetResult           = "CurrancyGetResult";
    public const string CurrencyFilterQuery = "CurrencyFilterQuery";
    public const string GetCurrencies       = "GetCurrencies";
    public const string GetCurrency         = "GetCurrency";
    public const string GetSimpleCurrencies = "GetSimpleCurrencies";
    public const string GetSimpleCurrency   = "GetSimpleCurrency";
    public const string CurrencyId          = "CurrencyId";
}
