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
public class TransactionSteps(ScenarioContext scenarioContext, ITransactionService transactionService)
{
    private readonly ScenarioContext     m_ScenarioContext    = scenarioContext;
    private readonly ITransactionService m_TransactionService = transactionService;

    [Given(@"transaction get request with query filter parameters")]
    public void GivenTransactionGetRequestWithQueryFilterParameters()
    {
        m_ScenarioContext[Constant.FilterParam] = Example.Entity.Transaction.FilterQuery;
    }

    [Given(@"transaction get request with query pageable")]
    public void GivenTransactionGetRequestWithQueryPageable()
    {
        m_ScenarioContext[Constant.Pageable] = Example.Entity.Transaction.Pageable;
    }

    [When(@"transactions are fetched from the database")]
    public async Task WhenTransactionsAreFetchedFromTheDatabase()
    {
        var filterQuery = m_ScenarioContext.Get<TransactionFilterQuery>(Constant.FilterParam);
        var pageable    = m_ScenarioContext.Get<Pageable>(Constant.Pageable);
        var result      = await m_TransactionService.GetAll(filterQuery, pageable);

        m_ScenarioContext[Constant.Result]       = result;
        m_ScenarioContext[Constant.ActionResult] = result.ActionResult;
    }

    [Then(@"transaction response should be (.*)")]
    public void ThenTransactionResponseShouldBe(int statusCode)
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

    [Then(@"response should contain the list of transactions matching the filter parameters")]
    public void ThenResponseShouldContainTheListOfTransactionsMatchingTheFilterParameters()
    {
        var fromDate = m_ScenarioContext.Get<TransactionFilterQuery>(Constant.FilterParam)
                                        .FromDate;

        var toDate = m_ScenarioContext.Get<TransactionFilterQuery>(Constant.FilterParam)
                                      .ToDate;

        var status = m_ScenarioContext.Get<TransactionFilterQuery>(Constant.FilterParam)
                                      .Status;

        var result = m_ScenarioContext.Get<Result<Page<TransactionResponse>>>(Constant.Result);
        result.Value.ShouldNotBeNull();

        foreach (var transaction in result.Value.Items)
        {
            if (fromDate != DateOnly.MinValue)
                DateOnly.FromDateTime(transaction.CreatedAt)
                        .ShouldBeGreaterThan(fromDate);

            if (toDate != DateOnly.MinValue)
                DateOnly.FromDateTime(transaction.CreatedAt)
                        .ShouldBeGreaterThan(toDate);

            if (status != TransactionStatus.Invalid)
                transaction.Status.ShouldBe(status);
        }
    }

    [Given(@"transaction get request with Id")]
    public void GivenTransactionGetRequestWithId()
    {
        m_ScenarioContext[Constant.Id] = Example.Entity.Transaction.TransactionId;
    }

    [When(@"transaction is fetched by Id from the database")]
    public async Task WhenTransactionIsFetchedByIdFromTheDatabase()
    {
        var id     = m_ScenarioContext.Get<Guid>(Constant.Id);
        var result = await m_TransactionService.GetOne(id);

        m_ScenarioContext[Constant.Result]       = result;
        m_ScenarioContext[Constant.ActionResult] = result.ActionResult;
    }

    [Then(@"response should contain the transaction with the given Id")]
    public void ThenResponseShouldContainTheTransactionWithTheGivenId()
    {
        var result = m_ScenarioContext.Get<Result<TransactionResponse>>(Constant.Result);

        result.Value.ShouldNotBeNull();
        result.Value.Id.ShouldBe(m_ScenarioContext.Get<Guid>(Constant.Id));
    }

    [Given(@"transaction get request with account Id")]
    public void GivenTransactionGetRequestWithAccountId()
    {
        m_ScenarioContext[Constant.AccountId] = Example.Entity.Transaction.AccountId;
    }

    [When(@"transactions are fetched by account Id from the database")]
    public async Task WhenTransactionsAreFetchedByAccountIdFromTheDatabase()
    {
        var id                = m_ScenarioContext.Get<Guid>(Constant.AccountId);
        var filterQuery       = m_ScenarioContext.Get<TransactionFilterQuery>(Constant.FilterParam);
        var pageable          = m_ScenarioContext.Get<Pageable>(Constant.Pageable);
        var transactionResult = await m_TransactionService.GetAllByAccountId(id, filterQuery, pageable);

        m_ScenarioContext[Constant.Result]       = transactionResult;
        m_ScenarioContext[Constant.ActionResult] = transactionResult.ActionResult;
    }

