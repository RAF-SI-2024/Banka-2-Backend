using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.Application.Utilities;
using Bank.UserService.Mappers;
using Bank.UserService.Models;
using Bank.UserService.Repositories;
using Bank.UserService.Security;
using Bank.UserService.Services;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Bank.UserService.Test.Unit;

public class UserServiceTests
{
    private readonly IUserService  m_Sut;
    private readonly TokenProvider m_TokenProvider;
    private readonly IEmailService m_EmailService;

    private readonly IEmailRepository m_EmailRepository = Substitute.For<IEmailRepository>();
    private readonly IUserRepository  m_UserRepository  = Substitute.For<IUserRepository>();
    private readonly List<User>       mockUsers;

    #region Constructor

    public UserServiceTests()
    {
        m_EmailService  = new EmailService(m_EmailRepository, null); //TODO: AuthorizationService
        m_TokenProvider = new TokenProvider();
        m_Sut           = new Services.UserService(m_UserRepository, m_EmailService, null); //TODO: AuthorizationService
        var now = DateTime.UtcNow;

        mockUsers =
        [
            new User
            {
                Id                         = Guid.NewGuid(),
                FirstName                  = "Admin",
                LastName                   = "Admin",
                DateOfBirth                = new DateOnly(1990, 1, 1),
                Gender                     = Gender.Male,
                UniqueIdentificationNumber = "0101990710024",
                Email                      = "admin@gmail.com",
                Password                   = "admin",
                Username                   = "Admin123",
                PhoneNumber                = "+38164123456",
                Address                    = "Admin 01",
                Salt                       = Guid.NewGuid(),
                Role                       = Role.Admin,
                Department                 = "Department 01",
                CreatedAt                  = DateTime.UtcNow,
                ModifiedAt                 = DateTime.UtcNow,
                Employed                   = true,
                Activated                  = true
            },
            new User
            {
                Id                         = Guid.NewGuid(),
                FirstName                  = "John",
                LastName                   = "Smith",
                DateOfBirth                = new DateOnly(1990, 7, 20),
                Gender                     = Gender.Male,
                UniqueIdentificationNumber = "2007990500012",
                Email                      = "employee1@gmail.com",
                Username                   = "employee1",
                PhoneNumber                = "+38162345678",
                Address                    = "Oak Avenue 45",
                Password                   = "employee1",
                Salt                       = Guid.NewGuid(),
                Role                       = Role.Employee,
                Department                 = "Customer Service",
                CreatedAt                  = now,
                ModifiedAt                 = now,
                Employed                   = true,
                Activated                  = true
            },

            new User
            {
                Id                         = Guid.Parse("f63d4dae-b9d7-4d5a-9d5a-6b831c7e8b9a"),
                FirstName                  = "Maria",
                LastName                   = "Jones",
                DateOfBirth                = new DateOnly(1988, 3, 12),
                Gender                     = Gender.Female,
                UniqueIdentificationNumber = "1203988715015",
                Email                      = "employee2@bankapp.com",
                Username                   = "maria.jones",
                PhoneNumber                = "+38163456789",
                Address                    = "Pine Street 78",
                Password                   = "employee2",
                Salt                       = Guid.NewGuid(),
                Role                       = Role.Employee,
                Department                 = "Loans",
                CreatedAt                  = now,
                ModifiedAt                 = now,
                Employed                   = true,
                Activated                  = true,
                Accounts                   = []
            },
            new User
            {
                Id                         = Guid.Parse("a1b2c3d4-e5f6-4a5b-8c9d-1e2f3a4b5c6d"),
                FirstName                  = "Peter",
                LastName                   = "Parker",
                DateOfBirth                = new DateOnly(1992, 8, 10),
                Gender                     = Gender.Male,
                UniqueIdentificationNumber = "1008992450037",
                Email                      = "client1@gmail.com",
                Username                   = "peter.parker",
                PhoneNumber                = "+38166567890",
                Address                    = "Queens Boulevard 20",
                Password                   = "client1",
                Salt                       = Guid.NewGuid(),
                Role                       = Role.Client,
                Department                 = null,
                CreatedAt                  = now,
                ModifiedAt                 = now,
                Employed                   = null,
                Activated                  = true
            },
            new User
            {
                Id                         = Guid.Parse("b2c3d4e5-f6a7-4b5c-9d0e-1f2a3b4c5d6e"),
                FirstName                  = "Mary",
                LastName                   = "Watson",
                DateOfBirth                = new DateOnly(1993, 4, 15),
                Gender                     = Gender.Female,
                UniqueIdentificationNumber = "1504993725015",
                Email                      = "client2@gmail.com",
                Username                   = "mary.watson",
                PhoneNumber                = "+38167678901",
                Address                    = "Manhattan Avenue 30",
                Password                   = "client2",
                Salt                       = Guid.NewGuid(),
                Role                       = Role.Client,
                Department                 = null,
                CreatedAt                  = now,
                ModifiedAt                 = now,
                Employed                   = null,
                Activated                  = true
            },
            new User
            {
                Id                         = Guid.Parse("c3d4e5f6-a7b8-4c5d-9e0f-1a2b3c4d5e6f"),
                FirstName                  = "Marko",
                LastName                   = "Jovanović",
                DateOfBirth                = new DateOnly(1989, 9, 25),
                Gender                     = Gender.Male,
                UniqueIdentificationNumber = "2509989300007",
                Email                      = "client3@gmail.com",
                Username                   = "marko.jovanovic",
                PhoneNumber                = "+38168789012",
                Address                    = "Cara Dušana 55",
                Password                   = "client3",
                Salt                       = Guid.NewGuid(),
                Role                       = Role.Client,
                Department                 = null,
                CreatedAt                  = now,
                ModifiedAt                 = now,
                Employed                   = null,
                Activated                  = true
            }
        ];
    }

