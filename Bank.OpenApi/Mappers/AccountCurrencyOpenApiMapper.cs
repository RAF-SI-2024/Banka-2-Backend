using Bank.Application.Extensions;
using Bank.Application.Requests;
using Bank.Application.Responses;

using Microsoft.OpenApi.Any;

namespace Bank.OpenApi.Mappers;

internal static class AccountCurrencyOpenApiMapper
{
    public static OpenApiObject ToOpenApiObject(this AccountCurrencyCreateRequest value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this AccountCurrencyCreateRequest value, OpenApiObject @object)
    {
        @object[nameof(value.EmployeeId)
                .ToCamelCase()] = new OpenApiString(value.EmployeeId.ToString());

        @object[nameof(value.AccountId)
                .ToCamelCase()] = new OpenApiString(value.AccountId.ToString());

        @object[nameof(value.CurrencyId)
                .ToCamelCase()] = new OpenApiString(value.CurrencyId.ToString());

        @object[nameof(value.DailyLimit)
                .ToCamelCase()] = new OpenApiDouble((double)value.DailyLimit);

        @object[nameof(value.MonthlyLimit)
                .ToCamelCase()] = new OpenApiDouble((double)value.MonthlyLimit);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this AccountCurrencyClientUpdateRequest value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this AccountCurrencyClientUpdateRequest value, OpenApiObject @object)
    {
        @object[nameof(value.DailyLimit)
                .ToCamelCase()] = new OpenApiDouble((double)value.DailyLimit);

        @object[nameof(value.MonthlyLimit)
                .ToCamelCase()] = new OpenApiDouble((double)value.MonthlyLimit);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this AccountCurrencyResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this AccountCurrencyResponse value, OpenApiObject @object)
    {
        @object[nameof(value.Id)
                .ToCamelCase()] = new OpenApiString(value.Id.ToString());

        @object[nameof(value.Account)
                .ToCamelCase()] = value.Account?.ToOpenApiObject();

        @object[nameof(value.Currency)
                .ToCamelCase()] = value.Currency?.ToOpenApiObject();

        @object[nameof(value.Employee)
                .ToCamelCase()] = value.Employee?.ToOpenApiObject();

        @object[nameof(value.Balance)
                .ToCamelCase()] = new OpenApiDouble((double)value.Balance);

        @object[nameof(value.AvailableBalance)
                .ToCamelCase()] = new OpenApiDouble((double)value.AvailableBalance);

        @object[nameof(value.DailyLimit)
                .ToCamelCase()] = new OpenApiDouble((double)value.DailyLimit);

        @object[nameof(value.MonthlyLimit)
                .ToCamelCase()] = new OpenApiDouble((double)value.MonthlyLimit);

        @object[nameof(value.CreatedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.CreatedAt);

        @object[nameof(value.ModifiedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.ModifiedAt);

        return @object;
    }
}
