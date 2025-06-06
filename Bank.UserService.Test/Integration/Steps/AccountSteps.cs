using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.Permissions.Services;
using Bank.UserService.Controllers;
using Bank.UserService.Services;
using Bank.UserService.Test.Examples.Entities;
using Bank.UserService.Test.Integration.Services;

using Microsoft.AspNetCore.Mvc;

using Shouldly;

namespace Bank.UserService.Test.Steps;

[Binding]
public class AccountSteps(ScenarioContext context, IAccountService accountService, IAuthorizationServiceFactory authorizationServiceFactory, AccountController accountController)
{
    private readonly ScenarioContext              m_ScenarioContext             = context;
    private readonly IAccountService              m_AccountService              = accountService;
    private readonly IAuthorizationServiceFactory m_AuthorizationServiceFactory = authorizationServiceFactory;
    private readonly AccountController            m_AccountController           = accountController;

    [Given(@"account create request")]
    public void GivenAccountCreateRequest()
    {
        var instance = m_AuthorizationServiceFactory as TestAuthorizationServiceFactory;

        instance!.UserId = Guid.Parse("5817c260-e4a9-4dc1-87d9-2fa12af157d9");

        m_ScenarioContext[Constant.AccountCreateRequest] = Example.Entity.Account.CreateRequest;
    }

    [When(@"account is created in the database")]
    public async Task WhenAccountIsCreatedInTheDatabase()
    {
        var accountCreateRequest = m_ScenarioContext.Get<AccountCreateRequest>(Constant.AccountCreateRequest);

        var accountResult = await m_AccountService.Create(accountCreateRequest);

        m_ScenarioContext[Constant.AccountCreateResult] = accountResult;
    }

    [When(@"account is fetched by Id")]
    public async Task WhenAccountIsFetchedById()
    {
        var createAccountResult = m_ScenarioContext.Get<Result<AccountResponse>>(Constant.AccountCreateResult);

        var getAccountResult = createAccountResult.Value != null ? await m_AccountService.GetOne(createAccountResult.Value.Id) : Result.BadRequest<AccountResponse>();

        m_ScenarioContext[Constant.GetAccountResult] = getAccountResult;
    }

    [Then(@"account details should match the created account")]
    public void ThenAccountDetailsShouldMatchTheCreatedAccount()
    {
        var getAccountResult = m_ScenarioContext.Get<Result<AccountResponse>>(Constant.AccountCreateResult);

        getAccountResult.ActionResult.ShouldBeOfType<OkObjectResult>();
        getAccountResult.Value.ShouldNotBeNull();
        getAccountResult.Value.Name.ShouldBe(Example.Entity.Account.CreateRequest.Name);
        getAccountResult.Value.Balance.ShouldBe(Example.Entity.Account.CreateRequest.Balance);
        getAccountResult.Value.Status.ShouldBe(Example.Entity.Account.CreateRequest.Status);
        getAccountResult.Value.Currency.Id.ShouldBe(Example.Entity.Account.CreateRequest.CurrencyId);
        getAccountResult.Value.Client.Id.ShouldBe(Example.Entity.Account.CreateRequest.ClientId);
        getAccountResult.Value.Type.Id.ShouldBe(Example.Entity.Account.CreateRequest.AccountTypeId);
        getAccountResult.Value.DailyLimit.ShouldBe(Example.Entity.Account.CreateRequest.DailyLimit);
        getAccountResult.Value.MonthlyLimit.ShouldBe(Example.Entity.Account.CreateRequest.MonthlyLimit);
    }

    [Given(@"account update client request")]
    public void GivenAccountUpdateClientRequest()
    {
        m_ScenarioContext[Constant.AccountUpdateClientRequest] = Example.Entity.Account.UpdateClientRequest;
    }

    [Given(@"account Id")]
    public void GivenAccountId()
    {
        m_ScenarioContext[Constant.AccountId] = Example.Entity.Account.AccountId;
    }

