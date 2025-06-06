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
public class EmployeeSteps(ScenarioContext scenarioContext, IEmployeeService employeeService, EmployeeController employeeController)
{
    private readonly IEmployeeService   m_EmployeeService    = employeeService;
    private readonly ScenarioContext    m_ScenarioContext    = scenarioContext;
    private readonly EmployeeController m_EmployeeController = employeeController;

    [Given(@"employee create request")]
    public void GivenEmployeeCreateRequest()
    {
        m_ScenarioContext[Constant.CreateRequest] = Example.Entity.Employee.CreateRequest;
    }

    [When(@"employee is created in the database")]
    public async Task WhenEmployeeIsCreatedInTheDatabase()
    {
        var employeeCreateRequest = m_ScenarioContext.Get<EmployeeCreateRequest>(Constant.CreateRequest);

        var createEmployeeResult = await m_EmployeeService.Create(employeeCreateRequest);

        m_ScenarioContext[Constant.CreateResult] = createEmployeeResult;
    }

    [When(@"employee is fetched by Id")]
    public async Task WhenEmployeeIsFetchedById()
    {
        var id     = m_ScenarioContext.Get<Guid>(Constant.GetById);
        var result = await m_EmployeeService.GetOne(id);
        m_ScenarioContext[Constant.GetResult] = result;
    }

    [Then(@"employee details should match the created employee")]
    public void ThenEmployeeDetailsShouldMatchTheCreatedEmployee()
    {
        var getEmployeeResult = m_ScenarioContext.Get<Result<EmployeeResponse>>(Constant.CreateResult);

        getEmployeeResult.ActionResult.ShouldBeOfType<OkObjectResult>();
        getEmployeeResult.Value.ShouldNotBeNull();
        getEmployeeResult.Value.FirstName.ShouldBe(Example.Entity.Employee.CreateRequest.FirstName);
        getEmployeeResult.Value.LastName.ShouldBe(Example.Entity.Employee.CreateRequest.LastName);
        getEmployeeResult.Value.DateOfBirth.ShouldBe(Example.Entity.Employee.CreateRequest.DateOfBirth);
        getEmployeeResult.Value.Gender.ShouldBe(Example.Entity.Employee.CreateRequest.Gender);
        getEmployeeResult.Value.UniqueIdentificationNumber.ShouldBe(Example.Entity.Employee.CreateRequest.UniqueIdentificationNumber);
        getEmployeeResult.Value.Username.ShouldBe(Example.Entity.Employee.CreateRequest.Username);
        getEmployeeResult.Value.Email.ShouldBe(Example.Entity.Employee.CreateRequest.Email);
        getEmployeeResult.Value.PhoneNumber.ShouldBe(Example.Entity.Employee.CreateRequest.PhoneNumber);
        getEmployeeResult.Value.Address.ShouldBe(Example.Entity.Employee.CreateRequest.Address);
        getEmployeeResult.Value.Role.ShouldBe(Example.Entity.Employee.CreateRequest.Role);
        getEmployeeResult.Value.Department.ShouldBe(Example.Entity.Employee.CreateRequest.Department);
        getEmployeeResult.Value.Employed.ShouldBe(Example.Entity.Employee.CreateRequest.Employed);
    }

    [Given(@"employee update request")]
    public void GivenEmployeeUpdateRequest()
    {
        m_ScenarioContext[Constant.UpdateRequest] = Example.Entity.Employee.UpdateRequest;
    }

    [Given(@"employee filter request")]
    public void GivenEmployeeFilterRequest()
    {
        m_ScenarioContext[Constant.FilterQuery] = new UserFilterQuery();
    }

    [Given(@"pageable parameters for employee list")]
    public void GivenPageableParametersForEmployeeList()
    {
        m_ScenarioContext[Constant.Pageable] = new Pageable();
    }

    [When(@"employee list is fetched from the database")]
    public async Task WhenEmployeeListIsFetchedFromTheDatabase()
    {
        var filter = m_ScenarioContext.Get<UserFilterQuery>(Constant.FilterQuery);
        var page   = m_ScenarioContext.Get<Pageable>(Constant.Pageable);

        var result = await m_EmployeeService.GetAll(filter, page);
        m_ScenarioContext[Constant.GetAllResult] = result;
    }

    [Then(@"the result should contain a list of employees")]
    public void ThenTheResultShouldContainAListOfEmployees()
    {
        var getAllResult = m_ScenarioContext.Get<Result<Page<EmployeeResponse>>>(Constant.GetAllResult);

        getAllResult.ActionResult.ShouldBeOfType<OkObjectResult>();
        getAllResult.Value.ShouldNotBeNull();
        getAllResult.Value.Items.ShouldNotBeEmpty();
        getAllResult.Value.TotalElements.ShouldBeGreaterThan(0);
    }

