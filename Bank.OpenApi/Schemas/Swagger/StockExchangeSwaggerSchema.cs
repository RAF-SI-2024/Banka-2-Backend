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
        internal static class StockExchange
        {
            internal class CreateRequest(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<ExchangeCreateRequest>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject();
                }
            }

            internal class Response(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<StockExchangeResponse>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject()
                                  .SetProperty(nameof(Example.Currency)
                                               .ToCamelCase(), GetSchemaExample<CurrencySimpleResponse>());
                }
            }
        }
    }
}
