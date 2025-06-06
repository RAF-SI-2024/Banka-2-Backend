﻿using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Controllers;
using Bank.UserService.Services;
using Bank.UserService.Test.Examples.Entities;

using Microsoft.AspNetCore.Mvc;

using Shouldly;

namespace Bank.UserService.Test.Steps;

[Binding]
public class ExchangeSteps(ScenarioContext scenarioContext, IExchangeService exchangeService, ExchangeController exchangeController)
{
    private readonly IExchangeService   m_ExchangeService    = exchangeService;
    private readonly ScenarioContext    m_ScenarioContext    = scenarioContext;
    private readonly ExchangeController m_ExchangeController = exchangeController;

    [StepArgumentTransformation("^([A-Z_]+_ID)$")]
    public Guid TransformId(string input)
    {
        return input switch
               {
                   "VALID_EXCHANGE_ID"        => Example.Entity.Exchange.GetExchange.Id,
                   "INVALID_EXCHANGE_ID"      => Guid.Empty,
                   "UPDATE_VALID_EXCHANGE_ID" => Example.Entity.Exchange.UpdateId,
                   "RSD_ID"                   => Example.Entity.Currency.SerbianDinar.Id,
                   "EUR_ID"                   => Example.Entity.Currency.Euro.Id,
                   ""                         => Guid.Empty,
                   _                          => throw new ArgumentException($"Transform unknown Id: {input}")
               };
    }

    [StepArgumentTransformation("^([A-Z]{3}_CODE)$")]
    public string TransformCurrencyCode(string input)
    {
        return input switch
               {
                   "RSD_CODE" => Example.Entity.Currency.SerbianDinar.Code,
                   "EUR_CODE" => Example.Entity.Currency.Euro.Code,
                   ""         => "",
                   _          => throw new ArgumentException($"Transform unknown Code: {input}")
               };
    }

    [StepArgumentTransformation("^([A-Z_]+_RETURN)$")]
    public bool TransformReturn(string input)
    {
        return input switch
               {
                   "NOT_RETURN" => false,
                   "DO_RETURN"  => true,
                   _            => throw new ArgumentException($"Transform unknown Id: {input}")
               };
    }

    [StepArgumentTransformation]
    public DateOnly TransformDate(string input)
    {
        return input switch
               {
                   "TODAY" => DateOnly.FromDateTime(DateTime.UtcNow),
                   ""      => DateOnly.FromDateTime(DateTime.MinValue),
                   _       => throw new ArgumentException($"Transform unknown Date: {input}")
               };
    }

    [Given(@"exchange filter query (.*), (.*), (.*)")]
    public void GivenExchangeFilterQuery(Guid currencyId, string currencyCode, DateOnly date)
    {
        var exchangeFilterQuery = new ExchangeFilterQuery();

        if (currencyId != Guid.Empty)
            exchangeFilterQuery.CurrencyId = currencyId;

        if (currencyCode != "")
            exchangeFilterQuery.CurrencyCode = currencyCode;

        if (date != DateOnly.MinValue)
            exchangeFilterQuery.Date = date;

        m_ScenarioContext[Constant.FilterQuery] = exchangeFilterQuery;
    }

    [When(@"exchanges are fetched from the database")]
    public async Task WhenExchangesAreFetchedFromTheDatabase()
    {
        var exchangeFilterQuery = m_ScenarioContext.Get<ExchangeFilterQuery>(Constant.FilterQuery);
        var result              = await m_ExchangeService.GetAll(exchangeFilterQuery);
        m_ScenarioContext[Constant.GetAll]       = result;
        m_ScenarioContext[Constant.ActionResult] = result.ActionResult;
    }

    [Then(@"the response code should be (.*)")]
    public void ThenTheResponseCodeShouldBe(int statusCode)
    {
        var actionResult = m_ScenarioContext.Get<ActionResult>(Constant.ActionResult);

        var resultStatusCode = actionResult switch
                               {
                                   ObjectResult objectResult         => objectResult.StatusCode,
                                   StatusCodeResult statusCodeResult => statusCodeResult.StatusCode,
                                   _ => throw new InvalidOperationException($"Unexpected ActionResult type: {actionResult.GetType()
                                                                                                                         .Name}")
                               };

        resultStatusCode.ShouldNotBeNull();
        resultStatusCode.ShouldBe(statusCode);
    }

