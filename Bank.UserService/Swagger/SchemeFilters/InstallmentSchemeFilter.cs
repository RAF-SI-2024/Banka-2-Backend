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
    public static class Installment
    {
        public static readonly InstallmentResponse Response = new()
                                                              {
                                                                  Id              = Sample.Installment.Request.InstallmentId,
                                                                  Loan            = null!,
                                                                  InterestRate    = Sample.Installment.Request.InterestRate,
                                                                  ExpectedDueDate = Sample.Installment.Request.ExpectedDueDate,
                                                                  ActualDueDate   = Sample.Installment.Request.ActualDueDate,
                                                                  Status          = Sample.Installment.Request.Status,
                                                                  CreatedAt       = DateTime.UtcNow,
                                                                  ModifiedAt      = DateTime.UtcNow,
                                                              };
    }
}

public static partial class SwaggerSchemaFilter
{
    public static class Installment
    {
        public class Request() : SwaggerSchemaFilter<InstallmentCreateRequest>(Sample.Installment.Request)
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
                            .ToCamelCase()] = new OpenApiInteger((int)Example.Status)
                       };
            }
        }

        public class UpdateRequest() : SwaggerSchemaFilter<InstallmentUpdateRequest>(Sample.Installment.UpdateRequest)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.ActualDueDate)
                            .ToCamelCase()] = new OpenApiDate(Example.ActualDueDate ?? DateTime.MinValue),
                           [nameof(Example.Status)
                            .ToCamelCase()] = new OpenApiInteger((int)Example.Status!),
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
                            .ToCamelCase()] = new OpenApiInteger((int)Example.Status),
                           [nameof(Example.CreatedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.CreatedAt),
                           [nameof(Example.ModifiedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.ModifiedAt)
                       };
            }
        }
    }
}
