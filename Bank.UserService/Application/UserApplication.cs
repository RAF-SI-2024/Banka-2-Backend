using Bank.Application;
using Bank.Database;
using Bank.Link;
using Bank.Link.Core;
using Bank.OpenApi;
using Bank.Permissions;
using Bank.UserService.BackgroundServices;
using Bank.UserService.Configurations;
using Bank.UserService.Database;
using Bank.UserService.Database.Seeders;
using Bank.UserService.HostedServices;
using Bank.UserService.Mappers;
using Bank.UserService.Repositories;
using Bank.UserService.Services;

using DotNetEnv;

using FluentValidation;
using FluentValidation.AspNetCore;

using Example = Bank.UserService.Database.Examples.Example;

namespace Bank.UserService.Application;

public class UserApplication
{
    public static void Run(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        Env.Load();

        builder.Services.AddValidation();
        builder.Services.AddServices();
        builder.Services.AddDatabaseServices<ApplicationContext>();
        builder.Services.AddHostedServices();
        builder.Services.AddBackgroundServices();
        builder.Services.AddHttpServices();
        builder.Services.AddBankLinkServices();

        builder.Services.AddCors();
        builder.Services.AddAuthenticationServices();
        builder.Services.AddAuthorizationServices();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApiServices();
        builder.Services.AddOpenApiExamples();

        var app = builder.Build();

        app.MapOpenApiServices();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.UseCors(Configuration.Policy.FrontendApplication);

        app.Run();
    }
}

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IBankRepository, BankRepository>();
        services.AddSingleton<IBankService, BankService>();
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IAccountRepository, AccountRepository>();
        services.AddSingleton<IUserService, Services.UserService>();
        services.AddSingleton<IClientService, ClientService>();
        services.AddSingleton<IEmployeeService, EmployeeService>();
        services.AddSingleton<IAccountCurrencyRepository, AccountCurrencyRepository>();
        services.AddSingleton<IEmailRepository, EmailRepository>();
        services.AddSingleton<IEmailService, EmailService>();
        services.AddSingleton<ICountryService, CountryService>();
        services.AddSingleton<ICardTypeService, CardTypeService>();
        services.AddSingleton<ICardService, CardService>();
        services.AddSingleton<ICountryRepository, CountryRepository>();
        services.AddSingleton<ICurrencyService, CurrencyService>();
        services.AddSingleton<ICurrencyRepository, CurrencyRepository>();
        services.AddSingleton<ICardTypeRepository, CardTypeRepository>();
        services.AddSingleton<ICardRepository, CardRepository>();
        services.AddSingleton<ICompanyService, CompanyService>();
        services.AddSingleton<ICompanyRepository, CompanyRepository>();
        services.AddSingleton<IAccountTypeRepository, AccountTypeRepository>();
        services.AddSingleton<IAccountTypeService, AccountTypeService>();
        services.AddSingleton<IAccountService, AccountService>();
        services.AddSingleton<IAccountCurrencyService, AccountCurrencyService>();
        services.AddSingleton<IExchangeRepository, ExchangeRepository>();
        services.AddSingleton<IExchangeService, ExchangeService>();
        services.AddSingleton<ILoanRepository, LoanRepository>();
        services.AddSingleton<ILoanTypeRepository, LoanTypeRepository>();
        services.AddSingleton<IInstallmentRepository, InstallmentRepository>();
        services.AddSingleton<ITransactionCodeRepository, TransactionCodeRepository>();
        services.AddSingleton<ITransactionCodeService, TransactionCodeService>();
        services.AddSingleton<ITransactionTemplateRepository, TransactionTemplateRepository>();
        services.AddSingleton<ITransactionTemplateService, TransactionTemplateService>();
        services.AddSingleton<ITransactionRepository, TransactionRepository>();
        services.AddSingleton<ITransactionService, TransactionService>();
        services.AddSingleton<ILoanService, LoanService>();
        services.AddSingleton<IInstallmentService, InstallmentService>();
        services.AddSingleton<ILoanTypeService, LoanTypeService>();
        services.AddSingleton<IDataService, DataService>();
        services.AddSingleton<Lazy<IDataService>>(provider => new Lazy<IDataService>(provider.GetRequiredService<IDataService>));
        services.AddSingleton<Lazy<ITransactionService>>(provider => new Lazy<ITransactionService>(provider.GetRequiredService<ITransactionService>));
        services.AddSingleton<Lazy<TransactionBackgroundService>>(provider => new Lazy<TransactionBackgroundService>(provider.GetRequiredService<TransactionBackgroundService>));

        return services;
    }

    public static IServiceCollection AddBackgroundServices(this IServiceCollection services)
    {
        services.AddSingleton<TransactionBackgroundService>();
        services.AddSingleton<DatabaseBackgroundService>();
        services.AddSingleton<CurrencyExchangeBackgroundService>();

        return services;
    }

    public static IServiceCollection AddHostedServices(this IServiceCollection services)
    {
        services.AddSingleton<LoanHostedService>();

        services.AddHostedService<ApplicationHostedService>();

        return services;
    }

    public static IServiceCollection AddHttpServices(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddHttpContextAccessor();

        return services;
    }

    public static IServiceCollection AddBankLinkServices(this IServiceCollection services)
    {
        services.AddLinkServices(new DefaultData(Seeder.Currency.All.Select(currency => currency.ToResponse())
                                                       .ToList(), Seeder.TransactionCode.All.Select(transactionCode => transactionCode.ToResponse())
                                                                        .ToList()))
                .AddB3Link(new BankData(Seeder.Bank.Bank03.Code, Seeder.Bank.Bank03.BaseUrl));

        return services;
    }

    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Continue;
        ValidatorOptions.Global.DefaultRuleLevelCascadeMode  = CascadeMode.Stop;

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<AssemblyInfo>();

        return services;
    }

    public static IServiceCollection AddCors(this IServiceCollection services)
    {
        services.AddCors(options => options.AddPolicy(Configuration.Policy.FrontendApplication, policy => policy.WithOrigins(Configuration.Frontend.BaseUrl)
                                                                                                                .AllowAnyHeader()
                                                                                                                .AllowAnyMethod()));

        return services;
    }

    public static IServiceCollection AddOpenApiExamples(this IServiceCollection services)
    {
        services.AddOpenApiExample(Example.AccountCurrency.CreateRequest);
        services.AddOpenApiExample(Example.AccountCurrency.ClientUpdateRequest);
        services.AddOpenApiExample(Example.AccountCurrency.Response);
        services.AddOpenApiExample(Example.Account.CreateRequest);
        services.AddOpenApiExample(Example.Account.UpdateClientRequest);
        services.AddOpenApiExample(Example.Account.UpdateEmployeeRequest);
        services.AddOpenApiExample(Example.Account.Response);
        services.AddOpenApiExample(Example.Account.SimpleResponse);
        services.AddOpenApiExample(Example.AccountType.CreateRequest);
        services.AddOpenApiExample(Example.AccountType.UpdateRequest);
        services.AddOpenApiExample(Example.AccountType.Response);
        services.AddOpenApiExample(Example.Bank.Response);
        services.AddOpenApiExample(Example.Card.CreateRequest);
        services.AddOpenApiExample(Example.Card.UpdateStatusRequest);
        services.AddOpenApiExample(Example.Card.UpdateLimitRequest);
        services.AddOpenApiExample(Example.Card.Response);
        services.AddOpenApiExample(Example.CardType.Response);
        services.AddOpenApiExample(Example.Client.CreateRequest);
        services.AddOpenApiExample(Example.Client.UpdateRequest);
        services.AddOpenApiExample(Example.Client.Response);
        services.AddOpenApiExample(Example.Client.SimpleResponse);
        services.AddOpenApiExample(Example.Company.CreateRequest);
        services.AddOpenApiExample(Example.Company.UpdateRequest);
        services.AddOpenApiExample(Example.Company.Response);
        services.AddOpenApiExample(Example.Company.SimpleResponse);
        services.AddOpenApiExample(Example.Country.Response);
        services.AddOpenApiExample(Example.Country.SimpleResponse);
        services.AddOpenApiExample(Example.Currency.Response);
        services.AddOpenApiExample(Example.Currency.SimpleResponse);
        services.AddOpenApiExample(Example.Employee.CreateRequest);
        services.AddOpenApiExample(Example.Employee.UpdateRequest);
        services.AddOpenApiExample(Example.Employee.Response);
        services.AddOpenApiExample(Example.Employee.SimpleResponse);
        services.AddOpenApiExample(Example.Exchange.MakeExchangeRequest);
        services.AddOpenApiExample(Example.Exchange.UpdateRequest);
        services.AddOpenApiExample(Example.Exchange.Response);
        services.AddOpenApiExample(Example.Installment.CreateRequest);
        services.AddOpenApiExample(Example.Installment.UpdateRequest);
        services.AddOpenApiExample(Example.Installment.Response);
        services.AddOpenApiExample(Example.Loan.CreateRequest);
        services.AddOpenApiExample(Example.Loan.UpdateRequest);
        services.AddOpenApiExample(Example.Loan.Response);
        services.AddOpenApiExample(Example.LoanType.CreateRequest);
        services.AddOpenApiExample(Example.LoanType.UpdateRequest);
        services.AddOpenApiExample(Example.LoanType.Response);
        services.AddOpenApiExample(Example.TransactionCode.Response);
        services.AddOpenApiExample(Example.Transaction.CreateRequest);
        services.AddOpenApiExample(Example.Transaction.UpdateRequest);
        services.AddOpenApiExample(Example.Transaction.Response);
        services.AddOpenApiExample(Example.Transaction.CreateResponse);
        services.AddOpenApiExample(Example.TransactionTemplate.CreateRequest);
        services.AddOpenApiExample(Example.TransactionTemplate.UpdateRequest);
        services.AddOpenApiExample(Example.TransactionTemplate.Response);
        services.AddOpenApiExample(Example.TransactionTemplate.SimpleResponse);
        services.AddOpenApiExample(Example.User.LoginRequest);
        services.AddOpenApiExample(Example.User.ActivationRequest);
        services.AddOpenApiExample(Example.User.PasswordResetRequest);
        services.AddOpenApiExample(Example.User.RequestPasswordResetRequest);
        services.AddOpenApiExample(Example.User.UpdatePermissionRequest);
        services.AddOpenApiExample(Example.User.Response);
        services.AddOpenApiExample(Example.User.SimpleResponse);
        services.AddOpenApiExample(Example.User.LoginResponse);

        return services;
    }
}
