using Bank.Application.Extensions;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.OpenApi.Core;
using Bank.OpenApi.Mappers;

namespace Bank.OpenApi.Schemas;

internal static partial class Schema
{
    internal static class Transaction
    {
        internal class CreateRequest() : AbstractOpenApiSchema<TransactionCreateRequest>()
        {
            public override void ApplyExample(OpenApiSchemaContext context)
            {
                if (!TryGetExample(context, out var example))
                    throw new MissingMemberException($"Missing Example for type {Type}");

                example.MapOpenApiObject(Object);
            }
        }

        internal class UpdateRequest() : AbstractOpenApiSchema<TransactionUpdateRequest>()
        {
            public override void ApplyExample(OpenApiSchemaContext context)
            {
                if (!TryGetExample(context, out var example))
                    throw new MissingMemberException($"Missing Example for type {Type}");

                example.MapOpenApiObject(Object);
            }
        }

        internal class Response() : AbstractOpenApiSchema<TransactionResponse>()
        {
            public override void ApplyExample(OpenApiSchemaContext context)
            {
                if (!TryGetExample(context, out var example))
                    throw new MissingMemberException($"Missing Example for type {Type}");

                example.MapOpenApiObject(Object);

                if (context.TryGetExample<AccountSimpleResponse>(out var fromAccount))
                    Object[nameof(example.FromAccount)
                           .ToCamelCase()] = fromAccount.ToOpenApiObject();

                if (context.TryGetExample<CurrencyResponse>(out var fromCurrency))
                    Object[nameof(example.FromCurrency)
                           .ToCamelCase()] = fromCurrency.ToOpenApiObject();

                if (context.TryGetExample<AccountSimpleResponse>(out var toAccount))
                    Object[nameof(example.ToAccount)
                           .ToCamelCase()] = toAccount.ToOpenApiObject();

                if (context.TryGetExample<CurrencyResponse>(out var toCurrency))
                    Object[nameof(example.ToCurrency)
                           .ToCamelCase()] = toCurrency.ToOpenApiObject();

                if (context.TryGetExample<TransactionCodeResponse>(out var transactionCode))
                    Object[nameof(example.Code)
                           .ToCamelCase()] = transactionCode.ToOpenApiObject();
            }
        }

        internal class CreateResponse() : AbstractOpenApiSchema<TransactionCreateResponse>()
        {
            public override void ApplyExample(OpenApiSchemaContext context)
            {
                if (!TryGetExample(context, out var example))
                    throw new MissingMemberException($"Missing Example for type {Type}");

                example.MapOpenApiObject(Object);

                if (context.TryGetExample<TransactionCodeResponse>(out var transactionCode))
                    Object[nameof(example.Code)
                           .ToCamelCase()] = transactionCode.ToOpenApiObject();
            }
        }
    }
}
