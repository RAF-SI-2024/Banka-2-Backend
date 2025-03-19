﻿using System.IdentityModel.Tokens.Jwt;
using System.Text;

using Bank.Application;
using Bank.Application.Domain;
using Bank.UserService.Configurations;
using Bank.UserService.Database;
using Bank.UserService.HostedServices;
using Bank.UserService.Repositories;
using Bank.UserService.Services;
using Bank.UserService.Swagger.SchemeFilters;

using DotNetEnv;

using FluentValidation;
using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Bank.UserService.Application;

public class UserApplication
{
    public static void Run(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        Env.Load();

        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        builder.Services.AddValidation();
        builder.Services.AddServices();
        builder.Services.AddDatabase();
        builder.Services.AddHostedServices();
        builder.Services.AddHttpServices();

        builder.Services.AddCors();
        builder.Services.AddAuthenticationServices();
        builder.Services.AddAuthorizationServices();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwagger();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(Configuration.Policy.FrontendApplication);

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IUserService, Services.UserService>();
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IAccountCurrencyRepository, AccountCurrencyRepository>();
        services.AddScoped<IEmailRepository, EmailRepository>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<ICountryService, CountryService>();
        services.AddScoped<ICardTypeService, CardTypeService>();
        services.AddScoped<ICardService, CardService>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<ICurrencyService, CurrencyService>();
        services.AddScoped<ICurrencyRepository, CurrencyRepository>();
        services.AddScoped<ICardTypeRepository, CardTypeRepository>();
        services.AddScoped<ICardRepository, CardRepository>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IAccountTypeRepository, AccountTypeRepository>();
        services.AddScoped<IAccountTypeService, AccountTypeService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAccountCurrencyService, AccountCurrencyService>();
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddScoped<IExchangeRepository, ExchangeRepository>();
        services.AddScoped<IExchangeService, ExchangeService>();
        services.AddScoped<ILoanRepository, LoanRepository>();
        services.AddScoped<ILoanTypeRepository, LoanTypeRepository>();
        services.AddScoped<IInstallmentRepository, InstallmentRepository>();
        services.AddScoped<ITransactionCodeRepository, TransactionCodeRepository>();
        services.AddScoped<ITransactionCodeService, TransactionCodeService>();
        services.AddScoped<ITransactionTemplateRepository, TransactionTemplateRepository>();
        services.AddScoped<ITransactionTemplateService, TransactionTemplateService>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<ILoanService, Services.LoanService>();
        services.AddScoped<IInstallmentService, InstallmentService>();
        services.AddScoped<ILoanTypeService, LoanTypeService>();

        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(Configuration.Database.GetConnectionString()), ServiceLifetime.Scoped, ServiceLifetime.Singleton);

