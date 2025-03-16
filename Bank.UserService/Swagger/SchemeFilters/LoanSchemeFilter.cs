using Bank.Application.Extensions;
using Bank.Application.Requests;
using Bank.Application.Responses;

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bank.UserService.Swagger.SchemeFilters;

file static class Example
{
    public static class Loan
    {
        public static readonly Guid     Id           = Guid.Parse("90a10f93-85cc-491a-8624-07c485a2b431");
        public static readonly Guid     TypeId       = Guid.Parse("af94b480-4c67-4281-962d-0d73efe48e4a");
        public static readonly Guid     AccountId    = Guid.Parse("69434456-99a3-4cef-a366-b98877b5d4fc");
        public const           decimal  Amount       = 50000.00m;
        public const           int      Period       = 60;
        public static readonly DateOnly CreationDate = new(2024, 3, 5);
        public static readonly DateOnly MaturityDate = new(2029, 3, 5);
        public static readonly Guid     CurrencyId   = Guid.Parse("2ae3889c-609f-4988-a334-0a37f3992e96");
        public const           int      Status       = 1;
        public const           int      InterestType = 2;
        public static readonly DateTime CreatedAt    = new(2024, 3, 5, 10, 30, 0);
        public static readonly DateTime ModifiedAt   = new(2025, 3, 5, 12, 45, 0);

        public static readonly LoanRequest Request = new()
                                                     {
                                                         TypeId       = TypeId,
                                                         AccountId    = AccountId,
                                                         Amount       = Amount,
                                                         Period       = Period,
                                                         CurrencyId   = CurrencyId,
                                                         InterestType = InterestType
                                                     };

        public static readonly LoanResponse Response = new()
                                                       {
                                                           Id           = Id,
                                                           Type         = null!,
                                                           Account      = null!,
                                                           Amount       = Amount,
                                                           Period       = Period,
                                                           CreationDate = CreationDate,
                                                           MaturityDate = MaturityDate,
                                                           Currency     = null!,
                                                           Status       = Status,
                                                           InterestType = InterestType,
                                                           CreatedAt    = CreatedAt,
                                                           ModifiedAt   = ModifiedAt
                                                       };
    }
}

public static partial class SwaggerSchemaFilter
{
    public static class Loan
    {
        public class Request() : SwaggerSchemaFilter<LoanRequest>(SchemeFilters.Example.Loan.Request)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.TypeId)
                            .ToCamelCase()] = new OpenApiString(Example.TypeId.ToString()),
                           [nameof(Example.AccountId)
                            .ToCamelCase()] = new OpenApiString(Example.AccountId.ToString()),
                           [nameof(Example.Amount)
                            .ToCamelCase()] = new OpenApiDouble((double)Example.Amount),
                           [nameof(Example.Period)
                            .ToCamelCase()] = new OpenApiInteger(Example.Period),
                           [nameof(Example.CurrencyId)
                            .ToCamelCase()] = new OpenApiString(Example.CurrencyId.ToString()),
                           [nameof(Example.InterestType)
                            .ToCamelCase()] = new OpenApiInteger(Example.InterestType)
                       };
            }
        }

        public class Response() : SwaggerSchemaFilter<LoanResponse>(SchemeFilters.Example.Loan.Response)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                var loanType = context.SchemaRepository.Schemas[nameof(LoanTypeResponse)].Example;
                var account  = context.SchemaRepository.Schemas[nameof(AccountResponse)].Example;
                var currency = context.SchemaRepository.Schemas[nameof(CurrencyResponse)].Example;

                return new OpenApiObject()
                       {
                           [nameof(Example.Id)
                            .ToCamelCase()] = new OpenApiString(Example.Id.ToString()),
                           [nameof(Example.Type)
                            .ToCamelCase()] = loanType,
                           [nameof(Example.Account)
                            .ToCamelCase()] = account,
                           [nameof(Example.Amount)
                            .ToCamelCase()] = new OpenApiDouble((double)Example.Amount),
                           [nameof(Example.Period)
                            .ToCamelCase()] = new OpenApiInteger(Example.Period),
                           [nameof(Example.CreationDate)
                            .ToCamelCase()] = new OpenApiDate(Example.CreationDate.ToDateTime(TimeOnly.MinValue)),
                           [nameof(Example.MaturityDate)
                            .ToCamelCase()] = new OpenApiDate(Example.MaturityDate.ToDateTime(TimeOnly.MinValue)),
                           [nameof(Example.Currency)
                            .ToCamelCase()] = currency,
                           [nameof(Example.Status)
                            .ToCamelCase()] = new OpenApiInteger(Example.Status),
                           [nameof(Example.InterestType)
                            .ToCamelCase()] = new OpenApiInteger(Example.InterestType),
                           [nameof(Example.CreatedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.CreatedAt),
                           [nameof(Example.ModifiedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.ModifiedAt)
                       };
            }
        }
    }
}
