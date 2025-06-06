using Bank.Application.Extensions;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.OpenApi.Core;
using Bank.OpenApi.Extensions;
using Bank.OpenApi.Mappers;

using Microsoft.OpenApi.Any;

namespace Bank.OpenApi.Schemas.Swagger;

internal static partial class Schema
{
    internal static partial class Swagger
    {
        internal static class TransactionTemplate
        {
            internal class CreateRequest(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<TransactionTemplateCreateRequest>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject();
                }
            }

            internal class UpdateRequest(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<TransactionTemplateUpdateRequest>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject();
                }
            }

            internal class Response(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<TransactionTemplateResponse>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject()
                                  .SetProperty(nameof(Example.Client)
                                               .ToCamelCase(), GetSchemaExample<ClientSimpleResponse>());
                }
            }

            internal class SimpleResponse(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<TransactionTemplateSimpleResponse>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject();
                }
            }
        }
    }
}
