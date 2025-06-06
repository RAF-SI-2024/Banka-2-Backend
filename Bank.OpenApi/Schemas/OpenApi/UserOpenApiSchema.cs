using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.OpenApi.Core;
using Bank.OpenApi.Mappers;

namespace Bank.OpenApi.Schemas.OpenApi;

internal static partial class Schema
{
    internal static partial class OpenApi
    {
        internal static class User
        {
            internal class LoginRequest() : AbstractOpenApiSchema<UserLoginRequest>()
            {
                public override void ApplyExample(OpenApiSchemaContext context)
                {
                    if (!TryGetExample(context, out var example))
                        throw new MissingMemberException($"Missing Example for type {Type}");

                    example.MapOpenApiObject(Object);
                }
            }

            internal class ActivationRequest() : AbstractOpenApiSchema<UserActivationRequest>()
            {
                public override void ApplyExample(OpenApiSchemaContext context)
                {
                    if (!TryGetExample(context, out var example))
                        throw new MissingMemberException($"Missing Example for type {Type}");

                    example.MapOpenApiObject(Object);
                }
            }

            internal class PasswordResetRequest() : AbstractOpenApiSchema<UserPasswordResetRequest>()
            {
                public override void ApplyExample(OpenApiSchemaContext context)
                {
                    if (!TryGetExample(context, out var example))
                        throw new MissingMemberException($"Missing Example for type {Type}");

                    example.MapOpenApiObject(Object);
                }
            }

            internal class RequestPasswordResetRequest() : AbstractOpenApiSchema<UserRequestPasswordResetRequest>()
            {
                public override void ApplyExample(OpenApiSchemaContext context)
                {
                    if (!TryGetExample(context, out var example))
                        throw new MissingMemberException($"Missing Example for type {Type}");

                    example.MapOpenApiObject(Object);
                }
            }

            internal class Response() : AbstractOpenApiSchema<UserResponse>()
            {
                public override void ApplyExample(OpenApiSchemaContext context)
                {
                    if (!TryGetExample(context, out var example))
                        throw new MissingMemberException($"Missing Example for type {Type}");

                    example.MapOpenApiObject(Object);
                }
            }

            internal class SimpleResponse() : AbstractOpenApiSchema<UserSimpleResponse>()
            {
                public override void ApplyExample(OpenApiSchemaContext context)
                {
                    if (!TryGetExample(context, out var example))
                        throw new MissingMemberException($"Missing Example for type {Type}");

                    example.MapOpenApiObject(Object);
                }
            }

            internal class LoginResponse() : AbstractOpenApiSchema<UserLoginResponse>()
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
