using Bank.Application.Domain;
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
    public static class Loan
    {
        public static readonly LoanResponse Response = new()
                                                       {
                                                           Id           = Guid.Parse("90a10f93-85cc-491a-8624-07c485a2b431"),
                                                           Type         = null!,
                                                           Account      = null!,
                                                           Amount       = Sample.Loan.Request.Amount,
                                                           Period       = Sample.Loan.Request.Period,
                                                           CreationDate = new(2024, 3, 5),
                                                           MaturityDate = new(2029, 3, 5),
                                                           Currency     = null!,
                                                           Status       = LoanStatus.Active,
                                                           InterestType = Sample.Loan.Request.InterestType,
                                                           CreatedAt    = DateTime.UtcNow,
                                                           ModifiedAt   = DateTime.UtcNow
                                                       };
    }
}

public static partial class SwaggerSchemaFilter
{
    public static class Loan
    {
        public class Request() : SwaggerSchemaFilter<LoanCreateRequest>(Sample.Loan.Request)
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
                            .ToCamelCase()] = new OpenApiInteger((int)Example.InterestType)
                       };
            }
        }

        public class UpdateRequest() : SwaggerSchemaFilter<LoanUpdateRequest>(Sample.Loan.UpdateRequest)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.Status)
                            .ToCamelCase()] = new OpenApiInteger((int)Example.Status!),
                           [nameof(Example.MaturityDate)
                            .ToCamelCase()] = new OpenApiDateTime(Example.MaturityDate ?? DateTime.MinValue)
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
                            .ToCamelCase()] = new OpenApiInteger((int)Example.Status),
                           [nameof(Example.InterestType)
                            .ToCamelCase()] = new OpenApiInteger((int)Example.InterestType),
                           [nameof(Example.CreatedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.CreatedAt),
                           [nameof(Example.ModifiedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.ModifiedAt)
                       };
            }
        }
    }
}
