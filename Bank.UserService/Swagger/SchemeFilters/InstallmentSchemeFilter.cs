using Bank.Application.Extensions;
using Bank.Application.Requests;
using Bank.Application.Responses;

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bank.UserService.Swagger.SchemeFilters;

file static class Example
{
    public static class Installment
    {
        public static readonly Guid     Id              = Guid.Parse("a52cbe51-d29e-486a-b7dd-079aa315883f");
        public static readonly Guid     LoanId          = Guid.Parse("c6dcae29-91cd-40d5-a75c-8df6f24cc257");
        public const           decimal  InterestRate    = 5.0m;
        public static readonly DateOnly ExpectedDueDate = new(2025, 6, 15);
        public static readonly DateOnly ActualDueDate   = new(2025, 6, 20);
        public const           int      Status          = 1;
        public static readonly DateTime CreatedAt       = new(2024, 3, 5, 10, 30, 0);
        public static readonly DateTime ModifiedAt      = new(2025, 3, 5, 12, 45, 0);

        public static readonly InstallmentRequest Request = new()
                                                            {
                                                                InstallmentId   = Id,
                                                                LoanId          = LoanId,
                                                                InterestRate    = InterestRate,
                                                                ExpectedDueDate = ExpectedDueDate,
                                                                ActualDueDate   = ActualDueDate,
                                                                Status          = Status
                                                            };

        public static readonly InstallmentResponse Response = new()
                                                              {
                                                                  Id              = Id,
                                                                  Loan            = null!,
                                                                  InterestRate    = InterestRate,
                                                                  ExpectedDueDate = ExpectedDueDate,
                                                                  ActualDueDate   = ActualDueDate,
                                                                  Status          = Status,
                                                                  CreatedAt       = CreatedAt,
                                                                  ModifiedAt      = ModifiedAt
                                                              };
    }
}

public static partial class SwaggerSchemaFilter
{
    public static class Installment
    {
        public class Request() : SwaggerSchemaFilter<InstallmentRequest>(SchemeFilters.Example.Installment.Request)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.InstallmentId)
                            .ToCamelCase()] = new OpenApiString(Example.InstallmentId.ToString()),
                           [nameof(Example.LoanId)
                            .ToCamelCase()] = new OpenApiString(Example.LoanId.ToString()),
                           [nameof(Example.InterestRate)
                            .ToCamelCase()] = new OpenApiDouble((double)Example.InterestRate),
                           [nameof(Example.ExpectedDueDate)
                            .ToCamelCase()] = new OpenApiDate(Example.ExpectedDueDate.ToDateTime(TimeOnly.MinValue)),
                           [nameof(Example.ActualDueDate)
                            .ToCamelCase()] = new OpenApiDate(Example.ActualDueDate.ToDateTime(TimeOnly.MinValue)),
                           [nameof(Example.Status)
                            .ToCamelCase()] = new OpenApiInteger(Example.Status)
                       };
            }
        }

        public class Response() : SwaggerSchemaFilter<InstallmentResponse>(SchemeFilters.Example.Installment.Response)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                var loan = context.SchemaRepository.Schemas[nameof(LoanResponse)].Example;

                return new OpenApiObject()
                       {
                           [nameof(Example.Id)
                            .ToCamelCase()] = new OpenApiString(Example.Id.ToString()),
                           [nameof(Example.Loan)
                            .ToCamelCase()] = loan,
                           [nameof(Example.InterestRate)
                            .ToCamelCase()] = new OpenApiDouble((double)Example.InterestRate),
                           [nameof(Example.ExpectedDueDate)
                            .ToCamelCase()] = new OpenApiDate(Example.ExpectedDueDate.ToDateTime(TimeOnly.MinValue)),
                           [nameof(Example.ActualDueDate)
                            .ToCamelCase()] = new OpenApiDate(Example.ActualDueDate.ToDateTime(TimeOnly.MinValue)),
                           [nameof(Example.Status)
                            .ToCamelCase()] = new OpenApiInteger(Example.Status),
                           [nameof(Example.CreatedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.CreatedAt),
                           [nameof(Example.ModifiedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.ModifiedAt)
                       };
            }
        }
    }
}
