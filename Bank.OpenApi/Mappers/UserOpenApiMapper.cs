using Bank.Application.Extensions;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.OpenApi.Extensions;

using Microsoft.OpenApi.Any;

namespace Bank.OpenApi.Mappers;

internal static class UserOpenApiMapper
{
    public static OpenApiObject ToOpenApiObject(this UserLoginRequest value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this UserLoginRequest value, OpenApiObject @object)
    {
        @object[nameof(value.Email)
                .ToCamelCase()] = new OpenApiString(value.Email);

        @object[nameof(value.Password)
                .ToCamelCase()] = new OpenApiString(value.Password);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this UserActivationRequest value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this UserActivationRequest value, OpenApiObject @object)
    {
        @object[nameof(value.Password)
                .ToCamelCase()] = new OpenApiString(value.Password);

        @object[nameof(value.ConfirmPassword)
                .ToCamelCase()] = new OpenApiString(value.ConfirmPassword);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this UserPasswordResetRequest value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this UserPasswordResetRequest value, OpenApiObject @object)
    {
        @object[nameof(value.Password)
                .ToCamelCase()] = new OpenApiString(value.Password);

        @object[nameof(value.ConfirmPassword)
                .ToCamelCase()] = new OpenApiString(value.ConfirmPassword);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this UserRequestPasswordResetRequest value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this UserRequestPasswordResetRequest value, OpenApiObject @object)
    {
        @object[nameof(value.Email)
                .ToCamelCase()] = new OpenApiString(value.Email);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this UserResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this UserResponse value, OpenApiObject @object)
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

    public static OpenApiObject ToOpenApiObject(this UserSimpleResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this UserSimpleResponse value, OpenApiObject @object)
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

        @object[nameof(value.CreatedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.CreatedAt);

        @object[nameof(value.ModifiedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.ModifiedAt);

        @object[nameof(value.Activated)
                .ToCamelCase()] = new OpenApiBoolean(value.Activated);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this UserLoginResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this UserLoginResponse value, OpenApiObject @object)
    {
        @object[nameof(value.Token)
                .ToCamelCase()] = new OpenApiString(value.Token);

        @object[nameof(value.User)
                .ToCamelCase()] = value.User?.ToOpenApiObject();

        return @object;
    }
}
