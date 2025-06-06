using Bank.Application.Extensions;
using Bank.Application.Responses;

using Microsoft.OpenApi.Any;

namespace Bank.OpenApi.Mappers;

internal static class StockExchangeOpenApiMapper
{
    public static OpenApiObject ToOpenApiObject(this ExchangeCreateRequest value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this ExchangeCreateRequest value, OpenApiObject @object)
    {
        @object[nameof(value.Name)
                .ToCamelCase()] = new OpenApiString(value.Name);

        @object[nameof(value.Acronym)
                .ToCamelCase()] = new OpenApiString(value.Acronym);

        @object[nameof(value.MIC)
                .ToCamelCase()] = new OpenApiString(value.MIC);

        @object[nameof(value.Polity)
                .ToCamelCase()] = new OpenApiString(value.Polity);

        @object[nameof(value.TimeZone)
                .ToCamelCase()] = new OpenApiString(value.TimeZone.ToString());

        @object[nameof(value.CurrencyId)
                .ToCamelCase()] = new OpenApiString(value.CurrencyId.ToString());

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this StockExchangeResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this StockExchangeResponse value, OpenApiObject @object)
    {
        @object[nameof(value.Id)
                .ToCamelCase()] = new OpenApiString(value.Id.ToString());

        @object[nameof(value.Name)
                .ToCamelCase()] = new OpenApiString(value.Name);

        @object[nameof(value.Acronym)
                .ToCamelCase()] = new OpenApiString(value.Acronym);

        @object[nameof(value.MIC)
                .ToCamelCase()] = new OpenApiString(value.MIC);

        @object[nameof(value.Polity)
                .ToCamelCase()] = new OpenApiString(value.Polity);

        @object[nameof(value.TimeZone)
                .ToCamelCase()] = new OpenApiString(value.TimeZone.ToString());

        @object[nameof(value.Currency)
                .ToCamelCase()] = value.Currency?.ToOpenApiObject();

        @object[nameof(value.CreatedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.CreatedAt);

        @object[nameof(value.ModifiedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.ModifiedAt);

        return @object;
    }
}
