using Bank.Application.Domain;
using Bank.Application.Extensions;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Database.Sample;

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bank.UserService.Swagger.SchemeFilters;

file static class Example
{
    public static class User
    {
        public static readonly Guid     Id                         = Guid.Parse("73b8f8ee-ff51-4247-b65b-52b8b9a494e5");
        public const           string   FirstName                  = "Marko";
        public const           string   LastName                   = "Petrović";
        public static readonly DateOnly DateOfBirth                = new(1995, 7, 21);
        public const           Gender   Gender                     = Bank.Application.Domain.Gender.Male;
        public const           string   UniqueIdentificationNumber = "2107953710020";
        public const           string   Username                   = "markop";
        public const           string   PhoneNumber                = "+381641234567";
        public const           string   Address                    = "Kraljice Natalije 45";
        public const           Role     Role                       = Bank.Application.Domain.Role.Admin;
        public const           string   Department                 = "IT department";
        public static readonly DateTime CreatedAt                  = new(2024, 10, 15, 9, 30, 0);
        public static readonly DateTime ModifiedAt                 = new(2025, 2, 28, 12, 45, 0);
        public const           bool     Activated                  = true;

        public static readonly UserResponse Response = new()
                                                       {
                                                           Id                         = Id,
                                                           FirstName                  = FirstName,
                                                           LastName                   = LastName,
                                                           DateOfBirth                = DateOfBirth,
                                                           Gender                     = Gender,
                                                           UniqueIdentificationNumber = UniqueIdentificationNumber,
                                                           Username                   = Username,
                                                           Email                      = Sample.User.LoginRequest.Email,
                                                           PhoneNumber                = PhoneNumber,
                                                           Address                    = Address,
                                                           Role                       = Role,
                                                           Department                 = Department,
                                                           Accounts                   = [],
                                                           CreatedAt                  = CreatedAt,
                                                           ModifiedAt                 = ModifiedAt,
                                                           Activated                  = Activated
                                                       };

        public static readonly UserSimpleResponse SimpleResponse = new()
                                                                   {
                                                                       Id                         = Id,
                                                                       FirstName                  = FirstName,
                                                                       LastName                   = LastName,
                                                                       DateOfBirth                = DateOfBirth,
                                                                       Gender                     = Gender,
                                                                       UniqueIdentificationNumber = UniqueIdentificationNumber,
                                                                       Username                   = Username,
                                                                       Email                      = Sample.User.LoginRequest.Email,
                                                                       PhoneNumber                = PhoneNumber,
                                                                       Address                    = Address,
                                                                       Role                       = Role,
                                                                       Department                 = Department,
                                                                       CreatedAt                  = CreatedAt,
                                                                       ModifiedAt                 = ModifiedAt,
                                                                       Activated                  = Activated
                                                                   };

        public static readonly UserLoginResponse LoginResponse = new()
                                                                 {
                                                                     Token =
                                                                     "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE3NDA3ODIyNjAsImlkIjoiNjFlMTY1OTMtM2EyNC00ZDVkLTg3MmMtMTdlMjJhMzQxZDMzIiwicm9sZSI6IkFkbWluIiwiaWF0IjoxNzQwNzgwNDYwLCJuYmYiOjE3NDA3ODA0NjB9.3DsroWriDMpHvuBNOSAiFq8gxdo4TEkc9WK1r2f0Ou0",
                                                                     User = null!
                                                                 };
    }
}