    [When(@"account is updated with client request in the database")]
    public async Task WhenAccountIsUpdatedWithClientRequestInTheDatabase()
    {
        var accountUpdateRequest = m_ScenarioContext.Get<AccountUpdateClientRequest>(Constant.AccountUpdateClientRequest);

        var accountId = m_ScenarioContext.Get<Guid>(Constant.AccountId);

        var accountResult = await m_AccountService.Update(accountUpdateRequest, accountId);

        m_ScenarioContext[Constant.AccountUpdateResult] = accountResult;
    }

    [Then(@"account details in client request should match the updated account")]
    public void ThenAccountDetailsInClientRequestShouldMatchTheUpdatedAccount()
    {
        var accountUpdateResult = m_ScenarioContext.Get<Result<AccountResponse>>(Constant.AccountUpdateResult);

        accountUpdateResult.ActionResult.ShouldBeOfType<OkObjectResult>();
        accountUpdateResult.Value.ShouldNotBeNull();
        accountUpdateResult.Value.Name.ShouldBe(Example.Entity.Account.UpdateClientRequest.Name);
        accountUpdateResult.Value.DailyLimit.ShouldBe(Example.Entity.Account.UpdateClientRequest.DailyLimit);
        accountUpdateResult.Value.MonthlyLimit.ShouldBe(Example.Entity.Account.UpdateClientRequest.MonthlyLimit);
    }

    [Given(@"account update employee request")]
    public void GivenAccountUpdateEmployeeRequest()
    {
        m_ScenarioContext[Constant.AccountUpdateEmployeeRequest] = Example.Entity.Account.UpdateEmployeeRequest;
    }

    [When(@"account is updated with employee request in the database")]
    public async Task WhenAccountIsUpdatedWithEmployeeRequestInTheDatabase()
    {
        var accountUpdateRequest = m_ScenarioContext.Get<AccountUpdateEmployeeRequest>(Constant.AccountUpdateEmployeeRequest);

        var accountId = m_ScenarioContext.Get<Guid>(Constant.AccountId);

        var accountResult = await m_AccountService.Update(accountUpdateRequest, accountId);

        m_ScenarioContext[Constant.AccountUpdateResult] = accountResult;
    }

    [Then(@"account details in employee request should match the updated account")]
    public void ThenAccountDetailsInEmployeeRequestShouldMatchTheUpdatedAccount()
    {
        var accountUpdateResult = m_ScenarioContext.Get<Result<AccountResponse>>(Constant.AccountUpdateResult);

        accountUpdateResult.ActionResult.ShouldBeOfType<OkObjectResult>();
        accountUpdateResult.Value.ShouldNotBeNull();
        accountUpdateResult.Value.Status.ShouldBe(Example.Entity.Account.UpdateEmployeeRequest.Status);
    }

    [When(@"all accounts are fetched")]
    public async Task WhenAllAccountsAreFetched()
    {
        var accounts = await m_AccountService.GetAll(new AccountFilterQuery(), new Pageable());

        m_ScenarioContext[Constant.Accounts] = accounts;
    }

    [Then(@"all accounts should be returned")]
    public void ThenAllAccountsShouldBeReturned()
    {
        var accounts = m_ScenarioContext.Get<Result<Page<AccountResponse>>>(Constant.Accounts);

        accounts.ActionResult.ShouldBeOfType<OkObjectResult>();
        accounts.Value.ShouldNotBeNull();
        accounts.Value.Items.ShouldNotBeEmpty();
    }

    [Given(@"client Id")]
    public void GivenClientId()
    {
        m_ScenarioContext[Constant.IdForAccount] = Example.Entity.Client.Id2;
    }

    [When(@"all accounts are fetched from the database")]
    public async Task WhenAllAccountsAreFetchedFromTheDatabase()
    {
        var id                = m_ScenarioContext.Get<Guid>(Constant.IdForAccount);
        var getAccountsResult = await m_AccountService.GetAllForClient(id, new AccountFilterQuery(), new Pageable());

        m_ScenarioContext[Constant.AccountResult] = getAccountsResult;
    }

