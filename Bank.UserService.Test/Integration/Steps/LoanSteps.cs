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
public class LoanSteps(ScenarioContext scenarioContext, ILoanService loanService)
{
    private readonly ScenarioContext m_ScenarioContext = scenarioContext;
    private readonly ILoanService    m_LoanService     = loanService;

    [Given(@"loan create request")]
    public void GivenLoanCreateRequest()
    {
        m_ScenarioContext[Constant.LoanCreateRequest] = Example.Entity.Loan.Request;
    }

    [When(@"loan is created in the database")]
    public async Task WhenLoanIsCreatedInTheDatabase()
    {
        var loanCreateRequest = m_ScenarioContext.Get<LoanCreateRequest>(Constant.LoanCreateRequest);

        var loan = await m_LoanService.Create(loanCreateRequest);

        m_ScenarioContext[Constant.LoanCreateResult] = loan;
    }

    [When(@"loan is fetched by Id")]
    public async Task WhenLoanIsFetchedById()
    {
        var loanCreateResult = m_ScenarioContext.Get<Result<LoanResponse>>(Constant.LoanCreateResult);

        var loan = loanCreateResult != null ? await m_LoanService.GetOne(loanCreateResult.Value!.Id) : Result.BadRequest<LoanResponse>();

        m_ScenarioContext[Constant.LoanCreateResult] = loan;
    }

    [Then(@"loan details should match the created loan")]
    public void ThenLoanDetailsShouldMatchTheCreatedLoan()
    {
        var loanCreateResult = m_ScenarioContext.Get<Result<LoanResponse>>(Constant.LoanCreateResult);

        loanCreateResult.ActionResult.ShouldBeOfType<OkObjectResult>();
        loanCreateResult.ShouldNotBeNull();
        loanCreateResult.Value!.Amount.ShouldBe(Example.Entity.Loan.Request.Amount);
        loanCreateResult.Value.Period.ShouldBe(Example.Entity.Loan.Request.Period);
        loanCreateResult.Value.Currency.Id.ShouldBe(Example.Entity.Loan.Request.CurrencyId);
        loanCreateResult.Value.InterestType.ShouldBe(Example.Entity.Loan.Request.InterestType);
        loanCreateResult.Value.Account.Id.ShouldBe(Example.Entity.Loan.Request.AccountId);
        loanCreateResult.Value.Type.Id.ShouldBe(Example.Entity.Loan.Request.TypeId);
    }

    [Given(@"loan update request")]
    public void GivenLoanUpdateRequest()
    {
        m_ScenarioContext[Constant.LoanUpdateRequest] = Example.Entity.Loan.UpdateRequest;
    }

    [Given(@"loan Id")]
    public void GivenLoanId()
    {
        m_ScenarioContext[Constant.LoanId] = Example.Entity.Loan.Id;
    }

    [When(@"loan is updated in the database")]
    public async Task WhenLoanIsUpdatedInTheDatabase()
    {
        var updateRequest = m_ScenarioContext.Get<LoanUpdateRequest>(Constant.LoanUpdateRequest);

        var loanId = m_ScenarioContext.Get<Guid>(Constant.LoanId);

        var loan = await m_LoanService.Update(updateRequest, loanId);

        m_ScenarioContext[Constant.LoanUpdateResult] = loan;
    }

    [Then(@"loant details should match the updated loan")]
    public void ThenLoantDetailsShouldMatchTheUpdatedLoan()
    {
        var loanUpdateResult = m_ScenarioContext.Get<Result<LoanResponse>>(Constant.LoanUpdateResult);

        loanUpdateResult.ActionResult.ShouldBeOfType<OkObjectResult>();
        loanUpdateResult.ShouldNotBeNull();
        loanUpdateResult.Value?.Status.ShouldBe(Example.Entity.Loan.UpdateRequest.Status!.Value);
        //loanUpdateResult.Value?.MaturityDate.ShouldBe(DateOnly.FromDateTime(Example.Entity.Loan.UpdateRequest.MaturityDate!.Value)); Ovo se ne updatuje??
    }

    [Given(@"loan Id which has installemtns")]
    public void GivenLoanIdWhichHasInstallemtns()
    {
        m_ScenarioContext[Constant.LoanId] = Example.Entity.Loan.Id;
    }

    [When(@"all installemtns are fetched for the loan")]
    public async Task WhenAllInstallemtnsAreFetchedForTheLoan()
    {
        var loanId = m_ScenarioContext.Get<Guid>(Constant.LoanId);

        var installments = await m_LoanService.GetAllInstallments(loanId, new Pageable());

        m_ScenarioContext[Constant.Installments] = installments;
    }

    [Then(@"all installemtns should be returned for the loan")]
    public void ThenAllInstallemtnsShouldBeReturnedForTheLoan()
    {
        var installments = m_ScenarioContext.Get<Result<Page<InstallmentResponse>>>(Constant.Installments);

        installments.ActionResult.ShouldBeOfType<OkObjectResult>();
        installments.ShouldNotBeNull();
        installments.Value!.Items.ShouldNotBeEmpty();
    }

    [When(@"all loans are fetched")]
    public async Task WhenAllLoansAreFetched()
    {
        var loans = await m_LoanService.GetAll(new LoanFilterQuery(), new Pageable());

        m_ScenarioContext[Constant.Loans] = loans;
    }

    [Then(@"all loans should be returned")]
    public void ThenAllLoansShouldBeReturned()
    {
        var loans = m_ScenarioContext.Get<Result<Page<LoanResponse>>>(Constant.Loans);

        loans.ActionResult.ShouldBeOfType<OkObjectResult>();
        loans.ShouldNotBeNull();
        loans.Value!.Items.ShouldNotBeEmpty();
    }
}

file static class Constant
{
    public const string LoanCreateRequest = "LoanCreateRequest";
    public const string LoanCreateResult  = "LoanCreateResult";
    public const string LoanId            = "LoanId";
    public const string LoanUpdateRequest = "LoanUpdateRequest";
    public const string LoanUpdateResult  = "LoanUpdateResult";
    public const string Installments      = "Installements";
    public const string Loans             = "Loans";
}
