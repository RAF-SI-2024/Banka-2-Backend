using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Controllers;
using Bank.UserService.Services;
using Bank.UserService.Test.Examples.Entities;

using Microsoft.AspNetCore.Mvc;

using Shouldly;

namespace Bank.UserService.Test.Steps;

[Binding]
public class InstallmentSteps(ScenarioContext scenarioContext, IInstallmentService installmentService, InstallmentController installmentController)
{
    private readonly ScenarioContext       m_ScenarioContext       = scenarioContext;
    private readonly IInstallmentService   m_InstallmentService    = installmentService;
    private readonly InstallmentController m_InstallmentController = installmentController;

    [Given(@"installment create request")]
    public void GivenInstallmentCreateRequest()
    {
        m_ScenarioContext[Constant.InstallmentCreateRequest] = Example.Entity.Installment.Request;
    }

    [When(@"installment is created in the database")]
    public async Task WhenInstallmentIsCreatedInTheDatabase()
    {
        var installmentCreateRequest = m_ScenarioContext.Get<InstallmentCreateRequest>(Constant.InstallmentCreateRequest);

        var installment = await m_InstallmentService.Create(installmentCreateRequest);

        m_ScenarioContext[Constant.InstallmentCreateResult] = installment;
    }

    [When(@"installment is fetched by Id")]
    public async Task WhenInstallmentIsFetchedById()
    {
        var installmentCreateResult = m_ScenarioContext.Get<Result<InstallmentResponse>>(Constant.InstallmentCreateResult);

        var installment = installmentCreateResult != null ? await m_InstallmentService.GetOne(installmentCreateResult.Value!.Id) : Result.BadRequest<InstallmentResponse>();

        m_ScenarioContext[Constant.InstallmentCreateResult] = installment;
    }

    [Then(@"installment details should match the created installment")]
    public void ThenInstallmentDetailsShouldMatchTheCreatedInstallment()
    {
        var installmentCreateResult = m_ScenarioContext.Get<Result<InstallmentResponse>>(Constant.InstallmentCreateResult);

        installmentCreateResult.ActionResult.ShouldBeOfType<OkObjectResult>();
        installmentCreateResult.ShouldNotBeNull();
        installmentCreateResult.Value!.Loan.Id.ShouldBe(Example.Entity.Installment.Request.LoanId);
        installmentCreateResult.Value.InterestRate.ShouldBe(Example.Entity.Installment.Request.InterestRate);
        installmentCreateResult.Value.ExpectedDueDate.ShouldBe(Example.Entity.Installment.Request.ExpectedDueDate);
        installmentCreateResult.Value.ActualDueDate.ShouldBe(Example.Entity.Installment.Request.ActualDueDate);
        installmentCreateResult.Value.Status.ShouldBe(Example.Entity.Installment.Request.Status);
    }

    [Given(@"installment update request")]
    public void GivenInstallmentUpdateRequest()
    {
        m_ScenarioContext[Constant.InstallmentUpdateRequest] = Example.Entity.Installment.UpdateRequest;
    }

    [Given(@"installment Id")]
    public void GivenInstallmentId()
    {
        m_ScenarioContext[Constant.InstallmentId] = Example.Entity.Installment.InstallmentId;
    }

    [When(@"installment is updated with request in the database")]
    public async Task WhenInstallmentIsUpdatedWithRequestInTheDatabase()
    {
        var installmentUpdateRequest = m_ScenarioContext.Get<InstallmentUpdateRequest>(Constant.InstallmentUpdateRequest);

        var installmentId = m_ScenarioContext.Get<Guid>(Constant.InstallmentId);

        var installment = await m_InstallmentService.Update(installmentUpdateRequest, installmentId);

        m_ScenarioContext[Constant.InstallmentUpdateResult] = installment;
    }

    [Then(@"installment details in request should match the updated installment")]
    public void ThenInstallmentDetailsInRequestShouldMatchTheUpdatedInstallment()
    {
        var installmentUpdateResult = m_ScenarioContext.Get<Result<InstallmentResponse>>(Constant.InstallmentUpdateResult);

        installmentUpdateResult.ActionResult.ShouldBeOfType<OkObjectResult>();
        installmentUpdateResult.ShouldNotBeNull();
        installmentUpdateResult.Value?.ActualDueDate.ShouldBe(DateOnly.FromDateTime((DateTime)Example.Entity.Installment.UpdateRequest.ActualDueDate!));
        installmentUpdateResult.Value?.Status.ShouldBe(Example.Entity.Installment.UpdateRequest.Status!.Value);
    }

