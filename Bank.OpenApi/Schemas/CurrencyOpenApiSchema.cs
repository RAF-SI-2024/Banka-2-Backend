using Bank.Application.Extensions;
using Bank.Application.Responses;
using Bank.OpenApi.Core;
using Bank.OpenApi.Extensions;
using Bank.OpenApi.Mappers;

namespace Bank.OpenApi.Schemas;

internal static partial class Schema
{
    internal static class Currency
    {
        internal class Response() : AbstractOpenApiSchema<CurrencyResponse>()
        {
            public override void ApplyExample(OpenApiSchemaContext context)
            {
                if (!TryGetExample(context, out var example))
                    throw new MissingMemberException($"Missing Example for type {Type}");

                example.MapOpenApiObject(Object);

                if (context.TryGetExample<List<CountrySimpleResponse>>(out var countries))
                    Object[nameof(example.Countries)
                           .ToCamelCase()] = countries.Select(country => country.ToOpenApiObject())
                                                      .ToOpenApiArray();
            }
        }

        internal class SimpleResponse() : AbstractOpenApiSchema<CurrencySimpleResponse>()
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
