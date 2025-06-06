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
        internal static class Loan
        {
            internal class CreateRequest() : AbstractOpenApiSchema<LoanCreateRequest>()
            {
                public override void ApplyExample(OpenApiSchemaContext context)
                {
                    if (!TryGetExample(context, out var example))
                        throw new MissingMemberException($"Missing Example for type {Type}");

                    example.MapOpenApiObject(Object);
                }
            }

            internal class UpdateRequest() : AbstractOpenApiSchema<LoanUpdateRequest>()
            {
                public override void ApplyExample(OpenApiSchemaContext context)
                {
                    if (!TryGetExample(context, out var example))
                        throw new MissingMemberException($"Missing Example for type {Type}");

                    example.MapOpenApiObject(Object);
                }
            }

            internal class Response() : AbstractOpenApiSchema<LoanResponse>()
            {
                public override void ApplyExample(OpenApiSchemaContext context)
                {
                    if (!TryGetExample(context, out var example))
                        throw new MissingMemberException($"Missing Example for type {Type}");

                    example.MapOpenApiObject(Object);

                    if (context.TryGetExample<LoanTypeResponse>(out var loanType))
                        Object[nameof(example.Type)
                               .ToCamelCase()] = loanType.ToOpenApiObject();

                    if (context.TryGetExample<AccountResponse>(out var account))
                        Object[nameof(example.Account)
                               .ToCamelCase()] = account.ToOpenApiObject();

                    if (context.TryGetExample<CurrencyResponse>(out var currency))
                        Object[nameof(example.Currency)
                               .ToCamelCase()] = currency.ToOpenApiObject();
                }
            }
        }
    }
}
