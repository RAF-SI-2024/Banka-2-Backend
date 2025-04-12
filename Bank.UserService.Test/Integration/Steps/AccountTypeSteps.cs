using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.UserService.Database.Seeders;
using Bank.UserService.Services;

using Microsoft.AspNetCore.Mvc;

using Shouldly;

namespace Bank.UserService.Test.Steps;

[Binding]
public class AccountTypeSteps(ScenarioContext scenarioContext, IAccountTypeService accountTypeService)
{
    private readonly ScenarioContext     m_ScenarioContext    = scenarioContext;
    private readonly IAccountTypeService m_AccountTypeService = accountTypeService;

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
        m_ScenarioContext[Constant.AccountTypeId] = Seeder.AccountType.CheckingAccount.Id;
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
}

file static class Constant
{
    public const string AccountTypes  = "AccountTypes";
    public const string AccountTypeId = "AccountTypeId";
    public const string AccountType   = "AccountType";
}
