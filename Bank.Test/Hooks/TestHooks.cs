using Bank.Application.Endpoints;
using Bank.UserService.Configurations;
using Bank.UserService.Database;
using Bank.UserService.Models;
using Bank.UserService.Repositories;
using Bank.UserService.Security;
using Bank.UserService.Services;

using Microsoft.EntityFrameworkCore;

using Reqnroll;
using Reqnroll.BoDi;

namespace Bank.Test2.Hooks;

[Binding]
public class TestHooks
{
    private readonly IObjectContainer _objectContainer;

    public TestHooks(IObjectContainer objectContainer)
    {
        _objectContainer = objectContainer;
    }

    private class DontSendEmailService : IEmailService
    {
        public Task<Result> Send(EmailType type, User user) => Task.FromResult(Result.Ok());
    }

    [BeforeScenario]
    public void RegisterDependencies()
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>().UseNpgsql(Configuration.Database.GetConnectionString())
                                                                       .Options;

        var dbContext = new ApplicationContext(options);
        dbContext.Database.EnsureCreated(); // Kreira bazu ako ne postoji

        var userRepository  = new UserRepository(new ApplicationContext(options));
        var tokenProvider   = new TokenProvider(); // Instanciraj TokenProvider
        var emailService    = new DontSendEmailService();
        var userService     = new UserService.Services.UserService(userRepository, emailService, null); //TODO: AuthorizationService
        var employeeService = new EmployeeService(userRepository, emailService);

        // 🔹 Registracija u Reqnroll DI kontejner
        _objectContainer.RegisterInstanceAs<ApplicationContext>(dbContext);
        _objectContainer.RegisterInstanceAs<TokenProvider>(tokenProvider);
        _objectContainer.RegisterInstanceAs<UserRepository>(userRepository);
        _objectContainer.RegisterInstanceAs<UserService.Services.UserService>(userService);
        _objectContainer.RegisterInstanceAs<EmployeeService>(employeeService);
    }
}
