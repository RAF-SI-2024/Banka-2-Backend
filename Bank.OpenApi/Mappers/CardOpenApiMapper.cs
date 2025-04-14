using Bank.Application.Extensions;
using Bank.Application.Requests;
using Bank.Application.Responses;

using Microsoft.OpenApi.Any;

namespace Bank.OpenApi.Mappers;

internal static class CardOpenApiMapper
{
    public static OpenApiObject ToOpenApiObject(this CardCreateRequest value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this CardCreateRequest value, OpenApiObject @object)
    {
        @object[nameof(value.CardTypeId)
                .ToCamelCase()] = new OpenApiString(value.CardTypeId.ToString());

        @object[nameof(value.AccountId)
                .ToCamelCase()] = new OpenApiString(value.AccountId.ToString());

        @object[nameof(value.Name)
                .ToCamelCase()] = new OpenApiString(value.Name);

        @object[nameof(value.Limit)
                .ToCamelCase()] = new OpenApiDouble((double)value.Limit);

        @object[nameof(value.Status)
                .ToCamelCase()] = new OpenApiBoolean(value.Status);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this CardUpdateLimitRequest value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this CardUpdateLimitRequest value, OpenApiObject @object)
    {
        @object[nameof(value.Limit)
                .ToCamelCase()] = new OpenApiDouble((double)value.Limit);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this CardUpdateStatusRequest value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this CardUpdateStatusRequest value, OpenApiObject @object)
    {
        @object[nameof(value.Status)
                .ToCamelCase()] = new OpenApiBoolean(value.Status);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this CardResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this CardResponse value, OpenApiObject @object)
    {
        @object[nameof(value.Id)
                .ToCamelCase()] = new OpenApiString(value.Id.ToString());

        @object[nameof(value.Type)
                .ToCamelCase()] = value.Type?.ToOpenApiObject();

        @object[nameof(value.Account)
                .ToCamelCase()] = value.Account?.ToOpenApiObject();

        @object[nameof(value.Number)
                .ToCamelCase()] = new OpenApiString(value.Number);

        @object[nameof(value.Name)
                .ToCamelCase()] = new OpenApiString(value.Name);

        @object[nameof(value.CVV)
                .ToCamelCase()] = new OpenApiString(value.CVV);

        @object[nameof(value.ExpiresAt)
                .ToCamelCase()] = new OpenApiDate(new DateTime(value.ExpiresAt, TimeOnly.MinValue));

        @object[nameof(value.Limit)
                .ToCamelCase()] = new OpenApiDouble((double)value.Limit);

        @object[nameof(value.Status)
                .ToCamelCase()] = new OpenApiBoolean(value.Status);

        @object[nameof(value.CreatedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.CreatedAt);

        @object[nameof(value.ModifiedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.ModifiedAt);

        return @object;
    }
}
