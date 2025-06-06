using Bank.OpenApi.Core;
using Bank.OpenApi.Examples;
using Bank.OpenApi.Schemas.OpenApi;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using Scalar.AspNetCore;

using Schema = Bank.OpenApi.Schemas.Swagger.Schema;
using SchemaOpenApi = Bank.OpenApi.Schemas.OpenApi.Schema.OpenApi;
using SchemaSwagger = Bank.OpenApi.Schemas.Swagger.Schema.Swagger;

namespace Bank.OpenApi;

public static class OpenApiExtensions
{
    public static IServiceCollection AddOpenApiServices(this IServiceCollection services)
    {
        services.AddSingleton<OpenApiSchemaContext>();
        services.AddDefaultExamples();
        services.AddSwaggerSchemas();

        return services;
    }

    public static IApplicationBuilder MapOpenApiServices(this WebApplication application)
    {
        if (!application.Environment.IsDevelopment())
            return application;

        application.MapSwagger(pattern: "/openapi/{documentName}.{extension:regex(^(json|ya?ml)$)}");

        application.MapScalarApiReference(options =>
                                          {
                                              options.Title               = "Uwubank";
                                              options.DefaultHttpClient   = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
                                              options.Theme               = ScalarTheme.DeepSpace;
                                              options.Theme               = ScalarTheme.Default;
                                              options.DarkMode            = true;
                                              options.HideDownloadButton  = true;
                                              options.OpenApiRoutePattern = "/openapi/{documentName}.json";

                                              options.EnabledClients =
                                              [
                                                  ScalarClient.Curl, ScalarClient.Http11, ScalarClient.Axios, ScalarClient.HttpClient, ScalarClient.OkHttp,
                                                  ScalarClient.NetHttp
                                              ];

                                              options.WithPreferredScheme("Bearer")
                                                     .WithHttpBearerAuthentication(bearer => bearer.Token = "enter token");
                                          });

        return application;
    }

    internal static IServiceCollection AddDefaultExamples(this IServiceCollection services)
    {
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
                .AddOpenApiDefaultExample(Example.ForexPair.DefaultResponse)
                .AddOpenApiDefaultExample(Example.ForexPair.DefaultSimpleResponse)
                .AddOpenApiDefaultExample(Example.ForexPair.DefaultDailyResponse)
                .AddOpenApiDefaultExample(Example.FutureContract.DeafultResponse)
                .AddOpenApiDefaultExample(Example.FutureContract.DefaultSimpleResponse)
                .AddOpenApiDefaultExample(Example.FutureContract.DefaultDailyResponse)
                .AddOpenApiDefaultExample(Example.Installment.DefaultCreateRequest)
                .AddOpenApiDefaultExample(Example.Installment.DefaultUpdateRequest)
                .AddOpenApiDefaultExample(Example.Installment.DefaultResponse)
                .AddOpenApiDefaultExample(Example.Loan.DefaultCreateRequest)
                .AddOpenApiDefaultExample(Example.Loan.DefaultUpdateRequest)
                .AddOpenApiDefaultExample(Example.Loan.DefaultResponse)
                .AddOpenApiDefaultExample(Example.LoanType.DefaultCreateRequest)
                .AddOpenApiDefaultExample(Example.LoanType.DefaultUpdateRequest)
                .AddOpenApiDefaultExample(Example.LoanType.DefaultResponse)
                .AddOpenApiDefaultExample(Example.Option.DefaultResponse)
                .AddOpenApiDefaultExample(Example.Option.DefaultSimpleResponse)
                .AddOpenApiDefaultExample(Example.Option.DefaultDailyResponse)
                .AddOpenApiDefaultExample(Example.Order.DefaultCreateRequest)
                .AddOpenApiDefaultExample(Example.Order.DefaultUpdateRequest)
                .AddOpenApiDefaultExample(Example.Order.DefaultResponse)
                .AddOpenApiDefaultExample(Example.Quote.DefaultSimpleResponse)
                .AddOpenApiDefaultExample([Example.Quote.DefaultSimpleResponse])
                .AddOpenApiDefaultExample(Example.Quote.DefaultDailySimpleResponse)
                .AddOpenApiDefaultExample([Example.Quote.DefaultDailySimpleResponse])
                .AddOpenApiDefaultExample(Example.Quote.DefaultLatestSimpleResponse)
                .AddOpenApiDefaultExample(Example.Stock.DefaultResponse)
                .AddOpenApiDefaultExample(Example.Stock.DefaultSimpleResponse)
                .AddOpenApiDefaultExample(Example.Stock.DefaultDailyResponse)
                .AddOpenApiDefaultExample(Example.StockExchange.DefaultCreateRequest)
                .AddOpenApiDefaultExample(Example.StockExchange.DefaultResponse)
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
                .AddOpenApiDefaultExample(Example.User.DefaultUpdatePermissionRequest)
                .AddOpenApiDefaultExample(Example.User.DefaultResponse)
                .AddOpenApiDefaultExample(Example.User.DefaultSimpleResponse)
                .AddOpenApiDefaultExample(Example.User.DefaultLoginResponse);

        return services;
    }

