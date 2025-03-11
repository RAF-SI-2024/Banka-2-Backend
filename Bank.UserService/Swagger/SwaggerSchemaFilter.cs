using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bank.UserService.Swagger;

public abstract class SwaggerSchemaFilter<T>(T example) : ISchemaFilter
{
    protected readonly T Example = example;

    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type != typeof(T))
            return;

        schema.Example = CreateExample(schema, context);
    }

    protected abstract IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context);
}
