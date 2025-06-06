using Bank.Application.Extensions;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.OpenApi.Core;
using Bank.OpenApi.Mappers;

namespace Bank.OpenApi.Schemas.OpenApi;

internal static partial class Schema
{
    internal static partial class OpenApi
    {
        internal static class AccountCurrency
        {
            internal class CreateRequest() : AbstractOpenApiSchema<AccountCurrencyCreateRequest>()
            {
                public override void ApplyExample(OpenApiSchemaContext context)
                {
                    if (!TryGetExample(context, out var example))
                        throw new MissingMemberException($"Missing Example for type {Type}");

                    example.MapOpenApiObject(Object);
                }
            }

            internal class ClientUpdateRequest() : AbstractOpenApiSchema<AccountCurrencyClientUpdateRequest>()
            {
                public override void ApplyExample(OpenApiSchemaContext context)
                {
                    if (!TryGetExample(context, out var example))
                        throw new MissingMemberException($"Missing Example for type {Type}");

                    example.MapOpenApiObject(Object);
                }
            }

            internal class Response() : AbstractOpenApiSchema<AccountCurrencyResponse>()
            {
                public override void ApplyExample(OpenApiSchemaContext context)
                {
                    if (!TryGetExample(context, out var example))
                        throw new MissingMemberException($"Missing Example for type {Type}");

                    example.MapOpenApiObject(Object);

                    if (context.TryGetExample<AccountSimpleResponse>(out var account))
                        Object[nameof(example.Account)
                               .ToCamelCase()] = account.ToOpenApiObject();

                    if (context.TryGetExample<EmployeeSimpleResponse>(out var employee))
                        Object[nameof(example.Employee)
                               .ToCamelCase()] = employee.ToOpenApiObject();

                    if (context.TryGetExample<CurrencyResponse>(out var currency))
                        Object[nameof(example.Currency)
                               .ToCamelCase()] = currency.ToOpenApiObject();
                }
            }
        }
    }
}
