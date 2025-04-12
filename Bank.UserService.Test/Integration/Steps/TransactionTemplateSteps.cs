using System.Reflection;

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
public class TransactionTemplateSteps(ScenarioContext scenarioContext, ITransactionTemplateService transactionTemplateService, IAuthorizationService authorizationService)
{
    private readonly ScenarioContext             m_ScenarioContext            = scenarioContext;
    private readonly ITransactionTemplateService m_TransactionTemplateService = transactionTemplateService;
    private readonly IAuthorizationService       m_AuthorizationService       = authorizationService;

    [Given(@"transaction template get request with query pageable")]
    public void GivenTransactionTemplateGetRequestWithQueryPageable()
    {
        m_ScenarioContext[Constant.Pageable] = new Pageable
                                               {
                                                   Page = 1,
                                                   Size = 20
                                               };
    }

    [Given(@"authorization for transaction template")]
    public void GivenAuthorizationForTransactionTemplate() //TODO What do to with AuthorizationService
    {
        typeof(AuthorizationService).GetField($"<{nameof(m_AuthorizationService.UserId)}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)
                                    ?.SetValue(m_AuthorizationService, Example.Entity.TransactionTemplate.GetTransactionTemplate.ClientId);
    }

    [When(@"transactions template are fetched from the database")]
    public async Task WhenTransactionsTemplateAreFetchedFromTheDatabase()
    {
        var pageable = m_ScenarioContext.Get<Pageable>(Constant.Pageable);
        var result   = await m_TransactionTemplateService.GetAll(pageable);

        m_ScenarioContext[Constant.Result]       = result;
        m_ScenarioContext[Constant.ActionResult] = result.ActionResult;
    }

    [Then(@"transaction template response should be (.*)")]
    public void ThenTransactionTemplateResponseShouldBe(int statusCode)
    {
        var actionResult = m_ScenarioContext.Get<ActionResult>(Constant.ActionResult);

        var resultStatusCode = actionResult switch
                               {
                                   ObjectResult objectResult         => objectResult.StatusCode,
                                   StatusCodeResult statusCodeResult => statusCodeResult.StatusCode,
                                   _ => throw new InvalidOperationException($"Unexpected ActionResult type: {actionResult.GetType()
                                                                                                                         .Name}")
                               };

        resultStatusCode.ShouldNotBeNull();
        resultStatusCode.ShouldBe(statusCode);
    }

    [Then(@"response should contain the list of transactions template matching the filter parameters")]
    public void ThenResponseShouldContainTheListOfTransactionsTemplateMatchingTheFilterParameters()
    {
        var pageable = m_ScenarioContext.Get<Pageable>(Constant.Pageable);
        var result   = m_ScenarioContext.Get<Result<Page<TransactionTemplateResponse>>>(Constant.Result);

        result.Value.ShouldNotBeNull();
        result.Value.Items.Count.ShouldBeGreaterThan(0);
        result.Value.PageNumber.ShouldBe(pageable.Page);
    }

    [Given(@"transaction template get request with Id")]
    public void GivenTransactionTemplateGetRequestWithId()
    {
        m_ScenarioContext[Constant.Id] = Example.Entity.TransactionTemplate.GetTransactionTemplate.Id;
    }

    [When(@"transaction template is fetched by Id from the database")]
    public async Task WhenTransactionTemplateIsFetchedByIdFromTheDatabase()
    {
        var id     = m_ScenarioContext.Get<Guid>(Constant.Id);
        var result = await m_TransactionTemplateService.GetOne(id);

        m_ScenarioContext[Constant.Result]       = result;
        m_ScenarioContext[Constant.ActionResult] = result.ActionResult;
    }

    [Then(@"response should contain the transaction template with the given Id")]
    public void ThenResponseShouldContainTheTransactionTemplateWithTheGivenId()
    {
        var result = m_ScenarioContext.Get<Result<TransactionTemplateResponse>>(Constant.Result);

        result.Value.ShouldNotBeNull();
        result.Value.Id.ShouldBe(m_ScenarioContext.Get<Guid>(Constant.Id));
    }

    [Given(@"transaction template create request")]
    public void GivenTransactionTemplateCreateRequest()
    {
        m_ScenarioContext[Constant.Create] = Example.Entity.TransactionTemplate.TransactionTemplateCreateRequest;
    }

    [When(@"transaction template is created in the database")]
    public async Task WhenTransactionTemplateIsCreatedInTheDatabase()
    {
        var transactionTemplateCreateRequest = m_ScenarioContext.Get<TransactionTemplateCreateRequest>(Constant.Create);
        var createTransactionTemplateResult  = await m_TransactionTemplateService.Create(transactionTemplateCreateRequest);

        m_ScenarioContext[Constant.Result]       = createTransactionTemplateResult;
        m_ScenarioContext[Constant.ActionResult] = createTransactionTemplateResult.ActionResult;
    }

    [Then(@"transaction template details should match the created transaction template")]
    public void ThenTransactionTemplateDetailsShouldMatchTheCreatedTransactionTemplate()
    {
        var result = m_ScenarioContext.Get<Result<TransactionTemplateResponse>>(Constant.Result);

        result.Value.ShouldNotBeNull();
        result.Value.AccountNumber.ShouldBe(Example.Entity.TransactionTemplate.TransactionTemplateCreateRequest.AccountNumber);
        result.Value.Name.ShouldBe(Example.Entity.TransactionTemplate.TransactionTemplateCreateRequest.Name);
    }

    [Given(@"a valid transaction template Id for update")]
    public void GivenAValidTransactionTemplateIdForUpdate()
    {
        m_ScenarioContext[Constant.Id] = Example.Entity.TransactionTemplate.UpdateTransactionTemplate.Id;
    }

    [Given(@"update request transaction template")]
    public void GivenUpdateRequestTransactionTemplate()
    {
        m_ScenarioContext[Constant.Update] = Example.Entity.TransactionTemplate.TransactionTemplateUpdateRequest;
    }

    [When(@"transaction template is updated in the database")]
    public async Task WhenTransactionTemplateIsUpdatedInTheDatabase()
    {
        var id            = m_ScenarioContext.Get<Guid>(Constant.Id);
        var updateRequest = m_ScenarioContext.Get<TransactionTemplateUpdateRequest>(Constant.Update);
        var result        = await m_TransactionTemplateService.Update(updateRequest, id);

        m_ScenarioContext[Constant.Result]       = result;
        m_ScenarioContext[Constant.ActionResult] = result.ActionResult;
    }

    [Then(@"transaction template details should match the updated transaction template")]
    public void ThenTransactionTemplateDetailsShouldMatchTheUpdatedTransactionTemplate()
    {
        var result        = m_ScenarioContext.Get<Result<TransactionTemplateResponse>>(Constant.Result);
        var updateRequest = m_ScenarioContext.Get<TransactionTemplateUpdateRequest>(Constant.Update);
        var seeder        = Example.Entity.TransactionTemplate.UpdateTransactionTemplate;

        result.Value.ShouldNotBeNull();
        result.Value.Id.ShouldBe(seeder.Id);
        result.Value.Client.Id.ShouldBe(seeder.ClientId);
        result.Value.Name.ShouldBe(updateRequest.Name);
        result.Value.AccountNumber.ShouldBe(updateRequest.AccountNumber);
        result.Value.Deleted.ShouldBe(updateRequest.Deleted);
        result.Value.CreatedAt.ShouldBeInRange(seeder.CreatedAt.Subtract(TimeSpan.FromSeconds(5)), seeder.CreatedAt.AddSeconds(5));
        result.Value.ModifiedAt.ShouldBeInRange(seeder.ModifiedAt.Subtract(TimeSpan.FromSeconds(5)), seeder.ModifiedAt.AddSeconds(5));
    }
}

file static class Constant
{
    public const string Pageable     = "TransactionTemplatePageable";
    public const string Id           = "TransactionTemplateId";
    public const string Result       = "TransactionTemplateResult";
    public const string Create       = "TransactionTemplateCreate";
    public const string Update       = "TransactionTemplateUpdate";
    public const string ActionResult = "TransactionTemplateActionResult";
}