    #endregion

    [Fact]
    public async Task GetAll_ShouldReturnEmptyList_WhenNoUsersExist()
    {
        // Arrange 
        var        userFilterQuery = new UserFilterQuery();
        var        pageable        = new Pageable();
        Page<User> users           = new([], pageable.Page, pageable.Size, 0);

        m_UserRepository.FindAll(userFilterQuery, pageable)
                        .Returns(users);

        // Act
        var result = await m_Sut.GetAll(userFilterQuery, pageable);

        // Assert
        result.Value!.Items.Should()
              .BeEmpty();
    }

    [Fact]
    public async Task GetAll_ShouldReturnUsers_WhenSomeUsersExist()
    {
        // Arrange
        var        userFilterQuery = new UserFilterQuery();
        var        pageable        = new Pageable();
        Page<User> users           = new(mockUsers, pageable.Page, pageable.Size, mockUsers.Count);

        m_UserRepository.FindAll(userFilterQuery, pageable)
                        .Returns(users);

        // Act
        var result = await m_Sut.GetAll(userFilterQuery, pageable);

        // Assert
        result.Value!.Items.Should()
              .BeEquivalentTo(mockUsers.Select(x => x.ToResponse()));
    }

    [Fact]
    public async Task GetAll_ShouldReturnUsers_WhenPageIsOneAndSizeIsTwo()
    {
        // Arrange
        var userFilterQuery = new UserFilterQuery();

        var pageable = new Pageable
                       {
                           Page = 1,
                           Size = 2,
                       };

        Page<User> users = new(mockUsers.GetRange((pageable.Page - 1) * pageable.Size, pageable.Size), pageable.Page, pageable.Size, mockUsers.Count);

        m_UserRepository.FindAll(userFilterQuery, pageable)
                        .Returns(users);

        // Act
        var result = await m_Sut.GetAll(userFilterQuery, pageable);

        // Assert
        result.Value!.Items.Should()
              .BeEquivalentTo(mockUsers.GetRange(0, 2)
                                       .Select(x => x.ToResponse()));
    }

    [Fact]
    public async Task GetAll_ShouldReturnUsers_WhenPageIsTwoAndSizeIsTwo()
    {
        // Arrange
        var userFilterQuery = new UserFilterQuery();

        var pageable = new Pageable
                       {
                           Page = 2,
                           Size = 2,
                       };

        Page<User> users = new(mockUsers.GetRange((pageable.Page - 1) * pageable.Size, pageable.Size), pageable.Page, pageable.Size, mockUsers.Count);

        m_UserRepository.FindAll(userFilterQuery, pageable)
                        .Returns(users);

        // Act
        var result = await m_Sut.GetAll(userFilterQuery, pageable);

        // Assert
        result.Value!.Items.Should()
              .BeEquivalentTo(mockUsers.GetRange(2, 2)
                                       .Select(x => x.ToResponse()));
    }

    //TODO GetAll check for Exceptions

    [Fact]
    public async Task GetOne_ShouldReturnNull_WhenThereIsNoUserExists()
    {
        // Arrange
        m_UserRepository.FindById(Arg.Any<Guid>())
                        .ReturnsNull();

        // Act
        var result = await m_Sut.GetOne(Guid.NewGuid());

        // Assert
        result.Value!.Should()
              .BeNull();
    }

    [Fact]
    public async Task GetOne_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var user = mockUsers[0];

        m_UserRepository.FindById(user.Id)
                        .Returns(user);

        // Act
        var result = await m_Sut.GetOne(user.Id);

