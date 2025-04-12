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
    public static class AccountType
    {
        public static readonly AccountTypeResponse Response = new()
                                                              {
                                                                  Id         = Guid.Parse("c3f7a5d4-e6b8-4d2a-a678-123456789abc"),
                                                                  Name       = Sample.AccountType.CreateRequest.Name,
                                                                  Code       = Sample.AccountType.CreateRequest.Code,
                                                                  CreatedAt  = DateTime.UtcNow,
                                                                  ModifiedAt = DateTime.UtcNow
                                                              };
    }
}

public static partial class SwaggerSchemaFilter
{
    public static class AccountType
    {
        public class CreateRequest() : SwaggerSchemaFilter<AccountTypeCreateRequest>(Sample.AccountType.CreateRequest)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.Name)
                            .ToCamelCase()] = new OpenApiString(Example.Name),
                           [nameof(Example.Code)
                            .ToCamelCase()] = new OpenApiString(Example.Code)
                       };
            }
        }

        public class UpdateRequest() : SwaggerSchemaFilter<AccountTypeUpdateRequest>(Sample.AccountType.UpdateRequest)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.Name)
                            .ToCamelCase()] = new OpenApiString(Example.Name),
                           [nameof(Example.Code)
                            .ToCamelCase()] = new OpenApiString(Example.Code)
                       };
            }
        }

        public class Response() : SwaggerSchemaFilter<AccountTypeResponse>(SchemeFilters.Example.AccountType.Response)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.Id)
                            .ToCamelCase()] = new OpenApiString(Example.Id.ToString()),
                           [nameof(Example.Name)
                            .ToCamelCase()] = new OpenApiString(Example.Name),
                           [nameof(Example.Code)
                            .ToCamelCase()] = new OpenApiString(Example.Code),
                           [nameof(Example.CreatedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.CreatedAt),
                           [nameof(Example.ModifiedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.ModifiedAt)
                       };
            }
        }
    }
}
