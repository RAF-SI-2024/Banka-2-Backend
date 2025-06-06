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
public class LoanTypeSteps(ScenarioContext scenarioContext, ILoanTypeService loanTypeService, LoanTypeController loanTypeController)
{
    private readonly ScenarioContext    m_ScenarioContext    = scenarioContext;
    private readonly ILoanTypeService   m_LoanTypeService    = loanTypeService;
    private readonly LoanTypeController m_LoanTypeController = loanTypeController;

    [Given(@"loan type create request")]
    public void GivenLoanTypeCreateRequest()
    {
        m_ScenarioContext[Constant.LoanTypeCreateRequest] = Example.Entity.LoanType.Request;
    }

    [When(@"loan type is created in the database")]
    public async Task WhenLoanTypeIsCreatedInTheDatabase()
    {
        var loanTypeCreateRequest = m_ScenarioContext.Get<LoanTypeCreateRequest>(Constant.LoanTypeCreateRequest);

        var loanType = await m_LoanTypeService.Create(loanTypeCreateRequest);

        m_ScenarioContext[Constant.LoanTypeCreateResult] = loanType;
    }

    [When(@"loan type is fetched by Id")]
    public async Task WhenLoanTypeIsFetchedById()
    {
        var loanTypeCreateResult = m_ScenarioContext.Get<Result<LoanTypeResponse>>(Constant.LoanTypeCreateResult);

        var loanType = loanTypeCreateResult != null ? await m_LoanTypeService.GetOne(loanTypeCreateResult.Value!.Id) : Result.BadRequest<LoanTypeResponse>();

        m_ScenarioContext[Constant.LoanTypeCreateResult] = loanType;
    }

    [Then(@"loan type details should match the created loan type")]
    public void ThenLoanTypeDetailsShouldMatchTheCreatedLoanType()
    {
        var loanTypeCreateResult = m_ScenarioContext.Get<Result<LoanTypeResponse>>(Constant.LoanTypeCreateResult);

        loanTypeCreateResult.ActionResult.ShouldBeOfType<OkObjectResult>();
        loanTypeCreateResult.ShouldNotBeNull();
        loanTypeCreateResult.Value!.Name.ShouldBe(Example.Entity.LoanType.Request.Name);
        loanTypeCreateResult.Value.Margin.ShouldBe(Example.Entity.LoanType.Request.Margin);
    }

    [Given(@"loan type update request")]
    public void GivenLoanTypeUpdateRequest()
    {
        m_ScenarioContext[Constant.LoanTypeUpdateRequest] = Example.Entity.LoanType.UpdateRequest;
    }

    [Given(@"loan type Id")]
    public void GivenLoanTypeId()
    {
        m_ScenarioContext[Constant.LoanTypeId] = Example.Entity.LoanType.Id;
    }

    [When(@"loan type is updated in the database")]
    public async Task WhenLoanTypeIsUpdatedInTheDatabase()
    {
        var loanTypeUpdateRequest = m_ScenarioContext.Get<LoanTypeUpdateRequest>(Constant.LoanTypeUpdateRequest);

        var loanTypeId = m_ScenarioContext.Get<Guid>(Constant.LoanTypeId);

        var loanType = await m_LoanTypeService.Update(loanTypeUpdateRequest, loanTypeId);

        m_ScenarioContext[Constant.LoanTypeUpdateResult] = loanType;
    }

    [Then(@"loant type details should match the updated loan type")]
    public void ThenLoantTypeDetailsShouldMatchTheUpdatedLoanType()
    {
        var loanTypeUpdateResult = m_ScenarioContext.Get<Result<LoanTypeResponse>>(Constant.LoanTypeUpdateResult);

        loanTypeUpdateResult.ActionResult.ShouldBeOfType<OkObjectResult>();
        loanTypeUpdateResult.ShouldNotBeNull();
        loanTypeUpdateResult.Value?.Name.ShouldBe(Example.Entity.LoanType.UpdateRequest.Name);
        loanTypeUpdateResult.Value?.Margin.ShouldBe(Example.Entity.LoanType.UpdateRequest.Margin!.Value);
    }

    [When(@"all loan types are fetched from the database")]
    public async Task WhenAllLoanTypesAreFetchedFromTheDatabase()
    {
        var loanTypes = await m_LoanTypeService.GetAll(new Pageable());

        m_ScenarioContext[Constant.LoanTypes] = loanTypes;
    }

    [Then(@"all loan types should be returned")]
    public void ThenAllLoanTypesShouldBeReturned()
    {
        var loanTypes = m_ScenarioContext.Get<Result<Page<LoanTypeResponse>>>(Constant.LoanTypes);

        loanTypes.ActionResult.ShouldBeOfType<OkObjectResult>();
        loanTypes.ShouldNotBeNull();
        loanTypes.Value?.Items.ShouldNotBeEmpty();
    }

