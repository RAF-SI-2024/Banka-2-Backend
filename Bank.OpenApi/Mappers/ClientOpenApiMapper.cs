using Bank.Application.Extensions;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.OpenApi.Extensions;

using Microsoft.OpenApi.Any;

namespace Bank.OpenApi.Mappers;

internal static class ClientOpenApiMapper
{
    public static OpenApiObject ToOpenApiObject(this ClientCreateRequest value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this ClientCreateRequest value, OpenApiObject @object)
    {
        @object[nameof(value.FirstName)
                .ToCamelCase()] = new OpenApiString(value.FirstName);

        @object[nameof(value.LastName)
                .ToCamelCase()] = new OpenApiString(value.LastName);

        @object[nameof(value.DateOfBirth)
                .ToCamelCase()] = new OpenApiDate(new DateTime(value.DateOfBirth, TimeOnly.MinValue));

        @object[nameof(value.Gender)
                .ToCamelCase()] = new OpenApiInteger((int)value.Gender);

        @object[nameof(value.UniqueIdentificationNumber)
                .ToCamelCase()] = new OpenApiString(value.UniqueIdentificationNumber);

        @object[nameof(value.Email)
                .ToCamelCase()] = new OpenApiString(value.Email);

        @object[nameof(value.PhoneNumber)
                .ToCamelCase()] = new OpenApiString(value.PhoneNumber);

        @object[nameof(value.Address)
                .ToCamelCase()] = new OpenApiString(value.Address);

        @object[nameof(value.Permissions)
                .ToCamelCase()] = new OpenApiLong(value.Permissions);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this ClientUpdateRequest value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this ClientUpdateRequest value, OpenApiObject @object)
    {
        @object[nameof(value.FirstName)
                .ToCamelCase()] = new OpenApiString(value.FirstName);

        @object[nameof(value.LastName)
                .ToCamelCase()] = new OpenApiString(value.LastName);

        @object[nameof(value.PhoneNumber)
                .ToCamelCase()] = new OpenApiString(value.PhoneNumber);

        @object[nameof(value.Address)
                .ToCamelCase()] = new OpenApiString(value.Address);

        @object[nameof(value.Activated)
                .ToCamelCase()] = new OpenApiBoolean(value.Activated);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this ClientResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this ClientResponse value, OpenApiObject @object)
    {
        @object[nameof(value.Id)
                .ToCamelCase()] = new OpenApiString(value.Id.ToString());

        @object[nameof(value.FirstName)
                .ToCamelCase()] = new OpenApiString(value.FirstName);

        @object[nameof(value.LastName)
                .ToCamelCase()] = new OpenApiString(value.LastName);

        @object[nameof(value.DateOfBirth)
                .ToCamelCase()] = new OpenApiDate(new DateTime(value.DateOfBirth, TimeOnly.MinValue));

        @object[nameof(value.Gender)
                .ToCamelCase()] = new OpenApiInteger((int)value.Gender);

        @object[nameof(value.UniqueIdentificationNumber)
                .ToCamelCase()] = new OpenApiString(value.UniqueIdentificationNumber);

        @object[nameof(value.Email)
                .ToCamelCase()] = new OpenApiString(value.Email);

        @object[nameof(value.PhoneNumber)
                .ToCamelCase()] = new OpenApiString(value.PhoneNumber);

        @object[nameof(value.Address)
                .ToCamelCase()] = new OpenApiString(value.Address);

        @object[nameof(value.Role)
                .ToCamelCase()] = new OpenApiInteger((int)value.Role);

        @object[nameof(value.Accounts)
                .ToCamelCase()] = value.Accounts.Select(account => account?.ToOpenApiObject())
                                       .ToOpenApiArray();

        @object[nameof(value.CreatedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.CreatedAt);

        @object[nameof(value.ModifiedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.ModifiedAt);

        @object[nameof(value.Activated)
                .ToCamelCase()] = new OpenApiBoolean(value.Activated);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this ClientSimpleResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this ClientSimpleResponse value, OpenApiObject @object)
    {
        @object[nameof(value.Id)
                .ToCamelCase()] = new OpenApiString(value.Id.ToString());

        @object[nameof(value.FirstName)
                .ToCamelCase()] = new OpenApiString(value.FirstName);

        @object[nameof(value.LastName)
                .ToCamelCase()] = new OpenApiString(value.LastName);

        @object[nameof(value.DateOfBirth)
                .ToCamelCase()] = new OpenApiDate(new DateTime(value.DateOfBirth, TimeOnly.MinValue));

        @object[nameof(value.Gender)
                .ToCamelCase()] = new OpenApiInteger((int)value.Gender);

        @object[nameof(value.UniqueIdentificationNumber)
                .ToCamelCase()] = new OpenApiString(value.UniqueIdentificationNumber);

        @object[nameof(value.Email)
                .ToCamelCase()] = new OpenApiString(value.Email);

        @object[nameof(value.PhoneNumber)
                .ToCamelCase()] = new OpenApiString(value.PhoneNumber);

        @object[nameof(value.Address)
                .ToCamelCase()] = new OpenApiString(value.Address);

        @object[nameof(value.Role)
                .ToCamelCase()] = new OpenApiInteger((int)value.Role);

        @object[nameof(value.Activated)
                .ToCamelCase()] = new OpenApiBoolean(value.Activated);

        @object[nameof(value.CreatedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.CreatedAt);

        @object[nameof(value.ModifiedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.ModifiedAt);

        return @object;
    }
}
