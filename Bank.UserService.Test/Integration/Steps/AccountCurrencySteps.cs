using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.Permissions.Services;
using Bank.UserService.Controllers;
using Bank.UserService.Services;
using Bank.UserService.Test.Examples.Entities;
using Bank.UserService.Test.Integration.Services;

using Microsoft.AspNetCore.Mvc;

using Shouldly;

namespace Bank.UserService.Test.Integration.Steps;

[Binding]
public class AccountCurrencySteps(
    ScenarioContext              context,
    IAccountCurrencyService      accountCurrencyService,
    IAuthorizationServiceFactory authorizationServiceFactory,
    AccountCurrencyController    accountCurrencyController
)
{
    private readonly ScenarioContext              m_ScenarioContext             = context;
    private readonly IAccountCurrencyService      m_AccountCurrencyService      = accountCurrencyService;
    private readonly IAuthorizationServiceFactory m_AuthorizationServiceFactory = authorizationServiceFactory;
    private readonly AccountCurrencyController    m_AccountCurrencyController   = accountCurrencyController;

    [Given(@"account currency create request")]
    public void GivenAccountCurrencyCreateRequest()
    {
        var instance = m_AuthorizationServiceFactory as TestAuthorizationServiceFactory;

        instance!.UserId = Example.Entity.AccountCurrency.EmployeeId;

        m_ScenarioContext[Constant.AccountCurrencyCreateRequest] = Example.Entity.AccountCurrency.CreateRequest;
    }

    [When(@"account currency is created in the database")]
    public async Task WhenAccountCurrencyIsCreatedInTheDatabase()
    {
        var accountCurrencyCreateRequest = m_ScenarioContext.Get<AccountCurrencyCreateRequest>(Constant.AccountCurrencyCreateRequest);

        var accountCurrencyResult = await m_AccountCurrencyService.Create(accountCurrencyCreateRequest);

        m_ScenarioContext[Constant.AccountCurrencyCreateResult] = accountCurrencyResult;
    }

    [When(@"account currency is fetched by Id")]
    public async Task WhenAccountCurrencyIsFetchedById()
    {
        var createAccountCurrencyResult = m_ScenarioContext.Get<Result<AccountCurrencyResponse>>(Constant.AccountCurrencyCreateResult);

        var getAccountCurrencyResult = createAccountCurrencyResult.Value != null
                                       ? await m_AccountCurrencyService.GetOne(createAccountCurrencyResult.Value.Id)
                                       : Result.BadRequest<AccountCurrencyResponse>();

        m_ScenarioContext[Constant.AccountCurrencyCreateResult] = getAccountCurrencyResult;
    }

    [Then(@"account currency details should match the created account currency")]
    public void ThenAccountCurrencyDetailsShouldMatchTheCreatedAccountCurrency()
    {
        var getAccountCurrencyResult = m_ScenarioContext.Get<Result<AccountCurrencyResponse>>(Constant.AccountCurrencyCreateResult);

        getAccountCurrencyResult.ActionResult.ShouldBeOfType<OkObjectResult>();
        getAccountCurrencyResult.Value.ShouldNotBeNull();
        getAccountCurrencyResult.Value!.Account.Id.ShouldBe(Example.Entity.AccountCurrency.CreateRequest.AccountId);
        getAccountCurrencyResult.Value!.Currency.Id.ShouldBe(Example.Entity.AccountCurrency.CreateRequest.CurrencyId);
        getAccountCurrencyResult.Value!.Employee.Id.ShouldBe(Example.Entity.AccountCurrency.CreateRequest.EmployeeId);
        getAccountCurrencyResult.Value!.DailyLimit.ShouldBe(Example.Entity.AccountCurrency.CreateRequest.DailyLimit);
        getAccountCurrencyResult.Value!.MonthlyLimit.ShouldBe(Example.Entity.AccountCurrency.CreateRequest.MonthlyLimit);
    }

    [Given(@"account currency update request")]
    public void GivenAccountCurrencyUpdateRequest()
    {
        m_ScenarioContext[Constant.AccountCurrencyUpdateRequest] = Example.Entity.AccountCurrency.ClientUpdateRequest;
    }

    [Given(@"account currency Id")]
    public void GivenAccountCurrencyId()
    {
        m_ScenarioContext[Constant.AccountCurrencyId] = Example.Entity.AccountCurrency.AccountCurrencyId;
    }

    [When(@"account currency is updated in the database")]
    public async Task WhenAccountCurrencyIsUpdatedInTheDatabase()
    {
        var accountCurrencyUpdateRequest = m_ScenarioContext.Get<AccountCurrencyClientUpdateRequest>(Constant.AccountCurrencyUpdateRequest);
        var accountCurrencyId            = m_ScenarioContext.Get<Guid>(Constant.AccountCurrencyId);

        var accountCurrencyResult = await m_AccountCurrencyService.Update(accountCurrencyUpdateRequest, accountCurrencyId);

        m_ScenarioContext[Constant.AccountCurrencyUpdateResult] = accountCurrencyResult;
    }

    [Then(@"account currency details should match the updated account currency")]
    public void ThenAccountCurrencyDetailsShouldMatchTheUpdatedAccountCurrency()
    {
        var getAccountCurrencyResult = m_ScenarioContext.Get<Result<AccountCurrencyResponse>>(Constant.AccountCurrencyUpdateResult);

        getAccountCurrencyResult.ActionResult.ShouldBeOfType<OkObjectResult>();
        getAccountCurrencyResult.Value.ShouldNotBeNull();
        getAccountCurrencyResult.Value!.DailyLimit.ShouldBe(Example.Entity.AccountCurrency.ClientUpdateRequest.DailyLimit);
        getAccountCurrencyResult.Value!.MonthlyLimit.ShouldBe(Example.Entity.AccountCurrency.ClientUpdateRequest.MonthlyLimit);
    }

    [When(@"all account currencies are fetched")]
    public async Task WhenAllAccountCurrenciesAreFetched()
    {
        var accountCurrencies = await m_AccountCurrencyService.GetAll(new Pageable());

        m_ScenarioContext[Constant.AccountCurrencies] = accountCurrencies;
    }

    [Then(@"all account currencies should be returned")]
    public void ThenAllAccountCurrenciesShouldBeReturned()
    {
        var accountCurrenciesResult = m_ScenarioContext.Get<Result<Page<AccountCurrencyResponse>>>(Constant.AccountCurrencies);

        accountCurrenciesResult.ActionResult.ShouldBeOfType<OkObjectResult>();
        accountCurrenciesResult.Value.ShouldNotBeNull();
        accountCurrenciesResult.Value!.Items.ShouldNotBeEmpty();
    }

    [Given(@"a valid account currency create request")]
    public void GivenAValidAccountCurrencyCreateRequest()
    {
        var instance = m_AuthorizationServiceFactory as TestAuthorizationServiceFactory;

        instance!.UserId = Example.Entity.AccountCurrency.EmployeeId;

        m_ScenarioContext[Constant.AccountCurrencyCreateRequest] = Example.Entity.AccountCurrency.CreateRequest;
    }

    [When(@"a POST request is sent to the account currency creation endpoint")]
    public async Task WhenAPostRequestIsSentToTheAccountCurrencyCreationEndpoint()
    {
        var createRequest = m_ScenarioContext.Get<AccountCurrencyCreateRequest>(Constant.AccountCurrencyCreateRequest);

        var createResult = await m_AccountCurrencyController.Create(createRequest);

        m_ScenarioContext[Constant.AccountCurrencyCreateResult] = createResult;
    }

    [Then(@"the response ActionResult should indicate successful account currency creation")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulAccountCurrencyCreation()
    {
        var createResult = m_ScenarioContext.Get<ActionResult<AccountCurrencyResponse>>(Constant.AccountCurrencyCreateResult);

        createResult.Result.ShouldBeOfType<OkObjectResult>();
        createResult.ShouldNotBeNull();
    }

    [Given(@"a valid account currency update request and account currency Id")]
    public void GivenAValidAccountCurrencyUpdateRequestAndAccountCurrencyId()
    {
        m_ScenarioContext[Constant.AccountCurrencyId]            = Example.Entity.AccountCurrency.AccountCurrencyId;
        m_ScenarioContext[Constant.AccountCurrencyUpdateRequest] = Example.Entity.AccountCurrency.ClientUpdateRequest;
    }

    [When(@"a PUT request is sent to the account currency update endpoint")]
    public async Task WhenAPutRequestIsSentToTheAccountCurrencyUpdateEndpoint()
    {
        var accountCurrencyId = m_ScenarioContext.Get<Guid>(Constant.AccountCurrencyId);

        var updateRequest = m_ScenarioContext.Get<AccountCurrencyClientUpdateRequest>(Constant.AccountCurrencyUpdateRequest);

        var updateResult = await m_AccountCurrencyController.Update(updateRequest, accountCurrencyId);

        m_ScenarioContext[Constant.AccountCurrencyUpdateResult] = updateResult;
    }

    [Then(@"the response ActionResult should indicate successful account currency update")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulAccountCurrencyUpdate()
    {
        var updateResult = m_ScenarioContext.Get<ActionResult<AccountCurrencyResponse>>(Constant.AccountCurrencyUpdateResult);

        updateResult.Result.ShouldBeOfType<OkObjectResult>();
        updateResult.ShouldNotBeNull();
    }

    [When(@"a GET request is sent to fetch all account currencies")]
    public async Task WhenAGetRequestIsSentToFetchAllAccountCurrencies()
    {
        var getCurrenciesResult = await m_AccountCurrencyController.GetAll(new Pageable());

        m_ScenarioContext[Constant.GetAccountCurrencies] = getCurrenciesResult;
    }

    [Then(@"the response ActionResult should indicate successful retrieval of all account currencies")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulRetrievalOfAllAccountCurrencies()
    {
        var getCurrenciesResult = m_ScenarioContext.Get<ActionResult<Page<AccountCurrencyResponse>>>(Constant.GetAccountCurrencies);

        getCurrenciesResult.Result.ShouldBeOfType<OkObjectResult>();
        getCurrenciesResult.ShouldNotBeNull();
    }

    [Given(@"an account currency Id to fetch")]
    public void GivenAnAccountCurrencyIdToFetch()
    {
        m_ScenarioContext[Constant.AccountCurrencyId] = Example.Entity.AccountCurrency.AccountCurrencyId;
    }

    [When(@"a GET request is sent to fetch the account currency by Id")]
    public async Task WhenAGetRequestIsSentToFetchTheAccountCurrencyById()
    {
        var accountCurrencyId = m_ScenarioContext.Get<Guid>(Constant.AccountCurrencyId);

        var getCurrencyResult = await m_AccountCurrencyController.GetOne(accountCurrencyId);

        m_ScenarioContext[Constant.AccountCurrencyGetResult] = getCurrencyResult;
    }

    [Then(@"the response ActionResult should indicate successful retrieval of the account currency")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulRetrievalOfTheAccountCurrency()
    {
        var getCurrencyResult = m_ScenarioContext.Get<ActionResult<AccountCurrencyResponse>>(Constant.AccountCurrencyGetResult);

        getCurrencyResult.Result.ShouldBeOfType<OkObjectResult>();
        getCurrencyResult.ShouldNotBeNull();
    }
}

file static class Constant
{
    public const string AccountCurrencyCreateRequest = "AccountCurrencyCreateRequest";
    public const string AccountCurrencyCreateResult  = "AccountCurrencyCreateResult";
    public const string AccountCurrencyUpdateRequest = "AccountCurrencyUpdateRequest";
    public const string AccountCurrencyId            = "AccountCurrencyId";
    public const string AccountCurrencyUpdateResult  = "AccountCurrencyUpdateResult";
    public const string AccountCurrencies            = "AccountCurrencies";
    public const string GetAccountCurrencies         = "GetAccountCurrencies";
    public const string AccountCurrencyGetResult     = "AccountCurrencyGetResult";
}
