using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Services;
using Bank.UserService.Test.Examples.Entities;

using Microsoft.AspNetCore.Mvc;

using Shouldly;

namespace Bank.UserService.Test.Steps;

[Binding]
public class InstallmentSteps(ScenarioContext scenarioContext, IInstallmentService installmentService)
{
    private readonly ScenarioContext     m_ScenarioContext    = scenarioContext;
    private readonly IInstallmentService m_InstallmentService = installmentService;

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
}
