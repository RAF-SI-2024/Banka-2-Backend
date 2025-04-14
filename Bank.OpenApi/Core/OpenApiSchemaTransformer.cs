using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace Bank.OpenApi.Core;

internal class OpenApiSchemaTransformer(OpenApiSchemaContext context) : IOpenApiSchemaTransformer
{
    private readonly OpenApiSchemaContext m_Context = context;

    public Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
    {
        if (m_Context.SchemaRepository.TryGetValue(context.JsonTypeInfo.Type, out var openApiSchema))
            openApiSchema.Apply(schema);

        return Task.CompletedTask;
    }
}
