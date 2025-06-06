using Bank.Application.Extensions;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.OpenApi.Core;
using Bank.OpenApi.Extensions;
using Bank.OpenApi.Mappers;

using Microsoft.OpenApi.Any;

namespace Bank.OpenApi.Schemas.Swagger;

internal static partial class Schema
{
    internal static partial class Swagger
    {
        internal static class User
        {
            internal class LoginRequest(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<UserLoginRequest>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject();
                }
            }

            internal class ActivationRequest(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<UserActivationRequest>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject();
                }
            }

            internal class RequestPasswordResetRequest(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<UserRequestPasswordResetRequest>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject();
                }
            }

            internal class PasswordResetRequest(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<UserPasswordResetRequest>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject();
                }
            }

            internal class UpdatePermissionRequest(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<UserUpdatePermissionRequest>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject();
                }
            }

            internal class Response(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<UserResponse>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject()
                                  .SetProperty(nameof(Example.Accounts)
                                               .ToCamelCase(), new OpenApiArray { GetSchemaExample<AccountSimpleResponse>() });
                }
            }

            internal class SimpleResponse(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<UserSimpleResponse>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject();
                }
            }

            internal class LoginResponse(OpenApiSchemaContext openApiContext) : AbstractSwaggerSchema<UserLoginResponse>(openApiContext)
            {
                protected override IOpenApiAny CreateExample()
                {
                    return Example.ToOpenApiObject()
                                  .SetProperty(nameof(Example.User)
                                               .ToCamelCase(), GetSchemaExample<UserResponse>());
                }
            }
        }
    }
}
