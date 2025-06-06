using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.OpenApi.Core;
using Bank.OpenApi.Mappers;

namespace Bank.OpenApi.Schemas.OpenApi;

internal static partial class Schema
{
    internal static partial class OpenApi
    {
        internal static class AccountType
        {
            internal class CreateRequest() : AbstractOpenApiSchema<AccountTypeCreateRequest>()
            {
                public override void ApplyExample(OpenApiSchemaContext context)
                {
                    if (!TryGetExample(context, out var example))
                        throw new MissingMemberException($"Missing Example for type {Type}");

                    example.MapOpenApiObject(Object);
                }
            }

            internal class UpdateRequest() : AbstractOpenApiSchema<AccountTypeUpdateRequest>()
            {
                public override void ApplyExample(OpenApiSchemaContext context)
                {
                    if (!TryGetExample(context, out var example))
                        throw new MissingMemberException($"Missing Example for type {Type}");

                    example.MapOpenApiObject(Object);
                }
            }

            internal class Response() : AbstractOpenApiSchema<AccountTypeResponse>()
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