        // Assert
        result.Value!.Should()
              .BeEquivalentTo(user.ToResponse());
    }

    //TODO GetOne check for Exceptions

    [Fact]
    public async Task Login_ShouldReturnNull_WhenThereIsNoUserWithEmail()
    {
        // Arrange
        var user = mockUsers[0];

        var userLoginRequest = new UserLoginRequest()
                               {
                                   Email    = user.Email,
                                   Password = user.Password!
                               };

        m_UserRepository.FindByEmail(Arg.Any<string>())
                        .ReturnsNull();

        // Act
        var result = await m_Sut.Login(userLoginRequest);

        // Assert
        result.Value.Should()
              .BeNull();
    }

    [Fact]
    public async Task Login_ShouldReturnNull_WhenUserIsNotActivated()
    {
        // Arrange
        var user = mockUsers[0];
        user.Activated = false;

        var userLoginRequest = new UserLoginRequest()
                               {
                                   Email    = user.Email,
                                   Password = user.Password ?? "rizzskibidi"
                               };

        m_UserRepository.FindByEmail(user.Email)
                        .Returns(user);

        // Act
        var result = await m_Sut.Login(userLoginRequest);

        // Assert
        result.Value.Should()
              .BeNull();
    }

    [Fact]
    public async Task Login_ShouldReturnNull_WhenUserPasswordIsInvalid()
    {
        // Arrange
        var user = mockUsers[0];

        var userLoginRequest = new UserLoginRequest()
                               {
                                   Email    = user.Email,
                                   Password = ""
                               };

        m_UserRepository.FindByEmail(user.Email)
                        .Returns(user);

        // Act
        var result = await m_Sut.Login(userLoginRequest);

        // Assert
        result.Value.Should()
              .BeNull();
    }

    [Fact]
    public async Task Login_ShouldReturnUser_WhenCredentialsAreValid()
    {
        // Arrange
        var user = mockUsers[0];

        var userLoginRequest = new UserLoginRequest()
                               {
                                   Email    = user.Email,
                                   Password = user.Password!
                               };

        user.Password = HashingUtilities.HashPassword(user.Password!, user.Salt);

        var tokenResponse = new UserLoginResponse()
                            {
                                Token = m_TokenProvider.Create(user),
                                User  = user.ToResponse()
                            };

        m_UserRepository.FindByEmail(user.Email)
                        .Returns(user);

        // Act
        var result = await m_Sut.Login(userLoginRequest);

        // Assert
        result.Value.Should()
              .BeEquivalentTo(tokenResponse);
    }

    //TODO Login check for Exceptions

    [Fact]
    public async Task Activate_ShouldReturnNull_WhenTokenIsInvalid()
    {
        // Arrange 
        var token = "invalid token";

        var userActivationRequest = new UserActivationRequest()
                                    {
                                        Password        = "",
                                        ConfirmPassword = ""
                                    };

        // Act
        var result = async () => await m_Sut.Activate(userActivationRequest, token);

        // Assert
        await result.Should()
                    .ThrowAsync<SecurityTokenMalformedException>();
    }

    //TODO Check for Result.BadRequest("Invalid token")

    [Fact]
    public async Task Activate_ShouldReturnNull_WhenPasswordIsInvalid()
    {
        // Arrange 
        var token =
        "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE3NDA2OTg4MjIsImlkIjoiYjUyYjY0NTAtNWQ3Yi00YTFhLTkwZmEtMWM3MTMzYjAzZWE3Iiwicm9sZSI6IkFkbWluIiwiaWF0IjoxNzQwNjk3MDIyLCJuYmYiOjE3NDA2OTcwMjJ9.xsFws-SGAKziwlKY66wj4BCrGHtdYeVSIhoUTpuLc3s";

        var userActivationRequest = new UserActivationRequest()
                                    {
                                        Password        = "OnePassword",
                                        ConfirmPassword = "OtherPassword"
                                    };

        // Act
        var result       = await m_Sut.Activate(userActivationRequest, token);
        var actionResult = (BadRequestObjectResult)result.ActionResult;

        // Assert
        actionResult.Value.Should()
                    .Be("Passwords do not match.");
    }

    [Fact]
    public async Task Activate_ShouldReturnAccepted_WhenPasswordIsCorrect()
    {
        // Arrange 
        var token =
        "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE3NDA2OTg4MjIsImlkIjoiYjUyYjY0NTAtNWQ3Yi00YTFhLTkwZmEtMWM3MTMzYjAzZWE3Iiwicm9sZSI6IkFkbWluIiwiaWF0IjoxNzQwNjk3MDIyLCJuYmYiOjE3NDA2OTcwMjJ9.xsFws-SGAKziwlKY66wj4BCrGHtdYeVSIhoUTpuLc3s";

        var user = mockUsers[0];

        var userActivationRequest = new UserActivationRequest()
                                    {
                                        Password        = user.Password!,
                                        ConfirmPassword = user.Password!
                                    };

        // Act
        var result       = await m_Sut.Activate(userActivationRequest, token);
        var actionResult = (AcceptedResult)result.ActionResult;

        // Assert
        actionResult.StatusCode.Should()
                    .Be(202);
    }

    //TODO Active check for Exceptions
    //TODO RequestPasswordReset tests
}
