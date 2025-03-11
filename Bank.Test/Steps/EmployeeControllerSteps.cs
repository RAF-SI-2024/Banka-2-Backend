using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.UserService.Models;
using Bank.UserService.Repositories;
using Bank.UserService.Services;

using Microsoft.AspNetCore.Mvc;

using Reqnroll;

namespace Bank.IntegrationTests.Steps;

[Binding]
public class EmployeeControllerSteps
{
    private readonly UserRepository  _userRepository;
    private readonly ScenarioContext _scenarioContext;
    private readonly EmployeeService _employeeService;

    public EmployeeControllerSteps(UserRepository employeeRepository, ScenarioContext scenarioContext, EmployeeService employeeService)
    {
        _userRepository  = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        _scenarioContext = scenarioContext;
        _employeeService = employeeService;
    }

    [Given(@"the database contains multiple employees")]
    public void GivenTheDatabaseContainsMultipleEmployees()
    {
        var employees = new List<User>
                        {
                            new User
                            {
                                Id                         = Guid.NewGuid(),
                                FirstName                  = "John",
                                LastName                   = "Doe",
                                Email                      = "john.doe@example.com",
                                Username                   = "johndoe",
                                Role                       = Role.Employee,
                                Address                    = "123 Main St",
                                PhoneNumber                = "+381601234567",
                                UniqueIdentificationNumber = "1234567890123",
                                Gender                     = Gender.Male,
                                DateOfBirth                = new DateOnly(1990, 1, 1),
                                CreatedAt                  = DateTime.UtcNow,
                                ModifiedAt                 = DateTime.UtcNow,
                                Employed                   = true,
                                Activated                  = false
                            },
                            new User
                            {
                                Id                         = Guid.NewGuid(),
                                FirstName                  = "Jane",
                                LastName                   = "Smith",
                                Email                      = "jane.smith@example.com",
                                Username                   = "janesmith",
                                Role                       = Role.Employee, // 🔹 Osiguravamo da su svi sa ulogom Employee
                                Address                    = "456 Elm St",
                                PhoneNumber                = "+381601234568",
                                UniqueIdentificationNumber = "9876543210987",
                                Gender                     = Gender.Female,
                                DateOfBirth                = new DateOnly(1992, 2, 2),
                                CreatedAt                  = DateTime.UtcNow,
                                ModifiedAt                 = DateTime.UtcNow,
                                Employed                   = true,
                                Activated                  = false
                            }
                        };

        foreach (var employee in employees)
        {
            _userRepository.Add(employee);
        }

        _scenarioContext["Employees"] = employees;
    }

    [When(@"I request all employees with no filters")]
    public async Task WhenIRequestAllEmployeesWithNoFilters()
    {
        var employeeFilterQuery = new UserFilterQuery
                                  {
                                      Role = Role.Employee
                                  };

        var pageable = new Pageable { Page = 1, Size = 10 }; // Paginacija

        var result = await _employeeService.GetAll(employeeFilterQuery, pageable);
        _scenarioContext["ApiResponse"] = result;
    }

    [Then(@"the response should contain a list of employees")]
    public void ThenTheResponseShouldContainAListOfEmployees()
    {
        var result = _scenarioContext.Get<Result<Page<EmployeeResponse>>>("ApiResponse");

        Assert.IsNotNull(result.Value, "Lista zaposlenih ne sme biti null.");
        Assert.IsTrue(result.Value.Items.Count > 0, "Lista zaposlenih treba da sadrži barem jednog zaposlenog.");
    }

    [Then(@"all employees should have the role Employee")]
    public void ThenAllEmployeesShouldHaveTheRoleEmployee()
    {
        var result = _scenarioContext.Get<Result<Page<EmployeeResponse>>>("ApiResponse");

        Assert.IsTrue(result.Value.Items.All(e => e.Role == Role.Employee), "Svi zaposleni u odgovoru treba da imaju ulogu Employee.");
    }

    [Then(@"the API should respond with status 200 OK")]
    public void ThenTheAPIShouldRespondWithStatus200OK()
    {
        var result = _scenarioContext.Get<Result<Page<EmployeeResponse>>>("ApiResponse");

        Assert.IsInstanceOf<OkObjectResult>(result.ActionResult, "Očekivan odgovor je 200 OK.");
    }

