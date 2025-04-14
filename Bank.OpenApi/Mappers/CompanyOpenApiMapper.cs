using Bank.Application.Extensions;
using Bank.Application.Requests;
using Bank.Application.Responses;

using Microsoft.OpenApi.Any;

namespace Bank.OpenApi.Mappers;

internal static class CompanyOpenApiMapper
{
    public static OpenApiObject ToOpenApiObject(this CompanyCreateRequest value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this CompanyCreateRequest value, OpenApiObject @object)
    {
        @object[nameof(value.Name)
                .ToCamelCase()] = new OpenApiString(value.Name);

        @object[nameof(value.RegistrationNumber)
                .ToCamelCase()] = new OpenApiString(value.RegistrationNumber);

        @object[nameof(value.TaxIdentificationNumber)
                .ToCamelCase()] = new OpenApiString(value.TaxIdentificationNumber);

        @object[nameof(value.ActivityCode)
                .ToCamelCase()] = new OpenApiString(value.ActivityCode);

        @object[nameof(value.Address)
                .ToCamelCase()] = new OpenApiString(value.Address);

        @object[nameof(value.MajorityOwnerId)
                .ToCamelCase()] = new OpenApiString(value.MajorityOwnerId.ToString());

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this CompanyUpdateRequest value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this CompanyUpdateRequest value, OpenApiObject @object)
    {
        @object[nameof(value.Name)
                .ToCamelCase()] = new OpenApiString(value.Name);

        @object[nameof(value.ActivityCode)
                .ToCamelCase()] = new OpenApiString(value.ActivityCode);

        @object[nameof(value.Address)
                .ToCamelCase()] = new OpenApiString(value.Address);

        @object[nameof(value.MajorityOwnerId)
                .ToCamelCase()] = new OpenApiString(value.MajorityOwnerId.ToString());

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this CompanyResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this CompanyResponse value, OpenApiObject @object)
    {
        @object[nameof(value.Id)
                .ToCamelCase()] = new OpenApiString(value.Id.ToString());

        @object[nameof(value.Name)
                .ToCamelCase()] = new OpenApiString(value.Name);

        @object[nameof(value.RegistrationNumber)
                .ToCamelCase()] = new OpenApiString(value.RegistrationNumber);

        @object[nameof(value.TaxIdentificationNumber)
                .ToCamelCase()] = new OpenApiString(value.TaxIdentificationNumber);

        @object[nameof(value.ActivityCode)
                .ToCamelCase()] = new OpenApiString(value.ActivityCode);

        @object[nameof(value.Address)
                .ToCamelCase()] = new OpenApiString(value.Address);

        @object[nameof(value.MajorityOwner)
                .ToCamelCase()] = value.MajorityOwner?.ToOpenApiObject();

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this CompanySimpleResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this CompanySimpleResponse value, OpenApiObject @object)
    {
        @object[nameof(value.Id)
                .ToCamelCase()] = new OpenApiString(value.Id.ToString());

        @object[nameof(value.Name)
                .ToCamelCase()] = new OpenApiString(value.Name);

        @object[nameof(value.RegistrationNumber)
                .ToCamelCase()] = new OpenApiString(value.RegistrationNumber);

        @object[nameof(value.TaxIdentificationNumber)
                .ToCamelCase()] = new OpenApiString(value.TaxIdentificationNumber);

        @object[nameof(value.ActivityCode)
                .ToCamelCase()] = new OpenApiString(value.ActivityCode);

        @object[nameof(value.Address)
                .ToCamelCase()] = new OpenApiString(value.Address);

        return @object;
    }
}
