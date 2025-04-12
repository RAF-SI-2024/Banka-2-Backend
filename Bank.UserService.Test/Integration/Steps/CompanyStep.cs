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
public class CompanyStep(ScenarioContext scenarioContext, ICompanyService companyService)
{
    private readonly ICompanyService m_CompanyService  = companyService;
    private readonly ScenarioContext m_ScenarioContext = scenarioContext;

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
        getCompanyResult.Value.TaxIdentificationNumber.ShouldBe(Example.Entity.Company.CreateRequest.TaxIdentificationNumber);
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
}

file static class Constant
{
    public const string CreateRequest  = "CompanyCreateRequest";
    public const string CreateResult   = "CompanyCreateResult";
    public const string GetResult      = "CompanyGetResult";
    public const string ValidCompanyId = "ValidCompanyId";
    public const string FilterQuery    = "CompanyFilterQuery";
    public const string Pageable       = "CompanyPageable";
    public const string GetAllResult   = "CompanyGetAllResult";
    public const string UpdateRequest  = "CompanyUpdateRequest";
    public const string UpdateResult   = "CompanyUpdateResult";
}
