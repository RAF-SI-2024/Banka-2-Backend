using System.Diagnostics.CodeAnalysis;

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bank.OpenApi.Core;

internal interface IOpenApiSchema
{
    public OpenApiObject Object { get; }
    public Type          Type   { get; }

    public void Apply(OpenApiSchema schema);

    public void ApplyExample(OpenApiSchemaContext context);
}

internal interface IDefaultOpenApiExample
{
    public object Example { get; }
    public Type   Type    { get; }
}

public interface IOpenApiExample
{
    public object Example { get; }
    public Type   Type    { get; }
}

internal abstract class AbstractOpenApiSchema<TExample> : IOpenApiSchema where TExample : class
{
    public Type Type => typeof(TExample);

    public OpenApiObject Object { get; } = new();

    protected bool TryGetExample(OpenApiSchemaContext context, [NotNullWhen(true)] out TExample example) => context.TryGetExample(out example);

    public void Apply(OpenApiSchema schema)
    {
        schema.Example = Object;
        schema.Default = Object;
    }

    public abstract void ApplyExample(OpenApiSchemaContext context);
}

internal class DefaultOpenApiExample<TExample>(TExample example) : IDefaultOpenApiExample where TExample : class
{
    public object Example { get; } = example;

    public Type Type => typeof(TExample);
}

public class OpenApiExample<TExample>(TExample example) : IOpenApiExample where TExample : class
{
    public object Example { get; } = example;

    public Type Type => typeof(TExample);
}

internal abstract class AbstractSwaggerSchema<TExample> : ISchemaFilter where TExample : class
{
    protected readonly TExample            Example;
    private            SchemaFilterContext m_Context = null!;

    protected AbstractSwaggerSchema(OpenApiSchemaContext openApiContext)
    {
        if (!openApiContext.TryGetExample<TExample>(out var example))
            throw new MissingMemberException($"Missing Example for type {typeof(TExample)}");

        Example = example;
    }

    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type != typeof(TExample))
            return;

        m_Context = context;

        schema.Example = CreateExample();
    }

    protected IOpenApiAny? GetSchemaExample<TSchema>()
    {
        return m_Context.SchemaRepository.Schemas.GetValueOrDefault(typeof(TSchema).Name)
                        ?.Example;
    }

    protected abstract IOpenApiAny CreateExample();
}

internal class OpenApiSchemaContext
{
    public readonly Dictionary<Type, IOpenApiSchema> SchemaRepository;
    public readonly Dictionary<Type, object>         ExampleRepository = new();

    public OpenApiSchemaContext(IEnumerable<IOpenApiSchema>  openApiSchemas, IEnumerable<IDefaultOpenApiExample> defaultOpenApiExamples,
                                IEnumerable<IOpenApiExample> openApiExamples)
    {
        foreach (var defaultOpenApiExample in defaultOpenApiExamples)
            ExampleRepository[defaultOpenApiExample.Type] = defaultOpenApiExample.Example;

        foreach (var openApiExample in openApiExamples)
            ExampleRepository[openApiExample.Type] = openApiExample.Example;

        SchemaRepository = openApiSchemas.ToDictionary(schema => schema.Type, schema => schema);

        foreach (var value in SchemaRepository.Values)
            value.ApplyExample(this);
    }

    public bool TryGetExample<TExample>([NotNullWhen(true)] out TExample example) where TExample : class
    {
        example = null!;

        if (!ExampleRepository.TryGetValue(typeof(TExample), out var repositoryValue) || repositoryValue is not TExample value)
            return false;

        example = value;

        return true;
    }

    public bool TryGetSchema<TExample>([NotNullWhen(true)] out TExample schema) where TExample : class
    {
        schema = null!;

        if (!SchemaRepository.TryGetValue(typeof(TExample), out var repositoryValue) || repositoryValue is not TExample value)
            return false;

        schema = value;

        return true;
    }
}
