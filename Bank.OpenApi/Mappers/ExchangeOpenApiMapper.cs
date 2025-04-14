using Bank.Application.Extensions;
using Bank.Application.Requests;
using Bank.Application.Responses;

using Microsoft.OpenApi.Any;

namespace Bank.OpenApi.Mappers;

internal static class ExchangeOpenApiMapper
{
    public static OpenApiObject ToOpenApiObject(this ExchangeMakeExchangeRequest value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this ExchangeMakeExchangeRequest value, OpenApiObject @object)
    {
        @object[nameof(value.CurrencyFromId)
                .ToCamelCase()] = new OpenApiString(value.CurrencyFromId.ToString());

        @object[nameof(value.CurrencyToId)
                .ToCamelCase()] = new OpenApiString(value.CurrencyToId.ToString());

        @object[nameof(value.Amount)
                .ToCamelCase()] = new OpenApiDouble((double)value.Amount);

        @object[nameof(value.AccountId)
                .ToCamelCase()] = new OpenApiString(value.AccountId.ToString());

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this ExchangeUpdateRequest value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this ExchangeUpdateRequest value, OpenApiObject @object)
    {
        @object[nameof(value.Commission)
                .ToCamelCase()] = new OpenApiDouble((double)value.Commission);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this ExchangeResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this ExchangeResponse value, OpenApiObject @object)
    {
        @object[nameof(value.Id)
                .ToCamelCase()] = new OpenApiString(value.Id.ToString());

        @object[nameof(value.CurrencyFrom)
                .ToCamelCase()] = value.CurrencyFrom?.ToOpenApiObject();

        @object[nameof(value.CurrencyTo)
                .ToCamelCase()] = value.CurrencyTo?.ToOpenApiObject();

        @object[nameof(value.Commission)
                .ToCamelCase()] = new OpenApiDouble((double)value.Commission);

        @object[nameof(value.Rate)
                .ToCamelCase()] = new OpenApiDouble((double)value.Rate);

        @object[nameof(value.InverseRate)
                .ToCamelCase()] = new OpenApiDouble((double)value.InverseRate);

        @object[nameof(value.CreatedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.CreatedAt);

        @object[nameof(value.ModifiedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.ModifiedAt);

        return @object;
    }
}
