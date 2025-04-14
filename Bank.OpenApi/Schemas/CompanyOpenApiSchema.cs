using Bank.Application.Extensions;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.OpenApi.Core;
using Bank.OpenApi.Mappers;

namespace Bank.OpenApi.Schemas;

internal static partial class Schema
{
    internal static class Company
    {
        internal class CreateRequest() : AbstractOpenApiSchema<CompanyCreateRequest>()
        {
            public override void ApplyExample(OpenApiSchemaContext context)
            {
                if (!TryGetExample(context, out var example))
                    throw new MissingMemberException($"Missing Example for type {Type}");

                example.MapOpenApiObject(Object);
            }
        }

        internal class UpdateRequest() : AbstractOpenApiSchema<CompanyUpdateRequest>()
        {
            public override void ApplyExample(OpenApiSchemaContext context)
            {
                if (!TryGetExample(context, out var example))
                    throw new MissingMemberException($"Missing Example for type {Type}");

                example.MapOpenApiObject(Object);
            }
        }

        internal class Response() : AbstractOpenApiSchema<CompanyResponse>()
        {
            public override void ApplyExample(OpenApiSchemaContext context)
            {
                if (!TryGetExample(context, out var example))
                    throw new MissingMemberException($"Missing Example for type {Type}");

                example.MapOpenApiObject(Object);

                if (context.TryGetExample<UserSimpleResponse>(out var owner))
                    Object[nameof(example.MajorityOwner)
                           .ToCamelCase()] = owner.ToOpenApiObject();
            }
        }

        internal class SimpleResponse() : AbstractOpenApiSchema<CompanySimpleResponse>()
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
