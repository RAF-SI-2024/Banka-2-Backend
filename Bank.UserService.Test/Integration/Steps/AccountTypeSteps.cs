using Bank.Application.Domain;
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
public class AccountTypeSteps(ScenarioContext scenarioContext, IAccountTypeService accountTypeService, AccountTypeController accountTypeController)
{
    private readonly ScenarioContext       m_ScenarioContext       = scenarioContext;
    private readonly IAccountTypeService   m_AccountTypeService    = accountTypeService;
    private readonly AccountTypeController m_AccountTypeController = accountTypeController;

    [When(@"I get all account types")]
    public async Task WhenIGetAllAccountTypes()
    {
        var accountTypes = await m_AccountTypeService.GetAll(new AccountTypeFilterQuery(), new Pageable());

        m_ScenarioContext[Constant.AccountTypes] = accountTypes;
    }

    [Then(@"I should get all account types")]
    public void ThenIShouldGetAllAccountTypes()
    {
        var accountTypes = m_ScenarioContext.Get<Result<Page<AccountTypeResponse>>>(Constant.AccountTypes);

        accountTypes.ActionResult.ShouldBeOfType<OkObjectResult>();
        accountTypes.ShouldNotBeNull();
        accountTypes.Value?.Items.ShouldNotBeEmpty();
    }

    [Given(@"I have an account type")]
    public void GivenIHaveAnAccountType()
    {
        m_ScenarioContext[Constant.AccountTypeId] = Example.Entity.AccountType.Id;
    }

    [When(@"I get one account type")]
    public void WhenIGetOneAccountType()
    {
        var accountId = m_ScenarioContext.Get<Guid>(Constant.AccountTypeId);

        var accountType = m_AccountTypeService.GetOne(accountId)
                                              .Result;

        m_ScenarioContext[Constant.AccountType] = accountType;
    }

    [Then(@"I should get one account type")]
    public void ThenIShouldGetOneAccountType()
    {
        var accountType = m_ScenarioContext.Get<Result<AccountTypeResponse>>(Constant.AccountType);

        accountType.ActionResult.ShouldBeOfType<OkObjectResult>();
        accountType.Value.ShouldNotBeNull();
        accountType.Value.Id.ShouldBe(m_ScenarioContext.Get<Guid>(Constant.AccountTypeId));
    }

    [When(@"a GET request is sent to fetch all account types")]
    public async Task WhenAGetRequestIsSentToFetchAllAccountTypes()
    {
        var getAccountTypesResult = await m_AccountTypeController.GetAll(new AccountTypeFilterQuery(), new Pageable());

        m_ScenarioContext[Constant.GetAccountTypes] = getAccountTypesResult;
    }

    [Then(@"the response ActionResult should indicate successful retrieval of all account types")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulRetrievalOfAllAccountTypes()
    {
        var getAccountTypesResult = m_ScenarioContext.Get<ActionResult<Page<AccountTypeResponse>>>(Constant.GetAccountTypes);

        getAccountTypesResult.Result.ShouldBeOfType<OkObjectResult>();
        getAccountTypesResult.ShouldNotBeNull();
    }

    [Given(@"an account type Id to fetch")]
    public void GivenAnAccountTypeIdToFetch()
    {
        m_ScenarioContext[Constant.AccountTypeId] = Example.Entity.AccountType.Id;
    }

    [When(@"a GET request is sent to fetch one account type")]
    public async Task WhenAGetRequestIsSentToFetchOneAccountType()
    {
        var accountTypeId = m_ScenarioContext.Get<Guid>(Constant.AccountTypeId);

        var getAccountTypeResult = await m_AccountTypeController.GetOne(accountTypeId);

        m_ScenarioContext[Constant.GetAccountType] = getAccountTypeResult;
    }

    [Then(@"the response ActionResult should indicate successful retrieval of the account type")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulRetrievalOfTheAccountType()
    {
        var getAccountTypeResult = m_ScenarioContext.Get<ActionResult<AccountTypeResponse>>(Constant.GetAccountType);

        getAccountTypeResult.Result.ShouldBeOfType<OkObjectResult>();
        getAccountTypeResult.ShouldNotBeNull();
    }
}

file static class Constant
{
    public const string AccountTypes    = "AccountTypes";
    public const string AccountTypeId   = "AccountTypeId";
    public const string AccountType     = "AccountType";
    public const string GetAccountTypes = "GetAccountTypes";
    public const string GetAccountType  = "GetAccountType";
}
