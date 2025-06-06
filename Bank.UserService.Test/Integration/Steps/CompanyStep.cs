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
public class CompanyStep(ScenarioContext scenarioContext, ICompanyService companyService, CompanyController companyController)
{
    private readonly ICompanyService   m_CompanyService    = companyService;
    private readonly ScenarioContext   m_ScenarioContext   = scenarioContext;
    private readonly CompanyController m_CompanyController = companyController;

    [Given(@"company create request")]
    public void GivenCompanyCreateRequest()
    {
        m_ScenarioContext[Constant.CreateRequest] = Example.Entity.Company.CreateRequest;
    }

    [When(@"company is created in the database")]
    public async Task WhenCompanyIsCreatedInTheDatabase()
    {
        var companyCreateRequest = m_ScenarioContext.Get<CompanyCreateRequest>(Constant.CreateRequest);
        var createCompanyResult  = await m_CompanyService.Create(companyCreateRequest);
        m_ScenarioContext[Constant.CreateResult] = createCompanyResult;
    }

    [Given(@"a valid company Id")]
    public async Task GivenAValidCompanyId()
    {
        var findCompanyResult = await m_CompanyService.GetOne(Example.Entity.Company.CompanyResponse2.Id);
        m_ScenarioContext[Constant.ValidCompanyId] = findCompanyResult.Value.Id;
    }

    [When(@"company is fetched by Id")]
    public async Task WhenCompanyIsFetchedById()
    {
        if (m_ScenarioContext.ContainsKey(Constant.CreateResult))
        {
            var createCompanyResult = m_ScenarioContext.Get<Result<CompanyResponse>>(Constant.CreateResult);
            var companyId           = createCompanyResult.Value.Id;
            var getCompanyResult    = await m_CompanyService.GetOne(companyId);
            m_ScenarioContext[Constant.GetResult] = getCompanyResult;
        }
        else if (m_ScenarioContext.ContainsKey(Constant.UpdateRequest))
        {
            var updateCompanyRequest = m_ScenarioContext.Get<Result<CompanyResponse>>(Constant.UpdateRequest);
            var companyId            = updateCompanyRequest.Value.Id;
            var getCompanyResult     = await m_CompanyService.GetOne(companyId);
            m_ScenarioContext[Constant.GetResult] = getCompanyResult;
        }
        else
        {
            var validCompanyId   = m_ScenarioContext.Get<Guid>(Constant.ValidCompanyId);
            var getCompanyResult = await m_CompanyService.GetOne(validCompanyId);
            m_ScenarioContext[Constant.GetResult] = getCompanyResult;
        }
    }

    [Then(@"company details should match the created company")]
    public void ThenCompanyDetailsShouldMatchTheCreatedCompany()
    {
        var getCompanyResult = m_ScenarioContext.Get<Result<CompanyResponse>>(Constant.GetResult);

        getCompanyResult.ActionResult.ShouldBeOfType<OkObjectResult>();
        getCompanyResult.Value.ShouldNotBeNull();
        getCompanyResult.Value.Name.ShouldBe(Example.Entity.Company.CreateRequest.Name);
        getCompanyResult.Value.RegistrationNumber.ShouldBe(Example.Entity.Company.CreateRequest.RegistrationNumber);
        //getCompanyResult.Value.TaxIdentificationNumber.ShouldBe(Example.Entity.Company.CreateRequest.TaxIdentificationNumber);
        getCompanyResult.Value.ActivityCode.ShouldBe(Example.Entity.Company.CreateRequest.ActivityCode);
        getCompanyResult.Value.Address.ShouldBe(Example.Entity.Company.CreateRequest.Address);
        getCompanyResult.Value.MajorityOwner!.Id.ShouldBe(Example.Entity.Company.CreateRequest.MajorityOwnerId);
    }

    [Then(@"the result should contain the company details")]
    public void ThenTheResultShouldContainTheCompanyDetails()
    {
        var getCompanyResult = m_ScenarioContext.Get<Result<CompanyResponse>>(Constant.GetResult);

        getCompanyResult.ActionResult.ShouldBeOfType<OkObjectResult>();
        getCompanyResult.Value.ShouldNotBeNull();
        getCompanyResult.Value.Name.ShouldBe(Example.Entity.Company.CompanyResponse.Name);
        getCompanyResult.Value.RegistrationNumber.ShouldBe(Example.Entity.Company.CompanyResponse.RegistrationNumber);
        getCompanyResult.Value.TaxIdentificationNumber.ShouldBe(Example.Entity.Company.CompanyResponse.TaxIdentificationNumber);
        getCompanyResult.Value.ActivityCode.ShouldBe(Example.Entity.Company.CompanyResponse.ActivityCode);
        getCompanyResult.Value.Address.ShouldBe(Example.Entity.Company.CompanyResponse.Address);
    }

