using Bank.Application.Extensions;
using Bank.Application.Responses;
using Bank.OpenApi.Extensions;

using Microsoft.OpenApi.Any;

namespace Bank.OpenApi.Mappers;

internal static class CurrencyOpenApiMapper
{
    public static OpenApiObject ToOpenApiObject(this CurrencyResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this CurrencyResponse value, OpenApiObject @object)
    {
        @object[nameof(value.Id)
                .ToCamelCase()] = new OpenApiString(value.Id.ToString());

        @object[nameof(value.Name)
                .ToCamelCase()] = new OpenApiString(value.Name);

        @object[nameof(value.Code)
                .ToCamelCase()] = new OpenApiString(value.Code);

        @object[nameof(value.Symbol)
                .ToCamelCase()] = new OpenApiString(value.Symbol);

        @object[nameof(value.Countries)
                .ToCamelCase()] = value.Countries.Select(country => country.ToOpenApiObject())
                                       .ToOpenApiArray();

        @object[nameof(value.Description)
                .ToCamelCase()] = new OpenApiString(value.Description);

        @object[nameof(value.Status)
                .ToCamelCase()] = new OpenApiBoolean(value.Status);

        @object[nameof(value.CreatedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.CreatedAt);

        @object[nameof(value.ModifiedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.ModifiedAt);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this CurrencySimpleResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this CurrencySimpleResponse value, OpenApiObject @object)
    {
        @object[nameof(value.Id)
                .ToCamelCase()] = new OpenApiString(value.Id.ToString());

        @object[nameof(value.Name)
                .ToCamelCase()] = new OpenApiString(value.Name);

        @object[nameof(value.Code)
                .ToCamelCase()] = new OpenApiString(value.Code);

        @object[nameof(value.Symbol)
                .ToCamelCase()] = new OpenApiString(value.Symbol);

        @object[nameof(value.Description)
                .ToCamelCase()] = new OpenApiString(value.Description);

        @object[nameof(value.Status)
                .ToCamelCase()] = new OpenApiBoolean(value.Status);

        @object[nameof(value.CreatedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.CreatedAt);

        @object[nameof(value.ModifiedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.ModifiedAt);

        return @object;
    }
}
