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
    public static class TransactionTemplate
    {
        public static readonly Guid Id = Guid.Parse("44ca0bef-1783-40b9-9b41-51e196d4f6b3");

        public static readonly TransactionTemplateResponse Response = new()
                                                                      {
                                                                          Id            = Id,
                                                                          Client        = null!,
                                                                          Name          = Sample.TransactionTemplate.CreateRequest.Name,
                                                                          AccountNumber = Sample.TransactionTemplate.CreateRequest.AccountNumber,
                                                                          Deleted       = Sample.TransactionTemplate.UpdateRequest.Deleted,
                                                                          CreatedAt     = DateTime.UtcNow,
                                                                          ModifiedAt    = DateTime.UtcNow
                                                                      };

        public static readonly TransactionTemplateSimpleResponse SimpleResponse = new()
                                                                                  {
                                                                                      Id            = Id,
                                                                                      Name          = Sample.TransactionTemplate.CreateRequest.Name,
                                                                                      AccountNumber = Sample.TransactionTemplate.CreateRequest.AccountNumber,
                                                                                      Deleted       = Sample.TransactionTemplate.UpdateRequest.Deleted,
                                                                                  };
    }
}

public static partial class SwaggerSchemaFilter
{
    public static class TransactionTemplate
    {
        public class CreateRequest() : SwaggerSchemaFilter<TransactionTemplateCreateRequest>(Sample.TransactionTemplate.CreateRequest)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.Name)
                            .ToCamelCase()] = new OpenApiString(Example.Name),
                           [nameof(Example.AccountNumber)
                            .ToCamelCase()] = new OpenApiString(Example.AccountNumber)
                       };
            }
        }

        public class UpdateRequest() : SwaggerSchemaFilter<TransactionTemplateUpdateRequest>(Sample.TransactionTemplate.UpdateRequest)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.Name)
                            .ToCamelCase()] = new OpenApiString(Example.Name),
                           [nameof(Example.AccountNumber)
                            .ToCamelCase()] = new OpenApiString(Example.AccountNumber),
                           [nameof(Example.Deleted)
                            .ToCamelCase()] = new OpenApiBoolean(Example.Deleted)
                       };
            }
        }

        public class Response() : SwaggerSchemaFilter<TransactionTemplateResponse>(SchemeFilters.Example.TransactionTemplate.Response)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                var client = context.SchemaRepository.Schemas[nameof(ClientSimpleResponse)].Example;

                return new OpenApiObject()
                       {
                           [nameof(Example.Id)
                            .ToCamelCase()] = new OpenApiString(Example.Id.ToString()),
                           [nameof(Example.Client)
                            .ToCamelCase()] = client,
                           [nameof(Example.Name)
                            .ToCamelCase()] = new OpenApiString(Example.Name),
                           [nameof(Example.AccountNumber)
                            .ToCamelCase()] = new OpenApiString(Example.AccountNumber),
                           [nameof(Example.Deleted)
                            .ToCamelCase()] = new OpenApiBoolean(Example.Deleted),
                           [nameof(Example.CreatedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.CreatedAt),
                           [nameof(Example.ModifiedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.ModifiedAt)
                       };
            }
        }

        public class SimpleResponse() : SwaggerSchemaFilter<TransactionTemplateSimpleResponse>(SchemeFilters.Example.TransactionTemplate.SimpleResponse)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.Id)
                            .ToCamelCase()] = new OpenApiString(Example.Id.ToString()),
                           [nameof(Example.Name)
                            .ToCamelCase()] = new OpenApiString(Example.Name),
                           [nameof(Example.AccountNumber)
                            .ToCamelCase()] = new OpenApiString(Example.AccountNumber),
                           [nameof(Example.Deleted)
                            .ToCamelCase()] = new OpenApiBoolean(Example.Deleted)
                       };
            }
        }
    }
}
