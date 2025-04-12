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
public class UserSteps(IUserService userService, ScenarioContext scenarioContext, IAuthorizationService authorizationService)
{
    private readonly IUserService          m_UserService          = userService;
    private readonly ScenarioContext       m_ScenarioContext      = scenarioContext;
    private readonly IAuthorizationService m_AuthorizationService = authorizationService;

    [Given(@"user login request")]
    public void GivenUserLoginRequest()
    {
        m_ScenarioContext[Constant.LoginRequest] = Example.Entity.User.LoginRequest;
    }

    [When(@"user sends valid login request")]
    public async Task WhenUserSendsValidLoginRequest()
    {
        var userLoginRequest = m_ScenarioContext.Get<UserLoginRequest>(Constant.LoginRequest);

        var userLoginResult = await m_UserService.Login(userLoginRequest);

        m_ScenarioContext[Constant.LoginResult] = userLoginResult;
    }

    [Then(@"login should be successful")]
    public void ThenLoginShouldBeSuccessful()
    {
        var userLoginResult = m_ScenarioContext.Get<Result<UserLoginResponse>>(Constant.LoginResult);

        userLoginResult.ActionResult.ShouldBeOfType<OkObjectResult>();
    }

    [Then(@"user should get valid jwt")]
    public void ThenUserShouldGetValidJwt()
    {
        var userLoginResult = m_ScenarioContext.Get<Result<UserLoginResponse>>(Constant.LoginResult);

        userLoginResult.Value.ShouldNotBeNull();
        userLoginResult.Value.Token.ShouldNotBeEmpty();
    }

    [Given(@"user get request with query filter parameters")]
    public void GivenUserGetRequestWithQueryFilterParameters()
    {
        m_ScenarioContext[Constant.FilterParam] = new UserFilterQuery();
    }

    [Given(@"user get request with query pageable")]
    public void GivenUserGetRequestWithQueryPageable()
    {
        m_ScenarioContext[Constant.Pageable] = new Pageable
                                               {
                                                   Page = 1,
                                                   Size = 10
                                               };
    }

    [When(@"they request to get all users")]
    public async Task WhenTheyRequestToGetAllUsers()
    {
        var filterQuery = m_ScenarioContext.Get<UserFilterQuery>(Constant.FilterParam);
        var pageable    = m_ScenarioContext.Get<Pageable>(Constant.Pageable);
        var result      = await m_UserService.GetAll(filterQuery, pageable);

        m_ScenarioContext[Constant.Result]       = result;
        m_ScenarioContext[Constant.ActionResult] = result.ActionResult;
    }

    [Then(@"the system returns a paginated list of users")]
    public void ThenTheSystemReturnsAPaginatedListOfUsers()
    {
        var pageable = m_ScenarioContext.Get<Pageable>(Constant.Pageable);
        var result   = m_ScenarioContext.Get<Result<Page<UserResponse>>>(Constant.Result);

        result.Value.ShouldNotBeNull();
        result.Value.Items.Count.ShouldBe(pageable.Size);
        result.Value.PageNumber.ShouldBe(pageable.Page);
    }

    [Given(@"user get request with Id")]
    public void GivenUserGetRequestWithId()
    {
        m_ScenarioContext[Constant.Id] = Example.Entity.User.GetEmployee.Id;
    }

    [When(@"user is fetched by Id from the database")]
    public async Task WhenUserIsFetchedByIdFromTheDatabase()
    {
        var id     = m_ScenarioContext.Get<Guid>(Constant.Id);
        var result = await m_UserService.GetOne(id);

        m_ScenarioContext[Constant.Result]       = result;
        m_ScenarioContext[Constant.ActionResult] = result.ActionResult;
    }

    [Then(@"user response should be (.*)")]
    public void ThenUserResponseShouldBe(int statusCode)
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

