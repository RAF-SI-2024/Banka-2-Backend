using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.UserService.Services;
using Bank.UserService.Test.Examples.Entities;

using Microsoft.AspNetCore.Mvc;

using Shouldly;

namespace Bank.UserService.Test.Steps;

[Binding]
public class CountrySteps(ScenarioContext scenarioContext, ICountryService countryService)
{
    private readonly ICountryService m_CountryService  = countryService;
    private readonly ScenarioContext m_ScenarioContext = scenarioContext;

    [Given(@"country get request with name filter parameter")]
    public void GivenCountryGetRequestWithNameFilterParameter()
    {
        m_ScenarioContext[Constant.FilterParam] = Example.Entity.Country.FilterQueryWithName;
        m_ScenarioContext[Constant.Pageable]    = Example.Entity.Country.Pageable;
    }

    [Given(@"country get request with currency code filter parameter")]
    public void GivenCountryGetRequestWithCurrencyCodeFilterParameter()
    {
        m_ScenarioContext[Constant.FilterParam] = Example.Entity.Country.FilterQueryWithCurrencyCode;
        m_ScenarioContext[Constant.Pageable]    = Example.Entity.Country.Pageable;
    }

    [Given(@"country get request with currency name filter parameter")]
    public void GivenCountryGetRequestWithCurrencyNameFilterParameter()
    {
        m_ScenarioContext[Constant.FilterParam] = Example.Entity.Country.FilterQueryWithCurrencyName;
        m_ScenarioContext[Constant.Pageable]    = Example.Entity.Country.Pageable;
    }

    [Given(@"country get request with Id")]
    public void GivenCountryGetRequestWithId()
    {
        m_ScenarioContext[Constant.Id] = Example.Entity.Country.GetById;
    }

    [When(@"countries are fetched from the database")]
    public async Task WhenCountriesAreFetchedFromTheDatabase()
    {
        var filterQuery = m_ScenarioContext.Get<CountryFilterQuery>(Constant.FilterParam);
        var pageable    = m_ScenarioContext.Get<Pageable>(Constant.Pageable);
        var result      = await m_CountryService.FindAll(filterQuery, pageable);
        m_ScenarioContext[Constant.GetResult] = result;
    }

    [When(@"country is fetched by Id from the database")]
    public async Task WhenCountryIsFetchedByIdFromTheDatabase()
    {
        var id     = m_ScenarioContext.Get<Guid>(Constant.Id);
        var result = await m_CountryService.FindById(id);
        m_ScenarioContext[Constant.GetResult] = result;
    }

    [Then(@"response should be (.*)")]
    public void ThenResponseShouldBe(int p0)
    {
        var result = m_ScenarioContext.Get<Result<Page<CountryResponse>>>(Constant.GetResult);

        if (p0 == 200)
        {
            result.ActionResult.ShouldBeOfType<OkObjectResult>();
            ((OkObjectResult)result.ActionResult).StatusCode.ShouldBe(p0);
        }
        else
        {
            result.ActionResult.ShouldBeOfType<StatusCodeResult>();
            ((StatusCodeResult)result.ActionResult).StatusCode.ShouldBe(p0);
        }
    }

    [Then(@"response should contain a list of countries matching the name parameter")]
    public void ThenResponseShouldContainAListOfCountriesMatchingTheNameParameter()
    {
        var name = m_ScenarioContext.Get<CountryFilterQuery>(Constant.FilterParam)
                                    .Name;

        var result = m_ScenarioContext.Get<Result<Page<CountryResponse>>>(Constant.GetResult);
        result.ActionResult.ShouldBeOfType<OkObjectResult>();
        result.Value.ShouldNotBeNull();
        name.ShouldNotBeNull();
        result.Value.Items.ShouldAllBe(c => c.Name.Contains(name));
    }

    [Then(@"response should contain a list od countries matching the currency code parameter")]
    public void ThenResponseShouldContainAListOdCountriesMatchingTheCurrencyCodeParameter()
    {
        var code = m_ScenarioContext.Get<CountryFilterQuery>(Constant.FilterParam)
                                    .CurrencyCode;

        var result = m_ScenarioContext.Get<Result<Page<CountryResponse>>>(Constant.GetResult);
        result.ActionResult.ShouldBeOfType<OkObjectResult>();
        result.Value.ShouldNotBeNull();
        code.ShouldNotBeNull();
        result.Value.Items.ShouldAllBe(c => c.Currency != null && c.Currency.Code == code);
    }

    [Then(@"response should contain a list of countries matching the currency name parameter")]
    public void ThenResponseShouldContainAListOfCountriesMatchingTheCurrencyNameParameter()
    {
        var name = m_ScenarioContext.Get<CountryFilterQuery>(Constant.FilterParam)
                                    .CurrencyName;

        var result = m_ScenarioContext.Get<Result<Page<CountryResponse>>>(Constant.GetResult);
        result.ActionResult.ShouldBeOfType<OkObjectResult>();
        result.Value.ShouldNotBeNull();
        name.ShouldNotBeNull();
        result.Value.Items.ShouldAllBe(c => c.Currency != null && c.Currency.Name.Contains(name));
    }

    [Then(@"response should contain the country with the given Id")]
    public void ThenResponseShouldContainTheCountryWithTheGivenId()
    {
        var result = m_ScenarioContext.Get<Result<CountryResponse>>(Constant.GetResult);
        result.ActionResult.ShouldBeOfType<OkObjectResult>();
        result.Value.ShouldNotBeNull();
        result.Value.Id.ShouldBe(m_ScenarioContext.Get<Guid>(Constant.Id));
    }
}

file static class Constant
{
    public const string FilterParam = "CountryFilterQuery";
    public const string Pageable    = "CountryPageable";
    public const string Id          = "CountryId";
    public const string GetResult   = "CountryGetResult";
}
