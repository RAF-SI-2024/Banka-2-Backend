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
        internal static class Account
        {
            internal class CreateRequest(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<AccountCreateRequest>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject();
                }
            }

            internal class UpdateClientRequest(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<AccountUpdateClientRequest>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject();
                }
            }

            internal class UpdateEmployeeRequest(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<AccountUpdateEmployeeRequest>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject();
                }
            }

            internal class Response(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<AccountResponse>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject()
                                  .SetProperty(nameof(Example.Client)
                                               .ToCamelCase(), GetSchemaExample<ClientSimpleResponse>())
                                  .SetProperty(nameof(Example.Employee)
                                               .ToCamelCase(), GetSchemaExample<EmployeeSimpleResponse>())
                                  .SetProperty(nameof(Example.Currency)
                                               .ToCamelCase(), GetSchemaExample<CurrencyResponse>())
                                  .SetProperty(nameof(Example.Type)
                                               .ToCamelCase(), GetSchemaExample<AccountTypeResponse>())
                                  .SetProperty(nameof(Example.AccountCurrencies)
                                               .ToCamelCase(), new OpenApiArray { GetSchemaExample<AccountCurrencyResponse>() });
                }
            }

            internal class SimpleResponse(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<AccountSimpleResponse>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject();
                }
            }
        }
    }
}
