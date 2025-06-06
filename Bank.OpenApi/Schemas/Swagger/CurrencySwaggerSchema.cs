using Bank.Application.Extensions;
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
        internal static class Currency
        {
            internal class Response(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<CurrencyResponse>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject()
                                  .SetProperty(nameof(Example.Countries)
                                               .ToCamelCase(), new OpenApiArray { GetSchemaExample<CountrySimpleResponse>() });
                }
            }

            internal class SimpleResponse(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<CurrencySimpleResponse>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject();
                }
            }
        }
    }
}