    [Given(@"company filter query")]
    public void GivenCompanyFilterQuery()
    {
        m_ScenarioContext[Constant.FilterQuery] = Example.Entity.Company.CompanyFilterQuery;
    }

    [Given(@"pageable parameters")]
    public void GivenPageableParameters()
    {
        m_ScenarioContext[Constant.Pageable] = Example.Entity.Company.Pageable;
    }

    [When(@"all companies are fetched")]
    public async Task WhenAllCompaniesAreFetched()
    {
        var filterQuery = m_ScenarioContext.Get<CompanyFilterQuery>(Constant.FilterQuery);
        var pageable    = m_ScenarioContext.Get<Pageable>(Constant.Pageable);

        var getAllResult = await m_CompanyService.GetAll(filterQuery, pageable);
        m_ScenarioContext[Constant.GetAllResult] = getAllResult;
    }

    [Then(@"the result should contain a list of companies")]
    public void ThenTheResultShouldContainAListOfCompanies()
    {
        var getAllResult = m_ScenarioContext.Get<Result<Page<CompanyResponse>>>(Constant.GetAllResult);

        getAllResult.ActionResult.ShouldBeOfType<OkObjectResult>();
        getAllResult.Value.ShouldNotBeNull();
        getAllResult.Value.Items.ShouldNotBeEmpty();
        getAllResult.Value.TotalElements.ShouldBeGreaterThan(0);
    }

    [Given(@"update request")]
    public void GivenUpdateRequest()
    {
        m_ScenarioContext[Constant.UpdateRequest] = Example.Entity.Company.UpdateRequest;
    }

    [When(@"the company is updated")]
    public async Task WhenTheCompanyIsUpdated()
    {
        var validCompanyId = m_ScenarioContext.Get<Guid>(Constant.ValidCompanyId);
        var updateRequest  = m_ScenarioContext.Get<CompanyUpdateRequest>(Constant.UpdateRequest);

        var updateResult = await m_CompanyService.Update(updateRequest, validCompanyId);
        m_ScenarioContext[Constant.UpdateResult] = updateResult;
    }

    [Then(@"the result contains the updated company response")]
    public void ThenTheResultContainsTheUpdatedCompanyResponse()
    {
        var updateResult = m_ScenarioContext.Get<Result<CompanyResponse>>(Constant.UpdateResult);

        updateResult.ActionResult.ShouldBeOfType<OkObjectResult>();
        updateResult.Value.ShouldNotBeNull();
        updateResult.Value.Name.ShouldBe(Example.Entity.Company.UpdateRequest.Name);
        updateResult.Value.ActivityCode.ShouldBe(Example.Entity.Company.UpdateRequest.ActivityCode);
        updateResult.Value.Address.ShouldBe(Example.Entity.Company.UpdateRequest.Address);
        updateResult.Value.MajorityOwner!.Id.ShouldBe(Example.Entity.Company.UpdateRequest.MajorityOwnerId);
    }

    [Given(@"a valid company Id for fetching")]
    public async Task GivenAValidCompanyIdForFetching()
    {
        var findCompanyResult = await m_CompanyService.GetOne(Example.Entity.Company.CompanyResponse.Id);
        m_ScenarioContext[Constant.ValidCompanyId] = findCompanyResult.Value.Id;
    }

    [Given(@"a valid company create request")]
    public void GivenAValidCompanyCreateRequest()
    {
        m_ScenarioContext[Constant.CompanyCreateRequest] = Example.Entity.Company.CreateRequest;
    }

    [When(@"a POST request is sent to the company creation endpoint")]
    public async Task WhenAPostRequestIsSentToTheCompanyCreationEndpoint()
    {
        var createRequest = m_ScenarioContext.Get<CompanyCreateRequest>(Constant.CompanyCreateRequest);

        var createResult = await m_CompanyController.Create(createRequest);

        m_ScenarioContext[Constant.CompanyCreateResult] = createResult;
    }

