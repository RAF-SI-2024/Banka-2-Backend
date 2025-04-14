using Bank.Application.Extensions;
using Bank.Application.Requests;
using Bank.Application.Responses;

using Microsoft.OpenApi.Any;

namespace Bank.OpenApi.Mappers;

internal static class TransactionTemplateOpenApiMapper
{
    public static OpenApiObject ToOpenApiObject(this TransactionTemplateCreateRequest value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this TransactionTemplateCreateRequest value, OpenApiObject @object)
    {
        @object[nameof(value.Name)
                .ToCamelCase()] = new OpenApiString(value.Name);

        @object[nameof(value.AccountNumber)
                .ToCamelCase()] = new OpenApiString(value.AccountNumber);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this TransactionTemplateUpdateRequest value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this TransactionTemplateUpdateRequest value, OpenApiObject @object)
    {
        @object[nameof(value.Name)
                .ToCamelCase()] = new OpenApiString(value.Name);

        @object[nameof(value.AccountNumber)
                .ToCamelCase()] = new OpenApiString(value.AccountNumber);

        @object[nameof(value.Deleted)
                .ToCamelCase()] = new OpenApiBoolean(value.Deleted);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this TransactionTemplateResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this TransactionTemplateResponse value, OpenApiObject @object)
    {
        @object[nameof(value.Id)
                .ToCamelCase()] = new OpenApiString(value.Id.ToString());

        @object[nameof(value.Client)
                .ToCamelCase()] = value.Client?.ToOpenApiObject();

        @object[nameof(value.Name)
                .ToCamelCase()] = new OpenApiString(value.Name);

        @object[nameof(value.AccountNumber)
                .ToCamelCase()] = new OpenApiString(value.AccountNumber);

        @object[nameof(value.Deleted)
                .ToCamelCase()] = new OpenApiBoolean(value.Deleted);

        @object[nameof(value.CreatedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.CreatedAt);

        @object[nameof(value.ModifiedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.ModifiedAt);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this TransactionTemplateSimpleResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this TransactionTemplateSimpleResponse value, OpenApiObject @object)
    {
        @object[nameof(value.Id)
                .ToCamelCase()] = new OpenApiString(value.Id.ToString());

        @object[nameof(value.Name)
                .ToCamelCase()] = new OpenApiString(value.Name);

        @object[nameof(value.AccountNumber)
                .ToCamelCase()] = new OpenApiString(value.AccountNumber);

        @object[nameof(value.Deleted)
                .ToCamelCase()] = new OpenApiBoolean(value.Deleted);

        return @object;
    }
}
