using Bank.Application.Domain;
using Bank.Application.Extensions;
using Bank.Application.Requests;
using Bank.Application.Responses;

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bank.UserService.Swagger.SchemeFilters;

file static class Example
{
    public static class Client
    {
        public static readonly Guid     Id                         = Guid.Parse("f39d319e-db3e-4af5-bada-6bcb908b29e3");
        public const           string   Email                      = "aleksandar.ivanovic@gmail.com";
        public const           string   FirstName                  = "Aleksandar";
        public const           string   LastName                   = "Ivanović";
        public static readonly DateOnly DateOfBirth                = new(1995, 7, 12);
        public const           Gender   Gender                     = Bank.Application.Domain.Gender.Male;
        public const           string   UniqueIdentificationNumber = "1207995710029";
        public const           string   PhoneNumber                = "+381698812321";
        public const           string   Address                    = "Kralja Petra 12";
        public const           Role     Role                       = Bank.Application.Domain.Role.Client;
        public static readonly DateTime CreatedAt                  = new(2025, 1, 22, 13, 15, 12);
        public static readonly DateTime ModifiedAt                 = new(2025, 1, 29, 10, 24, 13);
        public const           bool     Activated                  = true;

        public static readonly ClientCreateRequest CreateRequest = new()
                                                                   {
                                                                       FirstName                  = FirstName,
                                                                       LastName                   = LastName,
                                                                       DateOfBirth                = DateOfBirth,
                                                                       Gender                     = Gender,
                                                                       UniqueIdentificationNumber = UniqueIdentificationNumber,
                                                                       Email                      = Email,
                                                                       PhoneNumber                = PhoneNumber,
                                                                       Address                    = Address
                                                                   };

        public static readonly ClientUpdateRequest UpdateRequest = new()
                                                                   {
                                                                       FirstName   = FirstName,
                                                                       LastName    = LastName,
                                                                       PhoneNumber = PhoneNumber,
                                                                       Address     = Address,
                                                                       Activated   = Activated
                                                                   };

        public static readonly ClientResponse Response = new()
                                                         {
                                                             Id                         = Id,
                                                             FirstName                  = FirstName,
                                                             LastName                   = LastName,
                                                             DateOfBirth                = DateOfBirth,
                                                             Gender                     = Gender,
                                                             UniqueIdentificationNumber = UniqueIdentificationNumber,
                                                             Email                      = Email,
                                                             PhoneNumber                = PhoneNumber,
                                                             Address                    = Address,
                                                             Role                       = Role,
                                                             Accounts                   = [],
                                                             CreatedAt                  = CreatedAt,
                                                             ModifiedAt                 = ModifiedAt,
                                                             Activated                  = Activated
                                                         };

        public static readonly ClientSimpleResponse SimpleResponse = new()
                                                                     {
                                                                         Id                         = Id,
                                                                         FirstName                  = FirstName,
                                                                         LastName                   = LastName,
                                                                         DateOfBirth                = DateOfBirth,
                                                                         Gender                     = Gender,
                                                                         UniqueIdentificationNumber = UniqueIdentificationNumber,
                                                                         Email                      = Email,
                                                                         PhoneNumber                = PhoneNumber,
                                                                         Address                    = Address,
                                                                         Role                       = Role,
                                                                         CreatedAt                  = CreatedAt,
                                                                         ModifiedAt                 = ModifiedAt,
                                                                         Activated                  = Activated
                                                                     };
    }
}

public static partial class SwaggerSchemaFilter
{
    public static class Client
    {
        public class CreateRequest() : SwaggerSchemaFilter<ClientCreateRequest>(SchemeFilters.Example.Client.CreateRequest)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
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
                           [nameof(Example.Email)
                            .ToCamelCase()] = new OpenApiString(Example.Email),
                           [nameof(Example.PhoneNumber)
                            .ToCamelCase()] = new OpenApiString(Example.PhoneNumber),
                           [nameof(Example.Address)
                            .ToCamelCase()] = new OpenApiString(Example.Address)
                       };
            }
        }

        public class UpdateRequest() : SwaggerSchemaFilter<ClientUpdateRequest>(SchemeFilters.Example.Client.UpdateRequest)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.FirstName)
                            .ToCamelCase()] = new OpenApiString(Example.FirstName),
                           [nameof(Example.LastName)
                            .ToCamelCase()] = new OpenApiString(Example.LastName),
                           [nameof(Example.PhoneNumber)
                            .ToCamelCase()] = new OpenApiString(Example.PhoneNumber),
                           [nameof(Example.Address)
                            .ToCamelCase()] = new OpenApiString(Example.Address),
                           [nameof(Example.Activated)
                            .ToCamelCase()] = new OpenApiBoolean(Example.Activated)
                       };
            }
        }

        public class Response() : SwaggerSchemaFilter<ClientResponse>(SchemeFilters.Example.Client.Response)
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
                           [nameof(Example.Email)
                            .ToCamelCase()] = new OpenApiString(Example.Email),
                           [nameof(Example.PhoneNumber)
                            .ToCamelCase()] = new OpenApiString(Example.PhoneNumber),
                           [nameof(Example.Address)
                            .ToCamelCase()] = new OpenApiString(Example.Address),
                           [nameof(Example.Role)
                            .ToCamelCase()] = new OpenApiInteger((int)Example.Role),
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

        public class SimpleResponse() : SwaggerSchemaFilter<ClientSimpleResponse>(SchemeFilters.Example.Client.SimpleResponse)
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
                           [nameof(Example.Email)
                            .ToCamelCase()] = new OpenApiString(Example.Email),
                           [nameof(Example.PhoneNumber)
                            .ToCamelCase()] = new OpenApiString(Example.PhoneNumber),
                           [nameof(Example.Address)
                            .ToCamelCase()] = new OpenApiString(Example.Address),
                           [nameof(Example.Role)
                            .ToCamelCase()] = new OpenApiInteger((int)Example.Role),
                           [nameof(Example.CreatedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.CreatedAt),
                           [nameof(Example.ModifiedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.ModifiedAt),
                           [nameof(Example.Activated)
                            .ToCamelCase()] = new OpenApiBoolean(Example.Activated)
                       };
            }
        }
    }
}
