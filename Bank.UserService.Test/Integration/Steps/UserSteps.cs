using System.Reflection;

using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.Permissions.Services;
using Bank.UserService.Controllers;
using Bank.UserService.Services;
using Bank.UserService.Test.Examples.Entities;
using Bank.UserService.Test.Integration.Services;

using Microsoft.AspNetCore.Mvc;

using Shouldly;

namespace Bank.UserService.Test.Steps;

[Binding]
public class UserSteps(IUserService userService, ScenarioContext scenarioContext, IAuthorizationServiceFactory authorizationServiceFactory, UserController userController)
{
    private readonly IUserService                 m_UserService                 = userService;
    private readonly ScenarioContext              m_ScenarioContext             = scenarioContext;
    private readonly IAuthorizationServiceFactory m_AuthorizationServiceFactory = authorizationServiceFactory;
    private readonly UserController               m_UserController              = userController;

    [Given(@"user login request")]
    public void GivenUserLoginRequest()
    {
        m_ScenarioContext[Constant.LoginRequest] = Example.Entity.User.LoginRequest;
    }

    [When(@"user sends valid login request")]
    public async Task WhenUserSendsValidLoginRequest()
    {
        var instance = m_AuthorizationServiceFactory as TestAuthorizationServiceFactory;

        instance!.Permissions = new Permissions.Domain.Permissions(Permission.Admin);

        instance.UserId = Example.Entity.AccountCurrency.EmployeeId;

        var field = m_UserService.GetType()
                                 .GetField("m_AuthorizationServiceFactory", BindingFlags.Instance | BindingFlags.NonPublic);

        field?.SetValue(m_UserService, m_AuthorizationServiceFactory);

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
        var instance = m_AuthorizationServiceFactory as TestAuthorizationServiceFactory;

        string token = instance.AuthorizationService.GenerateTokenFor(Example.Entity.User.GetEmployee.Id, Example.Entity.User.GetEmployee.Permissions);

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

    [Given(@"user update permission request with data")]
    public void GivenUserUpdatePermissionRequestWithData()
    {
        m_ScenarioContext[Constant.Permission] = Example.Entity.User.UserUpdatePermissionRequest;
    }

    [Given(@"user id")]
    public void GivenUserId()
    {
        m_ScenarioContext[Constant.Id] = Example.Entity.User.Id;
    }

    [When(@"they request to update user permission")]
    public async Task WhenTheyRequestToUpdateUserPermission()
    {
        var request = m_ScenarioContext.Get<UserUpdatePermissionRequest>(Constant.Permission);

        var id = m_ScenarioContext.Get<Guid>(Constant.Id);

        var result = await m_UserService.UpdatePermission(id, request);

        m_ScenarioContext[Constant.Result]       = result;
        m_ScenarioContext[Constant.ActionResult] = result.ActionResult;
    }

    [Given(@"user password reset request with data")]
    public void GivenUserPasswordResetRequestWithData()
    {
        m_ScenarioContext[Constant.PasswordReset] = Example.Entity.User.UserPasswordResetRequest;
    }

    [Given(@"a user has received a valid activation token for password reset")]
    public void GivenAUserHasReceivedAValidActivationTokenForPasswordReset()
    {
        var instance = m_AuthorizationServiceFactory as TestAuthorizationServiceFactory;

        instance!.Permissions = new Permissions.Domain.Permissions(Permission.Admin);

        instance.UserId = Example.Entity.AccountCurrency.EmployeeId;

        var field = m_UserService.GetType()
                                 .GetField("m_AuthorizationServiceFactory", BindingFlags.Instance | BindingFlags.NonPublic);

        field?.SetValue(m_UserService, m_AuthorizationServiceFactory);

        string token = m_AuthorizationServiceFactory.AuthorizationService.GenerateTokenFor(Example.Entity.User.GetEmployee.Id, Example.Entity.User.GetEmployee.Permissions);

        m_ScenarioContext[Constant.Token] = token;
    }

    [When(@"they request to reset user password")]
    public async Task WhenTheyRequestToResetUserPassword()
    {
        var request = m_ScenarioContext.Get<UserPasswordResetRequest>(Constant.PasswordReset);

        var token = m_ScenarioContext.Get<string>(Constant.Token);

        var result = await m_UserService.PasswordReset(request, token);

        m_ScenarioContext[Constant.Result]       = result;
        m_ScenarioContext[Constant.ActionResult] = result.ActionResult;
    }

    [Given(@"a user filter query and pageable parameters")]
    public void GivenUserFilterQueryAndPageable()
    {
        m_ScenarioContext[Constant.UserFilterQuery] = new UserFilterQuery();
        m_ScenarioContext[Constant.Pageable]        = new Pageable();
    }

    [When(@"a GET request is sent to fetch all users")]
    public async Task WhenGetAllUsers()
    {
        var query    = m_ScenarioContext.Get<UserFilterQuery>(Constant.UserFilterQuery);
        var pageable = m_ScenarioContext.Get<Pageable>(Constant.Pageable);

        var result = await m_UserController.GetAll(query, pageable);
        m_ScenarioContext[Constant.GetUsersResult] = result;
    }

    [Then(@"the response should contain the list of users")]
    public void ThenResponseContainsUsers()
    {
        var result = m_ScenarioContext.Get<ActionResult<Page<UserResponse>>>(Constant.GetUsersResult);
        result.Result.ShouldBeOfType<OkObjectResult>();
        result.ShouldNotBeNull();
    }

    [Given(@"a valid user Id")]
    public void GivenValidUserId()
    {
        m_ScenarioContext[Constant.UserId] = Example.Entity.User.Id;
    }

    [When(@"a GET request is sent to fetch a user by Id")]
    public async Task WhenGetUserById()
    {
        var id     = m_ScenarioContext.Get<Guid>(Constant.UserId);
        var result = await m_UserController.GetOne(id);
        m_ScenarioContext[Constant.GetUserResult] = result;
    }

    [Then(@"the response should contain the user")]
    public void ThenResponseShouldContainUser()
    {
        var result = m_ScenarioContext.Get<ActionResult<UserResponse>>(Constant.GetUserResult);
        result.Result.ShouldBeOfType<OkObjectResult>();
        result.ShouldNotBeNull();
    }

    [Given(@"a valid user login request")]
    public void GivenLoginRequest()
    {
        m_ScenarioContext[Constant.UserLoginRequest] = Example.Entity.User.LoginRequest;
    }

    [When(@"a POST request is sent to the login endpoint")]
    public async Task WhenLoginPost()
    {
        var request = m_ScenarioContext.Get<UserLoginRequest>(Constant.UserLoginRequest);
        var result  = await m_UserController.Login(request);
        m_ScenarioContext[Constant.LoginResult] = result;
    }

    [Then(@"the login response should be successfully returned")]
    public void ThenLoginResponseShouldBeSuccessfullyReturned()
    {
        var result = m_ScenarioContext.Get<ActionResult<UserLoginResponse>>(Constant.LoginResult);
        result.Result.ShouldBeOfType<OkObjectResult>();
        result.ShouldNotBeNull();
    }

    [Given(@"a valid activation request and token")]
    public void GivenActivationRequest()
    {
        var instance = m_AuthorizationServiceFactory as TestAuthorizationServiceFactory;

        string token = instance.AuthorizationService.GenerateTokenFor(Example.Entity.User.GetEmployee.Id, Example.Entity.User.GetEmployee.Permissions);

        m_ScenarioContext[Constant.Token] = token;

        m_ScenarioContext[Constant.ActivationRequest] = Example.Entity.User.UserActivationRequest;
    }

    [When(@"a POST request is sent to the activation endpoint")]
    public async Task WhenActivate()
    {
        var request = m_ScenarioContext.Get<UserActivationRequest>(Constant.ActivationRequest);
        var token   = m_ScenarioContext.Get<string>(Constant.Token);
        var result  = await m_UserController.Activate(request, token);
        m_ScenarioContext[Constant.ActivationResult] = result;
    }

    [Then(@"the response should indicate successful activation")]
    public void ThenActivationSuccess()
    {
        var result = m_ScenarioContext.Get<ActionResult>(Constant.ActivationResult);
        result.ShouldBeOfType<AcceptedResult>();
    }

    [Given(@"a valid password reset request")]
    public void GivenPasswordResetRequest()
    {
        m_ScenarioContext[Constant.PasswordResetRequest] = new UserRequestPasswordResetRequest
                                                           {
                                                               Email = Example.Entity.User.GetEmployee.Email
                                                           };
    }

    [When(@"a POST request is sent to request password reset")]
    public async Task WhenRequestReset()
    {
        var request = m_ScenarioContext.Get<UserRequestPasswordResetRequest>(Constant.PasswordResetRequest);
        var result  = await m_UserController.RequestPasswordReset(request);
        m_ScenarioContext[Constant.PasswordResetResult] = result;
    }

    [Then(@"the response should indicate reset email was sent")]
    public void ThenResetEmailSent()
    {
        var result = m_ScenarioContext.Get<ActionResult>(Constant.PasswordResetResult);
        result.ShouldBeOfType<AcceptedResult>();
    }

    [Given(@"a valid new password and reset token")]
    public void GivenNewPasswordAndToken()
    {
        m_ScenarioContext[Constant.PasswordReset] = Example.Entity.User.UserPasswordResetRequest;

        var instance = m_AuthorizationServiceFactory as TestAuthorizationServiceFactory;

        instance!.Permissions = new Permissions.Domain.Permissions(Permission.Admin);

        instance.UserId = Example.Entity.AccountCurrency.EmployeeId;

        var field = m_UserService.GetType()
                                 .GetField("m_AuthorizationServiceFactory", BindingFlags.Instance | BindingFlags.NonPublic);

        field?.SetValue(m_UserService, m_AuthorizationServiceFactory);

        string token = m_AuthorizationServiceFactory.AuthorizationService.GenerateTokenFor(Example.Entity.User.GetEmployee.Id, Example.Entity.User.GetEmployee.Permissions);

        m_ScenarioContext[Constant.Token] = token;
    }

    [When(@"a POST request is sent to reset password")]
    public async Task WhenPostPasswordReset()
    {
        var request = m_ScenarioContext.Get<UserPasswordResetRequest>(Constant.PasswordReset);
        var token   = m_ScenarioContext.Get<string>(Constant.Token);
        var result  = await m_UserController.PasswordReset(request, token);
        m_ScenarioContext[Constant.PasswordResetConfirmResult] = result;
    }

    [Then(@"the password should be reset successfully")]
    public void ThenPasswordResetSuccessful()
    {
        var result = m_ScenarioContext.Get<ActionResult>(Constant.PasswordResetConfirmResult);
        result.ShouldBeOfType<AcceptedResult>();
    }

    [Given(@"a user Id and permission update request")]
    public void GivenUserIdAndPermissionUpdate()
    {
        m_ScenarioContext[Constant.UserId]                  = Example.Entity.User.Id;
        m_ScenarioContext[Constant.PermissionUpdateRequest] = Example.Entity.User.UserUpdatePermissionRequest;
    }

    [When(@"a PUT request is sent to update user permission")]
    public async Task WhenPutPermissionUpdate()
    {
        var id      = m_ScenarioContext.Get<Guid>(Constant.UserId);
        var request = m_ScenarioContext.Get<UserUpdatePermissionRequest>(Constant.PermissionUpdateRequest);
        var result  = await m_UserController.UpdatePermission(id, request);
        m_ScenarioContext[Constant.PermissionUpdateResult] = result;
    }

    [Then(@"the user permission should be updated successfully")]
    public void ThenPermissionUpdatedSuccessfully()
    {
        var result = m_ScenarioContext.Get<ActionResult>(Constant.PermissionUpdateResult);
        result.ShouldBeOfType<OkResult>();
    }
}

file static class Constant
{
    public const string LoginRequest                = "EmployeeCreateRequest";
    public const string LoginResult                 = "EmployeeCreateResult";
    public const string FilterParam                 = "UserFilterQuery";
    public const string Pageable                    = "UserPageable";
    public const string Id                          = "UserId";
    public const string Result                      = "UserResult";
    public const string ActionResult                = "UserActionResult";
    public const string Token                       = "UserToken";
    public const string ActivationRequest           = "UserActivatioRequest";
    public const string PasswordReset               = "UserPasswordReset";
    public const string Permission                  = "UserPermission";
    public const string PasswordResetRequest        = "UserPasswordResetRequest";
    public const string PasswordResetConfirmRequest = "UserPasswordResetConfirmRequest";
    public const string GetUsersResult              = "GetUsersResult";
    public const string GetUserResult               = "GetUserResult";
    public const string UserId                      = "UserId";
    public const string UserLoginRequest            = "UserLoginRequest";
    public const string ActivationResult            = "ActivationResult";
    public const string PasswordResetResult         = "PasswordResetResult";
    public const string PasswordResetConfirmResult  = "PasswordResetConfirmResult";
    public const string PermissionUpdateRequest     = "PermissionUpdateRequest";
    public const string PermissionUpdateResult      = "PermissionUpdateResult";
    public const string UserFilterQuery             = "UserFilterQuery";
}