    [Then(@"response should contain exchanges that pass filter")]
    public void ThenResponseShouldContainExchangesThatPassFilter()
    {
        var result      = m_ScenarioContext.Get<Result<List<ExchangeResponse>>>(Constant.GetAll);
        var queryFilter = m_ScenarioContext.Get<ExchangeFilterQuery>(Constant.FilterQuery);

        if (queryFilter.Date == DateOnly.MinValue)
            queryFilter.Date = DateOnly.FromDateTime(DateTime.UtcNow);

        result.Value.ShouldNotBeNull();
        result.Value.ShouldNotBeEmpty();

        foreach (var exchange in result.Value)
        {
            if (queryFilter.CurrencyId != Guid.Empty)
                (exchange.CurrencyFrom.Id == queryFilter.CurrencyId || exchange.CurrencyTo.Id == queryFilter.CurrencyId).ShouldBeTrue();

            if (queryFilter.CurrencyCode is not null)
                (exchange.CurrencyFrom.Code == queryFilter.CurrencyCode || exchange.CurrencyTo.Code == queryFilter.CurrencyCode).ShouldBeTrue();

            (queryFilter.Date <= DateOnly.FromDateTime(exchange.CreatedAt) && DateOnly.FromDateTime(exchange.CreatedAt) < queryFilter.Date.AddDays(1)).ShouldBeTrue();
        }
    }

    [Given(@"exchange id (.*)")]
    public void GivenExchangeId(Guid exchangeId)
    {
        m_ScenarioContext[Constant.GivenId] = exchangeId;
    }

    [When(@"exchange is fetched from the database with id")]
    public async Task WhenExchangeIsFetchedFromTheDatabaseWithId()
    {
        var id     = m_ScenarioContext.Get<Guid>(Constant.GivenId);
        var result = await m_ExchangeService.GetById(id);

        m_ScenarioContext[Constant.GetOne]       = result;
        m_ScenarioContext[Constant.ActionResult] = result.ActionResult;
    }

    [Given(@"currency from (.*)")]
    public void GivenCurrencyFrom(string currencyFromCode)
    {
        m_ScenarioContext[Constant.CurrencyFromCode] = currencyFromCode;
    }

    [Given(@"currency to (.*)")]
    public void GivenCurrencyTo(string currencyToCode)
    {
        m_ScenarioContext[Constant.CurrencyToCode] = currencyToCode;
    }

    [When(@"exchange is fetched from the database with currencies")]
    public async Task WhenExchangeIsFetchedFromTheDatabaseWithCurrencies()
    {
        var filter = new ExchangeBetweenQuery()
                     {
                         CurrencyFromCode = m_ScenarioContext.Get<string>(Constant.CurrencyFromCode),
                         CurrencyToCode   = m_ScenarioContext.Get<string>(Constant.CurrencyToCode)
                     };

        var result = await m_ExchangeService.GetByCurrencies(filter);
        m_ScenarioContext[Constant.GetOne]       = result;
        m_ScenarioContext[Constant.ActionResult] = result.ActionResult;
    }

    [Then(@"exchange response should (.*)")]
    public void ThenExchangeResponseShould(bool shouldCheck)
    {
        if (!shouldCheck)
            return;

        var exchangeResult = m_ScenarioContext.Get<Result<ExchangeResponse>>(Constant.GetOne);
        var seeder         = Example.Entity.Exchange.GetExchange;

        exchangeResult.Value.ShouldNotBeNull();
        exchangeResult.Value.Id.ShouldBe(seeder.Id);
        exchangeResult.Value.CurrencyFrom.Id.ShouldBe(seeder.CurrencyFromId);
        exchangeResult.Value.CurrencyTo.Id.ShouldBe(seeder.CurrencyToId);
        exchangeResult.Value.Commission.ShouldBe(seeder.Commission);
        exchangeResult.Value.Rate.ShouldBe(seeder.Rate);
        exchangeResult.Value.CreatedAt.ShouldBeInRange(seeder.CreatedAt.Subtract(TimeSpan.FromSeconds(5)), seeder.CreatedAt.AddSeconds(5));
        exchangeResult.Value.ModifiedAt.ShouldBeInRange(seeder.ModifiedAt.Subtract(TimeSpan.FromSeconds(5)), seeder.ModifiedAt.AddSeconds(5));
    }