    [Then(@"response should contain the list of transactions matching the filter parameters and account Id")]
    public void ThenResponseShouldContainTheListOfTransactionsMatchingTheFilterParametersAndAccountId()
    {
        var fromDate = m_ScenarioContext.Get<TransactionFilterQuery>(Constant.FilterParam)
                                        .FromDate;

        var toDate = m_ScenarioContext.Get<TransactionFilterQuery>(Constant.FilterParam)
                                      .ToDate;

        var status = m_ScenarioContext.Get<TransactionFilterQuery>(Constant.FilterParam)
                                      .Status;

        var accountId = m_ScenarioContext.Get<Guid>(Constant.AccountId);

        var result = m_ScenarioContext.Get<Result<Page<TransactionResponse>>>(Constant.Result);
        result.Value.ShouldNotBeNull();

        foreach (var transaction in result.Value.Items)
        {
            if (fromDate != DateOnly.MinValue)
                DateOnly.FromDateTime(transaction.CreatedAt)
                        .ShouldBeGreaterThan(fromDate);

            if (toDate != DateOnly.MinValue)
                DateOnly.FromDateTime(transaction.CreatedAt)
                        .ShouldBeGreaterThan(toDate);

            if (status != TransactionStatus.Invalid)
                transaction.Status.ShouldBe(status);

            (transaction.FromAccount.Id == accountId || transaction.ToAccount.Id == accountId).ShouldBeTrue();
        }
    }

    [Given(@"transaction create request")]
    public void GivenTransactionCreateRequest()
    {
        m_ScenarioContext[Constant.Create] = Example.Entity.Transaction.CreateRequest;
    }

    [When(@"transaction is created in the database")]
    public async Task WhenTransactionIsCreatedInTheDatabase()
    {
        var transactionCreateRequest = m_ScenarioContext.Get<TransactionCreateRequest>(Constant.Create);
        var createTransactionResult  = await m_TransactionService.Create(transactionCreateRequest);
        m_ScenarioContext[Constant.Result]       = createTransactionResult;
        m_ScenarioContext[Constant.ActionResult] = createTransactionResult.ActionResult;
    }

    [Then(@"transaction details should match the created transaction")]
    public void ThenTransactionDetailsShouldMatchTheCreatedTransaction()
    {
        var result = m_ScenarioContext.Get<Result<TransactionCreateResponse>>(Constant.Result);

        result.Value.ShouldNotBeNull();
        result.Value.Code.Id.ShouldBe(Example.Entity.Transaction.CreateRequest.CodeId);
        result.Value.Purpose.ShouldBe(Example.Entity.Transaction.CreateRequest.Purpose);
        result.Value.ReferenceNumber.ShouldBe(Example.Entity.Transaction.CreateRequest.ReferenceNumber);
        result.Value.FromAmount.ShouldBe(Example.Entity.Transaction.CreateRequest.Amount);
    }

    [Given(@"a valid transaction Id for update")]
    public void GivenAValidTransactionIdForUpdate()
    {
        m_ScenarioContext[Constant.Id] = Example.Entity.Transaction.TransactionId;
    }

    [Given(@"update request transaction")]
    public void GivenUpdateRequestTransaction()
    {
        m_ScenarioContext[Constant.Update] = Example.Entity.Transaction.UpdateRequest;
    }

    [When(@"transaction is updated in the database")]
    public async Task WhenTransactionIsUpdatedInTheDatabase()
    {
        var id            = m_ScenarioContext.Get<Guid>(Constant.Id);
        var updateRequest = m_ScenarioContext.Get<TransactionUpdateRequest>(Constant.Update);
        var result        = await m_TransactionService.Update(updateRequest, id);

        m_ScenarioContext[Constant.Result]       = result;
        m_ScenarioContext[Constant.ActionResult] = result.ActionResult;
    }

    [Then(@"transaction details should match the updated transaction")]
    public void ThenTransactionDetailsShouldMatchTheUpdatedTransaction()
    {
        var id     = m_ScenarioContext.Get<Guid>(Constant.Id);
        var result = m_ScenarioContext.Get<Result<TransactionResponse>>(Constant.Result);

        result.Value.ShouldNotBeNull();
        result.Value.Id.ShouldBe(id);
        result.Value.Status.ShouldBe(Example.Entity.Transaction.UpdateRequest.Status);
    }
}

file static class Constant
{
    public const string FilterParam  = "TransactionFilterQuery";
    public const string Pageable     = "TransactionPageable";
    public const string Id           = "TransactionId";
    public const string Result       = "TransactionResult";
    public const string Create       = "TransactionCreate";
    public const string Update       = "TransactionUpdate";
    public const string ActionResult = "TransactionActionResult";
    public const string AccountId    = "AccountId";
}
