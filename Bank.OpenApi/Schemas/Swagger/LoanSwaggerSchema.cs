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
        internal static class Loan
        {
            internal class CreateRequest(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<LoanCreateRequest>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject();
                }
            }

            internal class UpdateRequest(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<LoanUpdateRequest>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject();
                }
            }

            internal class Response(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<LoanResponse>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject()
                                  .SetProperty(nameof(Example.Type)
                                               .ToCamelCase(), GetSchemaExample<LoanTypeResponse>())
                                  .SetProperty(nameof(Example.Account)
                                               .ToCamelCase(), GetSchemaExample<AccountResponse>())
                                  .SetProperty(nameof(Example.Currency)
                                               .ToCamelCase(), GetSchemaExample<CurrencyResponse>());
                }
            }
        }
    }
}