        return services;
    }

    public static IServiceCollection AddHostedServices(this IServiceCollection services)
    {
        services.AddSingleton<DatabaseHostedService>();
        services.AddSingleton<ExchangeHostedService>();

        services.AddHostedService<ApplicationHostedService>();

        return services;
    }

    public static IServiceCollection AddHttpServices(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddHttpContextAccessor();

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

    public static IServiceCollection AddAuthenticationServices(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwtOptions => jwtOptions.TokenValidationParameters = new TokenValidationParameters
                                                                                   {
                                                                                       IssuerSigningKey =
                                                                                       new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.Jwt
                                                                                                                                                    .SecretKey)),
                                                                                       ValidateIssuerSigningKey = true,
                                                                                       ValidateLifetime         = true,
                                                                                       ValidateIssuer           = false,
                                                                                       ValidateAudience         = false,
                                                                                       ClockSkew                = TimeSpan.Zero
                                                                                   });

        return services;
    }

    public static IServiceCollection AddAuthorizationServices(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
                .AddPolicy(Configuration.Policy.Role.Admin,    policy => policy.RequireRole(nameof(Role.Admin)))
                .AddPolicy(Configuration.Policy.Role.Employee, policy => policy.RequireRole(nameof(Role.Employee)))
                .AddPolicy(Configuration.Policy.Role.Client,   policy => policy.RequireRole(nameof(Role.Client)));

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(config =>
                               {
                                   config.SwaggerDoc("v1", new OpenApiInfo() { Title = "UserService", Version = "v1" });

                                   config.SchemaFilter<SwaggerSchemaFilter.AccountCurrency.CreateRequest>();
                                   config.SchemaFilter<SwaggerSchemaFilter.AccountCurrency.UpdateRequest>();
                                   config.SchemaFilter<SwaggerSchemaFilter.AccountCurrency.Response>();

                                   config.SchemaFilter<SwaggerSchemaFilter.Account.CreateRequest>();
                                   config.SchemaFilter<SwaggerSchemaFilter.Account.UpdateClientRequest>();
                                   config.SchemaFilter<SwaggerSchemaFilter.Account.UpdateEmployeeRequest>();
                                   config.SchemaFilter<SwaggerSchemaFilter.Account.SimpleResponse>();
                                   config.SchemaFilter<SwaggerSchemaFilter.Account.Response>();

                                   config.SchemaFilter<SwaggerSchemaFilter.AccountCurrency.CreateRequest>();
                                   config.SchemaFilter<SwaggerSchemaFilter.AccountCurrency.UpdateRequest>();
                                   config.SchemaFilter<SwaggerSchemaFilter.AccountCurrency.Response>();

                                   config.SchemaFilter<SwaggerSchemaFilter.AccountType.CreateRequest>();
                                   config.SchemaFilter<SwaggerSchemaFilter.AccountType.UpdateRequest>();
                                   config.SchemaFilter<SwaggerSchemaFilter.AccountType.Response>();

                                   config.SchemaFilter<SwaggerSchemaFilter.Card.CreateRequest>();
                                   config.SchemaFilter<SwaggerSchemaFilter.Card.LimitUpdateRequest>();
                                   config.SchemaFilter<SwaggerSchemaFilter.Card.StatusUpdateRequest>();
                                   config.SchemaFilter<SwaggerSchemaFilter.Card.Response>();

                                   config.SchemaFilter<SwaggerSchemaFilter.CardType.Response>();

                                   config.SchemaFilter<SwaggerSchemaFilter.Client.UpdateRequest>();
                                   config.SchemaFilter<SwaggerSchemaFilter.Client.CreateRequest>();
                                   config.SchemaFilter<SwaggerSchemaFilter.Client.Response>();
                                   config.SchemaFilter<SwaggerSchemaFilter.Client.SimpleResponse>();

                                   config.SchemaFilter<SwaggerSchemaFilter.Company.CreateRequest>();
                                   config.SchemaFilter<SwaggerSchemaFilter.Company.UpdateRequest>();
                                   config.SchemaFilter<SwaggerSchemaFilter.Company.SimpleResponse>();
                                   config.SchemaFilter<SwaggerSchemaFilter.Company.Response>();

                                   config.SchemaFilter<SwaggerSchemaFilter.Country.SimpleResponse>();
                                   config.SchemaFilter<SwaggerSchemaFilter.Country.Response>();

                                   config.SchemaFilter<SwaggerSchemaFilter.Currency.SimpleResponse>();
                                   config.SchemaFilter<SwaggerSchemaFilter.Currency.Response>();

                                   config.SchemaFilter<SwaggerSchemaFilter.Employee.CreateRequest>();
                                   config.SchemaFilter<SwaggerSchemaFilter.Employee.UpdateRequest>();
                                   config.SchemaFilter<SwaggerSchemaFilter.Employee.Response>();
                                   config.SchemaFilter<SwaggerSchemaFilter.Employee.SimpleResponse>();

                                   config.SchemaFilter<SwaggerSchemaFilter.Exchange.MakeExchangeRequest>();
                                   config.SchemaFilter<SwaggerSchemaFilter.Exchange.UpdateRequest>();
                                   config.SchemaFilter<SwaggerSchemaFilter.Exchange.BetweenRequest>();
                                   config.SchemaFilter<SwaggerSchemaFilter.Exchange.Response>();

                                   config.SchemaFilter<SwaggerSchemaFilter.Installment.Request>();
                                   config.SchemaFilter<SwaggerSchemaFilter.Installment.UpdateRequest>();
                                   config.SchemaFilter<SwaggerSchemaFilter.Installment.Response>();

                                   config.SchemaFilter<SwaggerSchemaFilter.Loan.Request>();
                                   config.SchemaFilter<SwaggerSchemaFilter.Loan.UpdateRequest>();
                                   config.SchemaFilter<SwaggerSchemaFilter.Loan.Response>();

                                   config.SchemaFilter<SwaggerSchemaFilter.LoanType.Request>();
                                   config.SchemaFilter<SwaggerSchemaFilter.LoanType.UpdateRequest>();
                                   config.SchemaFilter<SwaggerSchemaFilter.LoanType.Response>();

                                   config.SchemaFilter<SwaggerSchemaFilter.TransactionCode.Response>();

                                   config.SchemaFilter<SwaggerSchemaFilter.Transaction.CreateRequest>();
                                   config.SchemaFilter<SwaggerSchemaFilter.Transaction.UpdateRequest>();
                                   config.SchemaFilter<SwaggerSchemaFilter.Transaction.Response>();
                                   config.SchemaFilter<SwaggerSchemaFilter.Transaction.CreateResponse>();

                                   config.SchemaFilter<SwaggerSchemaFilter.TransactionTemplate.CreateRequest>();
                                   config.SchemaFilter<SwaggerSchemaFilter.TransactionTemplate.UpdateRequest>();
                                   config.SchemaFilter<SwaggerSchemaFilter.TransactionTemplate.Response>();
                                   config.SchemaFilter<SwaggerSchemaFilter.TransactionTemplate.SimpleResponse>();

                                   config.SchemaFilter<SwaggerSchemaFilter.User.ActivationRequest>();
                                   config.SchemaFilter<SwaggerSchemaFilter.User.LoginRequest>();
                                   config.SchemaFilter<SwaggerSchemaFilter.User.PasswordResetRequest>();
                                   config.SchemaFilter<SwaggerSchemaFilter.User.RequestPasswordResetRequest>();
                                   config.SchemaFilter<SwaggerSchemaFilter.User.Response>();
                                   config.SchemaFilter<SwaggerSchemaFilter.User.SimpleResponse>();
                                   config.SchemaFilter<SwaggerSchemaFilter.User.LoginResponse>();

                                   config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                                                                          {
                                                                              Description = "Authorization: Bearer {token}",
                                                                              Name        = "Authorization",
                                                                              In          = ParameterLocation.Header,
                                                                              Type        = SecuritySchemeType.ApiKey,
                                                                              Scheme      = "Bearer"
                                                                          });

                                   config.AddSecurityRequirement(new OpenApiSecurityRequirement
                                                                 {
                                                                     {
                                                                         new OpenApiSecurityScheme
                                                                         {
                                                                             Reference = new OpenApiReference
                                                                                         {
                                                                                             Type =
                                                                                             ReferenceType
                                                                                             .SecurityScheme,
                                                                                             Id =
                                                                                             "Bearer"
                                                                                         }
                                                                         },
                                                                         []
                                                                     }
                                                                 });
                               });

        return services;
    }
}
