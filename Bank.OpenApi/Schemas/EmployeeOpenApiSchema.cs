using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.OpenApi.Core;
using Bank.OpenApi.Mappers;

namespace Bank.OpenApi.Schemas;

internal static partial class Schema
{
    internal static class Employee
    {
        internal class CreateRequest() : AbstractOpenApiSchema<EmployeeCreateRequest>()
        {
            public override void ApplyExample(OpenApiSchemaContext context)
            {
                if (!TryGetExample(context, out var example))
                    throw new MissingMemberException($"Missing Example for type {Type}");

                example.MapOpenApiObject(Object);
            }
        }

        internal class UpdateRequest() : AbstractOpenApiSchema<EmployeeUpdateRequest>()
        {
            public override void ApplyExample(OpenApiSchemaContext context)
            {
                if (!TryGetExample(context, out var example))
                    throw new MissingMemberException($"Missing Example for type {Type}");

                example.MapOpenApiObject(Object);
            }
        }

        internal class Response() : AbstractOpenApiSchema<EmployeeResponse>()
        {
            public override void ApplyExample(OpenApiSchemaContext context)
            {
                if (!TryGetExample(context, out var example))
                    throw new MissingMemberException($"Missing Example for type {Type}");

                example.MapOpenApiObject(Object);
            }
        }

        internal class SimpleResponse() : AbstractOpenApiSchema<EmployeeSimpleResponse>()
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
