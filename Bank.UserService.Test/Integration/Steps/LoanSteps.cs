using Bank.Application.Domain;
using Bank.Application.Endpoints;
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
public class LoanSteps(ScenarioContext scenarioContext, ILoanService loanService, LoanController loanController)
{
    private readonly ScenarioContext m_ScenarioContext = scenarioContext;
    private readonly ILoanService    m_LoanService     = loanService;
    private readonly LoanController  m_LoanController  = loanController;

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

    [Then(@"loan details should match the updated loan")]
    public void ThenLoanDetailsShouldMatchTheUpdatedLoan()
    {
        var loanUpdateResult = m_ScenarioContext.Get<Result<LoanResponse>>(Constant.LoanUpdateResult);

        loanUpdateResult.ActionResult.ShouldBeOfType<OkObjectResult>();
        loanUpdateResult.ShouldNotBeNull();
        loanUpdateResult.Value?.Status.ShouldBe(Example.Entity.Loan.UpdateRequest.Status!.Value);
        //loanUpdateResult.Value?.MaturityDate.ShouldBe(DateOnly.FromDateTime(Example.Entity.Loan.UpdateRequest.MaturityDate!.Value)); Ovo se ne updatuje??
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

    [When(@"loan is provided by Id")]
    public async Task WhenLoanIsProvidedById()
    {
        var loanId = m_ScenarioContext.Get<Guid>(Constant.LoanId);

        var loan = await m_LoanService.GetOne(loanId);

        m_ScenarioContext[Constant.Loan] = loan;
    }

    [Then(@"loan details should be returned")]
    public void ThenLoanDetailsShouldBeReturned()
    {
        var loan = m_ScenarioContext.Get<Result<LoanResponse>>(Constant.Loan);

        loan.ActionResult.ShouldBeOfType<OkObjectResult>();
        loan.ShouldNotBeNull();
        loan.Value.ShouldNotBeNull();
        loan.Value!.Id.ShouldBe(Example.Entity.Loan.Id);
    }

    [Given(@"client Id which has loans")]
    public void GivenClientIdWhichHasLoans()
    {
        m_ScenarioContext[Constant.ClientId] = Example.Entity.Loan.ClientId;
    }

    [When(@"loans are fetched by client Id")]
    public async Task WhenLoansAreFetchedByClientId()
    {
        var clientId = m_ScenarioContext.Get<Guid>(Constant.ClientId);

        var loans = await m_LoanService.GetByClient(clientId, new Pageable());

        m_ScenarioContext[Constant.Loans] = loans;
    }

    [Then(@"all loans for the client should be returned")]
    public void ThenAllLoansForTheClientShouldBeReturned()
    {
        var loans = m_ScenarioContext.Get<Result<Page<LoanResponse>>>(Constant.Loans);

        loans.ActionResult.ShouldBeOfType<OkObjectResult>();
        loans.ShouldNotBeNull();
        loans.Value.ShouldNotBeNull();
        loans.Value!.Items.ShouldNotBeEmpty();
        loans.Value.Items.ShouldAllBe(x => x.Account.Client.Id == Example.Entity.Loan.ClientId);
    }

    [Given(@"a valid loan create request")]
    public void GivenAValidLoanCreateRequest()
    {
        m_ScenarioContext[Constant.LoanCreateRequest] = Example.Entity.Loan.Request;
    }

    [When(@"a POST request is sent to the loan creation endpoint")]
    public async Task WhenAPostRequestIsSentToTheLoanCreationEndpoint()
    {
        var createRequest = m_ScenarioContext.Get<LoanCreateRequest>(Constant.LoanCreateRequest);

        var createResult = await m_LoanController.Create(createRequest);

        m_ScenarioContext[Constant.LoanCreateResult] = createResult;
    }

    [Then(@"the response ActionResult should indicate successful loan creation")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulLoanCreation()
    {
        var createResult = m_ScenarioContext.Get<ActionResult<LoanResponse>>(Constant.LoanCreateResult);

        createResult.Result.ShouldBeOfType<OkObjectResult>();
        createResult.ShouldNotBeNull();
    }

    [Given(@"a valid loan Id and loan update request")]
    public void GivenAValidLoanIdAndLoanUpdateRequest()
    {
        m_ScenarioContext[Constant.LoanId]            = Example.Entity.Loan.Id;
        m_ScenarioContext[Constant.LoanUpdateRequest] = Example.Entity.Loan.UpdateRequest;
    }

    [When(@"a PUT request is sent to the loan update endpoint")]
    public async Task WhenAPutRequestIsSentToTheLoanUpdateEndpoint()
    {
        var loanId        = m_ScenarioContext.Get<Guid>(Constant.LoanId);
        var updateRequest = m_ScenarioContext.Get<LoanUpdateRequest>(Constant.LoanUpdateRequest);

        var updateResult = await m_LoanController.Update(updateRequest, loanId);

        m_ScenarioContext[Constant.LoanUpdateResult] = updateResult;
    }

    [Then(@"the response ActionResult should indicate successful loan update")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulLoanUpdate()
    {
        var updateResult = m_ScenarioContext.Get<ActionResult<LoanResponse>>(Constant.LoanUpdateResult);

        updateResult.Result.ShouldBeOfType<OkObjectResult>();
        updateResult.ShouldNotBeNull();
    }

    [Given(@"a valid loan filter query and pageable parameters")]
    public void GivenAValidLoanFilterQueryAndPageableParameters()
    {
        m_ScenarioContext[Constant.LoanFilterQuery] = new LoanFilterQuery();
        m_ScenarioContext[Constant.Pageable]        = new Pageable();
    }

    [When(@"a GET request is sent to fetch all loans")]
    public async Task WhenAGetRequestIsSentToFetchAllLoans()
    {
        var filterQuery = m_ScenarioContext.Get<LoanFilterQuery>(Constant.LoanFilterQuery);
        var pageable    = m_ScenarioContext.Get<Pageable>(Constant.Pageable);

        var getLoansResult = await m_LoanController.GetAll(filterQuery, pageable);

        m_ScenarioContext[Constant.GetLoans] = getLoansResult;
    }

    [Then(@"the response ActionResult should indicate successful retrieval of all loans")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulRetrievalOfAllLoans()
    {
        var getLoansResult = m_ScenarioContext.Get<ActionResult<Page<LoanResponse>>>(Constant.GetLoans);

        getLoansResult.Result.ShouldBeOfType<OkObjectResult>();
        getLoansResult.ShouldNotBeNull();
    }

    [Given(@"a valid loan Id")]
    public void GivenAValidLoanId()
    {
        m_ScenarioContext[Constant.LoanId] = Example.Entity.Loan.Id;
    }

    [When(@"a GET request is sent to fetch the loan by Id")]
    public async Task WhenAGetRequestIsSentToFetchTheLoanById()
    {
        var loanId = m_ScenarioContext.Get<Guid>(Constant.LoanId);

        var getLoanResult = await m_LoanController.GetOne(loanId);

        m_ScenarioContext[Constant.GetLoan] = getLoanResult;
    }

    [Then(@"the response ActionResult should indicate successful retrieval of the loan")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulRetrievalOfTheLoan()
    {
        var getLoanResult = m_ScenarioContext.Get<ActionResult<LoanResponse>>(Constant.GetLoan);

        getLoanResult.Result.ShouldBeOfType<OkObjectResult>();
        getLoanResult.ShouldNotBeNull();
    }

    [Given(@"a valid client Id")]
    public void GivenAValidClientId()
    {
        m_ScenarioContext[Constant.ClientId] = Example.Entity.Loan.ClientId;
    }

    [When(@"a GET request is sent to fetch loans by client Id")]
    public async Task WhenAGetRequestIsSentToFetchLoansByClientId()
    {
        var clientId = m_ScenarioContext.Get<Guid>(Constant.ClientId);

        var getLoansResult = await m_LoanController.GetByClient(clientId, new Pageable());

        m_ScenarioContext[Constant.GetLoansByClient] = getLoansResult;
    }

    [Then(@"the response ActionResult should indicate successful retrieval of all loans for the client")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulRetrievalOfAllLoansForTheClient()
    {
        var getLoansResult = m_ScenarioContext.Get<ActionResult<Page<LoanResponse>>>(Constant.GetLoansByClient);

        getLoansResult.Result.ShouldBeOfType<OkObjectResult>();
        getLoansResult.ShouldNotBeNull();
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
    public const string Loan              = "Loan";
    public const string ClientId          = "ClientId";
    public const string LoanFilterQuery   = "LoanFilterQuery";
    public const string Pageable          = "Pageable";
    public const string GetLoans          = "GetLoans";
    public const string GetLoan           = "GetLoan";
    public const string GetLoansByClient  = "GetLoansByClient";
}
