using Bank.Application.Extensions;
using Bank.Application.Requests;
using Bank.Application.Responses;

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bank.UserService.Swagger.SchemeFilters;

file static class Example
{
    public static class Account
    {
        public static readonly Guid     Id               = Guid.Parse("3f4b1e6e-a2f5-4e3b-8f88-2f70a6b42b19");
        public static readonly Guid     ClientId         = Guid.Parse("f39d319e-db3e-4af5-bada-6bcb908b29e3");
        public static readonly Guid     CurrencyId       = Guid.Parse("e8ea1dd5-45a4-4837-9bd6-f41b049217f5");
        public static readonly Guid     AccountTypeId    = Guid.Parse("1e94b664-de60-490c-94c2-c2952b9e9e22");
        public const           string   AccountNumber    = "1234567890123456";
        public const           string   Name             = "Štedni račun";
        public static readonly decimal  Balance          = 5000.75m;
        public static readonly decimal  AvailableBalance = 4500.50m;
        public static readonly decimal  DailyLimit       = 2000.00m;
        public static readonly decimal  MonthlyLimit     = 50000.00m;
        public static readonly DateOnly CreationDate     = new(2023, 5, 15);
        public static readonly DateOnly ExpirationDate   = new(2033, 5, 15);
        public const           bool     Status           = true;
        public static readonly DateTime CreatedAt        = new(2023, 5, 15, 10, 30, 0);
        public static readonly DateTime ModifiedAt       = new(2025, 3, 1, 15, 45, 0);

        public static readonly AccountCreateRequest CreateRequest = new()
                                                                    {
                                                                        Name          = Name,
                                                                        DailyLimit    = DailyLimit,
                                                                        MonthlyLimit  = MonthlyLimit,
                                                                        ClientId      = ClientId,
                                                                        Balance       = Balance,
                                                                        CurrencyId    = CurrencyId,
                                                                        AccountTypeId = AccountTypeId,
                                                                        Status        = Status
                                                                    };

        public static readonly AccountUpdateClientRequest UpdateClientRequest = new()
                                                                                {
                                                                                    Name         = Name,
                                                                                    DailyLimit   = DailyLimit,
                                                                                    MonthlyLimit = MonthlyLimit
                                                                                };

        public static readonly AccountUpdateEmployeeRequest UpdateEmployeeRequest = new()
                                                                                    {
                                                                                        Status = Status
                                                                                    };

        public static readonly AccountResponse Response = new()
                                                          {
                                                              Id                = Id,
                                                              AccountNumber     = AccountNumber,
                                                              Name              = Name,
                                                              Balance           = Balance,
                                                              AvailableBalance  = AvailableBalance,
                                                              Type              = null!,
                                                              Currency          = null!,
                                                              Employee          = null!,
                                                              Client            = null!,
                                                              AccountCurrencies = [],
                                                              DailyLimit        = DailyLimit,
                                                              MonthlyLimit      = MonthlyLimit,
                                                              CreationDate      = CreationDate,
                                                              ExpirationDate    = ExpirationDate,
                                                              Status            = Status,
                                                              CreatedAt         = CreatedAt,
                                                              ModifiedAt        = ModifiedAt
                                                          };

        public static readonly AccountSimpleResponse SimpleResponse = new()
                                                                      {
                                                                          Id            = Id,
                                                                          AccountNumber = AccountNumber
                                                                      };
    }
}

