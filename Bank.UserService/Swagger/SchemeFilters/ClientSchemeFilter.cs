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
    public static class Client
    {
        public static readonly Guid Id = Guid.Parse("f39d319e-db3e-4af5-bada-6bcb908b29e3");

        public static readonly ClientResponse Response = new()
                                                         {
                                                             Id                         = Id,
                                                             FirstName                  = Sample.Client.CreateRequest.FirstName,
                                                             LastName                   = Sample.Client.CreateRequest.LastName,
                                                             DateOfBirth                = Sample.Client.CreateRequest.DateOfBirth,
                                                             Gender                     = Sample.Client.CreateRequest.Gender,
                                                             UniqueIdentificationNumber = Sample.Client.CreateRequest.UniqueIdentificationNumber,
                                                             Email                      = Sample.Client.CreateRequest.Email,
                                                             PhoneNumber                = Sample.Client.CreateRequest.PhoneNumber,
                                                             Address                    = Sample.Client.CreateRequest.Address,
                                                             Role                       = Role.Client,
                                                             Accounts                   = [],
                                                             CreatedAt                  = DateTime.UtcNow,
                                                             ModifiedAt                 = DateTime.UtcNow,
                                                             Activated                  = Sample.Client.UpdateRequest.Activated
                                                         };

        public static readonly ClientSimpleResponse SimpleResponse = new()
                                                                     {
                                                                         Id                         = Id,
                                                                         FirstName                  = Sample.Client.CreateRequest.FirstName,
                                                                         LastName                   = Sample.Client.CreateRequest.LastName,
                                                                         DateOfBirth                = Sample.Client.CreateRequest.DateOfBirth,
                                                                         Gender                     = Sample.Client.CreateRequest.Gender,
                                                                         UniqueIdentificationNumber = Sample.Client.CreateRequest.UniqueIdentificationNumber,
                                                                         Email                      = Sample.Client.CreateRequest.Email,
                                                                         PhoneNumber                = Sample.Client.CreateRequest.PhoneNumber,
                                                                         Address                    = Sample.Client.CreateRequest.Address,
                                                                         Role                       = Role.Client,
                                                                         CreatedAt                  = DateTime.UtcNow,
                                                                         ModifiedAt                 = DateTime.UtcNow,
                                                                         Activated                  = Sample.Client.UpdateRequest.Activated
                                                                     };
    }
}

public static partial class SwaggerSchemaFilter
{
    public static class Client
    {
        public class CreateRequest() : SwaggerSchemaFilter<ClientCreateRequest>(Sample.Client.CreateRequest)
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

        public class UpdateRequest() : SwaggerSchemaFilter<ClientUpdateRequest>(Sample.Client.UpdateRequest)
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
