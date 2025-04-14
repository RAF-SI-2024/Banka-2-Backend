using Bank.Application.Extensions;
using Bank.Application.Requests;
using Bank.Application.Responses;

using Microsoft.OpenApi.Any;

namespace Bank.OpenApi.Mappers;

internal static class LoanOpenApiMapper
{
    public static OpenApiObject ToOpenApiObject(this LoanCreateRequest value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this LoanCreateRequest value, OpenApiObject @object)
    {
        @object[nameof(value.TypeId)
                .ToCamelCase()] = new OpenApiString(value.TypeId.ToString());

        @object[nameof(value.AccountId)
                .ToCamelCase()] = new OpenApiString(value.AccountId.ToString());

        @object[nameof(value.Amount)
                .ToCamelCase()] = new OpenApiDouble((double)value.Amount);

        @object[nameof(value.Period)
                .ToCamelCase()] = new OpenApiInteger(value.Period);

        @object[nameof(value.CurrencyId)
                .ToCamelCase()] = new OpenApiString(value.CurrencyId.ToString());

        @object[nameof(value.InterestType)
                .ToCamelCase()] = new OpenApiInteger((int)value.InterestType);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this LoanUpdateRequest value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this LoanUpdateRequest value, OpenApiObject @object)
    {
        @object[nameof(value.MaturityDate)
                .ToCamelCase()] = new OpenApiDateTime(value.MaturityDate ?? DateTime.Now);

        @object[nameof(value.Status)
                .ToCamelCase()] = new OpenApiInteger((int)value.Status!);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this LoanResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this LoanResponse value, OpenApiObject @object)
    {
        @object[nameof(value.Id)
                .ToCamelCase()] = new OpenApiString(value.Id.ToString());

        @object[nameof(value.Type)
                .ToCamelCase()] = value.Type?.ToOpenApiObject();

        @object[nameof(value.Account)
                .ToCamelCase()] = value.Account?.ToOpenApiObject();

        @object[nameof(value.Amount)
                .ToCamelCase()] = new OpenApiDouble((double)value.Amount);

        @object[nameof(value.Period)
                .ToCamelCase()] = new OpenApiInteger(value.Period);

        @object[nameof(value.CreationDate)
                .ToCamelCase()] = new OpenApiDate(new DateTime(value.CreationDate, TimeOnly.MinValue));

        @object[nameof(value.MaturityDate)
                .ToCamelCase()] = new OpenApiDate(new DateTime(value.MaturityDate, TimeOnly.MinValue));

        @object[nameof(value.Currency)
                .ToCamelCase()] = value.Currency?.ToOpenApiObject();

        @object[nameof(value.Status)
                .ToCamelCase()] = new OpenApiInteger((int)value.Status);

        @object[nameof(value.InterestType)
                .ToCamelCase()] = new OpenApiInteger((int)value.InterestType);

        @object[nameof(value.NominalInstallmentRate)
                .ToCamelCase()] = new OpenApiDouble((double)value.NominalInstallmentRate);

        @object[nameof(value.RemainingAmount)
                .ToCamelCase()] = new OpenApiDouble((double)value.RemainingAmount);

        @object[nameof(value.CreatedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.CreatedAt);

        @object[nameof(value.ModifiedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.ModifiedAt);

        return @object;
    }
}
