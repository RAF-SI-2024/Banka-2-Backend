using Bank.Application.Extensions;
using Bank.Application.Responses;

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bank.UserService.Swagger.SchemeFilters;

file static class Example
{
    public static class CardType
    {
        public static readonly Guid     Id         = Guid.Parse("123e4567-e89b-12d3-a456-426614174000");
        public const           string   Name       = "Credit Card";
        public static readonly DateTime CreatedAt  = new(2024, 6, 12, 10, 30, 0);
        public static readonly DateTime ModifiedAt = new(2025, 3, 5, 12, 45, 0);

        public static readonly CardTypeResponse Response = new()
                                                           {
                                                               Id         = Id,
                                                               Name       = Name,
                                                               CreatedAt  = CreatedAt,
                                                               ModifiedAt = ModifiedAt
                                                           };
    }
}

public static partial class SwaggerSchemaFilter
{
    public static class CardType
    {
        public class Response() : SwaggerSchemaFilter<CardTypeResponse>(SchemeFilters.Example.CardType.Response)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.Id)
                            .ToCamelCase()] = new OpenApiString(Example.Id.ToString()),
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
