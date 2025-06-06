using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.UserService.Services;
using Bank.UserService.Test.Examples.Entities;

using Microsoft.AspNetCore.Mvc;

using Shouldly;

namespace Bank.UserService.Test.Integration.Steps;

[Binding]
public class BankSteps(ScenarioContext scenarioContext, IBankService bankService)
{
    private readonly IBankService    m_BankService     = bankService;
    private readonly ScenarioContext m_ScenarioContext = scenarioContext;

    [When(@"all banks are fetched")]
    public async Task WhenAllBanksAreFetched()
    {
        var banks = await m_BankService.GetAll(new BankFilterQuery(), new Pageable());

        m_ScenarioContext[Constant.Banks] = banks;
    }

    [Then(@"all banks should be returned")]
    public void ThenAllBanksShouldBeReturned()
    {
        var banks = m_ScenarioContext.Get<Result<Page<BankResponse>>>(Constant.Banks);

        banks.ActionResult.ShouldBeOfType<OkObjectResult>();
        banks.Value.ShouldNotBeNull();
        banks.Value.Items.ShouldNotBeEmpty();
    }

    [Given(@"bank Id")]
    public void GivenBankId()
    {
        m_ScenarioContext[Constant.Banks] = Example.Entity.Bank.BankId;
    }

    [When(@"bank is fetched by Id")]
    public async Task WhenBankIsFetchedById()
    {
        var bankId = m_ScenarioContext.Get<Guid>(Constant.Banks);

        var bank = await m_BankService.GetOne(bankId);

        m_ScenarioContext[Constant.Bank] = bank;
    }

    [Then(@"bank details should match the expected bank")]
    public void ThenBankDetailsShouldMatchTheExpectedBank()
    {
        var bank = m_ScenarioContext.Get<Result<BankResponse>>(Constant.Bank);

        bank.ActionResult.ShouldBeOfType<OkObjectResult>();
        bank.Value.ShouldNotBeNull();
        bank.Value.Id.ShouldBe(m_ScenarioContext.Get<Guid>(Constant.Banks));
    }
}

file static class Constant
{
    public const string Banks = "Banks";
    public const string Bank  = "Bank";
}
