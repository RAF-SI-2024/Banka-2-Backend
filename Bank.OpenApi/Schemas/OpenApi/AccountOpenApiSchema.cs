using Bank.Application.Extensions;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.OpenApi.Core;
using Bank.OpenApi.Extensions;
using Bank.OpenApi.Mappers;

namespace Bank.OpenApi.Schemas.OpenApi;

internal static partial class Schema
{
    internal static partial class OpenApi
    {
        internal static class Account
        {
            internal class CreateRequest() : AbstractOpenApiSchema<AccountCreateRequest>()
            {
                public override void ApplyExample(OpenApiSchemaContext context)
                {
                    if (!TryGetExample(context, out var example))
                        throw new MissingMemberException($"Missing Example for type {Type}");

                    example.MapOpenApiObject(Object);
                }
            }

            internal class UpdateClientRequest() : AbstractOpenApiSchema<AccountUpdateClientRequest>()
            {
                public override void ApplyExample(OpenApiSchemaContext context)
                {
                    if (!TryGetExample(context, out var example))
                        throw new MissingMemberException($"Missing Example for type {Type}");

                    example.MapOpenApiObject(Object);
                }
            }

            internal class UpdateEmployeeRequest() : AbstractOpenApiSchema<AccountUpdateEmployeeRequest>()
            {
                public override void ApplyExample(OpenApiSchemaContext context)
                {
                    if (!TryGetExample(context, out var example))
                        throw new MissingMemberException($"Missing Example for type {Type}");

                    example.MapOpenApiObject(Object);
                }
            }

            internal class Response() : AbstractOpenApiSchema<AccountResponse>()
            {
                public override void ApplyExample(OpenApiSchemaContext context)
                {
                    if (!TryGetExample(context, out var example))
                        throw new MissingMemberException($"Missing Example for type {Type}");

                    example.MapOpenApiObject(Object);

                    if (context.TryGetExample<ClientSimpleResponse>(out var client))
                        Object[nameof(example.Client)
                               .ToCamelCase()] = client.ToOpenApiObject();

                    if (context.TryGetExample<EmployeeSimpleResponse>(out var employee))
                        Object[nameof(example.Employee)
                               .ToCamelCase()] = employee.ToOpenApiObject();

                    if (context.TryGetExample<CurrencyResponse>(out var currency))
                        Object[nameof(example.Currency)
                               .ToCamelCase()] = currency.ToOpenApiObject();

                    if (context.TryGetExample<AccountTypeResponse>(out var accountType))
                        Object[nameof(example.Type)
                               .ToCamelCase()] = accountType.ToOpenApiObject();

                    if (context.TryGetExample<List<AccountCurrencyResponse>>(out var accountCurrencies))
                        Object[nameof(example.AccountCurrencies)
                               .ToCamelCase()] = accountCurrencies.Select(accountCurrency => accountCurrency.ToOpenApiObject())
                                                                  .ToOpenApiArray();
                }
            }

            internal class SimpleResponse() : AbstractOpenApiSchema<AccountSimpleResponse>()
            {
                public override void ApplyExample(OpenApiSchemaContext context)
                {
                    if (!TryGetExample(context, out var example))
                        throw new MissingMemberException($"Missing Example for type {Type}");

                    example.MapOpenApiObject(Object);
                }
            }
        }
    }
}