public static partial class SwaggerSchemaFilter
{
    public static class Account
    {
        public class CreateRequest() : SwaggerSchemaFilter<AccountCreateRequest>(SchemeFilters.Example.Account.CreateRequest)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.Name)
                            .ToCamelCase()] = new OpenApiString(Example.Name),
                           [nameof(Example.DailyLimit)
                            .ToCamelCase()] = new OpenApiDouble((double)Example.DailyLimit),
                           [nameof(Example.MonthlyLimit)
                            .ToCamelCase()] = new OpenApiDouble((double)Example.MonthlyLimit),
                           [nameof(Example.ClientId)
                            .ToCamelCase()] = new OpenApiString(Example.ClientId.ToString()),
                           [nameof(Example.Balance)
                            .ToCamelCase()] = new OpenApiDouble((double)Example.Balance),
                           [nameof(Example.CurrencyId)
                            .ToCamelCase()] = new OpenApiString(Example.CurrencyId.ToString()),
                           [nameof(Example.AccountTypeId)
                            .ToCamelCase()] = new OpenApiString(Example.AccountTypeId.ToString()),
                           [nameof(Example.Status)
                            .ToCamelCase()] = new OpenApiBoolean(Example.Status)
                       };
            }
        }

        public class UpdateClientRequest() : SwaggerSchemaFilter<AccountUpdateClientRequest>(SchemeFilters.Example.Account.UpdateClientRequest)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.Name)
                            .ToCamelCase()] = new OpenApiString(Example.Name),
                           [nameof(Example.DailyLimit)
                            .ToCamelCase()] = new OpenApiDouble((double)Example.DailyLimit),
                           [nameof(Example.MonthlyLimit)
                            .ToCamelCase()] = new OpenApiDouble((double)Example.MonthlyLimit)
                       };
            }
        }

        public class UpdateEmployeeRequest() : SwaggerSchemaFilter<AccountUpdateEmployeeRequest>(SchemeFilters.Example.Account.UpdateEmployeeRequest)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.Status)
                            .ToCamelCase()] = new OpenApiBoolean(Example.Status)
                       };
            }
        }

        public class Response() : SwaggerSchemaFilter<AccountResponse>(SchemeFilters.Example.Account.Response)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                var client            = context.SchemaRepository.Schemas[nameof(ClientSimpleResponse)].Example;
                var employee          = context.SchemaRepository.Schemas[nameof(EmployeeSimpleResponse)].Example;
                var currency          = context.SchemaRepository.Schemas[nameof(CurrencyResponse)].Example;
                var accountType       = context.SchemaRepository.Schemas[nameof(AccountTypeResponse)].Example;
                var accountCurrencies = new OpenApiArray { context.SchemaRepository.Schemas[nameof(AccountCurrencyResponse)].Example };

                return new OpenApiObject()
                       {
                           [nameof(Example.Id)
                            .ToCamelCase()] = new OpenApiString(Example.Id.ToString()),
                           [nameof(Example.AccountNumber)
                            .ToCamelCase()] = new OpenApiString(Example.AccountNumber),
                           [nameof(Example.Name)
                            .ToCamelCase()] = new OpenApiString(Example.Name),
                           [nameof(Example.Client)
                            .ToCamelCase()] = client,
                           [nameof(Example.Balance)
                            .ToCamelCase()] = new OpenApiDouble((double)Example.Balance),
                           [nameof(Example.AvailableBalance)
                            .ToCamelCase()] = new OpenApiDouble((double)Example.AvailableBalance),
                           [nameof(Example.Employee)
                            .ToCamelCase()] = employee,
                           [nameof(Example.Currency)
                            .ToCamelCase()] = currency,
                           [nameof(Example.Type)
                            .ToCamelCase()] = accountType,
                           [nameof(Example.AccountCurrencies)
                            .ToCamelCase()] = accountCurrencies,
                           [nameof(Example.DailyLimit)
                            .ToCamelCase()] = new OpenApiDouble((double)Example.DailyLimit),
                           [nameof(Example.MonthlyLimit)
                            .ToCamelCase()] = new OpenApiDouble((double)Example.MonthlyLimit),
                           [nameof(Example.CreationDate)
                            .ToCamelCase()] = new OpenApiDate(new DateTime(Example.CreationDate, TimeOnly.MinValue)),
                           [nameof(Example.ExpirationDate)
                            .ToCamelCase()] = new OpenApiDate(new DateTime(Example.ExpirationDate, TimeOnly.MinValue)),
                           [nameof(Example.Status)
                            .ToCamelCase()] = new OpenApiBoolean(Example.Status),
                           [nameof(Example.CreatedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.CreatedAt),
                           [nameof(Example.ModifiedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.ModifiedAt)
                       };
            }
        }

        public class SimpleResponse() : SwaggerSchemaFilter<AccountSimpleResponse>(SchemeFilters.Example.Account.SimpleResponse)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.Id)
                            .ToCamelCase()] = new OpenApiString(Example.Id.ToString()),
                           [nameof(Example.AccountNumber)
                            .ToCamelCase()] = new OpenApiString(Example.AccountNumber),
                       };
            }
        }
    }
}
