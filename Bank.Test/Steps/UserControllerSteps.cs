using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.Application.Utilities;
using Bank.UserService.Mappers;
using Bank.UserService.Models;
using Bank.UserService.Repositories;
using Bank.UserService.Security;

using Microsoft.AspNetCore.Mvc;

using Reqnroll;

namespace Bank.IntegrationTests.Steps;

[Binding]
public class UserControllerSteps
{
    private readonly UserRepository                   _userRepository;
    private readonly ScenarioContext                  _scenarioContext;
    private readonly UserService.Services.UserService _userService;
    private readonly TokenProvider                    _tokenProvider;

    public UserControllerSteps(UserRepository userRepository, ScenarioContext scenarioContext, UserService.Services.UserService userService, TokenProvider tokenProvider)
    {
        _userRepository  = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _scenarioContext = scenarioContext;
        _userService     = userService;
        _tokenProvider   = tokenProvider;
    }

    [Given(@"user with ID ""(.*)"" exists in database")]
    public void GivenUserWithIdExistsInDatabase(Guid id)
    {
        var employee = new EmployeeCreateRequest
                       {
                           FirstName                  = "Marko",
                           LastName                   = "Marković",
                           DateOfBirth                = new DateOnly(1990, 5, 20),
                           Gender                     = Gender.Male,
                           UniqueIdentificationNumber = "1234567890123",
                           Username                   = "marko.m",
                           Email                      = "marko.markovic@example.com",
                           PhoneNumber                = "+381601234567",
                           Address                    = "Ulica 123, Beograd",
                           Role                       = Role.Employee,
                           Department                 = "IT",
                           Employed                   = true
                       };

        var user = employee.ToEmployee()
                           .ToUser();

        user.Id = id;

        _userRepository.Add(user);

        _scenarioContext["UserId"] = id;
        _scenarioContext["User"]   = user;
    }

    [When(@"I request user with ID ""(.*)""")]
    public async Task WhenIRequestUserWithId(Guid id)
    {
        if (_userService == null)
        {
            throw new InvalidOperationException("UserService nije inicijalizovan!");
        }

        var result = await _userService.GetOne(id);

        _scenarioContext["ApiResponse"] = result;
    }

    [Then(@"the response should contain user details")]
    public void ThenTheResponseShouldContainUserDetails()
    {
        var result = _scenarioContext.Get<Result<UserResponse>>("ApiResponse");

        if (result.ActionResult is OkObjectResult okResult)
        {
            var user = result.Value;
            Assert.IsNotNull(user, "Korisnik nije pronađen!");
            Assert.AreEqual("Marko",    user.FirstName, "Ime korisnika ne odgovara!");
            Assert.AreEqual("Marković", user.LastName,  "Prezime korisnika ne odgovara!");
        }
        else
        {
            Assert.Fail($"API odgovor nije uspešan! Očekivano: 200 OK, Dobijeno: {result.ActionResult.GetType()
                                                                                        .Name}");
        }
    }

    [Then(@"the response should be Not Found")]
    public void ThenTheResponseShouldBeNotFound()
    {
        var result = _scenarioContext.Get<Result<UserResponse>>("ApiResponse");

        Assert.IsInstanceOf<NotFoundResult>(result.ActionResult, "Očekivan odgovor je 404 Not Found, ali je dobijeno nešto drugo.");
    }

    [When(@"I request all users with no filters")]
    public async Task WhenIRequestAllUsersWithNoFilters()
    {
        if (_userService == null)
            throw new InvalidOperationException("UserService nije inicijalizovan!");

        if (_userRepository == null)
            throw new InvalidOperationException("UserRepository nije inicijalizovan!");

        var userFilterQuery = new UserFilterQuery
                              {
                                  Email     = string.Empty,
                                  FirstName = string.Empty,
                                  LastName  = string.Empty,
                                  Role      = Role.Invalid
                              };

        var pageable = new Pageable { Page = 1, Size = 10 };

        var result = await _userService.GetAll(userFilterQuery, pageable);
        _scenarioContext["ApiResponse"] = result;
    }

    [Then(@"the response should contain at least (.*) user")]
    public void ThenTheResponseShouldContainAtLeastUser(int expectedCount)
    {
        var result = _scenarioContext.Get<Result<Page<UserResponse>>>("ApiResponse");

        Assert.IsInstanceOf<OkObjectResult>(result.ActionResult, "Očekivan odgovor je 200 OK.");
        Assert.IsTrue(result.Value.Items.Count() >= expectedCount, "API nije vratio dovoljno korisnika.");
    }

