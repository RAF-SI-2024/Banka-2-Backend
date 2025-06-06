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
        internal static class Exchange
        {
            internal class MakeExchangeRequest(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<ExchangeMakeExchangeRequest>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject();
                }
            }

            internal class UpdateRequest(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<ExchangeUpdateRequest>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject();
                }
            }

            internal class Response(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<ExchangeResponse>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject()
                                  .SetProperty(nameof(Example.CurrencyFrom)
                                               .ToCamelCase(), GetSchemaExample<CurrencySimpleResponse>())
                                  .SetProperty(nameof(Example.CurrencyTo)
                                               .ToCamelCase(), GetSchemaExample<CurrencySimpleResponse>());
                }
            }
        }
    }
}
