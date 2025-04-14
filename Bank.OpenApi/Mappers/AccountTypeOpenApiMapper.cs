using Bank.Application.Extensions;
using Bank.Application.Requests;
using Bank.Application.Responses;

using Microsoft.OpenApi.Any;

namespace Bank.OpenApi.Mappers;

internal static class AccountTypeOpenApiMapper
{
    public static OpenApiObject ToOpenApiObject(this AccountTypeCreateRequest value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this AccountTypeCreateRequest value, OpenApiObject @object)
    {
        @object[nameof(value.Name)
                .ToCamelCase()] = new OpenApiString(value.Name);

        @object[nameof(value.Code)
                .ToCamelCase()] = new OpenApiString(value.Code);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this AccountTypeUpdateRequest value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this AccountTypeUpdateRequest value, OpenApiObject @object)
    {
        @object[nameof(value.Name)
                .ToCamelCase()] = new OpenApiString(value.Name);

        @object[nameof(value.Code)
                .ToCamelCase()] = new OpenApiString(value.Code);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this AccountTypeResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this AccountTypeResponse value, OpenApiObject @object)
    {
        @object[nameof(value.Id)
                .ToCamelCase()] = new OpenApiString(value.Id.ToString());

        @object[nameof(value.Name)
                .ToCamelCase()] = new OpenApiString(value.Name);

        @object[nameof(value.Code)
                .ToCamelCase()] = new OpenApiString(value.Code);

        @object[nameof(value.CreatedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.CreatedAt);

        @object[nameof(value.ModifiedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.ModifiedAt);

        return @object;
    }
}
