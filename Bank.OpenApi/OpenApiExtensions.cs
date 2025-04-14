using Bank.OpenApi.Core;
using Bank.OpenApi.Examples;
using Bank.OpenApi.Schemas;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using Scalar.AspNetCore;

namespace Bank.OpenApi;

public static class OpenApiExtensions
{
    public static IServiceCollection AddOpenApiServices(this IServiceCollection services)
    {
        services.AddOpenApi(options => options.AddSchemaTransformer<OpenApiSchemaTransformer>());
        services.AddSingleton<OpenApiSchemaContext>();

        services.AddOpenApiSchema<Schema.AccountCurrency.CreateRequest>()
                .AddOpenApiSchema<Schema.AccountCurrency.ClientUpdateRequest>()
                .AddOpenApiSchema<Schema.AccountCurrency.Response>()
                .AddOpenApiSchema<Schema.Account.CreateRequest>()
                .AddOpenApiSchema<Schema.Account.UpdateClientRequest>()
                .AddOpenApiSchema<Schema.Account.UpdateEmployeeRequest>()
                .AddOpenApiSchema<Schema.Account.Response>()
                .AddOpenApiSchema<Schema.Account.SimpleResponse>()
                .AddOpenApiSchema<Schema.AccountType.CreateRequest>()
                .AddOpenApiSchema<Schema.AccountType.UpdateRequest>()
                .AddOpenApiSchema<Schema.AccountType.Response>()
                .AddOpenApiSchema<Schema.Bank.Response>()
                .AddOpenApiSchema<Schema.Card.CreateRequest>()
                .AddOpenApiSchema<Schema.Card.UpdateLimitRequest>()
                .AddOpenApiSchema<Schema.Card.UpdateStatusRequest>()
                .AddOpenApiSchema<Schema.Card.Response>()
                .AddOpenApiSchema<Schema.CardType.Response>()
                .AddOpenApiSchema<Schema.Client.CreateRequest>()
                .AddOpenApiSchema<Schema.Client.UpdateRequest>()
                .AddOpenApiSchema<Schema.Client.Response>()
                .AddOpenApiSchema<Schema.Client.SimpleResponse>()
                .AddOpenApiSchema<Schema.Company.CreateRequest>()
                .AddOpenApiSchema<Schema.Company.UpdateRequest>()
                .AddOpenApiSchema<Schema.Company.Response>()
                .AddOpenApiSchema<Schema.Company.SimpleResponse>()
                .AddOpenApiSchema<Schema.Country.Response>()
                .AddOpenApiSchema<Schema.Country.SimpleResponse>()
                .AddOpenApiSchema<Schema.Currency.Response>()
                .AddOpenApiSchema<Schema.Currency.SimpleResponse>()
                .AddOpenApiSchema<Schema.Employee.CreateRequest>()
                .AddOpenApiSchema<Schema.Employee.UpdateRequest>()
                .AddOpenApiSchema<Schema.Employee.Response>()
                .AddOpenApiSchema<Schema.Employee.SimpleResponse>()
                .AddOpenApiSchema<Schema.Exchange.MakeExchangeRequest>()
                .AddOpenApiSchema<Schema.Exchange.UpdateRequest>()
                .AddOpenApiSchema<Schema.Exchange.Response>()
                .AddOpenApiSchema<Schema.Installment.CreateRequest>()
                .AddOpenApiSchema<Schema.Installment.UpdateRequest>()
                .AddOpenApiSchema<Schema.Installment.Response>()
                .AddOpenApiSchema<Schema.Loan.CreateRequest>()
                .AddOpenApiSchema<Schema.Loan.UpdateRequest>()
                .AddOpenApiSchema<Schema.Loan.Response>()
                .AddOpenApiSchema<Schema.LoanType.CreateRequest>()
                .AddOpenApiSchema<Schema.LoanType.UpdateRequest>()
                .AddOpenApiSchema<Schema.LoanType.Response>()
                .AddOpenApiSchema<Schema.TransactionCode.Response>()
                .AddOpenApiSchema<Schema.Transaction.CreateRequest>()
                .AddOpenApiSchema<Schema.Transaction.UpdateRequest>()
                .AddOpenApiSchema<Schema.Transaction.Response>()
                .AddOpenApiSchema<Schema.Transaction.CreateResponse>()
                .AddOpenApiSchema<Schema.TransactionTemplate.CreateRequest>()
                .AddOpenApiSchema<Schema.TransactionTemplate.UpdateRequest>()
                .AddOpenApiSchema<Schema.TransactionTemplate.Response>()
                .AddOpenApiSchema<Schema.TransactionTemplate.SimpleResponse>()
                .AddOpenApiSchema<Schema.User.LoginRequest>()
                .AddOpenApiSchema<Schema.User.ActivationRequest>()
                .AddOpenApiSchema<Schema.User.PasswordResetRequest>()
                .AddOpenApiSchema<Schema.User.RequestPasswordResetRequest>()
                .AddOpenApiSchema<Schema.User.Response>()
                .AddOpenApiSchema<Schema.User.SimpleResponse>()
                .AddOpenApiSchema<Schema.User.LoginResponse>();

        services.AddOpenApiDefaultExample(Example.AccountCurrency.DefaultCreateRequest)
                .AddOpenApiDefaultExample(Example.AccountCurrency.DefaultClientUpdateRequest)
                .AddOpenApiDefaultExample(Example.AccountCurrency.DefaultResponse)
                .AddOpenApiDefaultExample([Example.AccountCurrency.DefaultResponse])
                .AddOpenApiDefaultExample(Example.Account.DefaultCreateRequest)
                .AddOpenApiDefaultExample(Example.Account.DefaultUpdateClientRequest)
                .AddOpenApiDefaultExample(Example.Account.DefaultUpdateEmployeeRequest)
                .AddOpenApiDefaultExample(Example.Account.DefaultResponse)
                .AddOpenApiDefaultExample(Example.Account.DefaultSimpleResponse)
                .AddOpenApiDefaultExample([Example.Account.DefaultSimpleResponse])
                .AddOpenApiDefaultExample(Example.AccountType.DefaultCreateRequest)
                .AddOpenApiDefaultExample(Example.AccountType.DefaultUpdateRequest)
                .AddOpenApiDefaultExample(Example.AccountType.DefaultResponse)
                .AddOpenApiDefaultExample(Example.Bank.DefaultResponse)
                .AddOpenApiDefaultExample(Example.Card.DefaultCreateRequest)
                .AddOpenApiDefaultExample(Example.Card.DefaultUpdateLimitRequest)
                .AddOpenApiDefaultExample(Example.Card.DefaultUpdateStatusRequest)
                .AddOpenApiDefaultExample(Example.Card.DefaultResponse)
                .AddOpenApiDefaultExample(Example.CardType.DefaultResponse)
                .AddOpenApiDefaultExample(Example.Client.DefaultCreateRequest)
                .AddOpenApiDefaultExample(Example.Client.DefaultUpdateRequest)
                .AddOpenApiDefaultExample(Example.Client.DefaultResponse)
                .AddOpenApiDefaultExample(Example.Client.DefaultSimpleResponse)
                .AddOpenApiDefaultExample(Example.Company.DefaultCreateRequest)
                .AddOpenApiDefaultExample(Example.Company.DefaultUpdateRequest)
                .AddOpenApiDefaultExample(Example.Company.DefaultResponse)
                .AddOpenApiDefaultExample(Example.Company.DefaultSimpleResponse)
                .AddOpenApiDefaultExample(Example.Country.DefaultResponse)
                .AddOpenApiDefaultExample(Example.Country.DefaultSimpleResponse)
                .AddOpenApiDefaultExample([Example.Country.DefaultSimpleResponse])
                .AddOpenApiDefaultExample(Example.Currency.DefaultResponse)
                .AddOpenApiDefaultExample(Example.Currency.DefaultSimpleResponse)
                .AddOpenApiDefaultExample(Example.Employee.DefaultCreateRequest)
                .AddOpenApiDefaultExample(Example.Employee.DefaultUpdateRequest)
                .AddOpenApiDefaultExample(Example.Employee.DefaultResponse)
                .AddOpenApiDefaultExample(Example.Employee.DefaultSimpleResponse)
                .AddOpenApiDefaultExample(Example.Exchange.DefaultMakeExchangeRequest)
                .AddOpenApiDefaultExample(Example.Exchange.DefaultUpdateRequest)
                .AddOpenApiDefaultExample(Example.Exchange.DefaultResponse)
                .AddOpenApiDefaultExample(Example.Installment.DefaultCreateRequest)
                .AddOpenApiDefaultExample(Example.Installment.DefaultUpdateRequest)
                .AddOpenApiDefaultExample(Example.Installment.DefaultResponse)
                .AddOpenApiDefaultExample(Example.Loan.DefaultCreateRequest)
                .AddOpenApiDefaultExample(Example.Loan.DefaultUpdateRequest)
                .AddOpenApiDefaultExample(Example.Loan.DefaultResponse)
                .AddOpenApiDefaultExample(Example.LoanType.DefaultCreateRequest)
                .AddOpenApiDefaultExample(Example.LoanType.DefaultUpdateRequest)
                .AddOpenApiDefaultExample(Example.LoanType.DefaultResponse)
                .AddOpenApiDefaultExample(Example.TransactionCode.DefaultResponse)
                .AddOpenApiDefaultExample(Example.Transaction.DefaultCreateRequest)
                .AddOpenApiDefaultExample(Example.Transaction.DefaultUpdateRequest)
                .AddOpenApiDefaultExample(Example.Transaction.DefaultResponse)
                .AddOpenApiDefaultExample(Example.Transaction.DefaultCreateResponse)
                .AddOpenApiDefaultExample(Example.TransactionTemplate.DefaultCreateRequest)
                .AddOpenApiDefaultExample(Example.TransactionTemplate.DefaultUpdateRequest)
                .AddOpenApiDefaultExample(Example.TransactionTemplate.DefaultResponse)
                .AddOpenApiDefaultExample(Example.TransactionTemplate.DefaultSimpleResponse)
                .AddOpenApiDefaultExample(Example.User.DefaultLoginRequest)
                .AddOpenApiDefaultExample(Example.User.DefaultActivationRequest)
                .AddOpenApiDefaultExample(Example.User.DefaultPasswordResetRequest)
                .AddOpenApiDefaultExample(Example.User.DefaultRequestPasswordResetRequest)
                .AddOpenApiDefaultExample(Example.User.DefaultResponse)
                .AddOpenApiDefaultExample(Example.User.DefaultSimpleResponse)
                .AddOpenApiDefaultExample(Example.User.DefaultLoginResponse);

        return services;
    }

