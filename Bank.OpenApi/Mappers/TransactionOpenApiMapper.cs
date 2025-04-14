using Bank.Application.Extensions;
using Bank.Application.Requests;
using Bank.Application.Responses;

using Microsoft.OpenApi.Any;

namespace Bank.OpenApi.Mappers;

internal static class TransactionOpenApiMapper
{
    public static OpenApiObject ToOpenApiObject(this TransactionCreateRequest value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this TransactionCreateRequest value, OpenApiObject @object)
    {
        @object[nameof(value.FromAccountNumber)
                .ToCamelCase()] = new OpenApiString(value.FromAccountNumber);

        @object[nameof(value.FromCurrencyId)
                .ToCamelCase()] = new OpenApiString(value.FromCurrencyId.ToString());

        @object[nameof(value.ToAccountNumber)
                .ToCamelCase()] = new OpenApiString(value.ToAccountNumber);

        @object[nameof(value.ToCurrencyId)
                .ToCamelCase()] = new OpenApiString(value.ToCurrencyId.ToString());

        @object[nameof(value.Amount)
                .ToCamelCase()] = new OpenApiDouble((double)value.Amount);

        @object[nameof(value.CodeId)
                .ToCamelCase()] = new OpenApiString(value.CodeId.ToString());

        @object[nameof(value.ReferenceNumber)
                .ToCamelCase()] = new OpenApiString(value.ReferenceNumber);

        @object[nameof(value.Purpose)
                .ToCamelCase()] = new OpenApiString(value.Purpose);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this TransactionUpdateRequest value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this TransactionUpdateRequest value, OpenApiObject @object)
    {
        @object[nameof(value.Status)
                .ToCamelCase()] = new OpenApiInteger((int)value.Status);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this TransactionResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this TransactionResponse value, OpenApiObject @object)
    {
        @object[nameof(value.Id)
                .ToCamelCase()] = new OpenApiString(value.Id.ToString());

        @object[nameof(value.FromAccount)
                .ToCamelCase()] = value.FromAccount?.ToOpenApiObject();

        @object[nameof(value.FromCurrency)
                .ToCamelCase()] = value.FromCurrency?.ToOpenApiObject();

        @object[nameof(value.ToAccount)
                .ToCamelCase()] = value.ToAccount?.ToOpenApiObject();

        @object[nameof(value.ToCurrency)
                .ToCamelCase()] = value.ToCurrency?.ToOpenApiObject();

        @object[nameof(value.FromAmount)
                .ToCamelCase()] = new OpenApiDouble((double)value.FromAmount);

        @object[nameof(value.ToAmount)
                .ToCamelCase()] = new OpenApiDouble((double)value.ToAmount);

        @object[nameof(value.Code)
                .ToCamelCase()] = value.Code?.ToOpenApiObject();

        @object[nameof(value.ReferenceNumber)
                .ToCamelCase()] = new OpenApiString(value.ReferenceNumber);

        @object[nameof(value.Purpose)
                .ToCamelCase()] = new OpenApiString(value.Purpose);

        @object[nameof(value.Status)
                .ToCamelCase()] = new OpenApiInteger((int)value.Status);

        @object[nameof(value.CreatedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.CreatedAt);

        @object[nameof(value.ModifiedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.ModifiedAt);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this TransactionCreateResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this TransactionCreateResponse value, OpenApiObject @object)
    {
        @object[nameof(value.Id)
                .ToCamelCase()] = new OpenApiString(value.Id.ToString());

        @object[nameof(value.FromAmount)
                .ToCamelCase()] = new OpenApiDouble((double)value.FromAmount);

        @object[nameof(value.Code)
                .ToCamelCase()] = value.Code?.ToOpenApiObject();

        @object[nameof(value.ReferenceNumber)
                .ToCamelCase()] = new OpenApiString(value.ReferenceNumber);

        @object[nameof(value.Purpose)
                .ToCamelCase()] = new OpenApiString(value.Purpose);

        @object[nameof(value.Status)
                .ToCamelCase()] = new OpenApiInteger((int)value.Status);

        @object[nameof(value.CreatedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.CreatedAt);

        @object[nameof(value.ModifiedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.ModifiedAt);

        return @object;
    }
}
