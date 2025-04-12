using System.Diagnostics;
using System.Reflection;

using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Services;
using Bank.UserService.Test.Examples.Entities;

using Microsoft.AspNetCore.Mvc;

using Shouldly;

namespace Bank.UserService.Test.Steps;

[Binding]
public class AccountSteps(ScenarioContext context, IAccountService accountService, IAuthorizationService authorizationService)
{
    private readonly ScenarioContext       m_ScenarioContext      = context;
    private readonly IAccountService       m_AccountService       = accountService;
    private readonly IAuthorizationService m_AuthorizationService = authorizationService;

    [Given(@"account create request")]
    public void GivenAccountCreateRequest()
    {
        typeof(AuthorizationService).GetField("<UserId>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)
                                    ?.SetValue(m_AuthorizationService, Guid.Parse("5817c260-e4a9-4dc1-87d9-2fa12af157d9"));

        m_ScenarioContext[Constant.AccountCreateRequest] = Example.Entity.Account.CreateRequest;
    }

    [When(@"account is created in the database")]
    public async Task WhenAccountIsCreatedInTheDatabase()
    {
        var accountCreateRequest = m_ScenarioContext.Get<AccountCreateRequest>(Constant.AccountCreateRequest);

        Console.Write(accountCreateRequest.ClientId);
        Console.Write(accountCreateRequest.AccountTypeId);
        Console.Write(accountCreateRequest.CurrencyId);

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

        Debug.WriteLine(getAccountResult.ActionResult);

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

    [When(@"all cards are fetched for the account")]
    public async Task WhenAllCardsAreFetchedForTheAccount()
    {
        var accountId = m_ScenarioContext.Get<Guid>(Constant.AccountId);

        var cardResult = await m_AccountService.GetAllCards(accountId, new CardFilterQuery(), new Pageable());

        m_ScenarioContext[Constant.CardsResult] = cardResult;
    }

    [Then(@"all cards should be returned for the account")]
    public void ThenAllCardsShouldBeReturnedForTheAccount()
    {
        var cardResult = m_ScenarioContext.Get<Result<Page<CardResponse>>>(Constant.CardsResult);

        cardResult.ActionResult.ShouldBeOfType<OkObjectResult>();
        cardResult.Value.ShouldNotBeNull();

        cardResult.Value.Items.All(card => card.Account.Id == m_ScenarioContext.Get<Guid>(Constant.AccountId))
                  .ShouldBeTrue();
    }

    [When(@"all acounts are fetched")]
    public async Task WhenAllAcountsAreFetched()
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
    public const string CardsResult                  = "CardsResult";
    public const string Accounts                     = "Accounts";
}
