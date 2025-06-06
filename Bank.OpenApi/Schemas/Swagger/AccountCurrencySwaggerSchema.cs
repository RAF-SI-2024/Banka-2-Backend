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
        internal static class AccountCurrency
        {
            internal class CreateRequest(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<AccountCurrencyCreateRequest>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject();
                }
            }

            internal class ClientUpdateRequest(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<AccountCurrencyClientUpdateRequest>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject();
                }
            }

            internal class Response(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<AccountCurrencyResponse>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject()
                                  .SetProperty(nameof(Example.Account)
                                               .ToCamelCase(), GetSchemaExample<AccountSimpleResponse>())
                                  .SetProperty(nameof(Example.Employee)
                                               .ToCamelCase(), GetSchemaExample<EmployeeSimpleResponse>())
                                  .SetProperty(nameof(Example.Currency)
                                               .ToCamelCase(), GetSchemaExample<CurrencyResponse>());
                }
            }
        }
    }
}