    [Then(@"response should contain the user with the given Id")]
    public void ThenResponseShouldContainTheUserWithTheGivenId()
    {
        var result = m_ScenarioContext.Get<Result<UserResponse>>(Constant.Result);
        var seeder = Example.Entity.User.GetEmployee;

        result.Value.ShouldNotBeNull();
        result.Value.Id.ShouldBe(m_ScenarioContext.Get<Guid>(Constant.Id));
        result.Value.FirstName.ShouldBe(seeder.FirstName);
        result.Value.Activated.ShouldBe(seeder.Activated);
        result.Value.Address.ShouldBe(seeder.Address);
        result.Value.Department.ShouldBe(seeder.Department);
        result.Value.PhoneNumber.ShouldBe(seeder.PhoneNumber);
        result.Value.Role.ShouldBe(seeder.Role);
        result.Value.Username.ShouldBe(seeder.Username);
        result.Value.DateOfBirth.ShouldBe(seeder.DateOfBirth);
        result.Value.Email.ShouldBe(seeder.Email);
        result.Value.Gender.ShouldBe(seeder.Gender);
        result.Value.LastName.ShouldBe(seeder.LastName);
        result.Value.UniqueIdentificationNumber.ShouldBe(seeder.UniqueIdentificationNumber);
        result.Value.CreatedAt.ShouldBeInRange(seeder.CreatedAt.Subtract(TimeSpan.FromSeconds(5)), seeder.CreatedAt.AddSeconds(5));
        result.Value.ModifiedAt.ShouldBeInRange(seeder.ModifiedAt.Subtract(TimeSpan.FromSeconds(5)), seeder.ModifiedAt.AddSeconds(5));
    }

    [Given(@"a user has received a valid activation token")]
    public void GivenAUserHasReceivedAValidActivationToken()
    {
        string token = m_AuthorizationService.GenerateTokenFor(Example.Entity.User.GetEmployee);
        m_ScenarioContext[Constant.Token] = token;
    }

    [Given(@"user activation request")]
    public void GivenUserActivationRequest()
    {
        m_ScenarioContext[Constant.ActivationRequest] = Example.Entity.User.UserActivationRequest;
    }

    [When(@"they submit a matching password and confirm password")]
    public async Task WhenTheySubmitAMatchingPasswordAndConfirmPassword()
    {
        var actionRequest = m_ScenarioContext.Get<UserActivationRequest>(Constant.ActivationRequest);
        var token         = m_ScenarioContext.Get<string>(Constant.Token);

        var result = await m_UserService.Activate(actionRequest, token);
        m_ScenarioContext[Constant.Result]       = result;
        m_ScenarioContext[Constant.ActionResult] = result.ActionResult;
    }

    [Given(@"user request password reset request with data")]
    public void GivenUserRequestPasswordResetRequestWithData()
    {
        m_ScenarioContext[Constant.PasswordReset] = new UserRequestPasswordResetRequest
                                                    {
                                                        Email = Example.Entity.User.GetEmployee.Email
                                                    };
    }

    [When(@"they request a password reset")]
    public async Task WhenTheyRequestAPasswordReset()
    {
        var request = m_ScenarioContext.Get<UserRequestPasswordResetRequest>(Constant.PasswordReset);
        var result  = await m_UserService.RequestPasswordReset(request);

        m_ScenarioContext[Constant.Result]       = result;
        m_ScenarioContext[Constant.ActionResult] = result.ActionResult;
    }
}

file static class Constant
{
    public const string LoginRequest      = "EmployeeCreateRequest";
    public const string LoginResult       = "EmployeeCreateResult";
    public const string FilterParam       = "UserFilterQuery";
    public const string Pageable          = "UserPageable";
    public const string Id                = "UserId";
    public const string Result            = "UserResult";
    public const string ActionResult      = "UserActionResult";
    public const string Token             = "UserToken";
    public const string ActivationRequest = "UserActivatioRequest";
    public const string PasswordReset     = "UserPasswordReset";
}