    [Given(@"an employee exists with ID ""(.*)""")]
    public void GivenAnEmployeeExistsWithID(Guid id)
    {
        var employee = new User
                       {
                           Id                         = id,
                           FirstName                  = "John",
                           LastName                   = "Doe",
                           Email                      = "john.doe@example.com",
                           Username                   = "johndoe",
                           Role                       = Role.Employee,
                           Address                    = "123 Main St",
                           PhoneNumber                = "+381601234567",
                           UniqueIdentificationNumber = "1234567890123",
                           Gender                     = Gender.Male,
                           DateOfBirth                = new DateOnly(1990, 1, 1),
                           CreatedAt                  = DateTime.UtcNow,
                           ModifiedAt                 = DateTime.UtcNow,
                           Employed                   = true,
                           Activated                  = false
                       };

        _userRepository.Add(employee);
        _scenarioContext["EmployeeId"] = id;
        _scenarioContext["Employee"]   = employee;
    }

    [When(@"I request the employee with ID ""(.*)""")]
    public async Task WhenIRequestTheEmployeeWithID(Guid id)
    {
        var result = await _employeeService.GetOne(id);
        _scenarioContext["ApiResponse"] = result;
    }

    [Then(@"the response should contain the employee details")]
    public void ThenTheResponseShouldContainTheEmployeeDetails()
    {
        var result = _scenarioContext.Get<Result<EmployeeResponse>>("ApiResponse");

        Assert.IsNotNull(result.Value, "Odgovor ne sme biti null.");
        Assert.AreEqual("John",        result.Value.FirstName, "Ime zaposlenog ne odgovara.");
        Assert.AreEqual("Doe",         result.Value.LastName,  "Prezime zaposlenog ne odgovara.");
        Assert.AreEqual(Role.Employee, result.Value.Role,      "Uloga zaposlenog nije Employee.");
    }

    [Then(@"the request should be successful with status 200 OK")]
    public void ThenTheRequestShouldBeSuccessfulWithStatus200OK()
    {
        var result = _scenarioContext.Get<Result<EmployeeResponse>>("ApiResponse");

        Assert.IsInstanceOf<OkObjectResult>(result.ActionResult, "Očekivan odgovor je 200 OK, ali je dobijen drugačiji status.");
    }

    [Given(@"a user exists with ID ""(.*)"" and has role ""(.*)""")]
    [Given(@"a user exists with ID ""(.*)"" and has role ""(.*)""")]
    public void GivenAUserExistsWithIDAndHasRole(Guid id, string roleName)
    {
        var role = Enum.Parse<Role>(roleName); // Pretvara string u `Role` enum

        var user = new User
                   {
                       Id                         = id,
                       FirstName                  = "John",
                       LastName                   = "Doe",
                       Email                      = "john.doe@example.com",
                       Username                   = "johndoe",
                       Role                       = role, // ✅ Postavljamo rolu iz scenarija (Admin, Client, itd.)
                       Address                    = "123 Main St",
                       PhoneNumber                = "+381601234567",
                       UniqueIdentificationNumber = "1234567890123",
                       Gender                     = Gender.Male,
                       DateOfBirth                = new DateOnly(1990, 1, 1),
                       CreatedAt                  = DateTime.UtcNow,
                       ModifiedAt                 = DateTime.UtcNow,
                       Employed                   = true,
                       Activated                  = false
                   };

        _userRepository.Add(user);
        _scenarioContext["UserId"] = id;
        _scenarioContext["User"]   = user;
    }

    [Then(@"the request should return status 404 Not Found")]
    public void ThenTheRequestShouldReturnStatus404NotFound()
    {
        var result = _scenarioContext.Get<Result<EmployeeResponse>>("ApiResponse");

        Assert.IsInstanceOf<NotFoundObjectResult>(result.ActionResult, "Očekivan odgovor je 404 Not Found, ali je dobijen drugačiji status.");
    }

    [Then(@"the response message should indicate that no employee was found with the given ID")]
    public void ThenTheResponseMessageShouldIndicateThatNoEmployeeWasFoundWithTheGivenId()
    {
        var result = _scenarioContext.Get<Result<EmployeeResponse>>("ApiResponse");

        var actualMessage = (result.ActionResult as NotFoundObjectResult)?.Value?.ToString();
        StringAssert.Contains("No Employee found with Id", actualMessage, "Poruka o grešci nije tačna ili ne sadrži očekivani tekst.");
    }
}
