using Bank.Application.Extensions;
using Bank.Application.Responses;

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bank.UserService.Swagger.SchemeFilters;

file static class Example
{
    public static class Currency
    {
        public static readonly Guid     Id          = Guid.Parse("5efa312a-5ab6-4950-9579-0f605aeab4f8");
        public const           string   Name        = "Dolar";
        public const           string   Code        = "USD";
        public const           string   Symbol      = "$";
        public const           string   Description = "Zvanična valuta Sjedinjenih Američkih Država";
        public const           bool     Status      = true;
        public static readonly DateTime CreatedAt   = new(2024, 3, 5, 10, 30, 0);
        public static readonly DateTime ModifiedAt  = new(2025, 3, 5, 12, 45, 0);

        public static readonly CurrencyResponse Response = new()
                                                           {
                                                               Id          = Id,
                                                               Name        = Name,
                                                               Code        = Code,
                                                               Symbol      = Symbol,
                                                               Countries   = null!,
                                                               Description = Description,
                                                               Status      = Status,
                                                               CreatedAt   = CreatedAt,
                                                               ModifiedAt  = ModifiedAt
                                                           };

        public static readonly CurrencySimpleResponse SimpleResponse = new()
                                                                       {
                                                                           Id          = Id,
                                                                           Name        = Name,
                                                                           Code        = Code,
                                                                           Symbol      = Symbol,
                                                                           Description = Description,
                                                                           Status      = Status,
                                                                           CreatedAt   = CreatedAt,
                                                                           ModifiedAt  = ModifiedAt
                                                                       };
    }
}

public static partial class SwaggerSchemaFilter
{
    public static class Currency
    {
        public class Response() : SwaggerSchemaFilter<CurrencyResponse>(SchemeFilters.Example.Currency.Response)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                var countries = new OpenApiArray { context.SchemaRepository.Schemas[nameof(CountrySimpleResponse)].Example };

                return new OpenApiObject()
                       {
                           [nameof(Example.Id)
                            .ToCamelCase()] = new OpenApiString(Example.Id.ToString()),
                           [nameof(Example.Name)
                            .ToCamelCase()] = new OpenApiString(Example.Name),
                           [nameof(Example.Code)
                            .ToCamelCase()] = new OpenApiString(Example.Code),
                           [nameof(Example.Symbol)
                            .ToCamelCase()] = new OpenApiString(Example.Symbol),
                           [nameof(Example.Countries)
                            .ToCamelCase()] = countries,
                           [nameof(Example.Description)
                            .ToCamelCase()] = new OpenApiString(Example.Description),
                           [nameof(Example.Status)
                            .ToCamelCase()] = new OpenApiBoolean(Example.Status),
                           [nameof(Example.CreatedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.CreatedAt),
                           [nameof(Example.ModifiedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.ModifiedAt)
                       };
            }
        }

        public class SimpleResponse() : SwaggerSchemaFilter<CurrencySimpleResponse>(SchemeFilters.Example.Currency.SimpleResponse)
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
                           [nameof(Example.Symbol)
                            .ToCamelCase()] = new OpenApiString(Example.Symbol),
                           [nameof(Example.Description)
                            .ToCamelCase()] = new OpenApiString(Example.Description),
                           [nameof(Example.Status)
                            .ToCamelCase()] = new OpenApiBoolean(Example.Status),
                           [nameof(Example.CreatedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.CreatedAt),
                           [nameof(Example.ModifiedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.ModifiedAt)
                       };
            }
        }
    }
}
