using Bank.Application.Extensions;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.OpenApi.Core;
using Bank.OpenApi.Mappers;

namespace Bank.OpenApi.Schemas;

internal static partial class Schema
{
    internal static class Installment
    {
        internal class CreateRequest() : AbstractOpenApiSchema<InstallmentCreateRequest>()
        {
            public override void ApplyExample(OpenApiSchemaContext context)
            {
                if (!TryGetExample(context, out var example))
                    throw new MissingMemberException($"Missing Example for type {Type}");

                example.MapOpenApiObject(Object);
            }
        }

        internal class UpdateRequest() : AbstractOpenApiSchema<InstallmentUpdateRequest>()
        {
            public override void ApplyExample(OpenApiSchemaContext context)
            {
                if (!TryGetExample(context, out var example))
                    throw new MissingMemberException($"Missing Example for type {Type}");

                example.MapOpenApiObject(Object);
            }
        }

        internal class Response() : AbstractOpenApiSchema<InstallmentResponse>()
        {
            public override void ApplyExample(OpenApiSchemaContext context)
            {
                if (!TryGetExample(context, out var example))
                    throw new MissingMemberException($"Missing Example for type {Type}");

                example.MapOpenApiObject(Object);

                if (context.TryGetExample<LoanResponse>(out var loan))
                    Object[nameof(example.Loan)
                           .ToCamelCase()] = loan.ToOpenApiObject();
            }
        }
    }
}
