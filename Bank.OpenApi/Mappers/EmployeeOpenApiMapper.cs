using Bank.Application.Extensions;
using Bank.Application.Requests;
using Bank.Application.Responses;

using Microsoft.OpenApi.Any;

namespace Bank.OpenApi.Mappers;

internal static class EmployeeOpenApiMapper
{
    public static OpenApiObject ToOpenApiObject(this EmployeeCreateRequest value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this EmployeeCreateRequest value, OpenApiObject @object)
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

        @object[nameof(value.Username)
                .ToCamelCase()] = new OpenApiString(value.Username);

        @object[nameof(value.Email)
                .ToCamelCase()] = new OpenApiString(value.Email);

        @object[nameof(value.PhoneNumber)
                .ToCamelCase()] = new OpenApiString(value.PhoneNumber);

        @object[nameof(value.Address)
                .ToCamelCase()] = new OpenApiString(value.Address);

        @object[nameof(value.Role)
                .ToCamelCase()] = new OpenApiInteger((int)value.Role);

        @object[nameof(value.Department)
                .ToCamelCase()] = new OpenApiString(value.Department);

        @object[nameof(value.Employed)
                .ToCamelCase()] = new OpenApiBoolean(value.Employed);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this EmployeeUpdateRequest value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this EmployeeUpdateRequest value, OpenApiObject @object)
    {
        @object[nameof(value.FirstName)
                .ToCamelCase()] = new OpenApiString(value.FirstName);

        @object[nameof(value.LastName)
                .ToCamelCase()] = new OpenApiString(value.LastName);

        @object[nameof(value.Username)
                .ToCamelCase()] = new OpenApiString(value.Username);

        @object[nameof(value.PhoneNumber)
                .ToCamelCase()] = new OpenApiString(value.PhoneNumber);

        @object[nameof(value.Address)
                .ToCamelCase()] = new OpenApiString(value.Address);

        @object[nameof(value.Role)
                .ToCamelCase()] = new OpenApiInteger((int)value.Role);

        @object[nameof(value.Department)
                .ToCamelCase()] = new OpenApiString(value.Department);

        @object[nameof(value.Employed)
                .ToCamelCase()] = new OpenApiBoolean(value.Employed);

        @object[nameof(value.Activated)
                .ToCamelCase()] = new OpenApiBoolean(value.Activated);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this EmployeeResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this EmployeeResponse value, OpenApiObject @object)
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

        @object[nameof(value.Username)
                .ToCamelCase()] = new OpenApiString(value.Username);

        @object[nameof(value.Email)
                .ToCamelCase()] = new OpenApiString(value.Email);

        @object[nameof(value.PhoneNumber)
                .ToCamelCase()] = new OpenApiString(value.PhoneNumber);

        @object[nameof(value.Address)
                .ToCamelCase()] = new OpenApiString(value.Address);

        @object[nameof(value.Role)
                .ToCamelCase()] = new OpenApiInteger((int)value.Role);

        @object[nameof(value.Department)
                .ToCamelCase()] = new OpenApiString(value.Department);

        @object[nameof(value.Employed)
                .ToCamelCase()] = new OpenApiBoolean(value.Employed);

        @object[nameof(value.Employed)
                .ToCamelCase()] = new OpenApiBoolean(value.Employed);

        @object[nameof(value.Activated)
                .ToCamelCase()] = new OpenApiBoolean(value.Activated);

        @object[nameof(value.CreatedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.CreatedAt);

        @object[nameof(value.ModifiedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.ModifiedAt);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this EmployeeSimpleResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this EmployeeSimpleResponse value, OpenApiObject @object)
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

        @object[nameof(value.Username)
                .ToCamelCase()] = new OpenApiString(value.Username);

        @object[nameof(value.Email)
                .ToCamelCase()] = new OpenApiString(value.Email);

        @object[nameof(value.PhoneNumber)
                .ToCamelCase()] = new OpenApiString(value.PhoneNumber);

        @object[nameof(value.Address)
                .ToCamelCase()] = new OpenApiString(value.Address);

        @object[nameof(value.Role)
                .ToCamelCase()] = new OpenApiInteger((int)value.Role);

        @object[nameof(value.Department)
                .ToCamelCase()] = new OpenApiString(value.Department);

        @object[nameof(value.Employed)
                .ToCamelCase()] = new OpenApiBoolean(value.Employed);

        @object[nameof(value.Employed)
                .ToCamelCase()] = new OpenApiBoolean(value.Employed);

        @object[nameof(value.Activated)
                .ToCamelCase()] = new OpenApiBoolean(value.Activated);

        @object[nameof(value.CreatedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.CreatedAt);

        @object[nameof(value.ModifiedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.ModifiedAt);

        return @object;
    }
}
