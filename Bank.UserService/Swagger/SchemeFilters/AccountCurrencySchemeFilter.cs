using Bank.Application.Extensions;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Database.Sample;

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bank.UserService.Swagger.SchemeFilters;

file static class Example
{
    public static class AccountCurrency
    {
        public static readonly AccountCurrencyResponse Response = new()
                                                                  {
                                                                      Id               = Guid.Parse("d4e5f6a7-b8c9-40d1-a2b3-c4d5e6f78901"),
                                                                      Account          = null!,
                                                                      Employee         = null!,
                                                                      Currency         = null!,
                                                                      Balance          = 12000.75m,
                                                                      AvailableBalance = 8000.50m,
                                                                      DailyLimit       = Sample.AccountCurrency.CreateRequest.DailyLimit,
                                                                      MonthlyLimit     = Sample.AccountCurrency.CreateRequest.MonthlyLimit,
                                                                      CreatedAt        = DateTime.UtcNow,
                                                                      ModifiedAt       = DateTime.UtcNow
                                                                  };
    }
}

public static partial class SwaggerSchemaFilter
{
    public static class AccountCurrency
    {
        public class CreateRequest() : SwaggerSchemaFilter<AccountCurrencyCreateRequest>(Sample.AccountCurrency.CreateRequest)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.EmployeeId)
                            .ToCamelCase()] = new OpenApiString(Example.EmployeeId.ToString()),
                           [nameof(Example.CurrencyId)
                            .ToCamelCase()] = new OpenApiString(Example.CurrencyId.ToString()),
                           [nameof(Example.AccountId)
                            .ToCamelCase()] = new OpenApiString(Example.AccountId.ToString()),
                           [nameof(Example.DailyLimit)
                            .ToCamelCase()] = new OpenApiDouble((double)Example.DailyLimit),
                           [nameof(Example.MonthlyLimit)
                            .ToCamelCase()] = new OpenApiDouble((double)Example.MonthlyLimit)
                       };
            }
        }

        public class UpdateRequest() : SwaggerSchemaFilter<AccountCurrencyClientUpdateRequest>(Sample.AccountCurrency.UpdateRequest)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.DailyLimit)
                            .ToCamelCase()] = new OpenApiDouble((double)Example.DailyLimit),
                           [nameof(Example.MonthlyLimit)
                            .ToCamelCase()] = new OpenApiDouble((double)Example.MonthlyLimit)
                       };
            }
        }

        public class Response() : SwaggerSchemaFilter<AccountCurrencyResponse>(SchemeFilters.Example.AccountCurrency.Response)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                var account  = context.SchemaRepository.Schemas[nameof(AccountSimpleResponse)].Example;
                var employee = context.SchemaRepository.Schemas[nameof(EmployeeSimpleResponse)].Example;
                var currency = context.SchemaRepository.Schemas[nameof(CurrencyResponse)].Example;

                return new OpenApiObject()
                       {
                           [nameof(Example.Id)
                            .ToCamelCase()] = new OpenApiString(Example.Id.ToString()),
                           [nameof(Example.Account)
                            .ToCamelCase()] = account,
                           [nameof(Example.Employee)
                            .ToCamelCase()] = employee,
                           [nameof(Example.Currency)
                            .ToCamelCase()] = currency,
                           [nameof(Example.Balance)
                            .ToCamelCase()] = new OpenApiDouble((double)Example.Balance),
                           [nameof(Example.AvailableBalance)
                            .ToCamelCase()] = new OpenApiDouble((double)Example.AvailableBalance),
                           [nameof(Example.DailyLimit)
                            .ToCamelCase()] = new OpenApiDouble((double)Example.DailyLimit),
                           [nameof(Example.MonthlyLimit)
                            .ToCamelCase()] = new OpenApiDouble((double)Example.MonthlyLimit),
                           [nameof(Example.CreatedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.CreatedAt),
                           [nameof(Example.ModifiedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.ModifiedAt)
                       };
            }
        }
    }
}
