using Bank.Application.Extensions;
using Bank.Application.Responses;
using Bank.OpenApi.Core;
using Bank.OpenApi.Mappers;

namespace Bank.OpenApi.Schemas;

internal static partial class Schema
{
    internal static class Country
    {
        internal class Response() : AbstractOpenApiSchema<CountryResponse>()
        {
            public override void ApplyExample(OpenApiSchemaContext context)
            {
                if (!TryGetExample(context, out var example))
                    throw new MissingMemberException($"Missing Example for type {Type}");

                example.MapOpenApiObject(Object);

                if (context.TryGetExample<CurrencySimpleResponse>(out var currency))
                    Object[nameof(example.Currency)
                           .ToCamelCase()] = currency.ToOpenApiObject();
            }
        }

        internal class SimpleResponse() : AbstractOpenApiSchema<CountrySimpleResponse>()
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