    // 📌 Given - Dodavanje korisnika u bazu
    [Given(@"a user exists with email ""(.*)"" and password ""(.*)""")]
    public void GivenAUserExistsWithEmailAndPassword(string email, string password)
    {
        var salt = Guid.NewGuid();

        var user = new User
                   {
                       Id                         = Guid.NewGuid(),
                       FirstName                  = "Test",
                       LastName                   = "User",
                       Email                      = email,
                       Username                   = email,
                       Password                   = HashingUtilities.HashPassword(password, salt),
                       Salt                       = salt,
                       Role                       = Role.Client,
                       Address                    = "Test Address",
                       PhoneNumber                = "+381601234567",
                       UniqueIdentificationNumber = "1234567890123",
                       Gender                     = Gender.Male,
                       DateOfBirth                = new DateOnly(1995, 1, 1),
                       CreatedAt                  = DateTime.UtcNow,
                       ModifiedAt                 = DateTime.UtcNow,
                       Employed                   = true,
                       Activated                  = true
                   };

        _userRepository.Add(user);
        _scenarioContext["User"] = user;
    }

    [When(@"I send a login request with email ""(.*)"" and password ""(.*)""")]
    public async Task WhenISendALoginRequestWithEmailAndPassword(string email, string password)
    {
        var loginRequest = new UserLoginRequest
                           {
                               Email    = email,
                               Password = password
                           };

        var result = await _userService.Login(loginRequest);
        _scenarioContext["ApiResponse"] = result;
    }

    [Then(@"the response should return status 200 OK")]
    public void ThenTheResponseShouldReturnStatus200OK()
    {
        var result = _scenarioContext.Get<Result<UserLoginResponse>>("ApiResponse");

        Assert.IsInstanceOf<OkObjectResult>(result.ActionResult, "Očekivan odgovor je 200 OK.");
    }

    [Then(@"the response should contain a valid token")]
    public void ThenTheResponseShouldContainAValidToken()
    {
        var result = _scenarioContext.Get<Result<UserLoginResponse>>("ApiResponse");

        Assert.IsNotNull(result.Value, "TokenResponse nije vraćen.");
        Assert.IsFalse(string.IsNullOrEmpty(result.Value.Token), "Token je prazan.");
    }

    [Then(@"the response should return status 404 Not Found")]
    public void ThenTheResponseShouldReturnStatus404NotFound()
    {
        var result = _scenarioContext.Get<Result<UserLoginResponse>>("ApiResponse");

        Assert.IsInstanceOf<NotFoundObjectResult>(result.ActionResult, "Očekivan odgovor je 404 Not Found.");
    }

    [Then(@"the response should contain the message ""(.*)""")]
    public void ThenTheResponseShouldContainTheMessage(string expectedMessage)
    {
        var result = _scenarioContext.Get<Result<UserLoginResponse>>("ApiResponse");

        if (result.ActionResult is NotFoundObjectResult notFoundResult)
        {
            var actualMessage = notFoundResult.Value as string;
            Assert.IsNotNull(actualMessage, "Poruka u NotFoundObjectResult je null.");
            Assert.AreEqual(expectedMessage, actualMessage, "Poruka o grešci nije tačna.");
        }
        else
        {
            Assert.Fail($"Očekivan NotFoundObjectResult, ali je dobijen {result.ActionResult.GetType()
                                                                               .Name}");
        }
    }

    [When(@"I send a login request with an empty email and password ""(.*)""")]
    public async Task WhenISendALoginRequestWithAnEmptyEmailAndPassword(string password)
    {
        var loginRequest = new UserLoginRequest
                           {
                               Email    = "",
                               Password = password
                           };

        var result = await _userService.Login(loginRequest);
        _scenarioContext["ApiResponse"] = result;
    }

    [Then(@"the response should return status 400 Bad Request")]
    public void ThenTheResponseShouldReturnStatus400BadRequest()
    {
        var result = _scenarioContext.Get<Result<UserLoginResponse>>("ApiResponse");

        Assert.IsInstanceOf<BadRequestObjectResult>(result.ActionResult, "Očekivan odgovor je 400 Bad Request.");
    }

    [Then(@"the response should contain the message: The password is incorrect\.")]
    public void ThenTheResponseShouldContainTheMessageThePasswordIsIncorrect()
    {
        var result = _scenarioContext.Get<Result<UserLoginResponse>>("ApiResponse");

        // Proveravamo da li je rezultat BadRequestObjectResult
        if (result.ActionResult is BadRequestObjectResult badRequestResult)
        {
            var actualMessage = badRequestResult.Value as string; // API vraća poruku kao string
            Assert.IsNotNull(actualMessage, "Poruka u BadRequestObjectResult je null.");
            Assert.AreEqual("The password is incorrect.", actualMessage, "Poruka o grešci nije tačna.");
        }
        else
        {
            Assert.Fail($"Očekivan BadRequestObjectResult, ali je dobijen {result.ActionResult.GetType()
                                                                                 .Name}");
        }
    }