    [Given(@"a valid employee Id")]
    public void GivenAValidEmployeeId()
    {
        m_ScenarioContext[Constant.GetById] = Example.Entity.Employee.GetEmployee.Id;
    }

    [Then(@"the result should contain the employee details")]
    public void ThenTheResultShouldContainTheEmployeeDetails()
    {
        var getEmployeeResult = m_ScenarioContext.Get<Result<EmployeeResponse>>(Constant.GetResult);

        getEmployeeResult.ActionResult.ShouldBeOfType<OkObjectResult>();
        getEmployeeResult.Value.ShouldNotBeNull();
        getEmployeeResult.Value.Id.ShouldBe(m_ScenarioContext.Get<Guid>(Constant.GetById));
    }

    [Given(@"a valid employee Id for update")]
    public async Task GivenAValidEmployeeIdForUpdate()
    {
        var findEmployeeResult = await m_EmployeeService.GetOne(Example.Entity.Employee.UpdateEmployee.Id);
        m_ScenarioContext[Constant.EmployeeToBeUpdated] = findEmployeeResult.Value.Id;
    }

    [Given(@"update request employee")]
    public void GivenUpdateRequestEmployee()
    {
        m_ScenarioContext[Constant.UpdateRequest] = Example.Entity.Employee.UpdateRequest;
    }

    [When(@"employee is updated in the database")]
    public async Task WhenEmployeeIsUpdatedInTheDatabase()
    {
        var validEmployeeId = m_ScenarioContext.Get<Guid>(Constant.EmployeeToBeUpdated);
        var updateEmployee  = m_ScenarioContext.Get<EmployeeUpdateRequest>(Constant.UpdateRequest);

        var updateResult = await m_EmployeeService.Update(updateEmployee, validEmployeeId);
        m_ScenarioContext[Constant.UpdateResult] = updateResult;
    }

    [Then(@"employee details should match the updated employee")]
    public void ThenEmployeeDetailsShouldMatchTheUpdatedEmployee()
    {
        var getEmployeeResult = m_ScenarioContext.Get<Result<EmployeeResponse>>(Constant.UpdateResult);

        getEmployeeResult.ActionResult.ShouldBeOfType<OkObjectResult>();
        getEmployeeResult.Value.ShouldNotBeNull();
        getEmployeeResult.Value.FirstName.ShouldBe(Example.Entity.Employee.UpdateRequest.FirstName);
        getEmployeeResult.Value.LastName.ShouldBe(Example.Entity.Employee.UpdateRequest.LastName);
        getEmployeeResult.Value.Username.ShouldBe(Example.Entity.Employee.UpdateRequest.Username);
        getEmployeeResult.Value.PhoneNumber.ShouldBe(Example.Entity.Employee.UpdateRequest.PhoneNumber);
        getEmployeeResult.Value.Address.ShouldBe(Example.Entity.Employee.UpdateRequest.Address);
        getEmployeeResult.Value.Role.ShouldBe(Example.Entity.Employee.UpdateRequest.Role);
        getEmployeeResult.Value.Department.ShouldBe(Example.Entity.Employee.UpdateRequest.Department);
        getEmployeeResult.Value.Employed.ShouldBe(Example.Entity.Employee.UpdateRequest.Employed);
    }

    [Given(@"a valid employee filter request and pageable parameters")]
    public void GivenAValidEmployeeFilterRequestAndPageableParameters()
    {
        m_ScenarioContext[Constant.UserFilterQuery] = new UserFilterQuery();
        m_ScenarioContext[Constant.Pageable]        = new Pageable();
    }

    [When(@"a GET request is sent to fetch all employees")]
    public async Task WhenAGetRequestIsSentToFetchAllEmployees()
    {
        var userFilterQuery = m_ScenarioContext.Get<UserFilterQuery>(Constant.UserFilterQuery);
        var pageable        = m_ScenarioContext.Get<Pageable>(Constant.Pageable);

        var getEmployeesResult = await m_EmployeeController.GetAll(userFilterQuery, pageable);

        m_ScenarioContext[Constant.GetEmployees] = getEmployeesResult;
    }

