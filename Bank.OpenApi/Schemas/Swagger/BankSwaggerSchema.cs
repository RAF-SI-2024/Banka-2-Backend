using Bank.Application.Responses;
using Bank.OpenApi.Core;
using Bank.OpenApi.Mappers;

using Microsoft.OpenApi.Any;

namespace Bank.OpenApi.Schemas.Swagger;

internal static partial class Schema
{
    internal static partial class Swagger
    {
        internal static class Bank
        {
            internal class Response(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<BankResponse>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject();
                }
            }
        }
    }
}