    [Given(@"a valid activation token for user ""(.*)""")]
    public void GivenAValidActivationTokenForUser(string email)
    {
        var salt = Guid.NewGuid();

        var user = new User
                   {
                       Id                         = Guid.NewGuid(),
                       FirstName                  = "Test",
                       LastName                   = "User",
                       Email                      = email,
                       Username                   = email,
                       Password                   = "HashedPassword",
                       Salt                       = salt,
                       Role                       = Role.Client,
                       Address                    = "Test Address",
                       PhoneNumber                = "+381601234567",
                       UniqueIdentificationNumber = "1234567890123",
                       Gender                     = Gender.Male,
                       DateOfBirth                = new DateOnly(1995, 1, 1),
                       CreatedAt                  = DateTime.UtcNow,
                       ModifiedAt                 = DateTime.UtcNow,
                       Employed                   = true,
                       Activated                  = false
                   };

        _userRepository.Add(user);
        var token = _tokenProvider.Create(user);

        _scenarioContext["User"]            = user;
        _scenarioContext["ActivationToken"] = token;
    }

    [Given(@"a password ""(.*)"" and confirmation password ""(.*)""")]
    public void GivenAPasswordAndConfirmationPassword(string password, string confirmPassword)
    {
        var userActivationRequest = new UserActivationRequest
                                    {
                                        Password        = password,
                                        ConfirmPassword = confirmPassword
                                    };

        _scenarioContext["UserActivationRequest"] = userActivationRequest;
    }

    [When(@"I send an activation request with the token and passwords")]
    public async Task WhenISendAnActivationRequestWithTheTokenAndPasswords()
    {
        var userActivationRequest = _scenarioContext.Get<UserActivationRequest>("UserActivationRequest");
        var token                 = _scenarioContext.Get<string>("ActivationToken");

        var result = await _userService.Activate(userActivationRequest, token);
        _scenarioContext["ApiResponse"] = result;
    }

    [Then(@"the response should return status 202 Accepted")]
    public void ThenTheResponseShouldReturnStatus202Accepted()
    {
        var result = _scenarioContext.Get<Result>("ApiResponse");

        Assert.IsInstanceOf<AcceptedResult>(result.ActionResult, "Očekivan odgovor je 202 Accepted.");
    }

    [Given(@"an invalid activation token")]
    public void GivenAnInvalidActivationToken()
    {
        var invalidToken =
        "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEyMzQ1Ni03ODkwLTEyMzQtNTY3OC05MDEyMzQ1Njc4IiwibmJmIjoxNjQyMTYwNjQwLCJleHAiOjE2NzM2OTY2NDAsImlhdCI6MTY0MjE2MDY0MH0.invalidsignature";

        _scenarioContext["ActivationToken"] = invalidToken;
    }

    [Then(@"the response should contain the message: Invalid token")]
    public void ThenTheResponseShouldContainTheMessageInvalidToken()
    {
        var result = _scenarioContext.Get<Result>("ApiResponse");

        // Proveravamo da li je rezultat BadRequestObjectResult
        if (result.ActionResult is BadRequestObjectResult badRequestResult)
        {
            var actualMessage = badRequestResult.Value as string; // API vraća poruku kao string
            Assert.IsNotNull(actualMessage, "Poruka u BadRequestObjectResult je null.");
            Assert.AreEqual("Invalid token", actualMessage, "Poruka o grešci nije tačna.");
        }
        else
        {
            Assert.Fail($"Očekivan BadRequestObjectResult, ali je dobijen {result.ActionResult.GetType()
                                                                                 .Name}");
        }
    }

    [When(@"I attempt to activate the account using the invalid token")]
    public async Task WhenIAttemptToActivateTheAccountUsingTheInvalidToken()
    {
        var userActivationRequest = _scenarioContext.Get<UserActivationRequest>("UserActivationRequest");
        var token                 = _scenarioContext.Get<string>("ActivationToken");

        var result = await _userService.Activate(userActivationRequest, token);
        _scenarioContext["ApiResponse"] = result;
    }

    [Then(@"the activation response should return status 400 Bad Request")]
    public void ThenTheActivationResponseShouldReturnStatus400BadRequest()
    {
        var result = _scenarioContext.Get<Result>("ApiResponse");

        Assert.IsInstanceOf<BadRequestObjectResult>(result.ActionResult, "Očekivan odgovor je 400 Bad Request.");
    }
}
