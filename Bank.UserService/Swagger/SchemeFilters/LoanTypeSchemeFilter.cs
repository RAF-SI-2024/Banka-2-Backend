using Bank.Application.Extensions;
using Bank.Application.Requests;
using Bank.Application.Responses;

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bank.UserService.Swagger.SchemeFilters;

file static class Example
{
    public static class LoanType
    {
        public static readonly Guid    Id     = Guid.Parse("74358a7f-2b43-4839-a1f8-f48b7fc952e5");
        public const           string  Name   = "Lični kredit";
        public const           decimal Margin = 3.5m;

        public static readonly LoanTypeRequest Request = new()
                                                         {
                                                             Name   = Name,
                                                             Margin = Margin
                                                         };

        public static readonly LoanTypeResponse Response = new()
                                                           {
                                                               Id     = Id,
                                                               Name   = Name,
                                                               Margin = Margin
                                                           };
    }
}

public static partial class SwaggerSchemaFilter
{
    public static class LoanType
    {
        public class Request() : SwaggerSchemaFilter<LoanTypeRequest>(SchemeFilters.Example.LoanType.Request)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.Name)
                            .ToCamelCase()] = new OpenApiString(Example.Name),
                           [nameof(Example.Margin)
                            .ToCamelCase()] = new OpenApiDouble((double)Example.Margin)
                       };
            }
        }

        public class Response() : SwaggerSchemaFilter<LoanTypeResponse>(SchemeFilters.Example.LoanType.Response)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.Id)
                            .ToCamelCase()] = new OpenApiString(Example.Id.ToString()),
                           [nameof(Example.Name)
                            .ToCamelCase()] = new OpenApiString(Example.Name),
                           [nameof(Example.Margin)
                            .ToCamelCase()] = new OpenApiDouble((double)Example.Margin)
                       };
            }
        }
    }
}