    internal static IServiceCollection AddOpenApiSchemas(this IServiceCollection services)
    {
        services.AddOpenApi(options =>
                            {
                                options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
                                options.AddSchemaTransformer<OpenApiSchemaTransformer>();
                            });

        services.AddOpenApiSchema<SchemaOpenApi.AccountCurrency.CreateRequest>()
                .AddOpenApiSchema<SchemaOpenApi.AccountCurrency.ClientUpdateRequest>()
                .AddOpenApiSchema<SchemaOpenApi.AccountCurrency.Response>()
                .AddOpenApiSchema<SchemaOpenApi.Account.CreateRequest>()
                .AddOpenApiSchema<SchemaOpenApi.Account.UpdateClientRequest>()
                .AddOpenApiSchema<SchemaOpenApi.Account.UpdateEmployeeRequest>()
                .AddOpenApiSchema<SchemaOpenApi.Account.Response>()
                .AddOpenApiSchema<SchemaOpenApi.Account.SimpleResponse>()
                .AddOpenApiSchema<SchemaOpenApi.AccountType.CreateRequest>()
                .AddOpenApiSchema<SchemaOpenApi.AccountType.UpdateRequest>()
                .AddOpenApiSchema<SchemaOpenApi.AccountType.Response>()
                .AddOpenApiSchema<SchemaOpenApi.Bank.Response>()
                .AddOpenApiSchema<SchemaOpenApi.Card.CreateRequest>()
                .AddOpenApiSchema<SchemaOpenApi.Card.UpdateLimitRequest>()
                .AddOpenApiSchema<SchemaOpenApi.Card.UpdateStatusRequest>()
                .AddOpenApiSchema<SchemaOpenApi.Card.Response>()
                .AddOpenApiSchema<SchemaOpenApi.CardType.Response>()
                .AddOpenApiSchema<SchemaOpenApi.Client.CreateRequest>()
                .AddOpenApiSchema<SchemaOpenApi.Client.UpdateRequest>()
                .AddOpenApiSchema<SchemaOpenApi.Client.Response>()
                .AddOpenApiSchema<SchemaOpenApi.Client.SimpleResponse>()
                .AddOpenApiSchema<SchemaOpenApi.Company.CreateRequest>()
                .AddOpenApiSchema<SchemaOpenApi.Company.UpdateRequest>()
                .AddOpenApiSchema<SchemaOpenApi.Company.Response>()
                .AddOpenApiSchema<SchemaOpenApi.Company.SimpleResponse>()
                .AddOpenApiSchema<SchemaOpenApi.Country.Response>()
                .AddOpenApiSchema<SchemaOpenApi.Country.SimpleResponse>()
                .AddOpenApiSchema<SchemaOpenApi.Currency.Response>()
                .AddOpenApiSchema<SchemaOpenApi.Currency.SimpleResponse>()
                .AddOpenApiSchema<SchemaOpenApi.Employee.CreateRequest>()
                .AddOpenApiSchema<SchemaOpenApi.Employee.UpdateRequest>()
                .AddOpenApiSchema<SchemaOpenApi.Employee.Response>()
                .AddOpenApiSchema<SchemaOpenApi.Employee.SimpleResponse>()
                .AddOpenApiSchema<SchemaOpenApi.Exchange.MakeExchangeRequest>()
                .AddOpenApiSchema<SchemaOpenApi.Exchange.UpdateRequest>()
                .AddOpenApiSchema<SchemaOpenApi.Exchange.Response>()
                .AddOpenApiSchema<SchemaOpenApi.Installment.CreateRequest>()
                .AddOpenApiSchema<SchemaOpenApi.Installment.UpdateRequest>()
                .AddOpenApiSchema<SchemaOpenApi.Installment.Response>()
                .AddOpenApiSchema<SchemaOpenApi.Loan.CreateRequest>()
                .AddOpenApiSchema<SchemaOpenApi.Loan.UpdateRequest>()
                .AddOpenApiSchema<SchemaOpenApi.Loan.Response>()
                .AddOpenApiSchema<SchemaOpenApi.LoanType.CreateRequest>()
                .AddOpenApiSchema<SchemaOpenApi.LoanType.UpdateRequest>()
                .AddOpenApiSchema<SchemaOpenApi.LoanType.Response>()
                .AddOpenApiSchema<SchemaOpenApi.TransactionCode.Response>()
                .AddOpenApiSchema<SchemaOpenApi.Transaction.CreateRequest>()
                .AddOpenApiSchema<SchemaOpenApi.Transaction.UpdateRequest>()
                .AddOpenApiSchema<SchemaOpenApi.Transaction.Response>()
                .AddOpenApiSchema<SchemaOpenApi.Transaction.CreateResponse>()
                .AddOpenApiSchema<SchemaOpenApi.TransactionTemplate.CreateRequest>()
                .AddOpenApiSchema<SchemaOpenApi.TransactionTemplate.UpdateRequest>()
                .AddOpenApiSchema<SchemaOpenApi.TransactionTemplate.Response>()
                .AddOpenApiSchema<SchemaOpenApi.TransactionTemplate.SimpleResponse>()
                .AddOpenApiSchema<SchemaOpenApi.User.LoginRequest>()
                .AddOpenApiSchema<SchemaOpenApi.User.ActivationRequest>()
                .AddOpenApiSchema<SchemaOpenApi.User.PasswordResetRequest>()
                .AddOpenApiSchema<SchemaOpenApi.User.RequestPasswordResetRequest>()
                .AddOpenApiSchema<SchemaOpenApi.User.Response>()
                .AddOpenApiSchema<SchemaOpenApi.User.SimpleResponse>()
                .AddOpenApiSchema<SchemaOpenApi.User.LoginResponse>();

        return services;
    }