    [Given(@"loan Id for installments")]
    public void GivenLoanIdForInstallments()
    {
        m_ScenarioContext[Constant.LoanId] = Example.Entity.Installment.LoanId;
    }

    [When(@"all installments are fetched for the account")]
    public async Task WhenAllInstallmentsAreFetchedForTheAccount()
    {
        var loanId = m_ScenarioContext.Get<Guid>(Constant.LoanId);

        var installments = await m_InstallmentService.GetAllByLoanId(loanId, new Pageable());

        m_ScenarioContext[Constant.Installments] = installments;
    }

    [Then(@"all installments should be returned for the account")]
    public void ThenAllInstallmentsShouldBeReturnedForTheAccount()
    {
        var installments = m_ScenarioContext.Get<Result<Page<InstallmentResponse>>>(Constant.Installments);

        installments.ActionResult.ShouldBeOfType<OkObjectResult>();
        installments.ShouldNotBeNull();

        installments.Value!.Items.All(i => i.Loan.Id == m_ScenarioContext.Get<Guid>(Constant.LoanId))
                    .ShouldBeTrue();
    }

    [Given(@"loan Id which has installments")]
    public void GivenLoanIdWhichHasInstallments()
    {
        m_ScenarioContext[Constant.LoanId] = Example.Entity.Loan.Id;
    }

    [When(@"all installments are fetched for the loan")]
    public async Task WhenAllInstallmentsAreFetchedForTheLoan()
    {
        var loanId = m_ScenarioContext.Get<Guid>(Constant.LoanId);

        var installments = await m_InstallmentService.GetAllByLoanId(loanId, new Pageable());

        m_ScenarioContext[Constant.Installments] = installments;
    }

    [Then(@"all installments should be returned for the loan")]
    public void ThenAllInstallmentsShouldBeReturnedForTheLoan()
    {
        var installments = m_ScenarioContext.Get<Result<Page<InstallmentResponse>>>(Constant.Installments);

        installments.ActionResult.ShouldBeOfType<OkObjectResult>();
        installments.ShouldNotBeNull();
        installments.Value!.Items.ShouldNotBeEmpty();
    }

    [When(@"installment is provided by Id")]
    public async Task WhenInstallmentIsProvidedById()
    {
        var installmentId = m_ScenarioContext.Get<Guid>(Constant.InstallmentId);

        var installment = await m_InstallmentService.GetOne(installmentId);

        m_ScenarioContext[Constant.Installment] = installment;
    }

    [Then(@"installment details should be returned")]
    public void ThenInstallmentDetailsShouldBeReturned()
    {
        var installment = m_ScenarioContext.Get<Result<InstallmentResponse>>(Constant.Installment);

        installment.ActionResult.ShouldBeOfType<OkObjectResult>();
        installment.ShouldNotBeNull();
        installment.Value.ShouldNotBeNull();
        installment.Value!.Id.ShouldBe(Example.Entity.Installment.InstallmentId);
    }

    [Given(@"a valid installment create request")]
    public void GivenAValidInstallmentCreateRequest()
    {
        m_ScenarioContext[Constant.InstallmentCreateRequest] = Example.Entity.Installment.Request;
    }

    [When(@"a POST request is sent to the installment creation endpoint")]
    public async Task WhenAPostRequestIsSentToTheInstallmentCreationEndpoint()
    {
        var createRequest = m_ScenarioContext.Get<InstallmentCreateRequest>(Constant.InstallmentCreateRequest);

        var createResult = await m_InstallmentController.Create(createRequest);

        m_ScenarioContext[Constant.InstallmentCreateResult] = createResult;
    }

