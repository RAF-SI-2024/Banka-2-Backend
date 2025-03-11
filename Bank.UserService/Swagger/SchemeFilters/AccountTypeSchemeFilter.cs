using Bank.Application.Extensions;
using Bank.Application.Responses;

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bank.UserService.Swagger.SchemeFilters;

file static class Example
{
    public static class AccountType
    {
        public static readonly Guid     Id         = Guid.Parse("c3f7a5d4-e6b8-4d2a-a678-123456789abc");
        public const           string   Name       = "Štedni račun";
        public const           string   Code       = "SSS";
        public static readonly DateTime CreatedAt  = new(2024, 3, 5, 10, 30, 0);
        public static readonly DateTime ModifiedAt = new(2025, 3, 5, 12, 45, 0);

        public static readonly AccountTypeResponse Response = new()
                                                              {
                                                                  Id         = Id,
                                                                  Name       = Name,
                                                                  Code       = Code,
                                                                  CreatedAt  = CreatedAt,
                                                                  ModifiedAt = ModifiedAt
                                                              };
    }
}

public static partial class SwaggerSchemaFilter
{
    public static class AccountType
    {
        public class Response() : SwaggerSchemaFilter<AccountTypeResponse>(SchemeFilters.Example.AccountType.Response)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.Id)
                            .ToCamelCase()] = new OpenApiString(Example.Id.ToString()),
                           [nameof(Example.Name)
                            .ToCamelCase()] = new OpenApiString(Example.Name),
                           [nameof(Example.Code)
                            .ToCamelCase()] = new OpenApiString(Example.Code),
                           [nameof(Example.CreatedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.CreatedAt),
                           [nameof(Example.ModifiedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.ModifiedAt)
                       };
            }
        }
    }
}
