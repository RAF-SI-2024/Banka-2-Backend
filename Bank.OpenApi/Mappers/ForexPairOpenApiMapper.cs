using Bank.Application.Extensions;
using Bank.Application.Responses;
using Bank.OpenApi.Extensions;

using Microsoft.OpenApi.Any;

namespace Bank.OpenApi.Mappers;

internal static class ForexPairOpenApiMapper
{
    public static OpenApiObject ToOpenApiObject(this ForexPairResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this ForexPairResponse value, OpenApiObject @object)
    {
        @object[nameof(value.Id)
                .ToCamelCase()] = new OpenApiString(value.Id.ToString());

        @object[nameof(value.Liquidity)
                .ToCamelCase()] = new OpenApiDouble((double)value.Liquidity);

        @object[nameof(value.BaseCurrency)
                .ToCamelCase()] = value.BaseCurrency?.ToOpenApiObject();

        @object[nameof(value.QuoteCurrency)
                .ToCamelCase()] = value.QuoteCurrency?.ToOpenApiObject();

        @object[nameof(value.ExchangeRate)
                .ToCamelCase()] = new OpenApiDouble((double)value.ExchangeRate);

        @object[nameof(value.MaintenanceDecimal)
                .ToCamelCase()] = new OpenApiDouble((double)value.MaintenanceDecimal);

        @object[nameof(value.Name)
                .ToCamelCase()] = new OpenApiString(value.Name);

        @object[nameof(value.Ticker)
                .ToCamelCase()] = new OpenApiString(value.Ticker);

        @object[nameof(value.HighPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.HighPrice);

        @object[nameof(value.LowPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.LowPrice);

        @object[nameof(value.AskPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.AskPrice);

        @object[nameof(value.BidPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.BidPrice);

        @object[nameof(value.PriceChangeInInterval)
                .ToCamelCase()] = new OpenApiDouble((double)value.PriceChangeInInterval);

        @object[nameof(value.PriceChangePercentInInterval)
                .ToCamelCase()] = new OpenApiDouble((double)value.PriceChangePercentInInterval);

        @object[nameof(value.CreatedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.CreatedAt);

        @object[nameof(value.ModifiedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.ModifiedAt);

        @object[nameof(value.StockExchange)
                .ToCamelCase()] = value.StockExchange?.ToOpenApiObject();

        @object[nameof(value.Quotes)
                .ToCamelCase()] = value.Quotes?.Select(q => q.ToOpenApiObject())
                                       .ToOpenApiArray();

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this ForexPairSimpleResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this ForexPairSimpleResponse value, OpenApiObject @object)
    {
        @object[nameof(value.Id)
                .ToCamelCase()] = new OpenApiString(value.Id.ToString());

        @object[nameof(value.Liquidity)
                .ToCamelCase()] = new OpenApiDouble((double)value.Liquidity);

        @object[nameof(value.BaseCurrency)
                .ToCamelCase()] = value.BaseCurrency?.ToOpenApiObject();

        @object[nameof(value.QuoteCurrency)
                .ToCamelCase()] = value.QuoteCurrency?.ToOpenApiObject();

        @object[nameof(value.ExchangeRate)
                .ToCamelCase()] = new OpenApiDouble((double)value.ExchangeRate);

        @object[nameof(value.MaintenanceDecimal)
                .ToCamelCase()] = new OpenApiDouble((double)value.MaintenanceDecimal);

        @object[nameof(value.Name)
                .ToCamelCase()] = new OpenApiString(value.Name);

        @object[nameof(value.Ticker)
                .ToCamelCase()] = new OpenApiString(value.Ticker);

        @object[nameof(value.Price)
                .ToCamelCase()] = new OpenApiDouble((double)value.Price);

        @object[nameof(value.HighPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.HighPrice);

        @object[nameof(value.LowPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.LowPrice);

        @object[nameof(value.AskPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.AskPrice);

        @object[nameof(value.BidPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.BidPrice);

        @object[nameof(value.PriceChangeInInterval)
                .ToCamelCase()] = new OpenApiDouble((double)value.PriceChangeInInterval);

        @object[nameof(value.PriceChangePercentInInterval)
                .ToCamelCase()] = new OpenApiDouble((double)value.PriceChangePercentInInterval);

        @object[nameof(value.CreatedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.CreatedAt);

        @object[nameof(value.ModifiedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.ModifiedAt);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this ForexPairDailyResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this ForexPairDailyResponse value, OpenApiObject @object)
    {
        @object[nameof(value.Id)
                .ToCamelCase()] = new OpenApiString(value.Id.ToString());

        @object[nameof(value.Liquidity)
                .ToCamelCase()] = new OpenApiDouble((double)value.Liquidity);

        @object[nameof(value.BaseCurrency)
                .ToCamelCase()] = value.BaseCurrency?.ToOpenApiObject();

        @object[nameof(value.QuoteCurrency)
                .ToCamelCase()] = value.QuoteCurrency?.ToOpenApiObject();

        @object[nameof(value.ExchangeRate)
                .ToCamelCase()] = new OpenApiDouble((double)value.ExchangeRate);

        @object[nameof(value.MaintenanceDecimal)
                .ToCamelCase()] = new OpenApiDouble((double)value.MaintenanceDecimal);

        @object[nameof(value.Name)
                .ToCamelCase()] = new OpenApiString(value.Name);

        @object[nameof(value.Ticker)
                .ToCamelCase()] = new OpenApiString(value.Ticker);

        @object[nameof(value.HighPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.HighPrice);

        @object[nameof(value.LowPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.LowPrice);

        @object[nameof(value.OpeningPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.OpeningPrice);

        @object[nameof(value.ClosePrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.ClosePrice);

        @object[nameof(value.PriceChangeInInterval)
                .ToCamelCase()] = new OpenApiDouble((double)value.PriceChangeInInterval);

        @object[nameof(value.PriceChangePercentInInterval)
                .ToCamelCase()] = new OpenApiDouble((double)value.PriceChangePercentInInterval);

        @object[nameof(value.CreatedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.CreatedAt);

        @object[nameof(value.ModifiedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.ModifiedAt);

        @object[nameof(value.StockExchange)
                .ToCamelCase()] = value.StockExchange?.ToOpenApiObject();

        @object[nameof(value.Quotes)
                .ToCamelCase()] = value.Quotes?.Select(q => q.ToOpenApiObject())
                                       .ToOpenApiArray();

        return @object;
    }
}