    [Then(@"the response ActionResult should indicate successful retrieval of all employees")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulRetrievalOfAllEmployees()
    {
        var getEmployeesResult = m_ScenarioContext.Get<ActionResult<Page<EmployeeResponse>>>(Constant.GetEmployees);

        getEmployeesResult.Result.ShouldBeOfType<OkObjectResult>();
        getEmployeesResult.ShouldNotBeNull();
    }

    [Given(@"a valid employee create request")]
    public void GivenAValidEmployeeCreateRequest()
    {
        m_ScenarioContext[Constant.EmployeeCreateRequest] = Example.Entity.Employee.CreateRequest;
    }

    [When(@"a POST request is sent to the employee creation endpoint")]
    public async Task WhenAPostRequestIsSentToTheEmployeeCreationEndpoint()
    {
        var createRequest = m_ScenarioContext.Get<EmployeeCreateRequest>(Constant.EmployeeCreateRequest);

        var createResult = await m_EmployeeController.Create(createRequest);

        m_ScenarioContext[Constant.EmployeeCreateResult] = createResult;
    }

    [Then(@"the response ActionResult should indicate successful employee creation")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulEmployeeCreation()
    {
        var createResult = m_ScenarioContext.Get<ActionResult<EmployeeResponse>>(Constant.EmployeeCreateResult);

        createResult.Result.ShouldBeOfType<OkObjectResult>();
        createResult.ShouldNotBeNull();
    }

    [Given(@"a valid employee Id and update request")]
    public void GivenAValidEmployeeIdAndUpdateRequest()
    {
        m_ScenarioContext[Constant.EmployeeId]            = Example.Entity.Employee.GetEmployee.Id;
        m_ScenarioContext[Constant.EmployeeUpdateRequest] = Example.Entity.Employee.UpdateRequest;
    }

    [When(@"a PUT request is sent to the employee update endpoint")]
    public async Task WhenAPutRequestIsSentToTheEmployeeUpdateEndpoint()
    {
        var employeeId    = m_ScenarioContext.Get<Guid>(Constant.EmployeeId);
        var updateRequest = m_ScenarioContext.Get<EmployeeUpdateRequest>(Constant.EmployeeUpdateRequest);

        var updateResult = await m_EmployeeController.Update(updateRequest, employeeId);

        m_ScenarioContext[Constant.EmployeeUpdateResult] = updateResult;
    }

    [Then(@"the response ActionResult should indicate successful employee update")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulEmployeeUpdate()
    {
        var updateResult = m_ScenarioContext.Get<ActionResult<EmployeeResponse>>(Constant.EmployeeUpdateResult);

        updateResult.Result.ShouldBeOfType<OkObjectResult>();
        updateResult.ShouldNotBeNull();
    }

    [When(@"a GET request is sent to fetch the employee by Id")]
    public async Task WhenAGetRequestIsSentToFetchTheEmployeeById()
    {
        var employeeId = m_ScenarioContext.Get<Guid>(Constant.EmployeeId);

        var getEmployeeResult = await m_EmployeeController.GetOne(employeeId);

        m_ScenarioContext[Constant.GetEmployee] = getEmployeeResult;
    }

    [Then(@"the response ActionResult should indicate successful retrieval of the employee")]
    public void ThenTheResponseActionResultShouldIndicateSuccessfulRetrievalOfTheEmployee()
    {
        var getEmployeeResult = m_ScenarioContext.Get<ActionResult<EmployeeResponse>>(Constant.GetEmployee);

        getEmployeeResult.Result.ShouldBeOfType<OkObjectResult>();
        getEmployeeResult.ShouldNotBeNull();
    }

    [Given(@"a valid employee Id that exists")]
    public void GivenAValidEmployeeIdThatExists()
    {
        m_ScenarioContext[Constant.EmployeeId] = Example.Entity.Employee.UpdateEmployee.Id;
    }
}

file static class Constant
{
    public const string FilterQuery           = "EmployeeFilterQuery";
    public const string CreateRequest         = "EmployeeCreateRequest";
    public const string CreateResult          = "EmployeeCreateResult";
    public const string GetResult             = "EmployeeGetResult";
    public const string UpdateResult          = "EmployeeUpdateResult";
    public const string UpdateRequest         = "EmployeeUpdateRequest";
    public const string GetById               = "EmployeeGetById";
    public const string Pageable              = "EmployeePageable";
    public const string GetAllResult          = "EmployeeGetAllResult";
    public const string EmployeeToBeUpdated   = "EmployeeToBeUpdated";
    public const string EmployeeId            = "EmployeeId";
    public const string EmployeeUpdateRequest = "EmployeeUpdateRequest";
    public const string EmployeeCreateRequest = "EmployeeCreateRequest";
    public const string EmployeeCreateResult  = "EmployeeCreateResult";
    public const string EmployeeUpdateResult  = "EmployeeUpdateResult";
    public const string GetEmployees          = "GetEmployees";
    public const string GetEmployee           = "GetEmployee";
    public const string UserFilterQuery       = "UserFilterQuery";
}
