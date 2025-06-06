using Bank.Application.Responses;
using Bank.OpenApi.Core;
using Bank.OpenApi.Mappers;

using Microsoft.OpenApi.Any;

namespace Bank.OpenApi.Schemas.Swagger;

internal static partial class Schema
{
    internal static partial class Swagger
    {
        internal static class Quote
        {
            internal class SimpleResponse(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<QuoteSimpleResponse>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject();
                }
            }

            internal class LatestSimpleResponse(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<QuoteLatestSimpleResponse>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject();
                }
            }

            internal class DailySimpleResponse(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<QuoteDailySimpleResponse>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject();
                }
            }
        }
    }
}