    [Then(@"the response ActionResult should indicate successful company creation")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulCompanyCreation()
    {
        var createResult = m_ScenarioContext.Get<ActionResult<CompanyResponse>>(Constant.CompanyCreateResult);

        createResult.Result.ShouldBeOfType<OkObjectResult>();
        createResult.ShouldNotBeNull();
    }

    [Given(@"a valid company filter query and pageable parameters")]
    public void GivenAValidCompanyFilterQueryAndPageableParameters()
    {
        m_ScenarioContext[Constant.CompanyFilterQuery] = new CompanyFilterQuery();
        m_ScenarioContext[Constant.Pageable]           = new Pageable();
    }

    [When(@"a GET request is sent to fetch all companies")]
    public async Task WhenAGetRequestIsSentToFetchAllCompanies()
    {
        var filterQuery = m_ScenarioContext.Get<CompanyFilterQuery>(Constant.CompanyFilterQuery);
        var pageable    = m_ScenarioContext.Get<Pageable>(Constant.Pageable);

        var getCompaniesResult = await m_CompanyController.GetAll(filterQuery, pageable);

        m_ScenarioContext[Constant.GetCompanies] = getCompaniesResult;
    }

    [Then(@"the response ActionResult should indicate successful retrieval of all companies")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulRetrievalOfAllCompanies()
    {
        var getCompaniesResult = m_ScenarioContext.Get<ActionResult<Page<CompanyResponse>>>(Constant.GetCompanies);

        getCompaniesResult.Result.ShouldBeOfType<OkObjectResult>();
        getCompaniesResult.ShouldNotBeNull();
    }

    [Given(@"a valid company Id to fetch")]
    public void GivenAValidCompanyIdToFetch()
    {
        m_ScenarioContext[Constant.CompanyId] = Example.Entity.Company.Id;
    }

    [When(@"a GET request is sent to fetch a company by Id")]
    public async Task WhenAGetRequestIsSentToFetchACompanyById()
    {
        var companyId = m_ScenarioContext.Get<Guid>(Constant.CompanyId);

        var getCompanyResult = await m_CompanyController.GetOne(companyId);

        m_ScenarioContext[Constant.GetCompany] = getCompanyResult;
    }

    [Then(@"the response ActionResult should indicate successful retrieval of the company")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulRetrievalOfTheCompany()
    {
        var getCompanyResult = m_ScenarioContext.Get<ActionResult<CompanyResponse>>(Constant.GetCompany);

        getCompanyResult.Result.ShouldBeOfType<OkObjectResult>();
        getCompanyResult.ShouldNotBeNull();
    }

    [Given(@"a valid company Id and company update request")]
    public void GivenAValidCompanyIdAndCompanyUpdateRequest()
    {
        m_ScenarioContext[Constant.CompanyId]            = Example.Entity.Company.Id;
        m_ScenarioContext[Constant.CompanyUpdateRequest] = Example.Entity.Company.UpdateRequest;
    }

    [When(@"a PUT request is sent to update the company")]
    public async Task WhenAPutRequestIsSentToUpdateTheCompany()
    {
        var companyId     = m_ScenarioContext.Get<Guid>(Constant.CompanyId);
        var updateRequest = m_ScenarioContext.Get<CompanyUpdateRequest>(Constant.CompanyUpdateRequest);

        var updateResult = await m_CompanyController.Update(updateRequest, companyId);

        m_ScenarioContext[Constant.CompanyUpdateResult] = updateResult;
    }

    [Then(@"the response ActionResult should indicate successful company update")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulCompanyUpdate()
    {
        var updateResult = m_ScenarioContext.Get<ActionResult<CompanyResponse>>(Constant.CompanyUpdateResult);

        updateResult.Result.ShouldBeOfType<OkObjectResult>();
        updateResult.ShouldNotBeNull();
    }
}

file static class Constant
{
    public const string CreateRequest        = "CompanyCreateRequest";
    public const string CreateResult         = "CompanyCreateResult";
    public const string GetResult            = "CompanyGetResult";
    public const string ValidCompanyId       = "ValidCompanyId";
    public const string FilterQuery          = "CompanyFilterQuery";
    public const string Pageable             = "CompanyPageable";
    public const string GetAllResult         = "CompanyGetAllResult";
    public const string UpdateRequest        = "CompanyUpdateRequest";
    public const string UpdateResult         = "CompanyUpdateResult";
    public const string CompanyCreateRequest = "CompanyCreateRequest";
    public const string CompanyCreateResult  = "CompanyCreateResult";
    public const string CompanyFilterQuery   = "CompanyFilterQuery";
    public const string GetCompanies         = "GetCompanies";
    public const string CompanyId            = "CompanyId";
    public const string GetCompany           = "GetCompany";
    public const string CompanyUpdateRequest = "CompanyUpdateRequest";
    public const string CompanyUpdateResult  = "CompanyUpdateResult";
}
