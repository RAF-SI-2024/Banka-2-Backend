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
    public static class Employee
    {
        public static readonly Guid Id        = Guid.Parse("ae45452a-81fa-413b-9a3f-4e044ff13939");
        public const           bool Activated = true;

        public static readonly EmployeeResponse Response = new()
                                                           {
                                                               Id                         = Id,
                                                               FirstName                  = Sample.Employee.CreateRequest.FirstName,
                                                               LastName                   = Sample.Employee.CreateRequest.LastName,
                                                               DateOfBirth                = Sample.Employee.CreateRequest.DateOfBirth,
                                                               Gender                     = Sample.Employee.CreateRequest.Gender,
                                                               UniqueIdentificationNumber = Sample.Employee.CreateRequest.UniqueIdentificationNumber,
                                                               Username                   = Sample.Employee.CreateRequest.Username,
                                                               Email                      = Sample.Employee.CreateRequest.Email,
                                                               PhoneNumber                = Sample.Employee.CreateRequest.PhoneNumber,
                                                               Address                    = Sample.Employee.CreateRequest.Address,
                                                               Role                       = Sample.Employee.CreateRequest.Role,
                                                               Department                 = Sample.Employee.CreateRequest.Department,
                                                               CreatedAt                  = DateTime.UtcNow,
                                                               ModifiedAt                 = DateTime.UtcNow,
                                                               Employed                   = Sample.Employee.CreateRequest.Employed,
                                                               Activated                  = Activated
                                                           };

        public static readonly EmployeeSimpleResponse SimpleResponse = new()
                                                                       {
                                                                           Id                         = Id,
                                                                           FirstName                  = Sample.Employee.CreateRequest.FirstName,
                                                                           LastName                   = Sample.Employee.CreateRequest.LastName,
                                                                           DateOfBirth                = Sample.Employee.CreateRequest.DateOfBirth,
                                                                           Gender                     = Sample.Employee.CreateRequest.Gender,
                                                                           UniqueIdentificationNumber = Sample.Employee.CreateRequest.UniqueIdentificationNumber,
                                                                           Username                   = Sample.Employee.CreateRequest.Username,
                                                                           Email                      = Sample.Employee.CreateRequest.Email,
                                                                           PhoneNumber                = Sample.Employee.CreateRequest.PhoneNumber,
                                                                           Address                    = Sample.Employee.CreateRequest.Address,
                                                                           Role                       = Sample.Employee.CreateRequest.Role,
                                                                           Department                 = Sample.Employee.CreateRequest.Department,
                                                                           CreatedAt                  = DateTime.UtcNow,
                                                                           ModifiedAt                 = DateTime.UtcNow,
                                                                           Employed                   = Sample.Employee.CreateRequest.Employed,
                                                                           Activated                  = Activated
                                                                       };
    }
}

public static partial class SwaggerSchemaFilter
{
    public static class Employee
    {
        public class CreateRequest() : SwaggerSchemaFilter<EmployeeCreateRequest>(Sample.Employee.CreateRequest)
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
                           [nameof(Example.Employed)
                            .ToCamelCase()] = new OpenApiBoolean(Example.Employed)
                       };
            }
        }

        public class UpdateRequest() : SwaggerSchemaFilter<EmployeeUpdateRequest>(Sample.Employee.UpdateRequest)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.FirstName)
                            .ToCamelCase()] = new OpenApiString(Example.FirstName),
                           [nameof(Example.LastName)
                            .ToCamelCase()] = new OpenApiString(Example.LastName),
                           [nameof(Example.Username)
                            .ToCamelCase()] = new OpenApiString(Example.Username),
                           [nameof(Example.PhoneNumber)
                            .ToCamelCase()] = new OpenApiString(Example.PhoneNumber),
                           [nameof(Example.Address)
                            .ToCamelCase()] = new OpenApiString(Example.Address),
                           [nameof(Example.Role)
                            .ToCamelCase()] = new OpenApiInteger((int)Example.Role),
                           [nameof(Example.Department)
                            .ToCamelCase()] = new OpenApiString(Example.Department),
                           [nameof(Example.Employed)
                            .ToCamelCase()] = new OpenApiBoolean(Example.Employed),
                           [nameof(Example.Activated)
                            .ToCamelCase()] = new OpenApiBoolean(false)
                       };
            }
        }

        public class Response() : SwaggerSchemaFilter<EmployeeResponse>(SchemeFilters.Example.Employee.Response)
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
                           [nameof(Example.Employed)
                            .ToCamelCase()] = new OpenApiBoolean(Example.Employed),
                           [nameof(Example.Activated)
                            .ToCamelCase()] = new OpenApiBoolean(Example.Activated)
                       };
            }
        }

        public class SimpleResponse() : SwaggerSchemaFilter<EmployeeSimpleResponse>(SchemeFilters.Example.Employee.SimpleResponse)
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
                           [nameof(Example.Employed)
                            .ToCamelCase()] = new OpenApiBoolean(Example.Employed),
                           [nameof(Example.Activated)
                            .ToCamelCase()] = new OpenApiBoolean(Example.Activated)
                       };
            }
        }
    }
}
