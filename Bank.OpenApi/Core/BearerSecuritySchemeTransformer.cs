using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace Bank.OpenApi.Core;

internal sealed class BearerSecuritySchemeTransformer(IAuthenticationSchemeProvider authenticationSchemeProvider) : IOpenApiDocumentTransformer
{
    public async Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        var authenticationSchemes = await authenticationSchemeProvider.GetAllSchemesAsync();

        if (authenticationSchemes.All(authScheme => authScheme.Name != "Bearer"))
            return;

        var requirements = new Dictionary<string, OpenApiSecurityScheme>
                           {
                               ["Bearer"] = new()
                                            {
                                                Type         = SecuritySchemeType.Http,
                                                Description  = "Authorization: Bearer {token}",
                                                Scheme       = "bearer",
                                                In           = ParameterLocation.Header,
                                                BearerFormat = "Json Web Token"
                                            }
                           };

        document.Components                 ??= new OpenApiComponents();
        document.Components.SecuritySchemes =   requirements;

        foreach (var operation in document.Paths.Values.SelectMany(path => path.Operations))
            operation.Value.Security.Add(new OpenApiSecurityRequirement
                                         {
                                             [new OpenApiSecurityScheme { Reference = new OpenApiReference { Id = "Bearer", Type = ReferenceType.SecurityScheme } }] =
                                             []
                                         });
    }
}
