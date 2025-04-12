using Bank.Application.Extensions;
using Bank.Application.Responses;

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bank.UserService.Swagger.SchemeFilters;

file static class Example
{
    public static class TransactionCode
    {
        public static readonly TransactionCodeResponse Response = new()
                                                                  {
                                                                      Id         = Guid.Parse("9a25d56c-5244-4b5a-b39d-d07b0e1be150"),
                                                                      Code       = "289",
                                                                      Name       = "Platna transakcija",
                                                                      CreatedAt  = DateTime.UtcNow,
                                                                      ModifiedAt = DateTime.UtcNow,
                                                                  };
    }
}

public static partial class SwaggerSchemaFilter
{
    public static class TransactionCode
    {
        public class Response() : SwaggerSchemaFilter<TransactionCodeResponse>(SchemeFilters.Example.TransactionCode.Response)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.Id)
                            .ToCamelCase()] = new OpenApiString(Example.Id.ToString()),
                           [nameof(Example.Code)
                            .ToCamelCase()] = new OpenApiString(Example.Code),
                           [nameof(Example.Name)
                            .ToCamelCase()] = new OpenApiString(Example.Name),
                           [nameof(Example.CreatedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.CreatedAt),
                           [nameof(Example.ModifiedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.ModifiedAt)
                       };
            }
        }
    }
}