    [Then(@"all accounts should be returned for that client")]
    public void ThenAllAccountsShouldBeReturnedForThatClient()
    {
        var accountResult = m_ScenarioContext.Get<Result<Page<AccountResponse>>>(Constant.AccountResult);

        accountResult.ActionResult.ShouldBeOfType<OkObjectResult>();
        accountResult.Value.ShouldNotBeNull();
        accountResult.Value.Items.ShouldNotBeEmpty();
        accountResult.Value.Items.ShouldAllBe(account => account.Client.Id == m_ScenarioContext.Get<Guid>(Constant.IdForAccount));
    }

    [Given(@"a valid account create request")]
    public void GivenAValidAccountCreateRequest()
    {
        var instance = m_AuthorizationServiceFactory as TestAuthorizationServiceFactory;

        instance!.UserId = Guid.Parse("5817c260-e4a9-4dc1-87d9-2fa12af157d9");

        m_ScenarioContext[Constant.AccountCreateRequest] = Example.Entity.Account.CreateRequest;
    }

    [When(@"a POST request is sent to the account creation endpoint")]
    public async Task WhenAPostRequestIsSentToTheAccountCreationEndpoint()
    {
        var accountCreateRequest = m_ScenarioContext.Get<AccountCreateRequest>(Constant.AccountCreateRequest);

        var createAccountResult = await m_AccountController.Create(accountCreateRequest);

        m_ScenarioContext[Constant.AccountCreateResult] = createAccountResult;
    }