    [Given(@"a valid loan type create request")]
    public void GivenAValidLoanTypeCreateRequest()
    {
        m_ScenarioContext[Constant.LoanTypeCreateRequest] = Example.Entity.LoanType.Request;
    }

    [When(@"a POST request is sent to the loan type creation endpoint")]
    public async Task WhenAPostRequestIsSentToTheLoanTypeCreationEndpoint()
    {
        var createRequest = m_ScenarioContext.Get<LoanTypeCreateRequest>(Constant.LoanTypeCreateRequest);

        var createResult = await m_LoanTypeController.Create(createRequest);

        m_ScenarioContext[Constant.LoanTypeCreateResult] = createResult;
    }

    [Then(@"the response ActionResult should indicate successful loan type creation")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulLoanTypeCreation()
    {
        var createResult = m_ScenarioContext.Get<ActionResult<LoanTypeResponse>>(Constant.LoanTypeCreateResult);

        createResult.Result.ShouldBeOfType<OkObjectResult>();
        createResult.ShouldNotBeNull();
    }

    [Given(@"a valid loan type update request and loan type Id")]
    public void GivenAValidLoanTypeUpdateRequestAndLoanTypeId()
    {
        m_ScenarioContext[Constant.LoanTypeId]            = Example.Entity.LoanType.Id;
        m_ScenarioContext[Constant.LoanTypeUpdateRequest] = Example.Entity.LoanType.UpdateRequest;
    }

    [When(@"a PUT request is sent to the loan type update endpoint")]
    public async Task WhenAPutRequestIsSentToTheLoanTypeUpdateEndpoint()
    {
        var loanTypeId    = m_ScenarioContext.Get<Guid>(Constant.LoanTypeId);
        var updateRequest = m_ScenarioContext.Get<LoanTypeUpdateRequest>(Constant.LoanTypeUpdateRequest);

        var updateResult = await m_LoanTypeController.Update(updateRequest, loanTypeId);

        m_ScenarioContext[Constant.LoanTypeUpdateResult] = updateResult;
    }

    [Then(@"the response ActionResult should indicate successful loan type update")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulLoanTypeUpdate()
    {
        var updateResult = m_ScenarioContext.Get<ActionResult<LoanTypeResponse>>(Constant.LoanTypeUpdateResult);

        updateResult.Result.ShouldBeOfType<OkObjectResult>();
        updateResult.ShouldNotBeNull();
    }

    [When(@"a GET request is sent to fetch all loan types")]
    public async Task WhenAGetRequestIsSentToFetchAllLoanTypes()
    {
        var getLoanTypesResult = await m_LoanTypeController.GetAll(new Pageable());

        m_ScenarioContext[Constant.GetLoanTypes] = getLoanTypesResult;
    }

    [Then(@"the response ActionResult should indicate successful retrieval of all loan types")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulRetrievalOfAllLoanTypes()
    {
        var getLoanTypesResult = m_ScenarioContext.Get<ActionResult<Page<LoanTypeResponse>>>(Constant.GetLoanTypes);

        getLoanTypesResult.Result.ShouldBeOfType<OkObjectResult>();
        getLoanTypesResult.ShouldNotBeNull();
    }

    [Given(@"a valid loan type Id")]
    public void GivenAValidLoanTypeId()
    {
        m_ScenarioContext[Constant.LoanTypeId] = Example.Entity.LoanType.Id;
    }

    [When(@"a GET request is sent to fetch a loan type by Id")]
    public async Task WhenAGetRequestIsSentToFetchALoanTypeById()
    {
        var loanTypeId = m_ScenarioContext.Get<Guid>(Constant.LoanTypeId);

        var getLoanTypeResult = await m_LoanTypeController.GetOne(loanTypeId);

        m_ScenarioContext[Constant.GetLoanType] = getLoanTypeResult;
    }

    [Then(@"the response ActionResult should indicate successful retrieval of the loan type")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulRetrievalOfTheLoanType()
    {
        var getLoanTypeResult = m_ScenarioContext.Get<ActionResult<LoanTypeResponse>>(Constant.GetLoanType);

        getLoanTypeResult.Result.ShouldBeOfType<OkObjectResult>();
        getLoanTypeResult.ShouldNotBeNull();
    }
}

file static class Constant
{
    public const string LoanTypeCreateRequest = "LoanTypeCreateRequest";
    public const string LoanTypeCreateResult  = "LoantypeCreateResult";
    public const string LoanTypeUpdateRequest = "LoanTypeUpdateRequest";
    public const string LoanTypeUpdateResult  = "LoanTypeUpdateResult";
    public const string LoanTypeId            = "LoanTypeId";
    public const string LoanTypes             = "LoanTypes";
    public const string GetLoanTypes          = "GetLoanTypes";
    public const string GetLoanType           = "GetLoanType";
}
