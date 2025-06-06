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
        internal static class Transaction
        {
            internal class CreateRequest(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<TransactionCreateRequest>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject();
                }
            }

            internal class UpdateRequest(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<TransactionUpdateRequest>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject();
                }
            }

            internal class Response(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<TransactionResponse>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject()
                                  .SetProperty(nameof(Example.FromAccount)
                                               .ToCamelCase(), GetSchemaExample<AccountSimpleResponse>())
                                  .SetProperty(nameof(Example.FromCurrency)
                                               .ToCamelCase(), GetSchemaExample<CurrencyResponse>())
                                  .SetProperty(nameof(Example.ToAccount)
                                               .ToCamelCase(), GetSchemaExample<AccountSimpleResponse>())
                                  .SetProperty(nameof(Example.ToCurrency)
                                               .ToCamelCase(), GetSchemaExample<CurrencyResponse>())
                                  .SetProperty(nameof(Example.Code)
                                               .ToCamelCase(), GetSchemaExample<TransactionCodeResponse>());
                }
            }

            internal class CreateResponse(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<TransactionCreateResponse>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject()
                                  .SetProperty(nameof(Example.Code)
                                               .ToCamelCase(), GetSchemaExample<TransactionCodeResponse>());
                }
            }
        }
    }
}
