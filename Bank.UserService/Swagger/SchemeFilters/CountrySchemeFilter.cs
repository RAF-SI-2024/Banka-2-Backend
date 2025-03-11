using Bank.Application.Extensions;
using Bank.Application.Responses;

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bank.UserService.Swagger.SchemeFilters;

file static class Example
{
    public static class Country
    {
        public static readonly Guid     Id         = Guid.Parse("7f3e7c12-a8b4-47f9-b5a7-123456789abc");
        public const           string   Name       = "Sjedinjene Američke Države";
        public static readonly DateTime CreatedAt  = new(2024, 3, 5, 10, 30, 0);
        public static readonly DateTime ModifiedAt = new(2025, 3, 5, 12, 45, 0);

        public static readonly CountryResponse Response = new()
                                                          {
                                                              Id         = Id,
                                                              Name       = Name,
                                                              Currency   = null!,
                                                              CreatedAt  = CreatedAt,
                                                              ModifiedAt = ModifiedAt
                                                          };

        public static readonly CountrySimpleResponse SimpleResponse = new()
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
    public static class Country
    {
        public class Response() : SwaggerSchemaFilter<CountryResponse>(SchemeFilters.Example.Country.Response)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                var currency = context.SchemaRepository.Schemas[nameof(CurrencySimpleResponse)].Example;

                return new OpenApiObject()
                       {
                           [nameof(Example.Id)
                            .ToCamelCase()] = new OpenApiString(Example.Id.ToString()),
                           [nameof(Example.Name)
                            .ToCamelCase()] = new OpenApiString(Example.Name),
                           [nameof(Example.Currency)
                            .ToCamelCase()] = currency,
                           [nameof(Example.CreatedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.CreatedAt),
                           [nameof(Example.ModifiedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.ModifiedAt)
                       };
            }
        }

        public class SimpleResponse() : SwaggerSchemaFilter<CountrySimpleResponse>(SchemeFilters.Example.Country.SimpleResponse)
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
