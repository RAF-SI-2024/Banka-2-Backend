using Bank.Application.Extensions;
using Bank.Application.Responses;

using Microsoft.OpenApi.Any;

namespace Bank.OpenApi.Mappers;

internal static class QuoteOpenApiMapper
{
    public static OpenApiObject ToOpenApiObject(this QuoteSimpleResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this QuoteSimpleResponse value, OpenApiObject @object)
    {
        @object[nameof(value.Id)
                .ToCamelCase()] = new OpenApiString(value.Id.ToString());

        @object[nameof(value.HighPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.HighPrice);

        @object[nameof(value.LowPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.LowPrice);

        @object[nameof(value.AskPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.AskPrice);

        @object[nameof(value.BidPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.BidPrice);

        @object[nameof(value.Volume)
                .ToCamelCase()] = new OpenApiDouble((double)value.Volume);

        @object[nameof(value.CreatedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.CreatedAt);

        @object[nameof(value.ModifiedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.ModifiedAt);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this QuoteLatestSimpleResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this QuoteLatestSimpleResponse value, OpenApiObject @object)
    {
        @object[nameof(value.SecurityTicker)
                .ToCamelCase()] = new OpenApiString(value.SecurityTicker);

        @object[nameof(value.AskPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.AskPrice);

        @object[nameof(value.BidPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.BidPrice);

        @object[nameof(value.HighPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.HighPrice);

        @object[nameof(value.LowPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.LowPrice);

        @object[nameof(value.Volume)
                .ToCamelCase()] = new OpenApiDouble((double)value.Volume);

        @object[nameof(value.CreatedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.CreatedAt);

        @object[nameof(value.ModifiedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.ModifiedAt);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this QuoteDailySimpleResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this QuoteDailySimpleResponse value, OpenApiObject @object)
    {
        @object[nameof(value.HighPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.HighPrice);

        @object[nameof(value.LowPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.LowPrice);

        @object[nameof(value.OpenPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.OpenPrice);

        @object[nameof(value.ClosePrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.ClosePrice);

        @object[nameof(value.Volume)
                .ToCamelCase()] = new OpenApiDouble((double)value.Volume);

        @object[nameof(value.CreatedAt)
                .ToCamelCase()] = new OpenApiDate(new DateTime(value.CreatedAt, TimeOnly.MinValue));

        @object[nameof(value.ModifiedAt)
                .ToCamelCase()] = new OpenApiDate(new DateTime(value.ModifiedAt, TimeOnly.MinValue));

        return @object;
    }
}