public static partial class SwaggerSchemaFilter
{
    public static class User
    {
        public class LoginRequest() : SwaggerSchemaFilter<UserLoginRequest>(Sample.User.LoginRequest)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.Email)
                            .ToCamelCase()] = new OpenApiString(Example.Email),
                           [nameof(Example.Password)
                            .ToCamelCase()] = new OpenApiString(Example.Password)
                       };
            }
        }

        public class ActivationRequest() : SwaggerSchemaFilter<UserActivationRequest>(Sample.User.ActivationRequest)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.Password)
                            .ToCamelCase()] = new OpenApiString(Example.Password),
                           [nameof(Example.ConfirmPassword)
                            .ToCamelCase()] = new OpenApiString(Example.ConfirmPassword)
                       };
            }
        }

        public class RequestPasswordResetRequest() : SwaggerSchemaFilter<UserRequestPasswordResetRequest>(Sample.User.RequestPasswordResetRequest)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.Email)] = new OpenApiString(Example.Email),
                       };
            }
        }

        public class PasswordResetRequest() : SwaggerSchemaFilter<UserPasswordResetRequest>(Sample.User.PasswordResetRequest)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.Password)
                            .ToCamelCase()] = new OpenApiString(Example.Password),
                           [nameof(Example.ConfirmPassword)
                            .ToCamelCase()] = new OpenApiString(Example.ConfirmPassword)
                       };
            }
        }

        public class Response() : SwaggerSchemaFilter<UserResponse>(SchemeFilters.Example.User.Response)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                var accounts = new OpenApiArray { context.SchemaRepository.Schemas[nameof(AccountSimpleResponse)].Example };

                return new OpenApiObject()
                       {
                           [nameof(Example.Id)
                            .ToCamelCase()] = new OpenApiString(Example.Id.ToString()),
                           [nameof(Example.FirstName)
                            .ToCamelCase()] = new OpenApiString(Example.FirstName),
                           [nameof(Example.LastName)
                            .ToCamelCase()] = new OpenApiString(Example.LastName),
                           [nameof(Example.DateOfBirth)
                            .ToCamelCase()] = new OpenApiDate(new DateTime(Example.DateOfBirth, TimeOnly.MinValue)),
                           [nameof(Example.Gender)
                            .ToCamelCase()] = new OpenApiInteger((int)Example.Gender),
                           [nameof(Example.UniqueIdentificationNumber)
                            .ToCamelCase()] = new OpenApiString(Example.UniqueIdentificationNumber),
                           [nameof(Example.Username)
                            .ToCamelCase()] = new OpenApiString(Example.Username),
                           [nameof(Example.Email)
                            .ToCamelCase()] = new OpenApiString(Example.Email),
                           [nameof(Example.PhoneNumber)
                            .ToCamelCase()] = new OpenApiString(Example.PhoneNumber),
                           [nameof(Example.Address)
                            .ToCamelCase()] = new OpenApiString(Example.Address),
                           [nameof(Example.Role)
                            .ToCamelCase()] = new OpenApiInteger((int)Example.Role),
                           [nameof(Example.Department)
                            .ToCamelCase()] = new OpenApiString(Example.Department),
                           [nameof(Example.Accounts)
                            .ToCamelCase()] = accounts,
                           [nameof(Example.CreatedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.CreatedAt),
                           [nameof(Example.ModifiedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.ModifiedAt),
                           [nameof(Example.Activated)
                            .ToCamelCase()] = new OpenApiBoolean(Example.Activated)
                       };
            }
        }

        public class SimpleResponse() : SwaggerSchemaFilter<UserSimpleResponse>(SchemeFilters.Example.User.SimpleResponse)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.Id)
                            .ToCamelCase()] = new OpenApiString(Example.Id.ToString()),
                           [nameof(Example.FirstName)
                            .ToCamelCase()] = new OpenApiString(Example.FirstName),
                           [nameof(Example.LastName)
                            .ToCamelCase()] = new OpenApiString(Example.LastName),
                           [nameof(Example.DateOfBirth)
                            .ToCamelCase()] = new OpenApiDate(new DateTime(Example.DateOfBirth, TimeOnly.MinValue)),
                           [nameof(Example.Gender)
                            .ToCamelCase()] = new OpenApiInteger((int)Example.Gender),
                           [nameof(Example.UniqueIdentificationNumber)
                            .ToCamelCase()] = new OpenApiString(Example.UniqueIdentificationNumber),
                           [nameof(Example.Username)
                            .ToCamelCase()] = new OpenApiString(Example.Username),
                           [nameof(Example.Email)
                            .ToCamelCase()] = new OpenApiString(Example.Email),
                           [nameof(Example.PhoneNumber)
                            .ToCamelCase()] = new OpenApiString(Example.PhoneNumber),
                           [nameof(Example.Address)
                            .ToCamelCase()] = new OpenApiString(Example.Address),
                           [nameof(Example.Role)
                            .ToCamelCase()] = new OpenApiInteger((int)Example.Role),
                           [nameof(Example.Department)
                            .ToCamelCase()] = new OpenApiString(Example.Department),
                           [nameof(Example.CreatedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.CreatedAt),
                           [nameof(Example.ModifiedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.ModifiedAt),
                           [nameof(Example.Activated)
                            .ToCamelCase()] = new OpenApiBoolean(Example.Activated)
                       };
            }
        }

        public class LoginResponse() : SwaggerSchemaFilter<UserLoginResponse>(SchemeFilters.Example.User.LoginResponse)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                var user = context.SchemaRepository.Schemas[nameof(UserResponse)].Example;

                return new OpenApiObject()
                       {
                           [nameof(Example.Token)
                            .ToCamelCase()] = new OpenApiString(Example.Token),
                           [nameof(Example.User)
                            .ToCamelCase()] = user
                       };
            }
        }
    }
}