    public static IApplicationBuilder MapOpenApiScalar(this WebApplication application)
    {
        application.MapOpenApi();

        application.MapScalarApiReference(options =>
                                          {
                                              options.Title             = "Uwubank";
                                              options.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
                                              options.Theme             = ScalarTheme.DeepSpace;
                                              options.Theme             = ScalarTheme.Default;
                                              options.DarkMode          = true;

                                              options.EnabledClients =
                                              [
                                                  ScalarClient.Curl, ScalarClient.Http11, ScalarClient.Axios, ScalarClient.HttpClient, ScalarClient.OkHttp,
                                                  ScalarClient.NetHttp
                                              ];
                                          });

        return application;
    }

    public static IServiceCollection AddOpenApiExample<TExample>(this IServiceCollection services, List<TExample> openApiExample) where TExample : class
    {
        services.AddSingleton<IOpenApiExample, OpenApiExample<List<TExample>>>(_ => new(openApiExample));

        return services;
    }

    public static IServiceCollection AddOpenApiExample<TExample>(this IServiceCollection services, TExample openApiExample) where TExample : class
    {
        services.AddSingleton<IOpenApiExample, OpenApiExample<TExample>>(_ => new(openApiExample));

        return services;
    }

    internal static IServiceCollection AddOpenApiDefaultExample<TExample>(this IServiceCollection services, List<TExample> defaultOpenApiExample) where TExample : class
    {
        services.AddSingleton<IDefaultOpenApiExample, DefaultOpenApiExample<List<TExample>>>(_ => new(defaultOpenApiExample));

        return services;
    }

    internal static IServiceCollection AddOpenApiDefaultExample<TExample>(this IServiceCollection services, TExample defaultOpenApiExample) where TExample : class
    {
        services.AddSingleton<IDefaultOpenApiExample, DefaultOpenApiExample<TExample>>(_ => new(defaultOpenApiExample));

        return services;
    }

    internal static IServiceCollection AddOpenApiSchema<TImplementation>(this IServiceCollection services) where TImplementation : class, IOpenApiSchema
    {
        services.AddSingleton<IOpenApiSchema, TImplementation>();

        return services;
    }
}