    [Then(@"the response ActionResult should indicate successful installment creation")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulInstallmentCreation()
    {
        var createResult = m_ScenarioContext.Get<ActionResult<InstallmentResponse>>(Constant.InstallmentCreateResult);

        createResult.Result.ShouldBeOfType<OkObjectResult>();
        createResult.ShouldNotBeNull();
    }

    [Given(@"a valid installment update request and installment Id")]
    public void GivenAValidInstallmentUpdateRequestAndInstallmentId()
    {
        m_ScenarioContext[Constant.InstallmentId]            = Example.Entity.Installment.InstallmentId;
        m_ScenarioContext[Constant.InstallmentUpdateRequest] = Example.Entity.Installment.UpdateRequest;
    }

    [When(@"a PUT request is sent to the installment update endpoint")]
    public async Task WhenAPutRequestIsSentToTheInstallmentUpdateEndpoint()
    {
        var installmentId = m_ScenarioContext.Get<Guid>(Constant.InstallmentId);
        var updateRequest = m_ScenarioContext.Get<InstallmentUpdateRequest>(Constant.InstallmentUpdateRequest);

        var updateResult = await m_InstallmentController.Update(updateRequest, installmentId);

        m_ScenarioContext[Constant.InstallmentUpdateResult] = updateResult;
    }

    [Then(@"the response ActionResult should indicate successful installment update")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulInstallmentUpdate()
    {
        var updateResult = m_ScenarioContext.Get<ActionResult<InstallmentResponse>>(Constant.InstallmentUpdateResult);

        updateResult.Result.ShouldBeOfType<OkObjectResult>();
        updateResult.ShouldNotBeNull();
    }

    [Given(@"a valid loan Id with installments")]
    public void GivenAValidLoanIdWithInstallments()
    {
        m_ScenarioContext[Constant.LoanId] = Example.Entity.Loan.Id;
    }

    [When(@"a GET request is sent to fetch installments for the loan")]
    public async Task WhenAGetRequestIsSentToFetchInstallmentsForTheLoan()
    {
        var loanId = m_ScenarioContext.Get<Guid>(Constant.LoanId);

        var getInstallmentsResult = await m_InstallmentController.GetAllByLoan(loanId, new Pageable());

        m_ScenarioContext[Constant.GetInstallments] = getInstallmentsResult;
    }

    [Then(@"the response ActionResult should indicate successful retrieval of installments for the loan")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulRetrievalOfInstallmentsForTheLoan()
    {
        var getInstallmentsResult = m_ScenarioContext.Get<ActionResult<Page<InstallmentResponse>>>(Constant.GetInstallments);

        getInstallmentsResult.Result.ShouldBeOfType<OkObjectResult>();
        getInstallmentsResult.ShouldNotBeNull();
    }

    [Given(@"a valid installment Id")]
    public void GivenAValidInstallmentId()
    {
        m_ScenarioContext[Constant.InstallmentId] = Example.Entity.Installment.InstallmentId;
    }

    [When(@"a GET request is sent to fetch the installment by Id")]
    public async Task WhenAGetRequestIsSentToFetchTheInstallmentById()
    {
        var installmentId = m_ScenarioContext.Get<Guid>(Constant.InstallmentId);

        var getInstallmentResult = await m_InstallmentController.GetOne(installmentId);

        m_ScenarioContext[Constant.GetInstallment] = getInstallmentResult;
    }

    [Then(@"the response ActionResult should indicate successful retrieval of the installment")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulRetrievalOfTheInstallment()
    {
        var getInstallmentResult = m_ScenarioContext.Get<ActionResult<InstallmentResponse>>(Constant.GetInstallment);

        getInstallmentResult.Result.ShouldBeOfType<OkObjectResult>();
        getInstallmentResult.ShouldNotBeNull();
    }
}

file static class Constant
{
    public const string InstallmentCreateRequest = "InstallmentCreateRequest";
    public const string InstallmentCreateResult  = "InstallmentCreateResult";
    public const string InstallmentUpdateRequest = "InstallmentUpdateRequest";
    public const string InstallmentUpdateResult  = "InstallmentUpdateResult";
    public const string InstallmentId            = "InstallmentId";
    public const string LoanId                   = "LoanId";
    public const string Installments             = "Installments";
    public const string Installment              = "Installment";
    public const string GetInstallments          = "GetInstallments";
    public const string GetInstallment           = "GetInstallment";
}
