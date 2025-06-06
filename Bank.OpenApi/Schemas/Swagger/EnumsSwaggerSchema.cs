using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bank.OpenApi.Schemas.Swagger;

internal static partial class Schema
{
    internal static partial class Swagger
    {
        internal class Enums : ISchemaFilter
        {
            public void Apply(OpenApiSchema schema, SchemaFilterContext context)
            {
                if (!context.Type.IsEnum)
                    return;

                schema.Enum.Clear();

                var underlyingType = Enum.GetUnderlyingType(context.Type);

                foreach (var value in Enum.GetValues(context.Type))
                    schema.Enum.Add(new OpenApiString($"{Convert.ChangeType(value, underlyingType)} - {Enum.GetName(context.Type, value)}"));

                schema.Type   = "string";
                schema.Format = null;
            }
        }
    }
}
