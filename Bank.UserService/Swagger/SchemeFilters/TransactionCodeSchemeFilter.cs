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
        public static readonly Guid     Id         = Guid.Parse("9a25d56c-5244-4b5a-b39d-d07b0e1be150");
        public const           string   Code       = "289";
        public const           string   Name       = "Platna transakcija";
        public static readonly DateTime CreatedAt  = new(2024, 3, 5, 10, 30, 0);
        public static readonly DateTime ModifiedAt = new(2025, 3, 5, 12, 45, 0);

        public static readonly TransactionCodeResponse Response = new()
                                                                  {
                                                                      Id         = Id,
                                                                      Code       = Code,
                                                                      Name       = Name,
                                                                      CreatedAt  = CreatedAt,
                                                                      ModifiedAt = ModifiedAt
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