    [Given(@"updated exchange request")]
    public void GivenUpdatedExchangeRequest()
    {
        m_ScenarioContext[Constant.GivenUpdate] = Example.Entity.Exchange.ExchangeUpdateRequest;
    }

    [When(@"updated exchange is put into database")]
    public async Task WhenUpdatedExchangeIsPutIntoDatabase()
    {
        var id      = m_ScenarioContext.Get<Guid>(Constant.GivenId);
        var request = m_ScenarioContext.Get<ExchangeUpdateRequest>(Constant.GivenUpdate);
        var result  = await m_ExchangeService.Update(request, id);

        m_ScenarioContext[Constant.GetOne]       = result;
        m_ScenarioContext[Constant.ActionResult] = result.ActionResult;
    }

    [Then(@"exchange update response should (.*)")]
    public void ThenExchangeUpdateResponseShould(bool shouldCheck)
    {
        if (!shouldCheck)
            return;

        var exchangeResponse = m_ScenarioContext.Get<Result<ExchangeResponse>>(Constant.GetOne);
        var seeder           = Example.Entity.Exchange.UpdatedExchange;

        exchangeResponse.Value.ShouldNotBeNull();
        exchangeResponse.Value.Id.ShouldBe(seeder.Id);
        exchangeResponse.Value.CurrencyFrom.Id.ShouldBe(seeder.CurrencyFromId);
        exchangeResponse.Value.CurrencyTo.Id.ShouldBe(seeder.CurrencyToId);
        exchangeResponse.Value.Commission.ShouldBe(seeder.Commission);
        exchangeResponse.Value.Rate.ShouldBe(seeder.Rate);
        exchangeResponse.Value.CreatedAt.ShouldBeInRange(seeder.CreatedAt.Subtract(TimeSpan.FromSeconds(5)), seeder.CreatedAt.AddSeconds(5));
        exchangeResponse.Value.ModifiedAt.ShouldBeInRange(seeder.ModifiedAt.Subtract(TimeSpan.FromSeconds(5)), seeder.ModifiedAt.AddSeconds(5));
    }

    [Given(@"a valid exchange filter query")]
    public void GivenAValidExchangeFilterQuery()
    {
        m_ScenarioContext[Constant.ExchangeFilterQuery] = new ExchangeFilterQuery();
    }

    [When(@"a GET request is sent to the exchange list endpoint")]
    public async Task WhenGetAllExchanges()
    {
        var query = m_ScenarioContext.Get<ExchangeFilterQuery>(Constant.ExchangeFilterQuery);

        var result = await m_ExchangeController.GetAll(query);

        m_ScenarioContext[Constant.ExchangeListResult] = result;
    }

    [Then(@"the response ActionResult should indicate successful retrieval of the exchange list")]
    public void ThenExchangeListShouldBeRetrieved()
    {
        var result = m_ScenarioContext.Get<ActionResult<List<ExchangeResponse>>>(Constant.ExchangeListResult);

        result.Result.ShouldBeOfType<OkObjectResult>();
        result.ShouldNotBeNull();
    }

    [Given(@"a valid exchange Id")]
    public void GivenAValidExchangeId()
    {
        m_ScenarioContext[Constant.ExchangeId] = Example.Entity.Exchange.ExchangeId;
    }

    [When(@"a GET request is sent to fetch the exchange by Id")]
    public async Task WhenGetExchangeById()
    {
        var id = m_ScenarioContext.Get<Guid>(Constant.ExchangeId);

        var result = await m_ExchangeController.GetOne(id);

        m_ScenarioContext[Constant.ExchangeByIdResult] = result;
    }

    [Then(@"the response ActionResult should indicate successful retrieval of the exchange")]
    public void ThenExchangeByIdShouldBeRetrieved()
    {
        var result = m_ScenarioContext.Get<ActionResult<ExchangeResponse>>(Constant.ExchangeByIdResult);
        result.Result.ShouldBeOfType<OkObjectResult>();
        result.ShouldNotBeNull();
    }

    [Given(@"a valid exchange between query")]
    public void GivenAValidExchangeBetweenQuery()
    {
        m_ScenarioContext[Constant.ExchangeBetweenQuery] = Example.Entity.Exchange.ExchangeBetweenQuery;
    }

