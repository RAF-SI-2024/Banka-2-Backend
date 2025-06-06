using Microsoft.OpenApi.Any;

namespace Bank.OpenApi.Extensions;

public static class OpenApiExtensions
{
    public static OpenApiArray ToOpenApiArray(this IEnumerable<IOpenApiAny> values)
    {
        var array = new OpenApiArray();

        array.AddRange(values);

        return array;
    }

    public static OpenApiObject SetProperty(this OpenApiObject value, string propertyName, IOpenApiAny? propertyValue)
    {
        value[propertyName] = propertyValue;

        return value;
    }
}