    internal static IServiceCollection AddSwaggerSchemas(this IServiceCollection services)
    {
        services.AddSwaggerGen(config =>
                               {
                                   config.SwaggerDoc("v1", new OpenApiInfo { Title = "Bank Service", Version = "v1" });

                                   config.SchemaFilter<SchemaSwagger.Enums>();

                                   config.SchemaFilter<SchemaSwagger.AccountCurrency.CreateRequest>();
                                   config.SchemaFilter<SchemaSwagger.AccountCurrency.ClientUpdateRequest>();
                                   config.SchemaFilter<SchemaSwagger.AccountCurrency.Response>();
                                   config.SchemaFilter<SchemaSwagger.Account.CreateRequest>();
                                   config.SchemaFilter<SchemaSwagger.Account.UpdateClientRequest>();
                                   config.SchemaFilter<SchemaSwagger.Account.UpdateEmployeeRequest>();
                                   config.SchemaFilter<SchemaSwagger.Account.Response>();
                                   config.SchemaFilter<SchemaSwagger.Account.SimpleResponse>();
                                   config.SchemaFilter<SchemaSwagger.AccountType.CreateRequest>();
                                   config.SchemaFilter<SchemaSwagger.AccountType.UpdateRequest>();
                                   config.SchemaFilter<SchemaSwagger.AccountType.Response>();
                                   config.SchemaFilter<SchemaSwagger.Bank.Response>();
                                   config.SchemaFilter<SchemaSwagger.Card.CreateRequest>();
                                   config.SchemaFilter<SchemaSwagger.Card.UpdateLimitRequest>();
                                   config.SchemaFilter<SchemaSwagger.Card.UpdateStatusRequest>();
                                   config.SchemaFilter<SchemaSwagger.Card.Response>();
                                   config.SchemaFilter<SchemaSwagger.CardType.Response>();
                                   config.SchemaFilter<SchemaSwagger.Client.CreateRequest>();
                                   config.SchemaFilter<SchemaSwagger.Client.UpdateRequest>();
                                   config.SchemaFilter<SchemaSwagger.Client.Response>();
                                   config.SchemaFilter<SchemaSwagger.Client.SimpleResponse>();
                                   config.SchemaFilter<SchemaSwagger.Company.CreateRequest>();
                                   config.SchemaFilter<SchemaSwagger.Company.UpdateRequest>();
                                   config.SchemaFilter<SchemaSwagger.Company.Response>();
                                   config.SchemaFilter<SchemaSwagger.Company.SimpleResponse>();
                                   config.SchemaFilter<SchemaSwagger.Country.Response>();
                                   config.SchemaFilter<SchemaSwagger.Country.SimpleResponse>();
                                   config.SchemaFilter<SchemaSwagger.Currency.Response>();
                                   config.SchemaFilter<SchemaSwagger.Currency.SimpleResponse>();
                                   config.SchemaFilter<SchemaSwagger.Employee.CreateRequest>();
                                   config.SchemaFilter<SchemaSwagger.Employee.UpdateRequest>();
                                   config.SchemaFilter<SchemaSwagger.Employee.Response>();
                                   config.SchemaFilter<SchemaSwagger.Employee.SimpleResponse>();
                                   config.SchemaFilter<SchemaSwagger.Exchange.MakeExchangeRequest>();
                                   config.SchemaFilter<SchemaSwagger.Exchange.UpdateRequest>();
                                   config.SchemaFilter<SchemaSwagger.Exchange.Response>();
                                   config.SchemaFilter<SchemaSwagger.ForexPair.Response>();
                                   config.SchemaFilter<SchemaSwagger.ForexPair.SimpleResponse>();
                                   config.SchemaFilter<SchemaSwagger.ForexPair.DailyResponse>();
                                   config.SchemaFilter<SchemaSwagger.FutureContract.Response>();
                                   config.SchemaFilter<SchemaSwagger.FutureContract.SimpleResponse>();
                                   config.SchemaFilter<SchemaSwagger.FutureContract.DailyResponse>();
                                   config.SchemaFilter<SchemaSwagger.Installment.CreateRequest>();
                                   config.SchemaFilter<SchemaSwagger.Installment.UpdateRequest>();
                                   config.SchemaFilter<SchemaSwagger.Installment.Response>();
                                   config.SchemaFilter<SchemaSwagger.Loan.CreateRequest>();
                                   config.SchemaFilter<SchemaSwagger.Loan.UpdateRequest>();
                                   config.SchemaFilter<SchemaSwagger.Loan.Response>();
                                   config.SchemaFilter<SchemaSwagger.LoanType.CreateRequest>();
                                   config.SchemaFilter<SchemaSwagger.LoanType.UpdateRequest>();
                                   config.SchemaFilter<SchemaSwagger.LoanType.Response>();
                                   config.SchemaFilter<SchemaSwagger.Option.Response>();
                                   config.SchemaFilter<SchemaSwagger.Option.SimpleResponse>();
                                   config.SchemaFilter<SchemaSwagger.Option.DailyResponse>();
                                   config.SchemaFilter<SchemaSwagger.Order.CreateRequest>();
                                   config.SchemaFilter<SchemaSwagger.Order.UpdateRequest>();
                                   config.SchemaFilter<SchemaSwagger.Order.Response>();
                                   config.SchemaFilter<SchemaSwagger.Quote.SimpleResponse>();
                                   config.SchemaFilter<SchemaSwagger.Quote.DailySimpleResponse>();
                                   config.SchemaFilter<SchemaSwagger.Quote.LatestSimpleResponse>();
                                   config.SchemaFilter<SchemaSwagger.Stock.Response>();
                                   config.SchemaFilter<SchemaSwagger.Stock.SimpleResponse>();
                                   config.SchemaFilter<SchemaSwagger.Stock.DailyResponse>();
                                   config.SchemaFilter<SchemaSwagger.StockExchange.CreateRequest>();
                                   config.SchemaFilter<SchemaSwagger.StockExchange.Response>();
                                   config.SchemaFilter<SchemaSwagger.TransactionCode.Response>();
                                   config.SchemaFilter<SchemaSwagger.Transaction.CreateRequest>();
                                   config.SchemaFilter<SchemaSwagger.Transaction.UpdateRequest>();
                                   config.SchemaFilter<SchemaSwagger.Transaction.Response>();
                                   config.SchemaFilter<SchemaSwagger.Transaction.CreateResponse>();
                                   config.SchemaFilter<SchemaSwagger.TransactionTemplate.CreateRequest>();
                                   config.SchemaFilter<SchemaSwagger.TransactionTemplate.UpdateRequest>();
                                   config.SchemaFilter<SchemaSwagger.TransactionTemplate.Response>();
                                   config.SchemaFilter<SchemaSwagger.TransactionTemplate.SimpleResponse>();
                                   config.SchemaFilter<SchemaSwagger.User.LoginRequest>();
                                   config.SchemaFilter<SchemaSwagger.User.ActivationRequest>();
                                   config.SchemaFilter<SchemaSwagger.User.PasswordResetRequest>();
                                   config.SchemaFilter<SchemaSwagger.User.RequestPasswordResetRequest>();
                                   config.SchemaFilter<SchemaSwagger.User.UpdatePermissionRequest>();
                                   config.SchemaFilter<SchemaSwagger.User.Response>();
                                   config.SchemaFilter<SchemaSwagger.User.SimpleResponse>();
                                   config.SchemaFilter<SchemaSwagger.User.LoginResponse>();

                                   config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                                                                          {
                                                                              Description  = "Authorization: Bearer {token}",
                                                                              In           = ParameterLocation.Header,
                                                                              Type         = SecuritySchemeType.Http,
                                                                              Scheme       = "Bearer",
                                                                              BearerFormat = "Json Web Token"
                                                                          });

                                   config.AddSecurityRequirement(new OpenApiSecurityRequirement
                                                                 {
                                                                     {
                                                                         new OpenApiSecurityScheme
                                                                         {
                                                                             Reference = new()
                                                                                         {
                                                                                             Id = "Bearer",
                                                                                             Type = ReferenceType
                                                                                             .SecurityScheme
                                                                                         }
                                                                         },
                                                                         []
                                                                     }
                                                                 });
                               });

        return services;
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
