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
        internal static class Exchange
        {
            internal class MakeExchangeRequest() : AbstractOpenApiSchema<ExchangeMakeExchangeRequest>()
            {
                public override void ApplyExample(OpenApiSchemaContext context)
                {
                    if (!TryGetExample(context, out var example))
                        throw new MissingMemberException($"Missing Example for type {Type}");

                    example.MapOpenApiObject(Object);
                }
            }

            internal class UpdateRequest() : AbstractOpenApiSchema<ExchangeUpdateRequest>()
            {
                public override void ApplyExample(OpenApiSchemaContext context)
                {
                    if (!TryGetExample(context, out var example))
                        throw new MissingMemberException($"Missing Example for type {Type}");

                    example.MapOpenApiObject(Object);
                }
            }

            internal class Response() : AbstractOpenApiSchema<ExchangeResponse>()
            {
                public override void ApplyExample(OpenApiSchemaContext context)
                {
                    if (!TryGetExample(context, out var example))
                        throw new MissingMemberException($"Missing Example for type {Type}");

                    example.MapOpenApiObject(Object);

                    if (context.TryGetExample<CurrencySimpleResponse>(out var currencyFrom))
                        Object[nameof(example.CurrencyFrom)
                               .ToCamelCase()] = currencyFrom.ToOpenApiObject();

                    if (context.TryGetExample<CurrencySimpleResponse>(out var currencyTo))
                        Object[nameof(example.CurrencyTo)
                               .ToCamelCase()] = currencyTo.ToOpenApiObject();
                }
            }
        }
    }
}
