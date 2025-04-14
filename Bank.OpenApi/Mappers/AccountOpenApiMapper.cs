using Bank.Application.Extensions;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.OpenApi.Extensions;

using Microsoft.OpenApi.Any;

namespace Bank.OpenApi.Mappers;

internal static class AccountOpenApiMapper
{
    public static OpenApiObject ToOpenApiObject(this AccountCreateRequest value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this AccountCreateRequest value, OpenApiObject @object)
    {
        @object[nameof(value.Name)
                .ToCamelCase()] = new OpenApiString(value.Name);

        @object[nameof(value.ClientId)
                .ToCamelCase()] = new OpenApiString(value.ClientId.ToString());

        @object[nameof(value.CurrencyId)
                .ToCamelCase()] = new OpenApiString(value.CurrencyId.ToString());

        @object[nameof(value.AccountTypeId)
                .ToCamelCase()] = new OpenApiString(value.AccountTypeId.ToString());

        @object[nameof(value.Balance)
                .ToCamelCase()] = new OpenApiDouble((double)value.Balance);

        @object[nameof(value.DailyLimit)
                .ToCamelCase()] = new OpenApiDouble((double)value.DailyLimit);

        @object[nameof(value.MonthlyLimit)
                .ToCamelCase()] = new OpenApiDouble((double)value.MonthlyLimit);

        @object[nameof(value.Status)
                .ToCamelCase()] = new OpenApiBoolean(value.Status);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this AccountUpdateClientRequest value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this AccountUpdateClientRequest value, OpenApiObject @object)
    {
        @object[nameof(value.Name)
                .ToCamelCase()] = new OpenApiString(value.Name);

        @object[nameof(value.DailyLimit)
                .ToCamelCase()] = new OpenApiDouble((double)value.DailyLimit);

        @object[nameof(value.MonthlyLimit)
                .ToCamelCase()] = new OpenApiDouble((double)value.MonthlyLimit);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this AccountUpdateEmployeeRequest value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this AccountUpdateEmployeeRequest value, OpenApiObject @object)
    {
        @object[nameof(value.Status)
                .ToCamelCase()] = new OpenApiBoolean(value.Status);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this AccountResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this AccountResponse value, OpenApiObject @object)
    {
        @object[nameof(value.Id)
                .ToCamelCase()] = new OpenApiString(value.Id.ToString());

        @object[nameof(value.AccountNumber)
                .ToCamelCase()] = new OpenApiString(value.AccountNumber);

        @object[nameof(value.Name)
                .ToCamelCase()] = new OpenApiString(value.Name);

        @object[nameof(value.Client)
                .ToCamelCase()] = value.Client?.ToOpenApiObject();

        @object[nameof(value.Balance)
                .ToCamelCase()] = new OpenApiDouble((double)value.Balance);

        @object[nameof(value.AvailableBalance)
                .ToCamelCase()] = new OpenApiDouble((double)value.AvailableBalance);

        @object[nameof(value.DailyLimit)
                .ToCamelCase()] = new OpenApiDouble((double)value.DailyLimit);

        @object[nameof(value.MonthlyLimit)
                .ToCamelCase()] = new OpenApiDouble((double)value.MonthlyLimit);

        @object[nameof(value.Employee)
                .ToCamelCase()] = value.Employee?.ToOpenApiObject();

        @object[nameof(value.Currency)
                .ToCamelCase()] = value.Currency?.ToOpenApiObject();

        @object[nameof(value.Type)
                .ToCamelCase()] = value.Type?.ToOpenApiObject();

        @object[nameof(value.AccountCurrencies)
                .ToCamelCase()] = value.AccountCurrencies.Where(accountCurrency => accountCurrency != null)
                                       .Select(accountCurrency => accountCurrency.ToOpenApiObject())
                                       .ToOpenApiArray();

        @object[nameof(value.CreationDate)
                .ToCamelCase()] = new OpenApiDate(new DateTime(value.CreationDate, TimeOnly.MinValue));

        @object[nameof(value.ExpirationDate)
                .ToCamelCase()] = new OpenApiDate(new DateTime(value.ExpirationDate, TimeOnly.MinValue));

        @object[nameof(value.Status)
                .ToCamelCase()] = new OpenApiBoolean(value.Status);

        @object[nameof(value.CreatedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.CreatedAt);

        @object[nameof(value.ModifiedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.ModifiedAt);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this AccountSimpleResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this AccountSimpleResponse value, OpenApiObject @object)
    {
        @object[nameof(value.Id)
                .ToCamelCase()] = new OpenApiString(value.Id.ToString());

        @object[nameof(value.AccountNumber)
                .ToCamelCase()] = new OpenApiString(value.AccountNumber);

        return @object;
    }
}
