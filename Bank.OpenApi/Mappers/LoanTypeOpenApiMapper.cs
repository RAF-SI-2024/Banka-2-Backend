using Bank.Application.Extensions;
using Bank.Application.Requests;
using Bank.Application.Responses;

using Microsoft.OpenApi.Any;

namespace Bank.OpenApi.Mappers;

internal static class LoanTypeOpenApiMapper
{
    public static OpenApiObject ToOpenApiObject(this LoanTypeCreateRequest value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this LoanTypeCreateRequest value, OpenApiObject @object)
    {
        @object[nameof(value.Name)
                .ToCamelCase()] = new OpenApiString(value.Name);

        @object[nameof(value.Margin)
                .ToCamelCase()] = new OpenApiDouble((double)value.Margin);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this LoanTypeUpdateRequest value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this LoanTypeUpdateRequest value, OpenApiObject @object)
    {
        @object[nameof(value.Name)
                .ToCamelCase()] = new OpenApiString(value.Name);

        @object[nameof(value.Margin)
                .ToCamelCase()] = new OpenApiDouble((double)value.Margin!);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this LoanTypeResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this LoanTypeResponse value, OpenApiObject @object)
    {
        @object[nameof(value.Id)
                .ToCamelCase()] = new OpenApiString(value.Id.ToString());

        @object[nameof(value.Name)
                .ToCamelCase()] = new OpenApiString(value.Name);

        @object[nameof(value.Margin)
                .ToCamelCase()] = new OpenApiDouble((double)value.Margin);

        return @object;
    }
}