    [Then(@"the response ActionResult should indicate successful account creation")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulAccountCreation()
    {
        var createAccountResult = m_ScenarioContext.Get<ActionResult<AccountResponse>>(Constant.AccountCreateResult);

        createAccountResult.Result.ShouldBeOfType<OkObjectResult>();
        createAccountResult.ShouldNotBeNull();
    }

    [Given(@"a valid account client update request and account Id")]
    public void GivenAValidAccountClientUpdateRequestAndAccountId()
    {
        m_ScenarioContext[Constant.AccountId]            = Example.Entity.Account.AccountId;
        m_ScenarioContext[Constant.AccountClientRequest] = Example.Entity.Account.UpdateClientRequest;
    }

    [When(@"a PUT request is sent to the account client update endpoint")]
    public async Task WhenAPutRequestIsSentToTheAccountClientUpdateEndpoint()
    {
        var accountId = m_ScenarioContext.Get<Guid>(Constant.AccountId);

        var updateClientRequest = m_ScenarioContext.Get<AccountUpdateClientRequest>(Constant.AccountClientRequest);

        var updateAccountResult = await m_AccountController.Update(updateClientRequest, accountId);

        m_ScenarioContext[Constant.AccountUpdateResult] = updateAccountResult;
    }

    [Then(@"the response ActionResult should indicate successful account client update")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulAccountClientUpdate()
    {
        var updateAccountResult = m_ScenarioContext.Get<ActionResult<AccountResponse>>(Constant.AccountUpdateResult);

        updateAccountResult.Result.ShouldBeOfType<OkObjectResult>();
        updateAccountResult.ShouldNotBeNull();
    }

    [Given(@"a valid account employee update request and account Id")]
    public void GivenAValidAccountEmployeeUpdateRequestAndAccountId()
    {
        m_ScenarioContext[Constant.AccountId]              = Example.Entity.Account.AccountId;
        m_ScenarioContext[Constant.AccountEmployeeRequest] = Example.Entity.Account.UpdateEmployeeRequest;
    }

    [When(@"a PUT request is sent to the account employee update endpoint")]
    public async Task WhenAPutRequestIsSentToTheAccountEmployeeUpdateEndpoint()
    {
        var accountId = m_ScenarioContext.Get<Guid>(Constant.AccountId);

        var updateEmployeeRequest = m_ScenarioContext.Get<AccountUpdateEmployeeRequest>(Constant.AccountEmployeeRequest);

        var updateAccountResult = await m_AccountController.Update(updateEmployeeRequest, accountId);

        m_ScenarioContext[Constant.AccountUpdateResult] = updateAccountResult;
    }

    [Then(@"the response ActionResult should indicate successful account employee update")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulAccountEmployeeUpdate()
    {
        var updateAccountResult = m_ScenarioContext.Get<ActionResult<AccountResponse>>(Constant.AccountUpdateResult);

        updateAccountResult.Result.ShouldBeOfType<OkObjectResult>();
        updateAccountResult.ShouldNotBeNull();
    }

    [When(@"a GET request is sent to fetch all accounts")]
    public async Task WhenAGetRequestIsSentToFetchAllAccounts()
    {
        var getAccountsResult = await m_AccountController.GetAll(new AccountFilterQuery(), new Pageable());

        m_ScenarioContext[Constant.GetAccounts] = getAccountsResult;
    }

    [Then(@"the response ActionResult should indicate successful retrieval of all accounts")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulRetrievalOfAllAccounts()
    {
        var getAccountsResult = m_ScenarioContext.Get<ActionResult<Page<AccountResponse>>>(Constant.GetAccounts);

        getAccountsResult.Result.ShouldBeOfType<OkObjectResult>();
        getAccountsResult.ShouldNotBeNull();
    }

    [Given(@"a client Id to fetch related accounts")]
    public void GivenAClientIdToFetchRelatedAccounts()
    {
        m_ScenarioContext[Constant.Id] = Example.Entity.Client.Id2;
    }

    [When(@"a GET request is sent to fetch accounts for the client")]
    public async Task WhenAGetRequestIsSentToFetchAccountsForTheClient()
    {
        var clientId = m_ScenarioContext.Get<Guid>(Constant.Id);

        var getAccountsResult = await m_AccountController.GetAllAccounts(clientId, new AccountFilterQuery(), new Pageable());

        m_ScenarioContext[Constant.AccountsResult] = getAccountsResult;
    }

    [Then(@"the response should return all accounts for the client")]
    public void ThenTheResponseShouldReturnAllAccountsForTheClient()
    {
        var getAccountsResult = m_ScenarioContext.Get<ActionResult<Page<AccountResponse>>>(Constant.AccountsResult);

        getAccountsResult.Result.ShouldBeOfType<OkObjectResult>();
        getAccountsResult.ShouldNotBeNull();
    }

    [Given(@"an account Id to fetch")]
    public void GivenAnAccountIdToFetch()
    {
        m_ScenarioContext[Constant.Id] = Example.Entity.Account.AccountId;
    }

    [When(@"a GET request is sent to fetch the account by Id")]
    public async Task WhenAgetRequestIsSentToFetchTheAccountById()
    {
        var accountId = m_ScenarioContext.Get<Guid>(Constant.Id);

        var getAccountResult = await m_AccountController.GetOne(accountId);

        m_ScenarioContext[Constant.GetAccountResult] = getAccountResult;
    }

    [Then(@"the response ActionResult should indicate successful retrieval of the account")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulRetrievalOfTheAccount()
    {
        var getAccountResult = m_ScenarioContext.Get<ActionResult<AccountResponse>>(Constant.GetAccountResult);

        getAccountResult.Result.ShouldBeOfType<OkObjectResult>();
        getAccountResult.ShouldNotBeNull();
    }
}

file static class Constant
{
    public const string AccountCreateRequest         = "AccountCreateRequest";
    public const string AccountCreateResult          = "AccountCreateResult";
    public const string GetAccountResult             = "GetAccountResult";
    public const string AccountId                    = "AccountId";
    public const string AccountUpdateResult          = "AccountUpdateResult";
    public const string AccountUpdateClientRequest   = "AccountUpdateClientRequest";
    public const string AccountUpdateEmployeeRequest = "AccountUpdateEmployeeRequest";
    public const string Accounts                     = "Accounts";
    public const string AccountResult                = "AccountResult";
    public const string IdForAccount                 = "ClientIdForAccount";
    public const string Id                           = "ClientId";
    public const string AccountClientRequest         = "AccountClientRequest";
    public const string AccountEmployeeRequest       = "AccountEmployeeRequest";
    public const string GetAccounts                  = "GetAccounts";
    public const string AccountsResult               = "AccountsResult";
}
