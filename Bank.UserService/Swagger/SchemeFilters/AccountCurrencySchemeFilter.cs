using Bank.Application.Extensions;
using Bank.Application.Requests;
using Bank.Application.Responses;

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bank.UserService.Swagger.SchemeFilters;

file static class Example
{
    public static class AccountCurrency
    {
        public static readonly Guid     Id               = Guid.Parse("d4e5f6a7-b8c9-40d1-a2b3-c4d5e6f78901");
        public static readonly Guid     EmployeeId       = Guid.Parse("a1b2c3d4-e5f6-7890-1234-56789abcdef0");
        public static readonly Guid     CurrencyId       = Guid.Parse("f1e2d3c4-b5a6-7890-1234-56789abcdef0");
        public static readonly Guid     AccountId        = Guid.Parse("12345678-9abc-def0-1234-56789abcdef0");
        public const           decimal  DailyLimit       = 500.00m;
        public const           decimal  MonthlyLimit     = 15000.00m;
        public const           decimal  Balance          = 12000.75m;
        public const           decimal  AvailableBalance = 8000.50m;
        public static readonly DateTime CreatedAt        = new(2024, 3, 5, 10, 30, 0);
        public static readonly DateTime ModifiedAt       = new(2025, 3, 5, 12, 45, 0);

        public static readonly AccountCurrencyCreateRequest CreateRequest = new()
                                                                            {
                                                                                EmployeeId   = EmployeeId,
                                                                                CurrencyId   = CurrencyId,
                                                                                AccountId    = AccountId,
                                                                                DailyLimit   = DailyLimit,
                                                                                MonthlyLimit = MonthlyLimit
                                                                            };

        public static readonly AccountCurrencyClientUpdateRequest UpdateRequest = new()
                                                                                  {
                                                                                      DailyLimit   = DailyLimit,
                                                                                      MonthlyLimit = MonthlyLimit
                                                                                  };

        public static readonly AccountCurrencyResponse Response = new()
                                                                  {
                                                                      Id               = Id,
                                                                      Account          = null!,
                                                                      Employee         = null!,
                                                                      Currency         = null!,
                                                                      Balance          = Balance,
                                                                      AvailableBalance = AvailableBalance,
                                                                      DailyLimit       = DailyLimit,
                                                                      MonthlyLimit     = MonthlyLimit,
                                                                      CreatedAt        = CreatedAt,
                                                                      ModifiedAt       = ModifiedAt
                                                                  };
    }
}

public static partial class SwaggerSchemaFilter
{
    public static class AccountCurrency
    {
        public class CreateRequest() : SwaggerSchemaFilter<AccountCurrencyCreateRequest>(SchemeFilters.Example.AccountCurrency.CreateRequest)
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

        public class UpdateRequest() : SwaggerSchemaFilter<AccountCurrencyClientUpdateRequest>(SchemeFilters.Example.AccountCurrency.UpdateRequest)
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
