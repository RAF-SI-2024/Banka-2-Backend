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
        internal static class Stock
        {
            internal class Response(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<StockResponse>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject()
                                  .SetProperty(nameof(Example.StockExchange)
                                               .ToCamelCase(), GetSchemaExample<StockExchangeResponse>())
                                  .SetProperty(nameof(Example.Quotes)
                                               .ToCamelCase(), new OpenApiArray { GetSchemaExample<QuoteSimpleResponse>() });
                }
            }

            internal class SimpleResponse(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<StockSimpleResponse>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject();
                }
            }

            internal class DailyResponse(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<StockDailyResponse>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject()
                                  .SetProperty(nameof(Example.StockExchange)
                                               .ToCamelCase(), GetSchemaExample<StockExchangeResponse>())
                                  .SetProperty(nameof(Example.Quotes)
                                               .ToCamelCase(), new OpenApiArray { GetSchemaExample<QuoteDailySimpleResponse>() });
                }
            }
        }
    }
}
