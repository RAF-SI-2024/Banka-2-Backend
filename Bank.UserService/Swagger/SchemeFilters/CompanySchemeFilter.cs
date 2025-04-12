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
    public static class Company
    {
        public static readonly Guid Id = Guid.Parse("8a7b9f2d-4c8a-4b1d-b1cd-987654321abc");

        public static readonly CompanyResponse Response = new()
                                                          {
                                                              Id                      = Id,
                                                              Name                    = Sample.Company.CreateRequest.Name,
                                                              RegistrationNumber      = Sample.Company.CreateRequest.RegistrationNumber,
                                                              TaxIdentificationNumber = Sample.Company.CreateRequest.TaxIdentificationNumber,
                                                              ActivityCode            = Sample.Company.CreateRequest.ActivityCode,
                                                              Address                 = Sample.Company.CreateRequest.Address,
                                                              MajorityOwner           = null!
                                                          };

        public static readonly CompanySimpleResponse SimpleResponse = new()
                                                                      {
                                                                          Id                      = Id,
                                                                          Name                    = Sample.Company.CreateRequest.Name,
                                                                          RegistrationNumber      = Sample.Company.CreateRequest.RegistrationNumber,
                                                                          TaxIdentificationNumber = Sample.Company.CreateRequest.TaxIdentificationNumber,
                                                                          ActivityCode            = Sample.Company.CreateRequest.ActivityCode,
                                                                          Address                 = Sample.Company.CreateRequest.Address
                                                                      };
    }
}

public static partial class SwaggerSchemaFilter
{
    public static class Company
    {
        public class CreateRequest() : SwaggerSchemaFilter<CompanyCreateRequest>(Sample.Company.CreateRequest)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.Name)
                            .ToCamelCase()] = new OpenApiString(Example.Name),
                           [nameof(Example.RegistrationNumber)
                            .ToCamelCase()] = new OpenApiString(Example.RegistrationNumber),
                           [nameof(Example.TaxIdentificationNumber)
                            .ToCamelCase()] = new OpenApiString(Example.TaxIdentificationNumber),
                           [nameof(Example.ActivityCode)
                            .ToCamelCase()] = new OpenApiString(Example.ActivityCode),
                           [nameof(Example.Address)
                            .ToCamelCase()] = new OpenApiString(Example.Address),
                           [nameof(Example.MajorityOwnerId)
                            .ToCamelCase()] = new OpenApiString(Example.MajorityOwnerId.ToString())
                       };
            }
        }

        public class UpdateRequest() : SwaggerSchemaFilter<CompanyUpdateRequest>(Sample.Company.UpdateRequest)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.Name)
                            .ToCamelCase()] = new OpenApiString(Example.Name),
                           [nameof(Example.ActivityCode)
                            .ToCamelCase()] = new OpenApiString(Example.ActivityCode),
                           [nameof(Example.Address)
                            .ToCamelCase()] = new OpenApiString(Example.Address),
                           [nameof(Example.MajorityOwnerId)
                            .ToCamelCase()] = new OpenApiString(Example.MajorityOwnerId.ToString())
                       };
            }
        }

        public class Response() : SwaggerSchemaFilter<CompanyResponse>(SchemeFilters.Example.Company.Response)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                var user = context.SchemaRepository.Schemas[nameof(UserSimpleResponse)].Example;

                return new OpenApiObject()
                       {
                           [nameof(Example.Id)
                            .ToCamelCase()] = new OpenApiString(Example.Id.ToString()),
                           [nameof(Example.Name)
                            .ToCamelCase()] = new OpenApiString(Example.Name),
                           [nameof(Example.RegistrationNumber)
                            .ToCamelCase()] = new OpenApiString(Example.RegistrationNumber),
                           [nameof(Example.TaxIdentificationNumber)
                            .ToCamelCase()] = new OpenApiString(Example.TaxIdentificationNumber),
                           [nameof(Example.ActivityCode)
                            .ToCamelCase()] = new OpenApiString(Example.ActivityCode),
                           [nameof(Example.Address)
                            .ToCamelCase()] = new OpenApiString(Example.Address),
                           [nameof(Example.MajorityOwner)
                            .ToCamelCase()] = user
                       };
            }
        }

        public class SimpleResponse() : SwaggerSchemaFilter<CompanySimpleResponse>(SchemeFilters.Example.Company.SimpleResponse)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.Id)
                            .ToCamelCase()] = new OpenApiString(Example.Id.ToString()),
                           [nameof(Example.Name)
                            .ToCamelCase()] = new OpenApiString(Example.Name),
                           [nameof(Example.RegistrationNumber)
                            .ToCamelCase()] = new OpenApiString(Example.RegistrationNumber),
                           [nameof(Example.TaxIdentificationNumber)
                            .ToCamelCase()] = new OpenApiString(Example.TaxIdentificationNumber),
                           [nameof(Example.ActivityCode)
                            .ToCamelCase()] = new OpenApiString(Example.ActivityCode),
                           [nameof(Example.Address)
                            .ToCamelCase()] = new OpenApiString(Example.Address)
                       };
            }
        }
    }
}