    [When(@"a GET request is sent to fetch the exchange by currencies")]
    public async Task WhenGetExchangeByCurrencies()
    {
        var query = m_ScenarioContext.Get<ExchangeBetweenQuery>(Constant.ExchangeBetweenQuery);

        var result = await m_ExchangeController.GetByCurrencies(query);

        m_ScenarioContext[Constant.ExchangeByCurrenciesResult] = result;
    }

    [Then(@"the response ActionResult should indicate successful retrieval of the exchange for the given currencies")]
    public void ThenExchangeByCurrenciesShouldBeRetrieved()
    {
        var result = m_ScenarioContext.Get<ActionResult<ExchangeResponse>>(Constant.ExchangeByCurrenciesResult);
        result.Result.ShouldBeOfType<OkObjectResult>();
        result.ShouldNotBeNull();
    }

    [Given(@"a valid exchange make request")]
    public void GivenAValidExchangeMakeRequest()
    {
        m_ScenarioContext[Constant.ExchangeMakeRequest] = Example.Entity.Exchange.ExchangeMakeExchangeRequest;
    }

    [When(@"a POST request is sent to the exchange creation endpoint")]
    public async Task WhenPostExchangeMake()
    {
        var request = m_ScenarioContext.Get<ExchangeMakeExchangeRequest>(Constant.ExchangeMakeRequest);

        var result = await m_ExchangeController.MakeExchange(request);

        m_ScenarioContext[Constant.ExchangeMakeResult] = result;
    }

    [Then(@"the response ActionResult should indicate successful exchange creation")]
    public void ThenExchangeMakeShouldBeSuccessful()
    {
        var result = m_ScenarioContext.Get<ActionResult<ExchangeResponse>>(Constant.ExchangeMakeResult);
        result.Result.ShouldBeOfType<OkObjectResult>();
        result.ShouldNotBeNull();
    }

    [Given(@"a valid exchange update request and exchange Id")]
    public void GivenValidExchangeUpdateRequestAndId()
    {
        m_ScenarioContext[Constant.ExchangeId]            = Example.Entity.Exchange.ExchangeId;
        m_ScenarioContext[Constant.ExchangeUpdateRequest] = Example.Entity.Exchange.ExchangeUpdateRequest;
    }

    [When(@"a PUT request is sent to the exchange update endpoint")]
    public async Task WhenPutExchangeUpdate()
    {
        var id      = m_ScenarioContext.Get<Guid>(Constant.ExchangeId);
        var request = m_ScenarioContext.Get<ExchangeUpdateRequest>(Constant.ExchangeUpdateRequest);

        var result = await m_ExchangeController.Update(id, request);

        m_ScenarioContext[Constant.ExchangeUpdateResult] = result;
    }

    [Then(@"the response ActionResult should indicate successful exchange update")]
    public void ThenExchangeUpdateShouldBeSuccessful()
    {
        var result = m_ScenarioContext.Get<ActionResult<ExchangeResponse>>(Constant.ExchangeUpdateResult);
        result.Result.ShouldBeOfType<OkObjectResult>();
        result.ShouldNotBeNull();
    }
}

file static class Constant
{
    public const string FilterQuery                = "ExchangeFilterQuery";
    public const string GetAll                     = "ExchangeGetAll";
    public const string GivenId                    = "ExchangeGivenId";
    public const string GivenUpdate                = "ExchangeUpdate";
    public const string GetOne                     = "ExchangeGetOne";
    public const string ActionResult               = "ExchangeActionResult";
    public const string CurrencyFromCode           = "ExchangeCurrencyFromId";
    public const string CurrencyToCode             = "ExchangeCurrencyToId";
    public const string ExchangeFilterQuery        = "ExchangeFilterQuery";
    public const string ExchangeListResult         = "ExchangeListResult";
    public const string ExchangeId                 = "ExchangeId";
    public const string ExchangeByIdResult         = "ExchangeByIdResult";
    public const string ExchangeBetweenQuery       = "ExchangeBetweenQuery";
    public const string ExchangeByCurrenciesResult = "ExchangeByCurrenciesResult";
    public const string ExchangeMakeRequest        = "ExchangeMakeRequest";
    public const string ExchangeMakeResult         = "ExchangeMakeResult";
    public const string ExchangeUpdateRequest      = "ExchangeUpdateRequest";
    public const string ExchangeUpdateResult       = "ExchangeUpdateResult";
}
