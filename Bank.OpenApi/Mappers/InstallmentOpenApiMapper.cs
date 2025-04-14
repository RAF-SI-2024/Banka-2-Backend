using Bank.Application.Extensions;
using Bank.Application.Requests;
using Bank.Application.Responses;

using Microsoft.OpenApi.Any;

namespace Bank.OpenApi.Mappers;

internal static class InstallmentOpenApiMapper
{
    public static OpenApiObject ToOpenApiObject(this InstallmentCreateRequest value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this InstallmentCreateRequest value, OpenApiObject @object)
    {
        @object[nameof(value.InstallmentId)
                .ToCamelCase()] = new OpenApiString(value.InstallmentId.ToString());

        @object[nameof(value.LoanId)
                .ToCamelCase()] = new OpenApiString(value.LoanId.ToString());

        @object[nameof(value.InterestRate)
                .ToCamelCase()] = new OpenApiDouble((double)value.InterestRate);

        @object[nameof(value.ExpectedDueDate)
                .ToCamelCase()] = new OpenApiDate(new DateTime(value.ExpectedDueDate, TimeOnly.MinValue));

        @object[nameof(value.ActualDueDate)
                .ToCamelCase()] = new OpenApiDate(new DateTime(value.ActualDueDate, TimeOnly.MinValue));

        @object[nameof(value.Status)
                .ToCamelCase()] = new OpenApiInteger((int)value.Status);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this InstallmentUpdateRequest value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this InstallmentUpdateRequest value, OpenApiObject @object)
    {
        @object[nameof(value.ActualDueDate)
                .ToCamelCase()] = new OpenApiDate(value.ActualDueDate ?? DateTime.Now);

        @object[nameof(value.Status)
                .ToCamelCase()] = new OpenApiInteger((int)value.Status!);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this InstallmentResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this InstallmentResponse value, OpenApiObject @object)
    {
        @object[nameof(value.Id)
                .ToCamelCase()] = new OpenApiString(value.Id.ToString());

        @object[nameof(value.Loan)
                .ToCamelCase()] = value.Loan?.ToOpenApiObject();

        @object[nameof(value.InterestRate)
                .ToCamelCase()] = new OpenApiDouble((double)value.InterestRate);

        @object[nameof(value.ExpectedDueDate)
                .ToCamelCase()] = new OpenApiDate(new DateTime(value.ExpectedDueDate, TimeOnly.MinValue));

        @object[nameof(value.ActualDueDate)
                .ToCamelCase()] = new OpenApiDate(new DateTime(value.ActualDueDate, TimeOnly.MinValue));

        @object[nameof(value.Status)
                .ToCamelCase()] = new OpenApiInteger((int)value.Status);

        @object[nameof(value.CreatedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.CreatedAt);

        @object[nameof(value.ModifiedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.ModifiedAt);

        return @object;
    }
}
