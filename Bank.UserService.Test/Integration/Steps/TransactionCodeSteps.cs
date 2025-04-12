using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Responses;
using Bank.UserService.Services;
using Bank.UserService.Test.Examples.Entities;

using Microsoft.AspNetCore.Mvc;

using Shouldly;

namespace Bank.UserService.Test.Steps;

[Binding]
public class TransactionCodeSteps(ScenarioContext scenarioContext, ITransactionCodeService transactionCodeService)
{
    private readonly ITransactionCodeService m_TransactionCodeService = transactionCodeService;
    private readonly ScenarioContext         m_ScenarioContext        = scenarioContext;

    [Given(@"transaction code get request with query pageable")]
    public void GivenTransactionCodeGetRequestWithQueryPageable()
    {
        m_ScenarioContext[Constant.Pageable] = new Pageable
                                               {
                                                   Page = 1,
                                                   Size = 20
                                               };
    }

    [When(@"transaction codes are fecthed from the database")]
    public async Task WhenTransactionCodesAreFecthedFromTheDatabase()
    {
        var pageable = m_ScenarioContext.Get<Pageable>(Constant.Pageable);
        var result   = await m_TransactionCodeService.GetAll(pageable);

        m_ScenarioContext[Constant.GetAll]       = result;
        m_ScenarioContext[Constant.ActionResult] = result.ActionResult;
    }

    [Then(@"the transaction code response code should be (.*)")]
    public void ThenTheTransactionCodeResponseCodeShouldBe(int statusCode)
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

    [Then(@"response should contain list of transaction codes matching pageable")]
    public void ThenResponseShouldContainListOfTransactionCodesMatchingPageable()
    {
        var pageable = m_ScenarioContext.Get<Pageable>(Constant.Pageable);
        var result   = m_ScenarioContext.Get<Result<Page<TransactionCodeResponse>>>(Constant.GetAll);

        result.Value.ShouldNotBeNull();
        result.Value.Items.Count.ShouldBe(pageable.Size);
        result.Value.PageNumber.ShouldBe(pageable.Page);
    }

    [Given(@"given valid transaction code id")]
    public void GivenGivenValidTransactionCodeId()
    {
        m_ScenarioContext[Constant.GivenId] = Example.Entity.TransactionCode.GetTransactionCode.Id;
    }

    [When(@"transaction code is fecthed from the database")]
    public async Task WhenTransactionCodeIsFecthedFromTheDatabase()
    {
        var id     = m_ScenarioContext.Get<Guid>(Constant.GivenId);
        var result = await m_TransactionCodeService.GetOne(id);

        m_ScenarioContext[Constant.GetOne]       = result;
        m_ScenarioContext[Constant.ActionResult] = result.ActionResult;
    }

    [Then(@"response should contain transaction code with given id")]
    public void ThenResponseShouldContainTransactionCodeWithGivenId()
    {
        var result = m_ScenarioContext.Get<Result<TransactionCodeResponse>>(Constant.GetOne);
        var seeder = Example.Entity.TransactionCode.GetTransactionCode;

        result.Value.ShouldNotBeNull();
        result.Value.Id.ShouldBe(seeder.Id);
        result.Value.Code.ShouldBe(seeder.Code);
        result.Value.Name.ShouldBe(seeder.Name);
        result.Value.CreatedAt.ShouldBeInRange(seeder.CreatedAt.Subtract(TimeSpan.FromSeconds(5)), seeder.CreatedAt.AddSeconds(5));
        result.Value.ModifiedAt.ShouldBeInRange(seeder.ModifiedAt.Subtract(TimeSpan.FromSeconds(5)), seeder.ModifiedAt.AddSeconds(5));
    }
}

file static class Constant
{
    public const string Pageable     = "TransactionPageable";
    public const string GetAll       = "TransactionCodeGetAll";
    public const string GivenId      = "TransactionCodeGivenId";
    public const string GetOne       = "TransactionCodeGetOne";
    public const string ActionResult = "TransactionCodeActionResult";
}
