using Bank.Application.Extensions;
using Bank.Application.Requests;
using Bank.Application.Responses;

using Microsoft.OpenApi.Any;

namespace Bank.OpenApi.Mappers;

internal static class OrderOpenApiMapper
{
    public static OpenApiObject ToOpenApiObject(this OrderCreateRequest value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this OrderCreateRequest value, OpenApiObject @object)
    {
        @object[nameof(value.ActuaryId)
                .ToCamelCase()] = new OpenApiString(value.ActuaryId.ToString());

        @object[nameof(value.OrderType)
                .ToCamelCase()] = new OpenApiString(value.OrderType.ToString());

        @object[nameof(value.Quantity)
                .ToCamelCase()] = new OpenApiInteger(value.Quantity);

        @object[nameof(value.ContractCount)
                .ToCamelCase()] = new OpenApiInteger(value.ContractCount);

        @object[nameof(value.LimitPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.LimitPrice);

        @object[nameof(value.StopPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.StopPrice);

        @object[nameof(value.Direction)
                .ToCamelCase()] = new OpenApiString(value.Direction.ToString());

        @object[nameof(value.SupervisorId)
                .ToCamelCase()] = new OpenApiString(value.SupervisorId.ToString());

        @object[nameof(value.AccountNumber)
                .ToCamelCase()] = new OpenApiString(value.AccountNumber);

        @object[nameof(value.SecurityId)
                .ToCamelCase()] = new OpenApiString(value.SecurityId.ToString());

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this OrderUpdateRequest value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this OrderUpdateRequest value, OpenApiObject @object)
    {
        @object[nameof(value.Status)
                .ToCamelCase()] = new OpenApiString(value.Status.ToString());

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this OrderResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this OrderResponse value, OpenApiObject @object)
    {
        @object[nameof(value.Id)
                .ToCamelCase()] = new OpenApiString(value.Id.ToString());

        @object[nameof(value.Actuary)
                .ToCamelCase()] = value.Actuary?.ToOpenApiObject();

        @object[nameof(value.OrderType)
                .ToCamelCase()] = new OpenApiString(value.OrderType.ToString());

        @object[nameof(value.Quantity)
                .ToCamelCase()] = new OpenApiInteger(value.Quantity);

        @object[nameof(value.ContractCount)
                .ToCamelCase()] = new OpenApiInteger(value.ContractCount);

        @object[nameof(value.LimitPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.LimitPrice);

        @object[nameof(value.StopPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.StopPrice);

        @object[nameof(value.Direction)
                .ToCamelCase()] = new OpenApiString(value.Direction.ToString());

        @object[nameof(value.Supervisor)
                .ToCamelCase()] = value.Supervisor?.ToOpenApiObject();

        @object[nameof(value.Status)
                .ToCamelCase()] = new OpenApiString(value.Status.ToString());

        @object[nameof(value.CreatedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.CreatedAt);

        @object[nameof(value.ModifiedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.ModifiedAt);

        @object[nameof(value.Account)
                .ToCamelCase()] = value.Account?.ToOpenApiObject();

        return @object;
    }
}
