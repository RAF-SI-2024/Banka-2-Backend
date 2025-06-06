using Bank.Application.Extensions;
using Bank.Application.Responses;
using Bank.OpenApi.Extensions;

using Microsoft.OpenApi.Any;

namespace Bank.OpenApi.Mappers;

internal static class OptionOpenApiMapper
{
    public static OpenApiObject ToOpenApiObject(this OptionResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this OptionResponse value, OpenApiObject @object)
    {
        @object[nameof(value.Id)
                .ToCamelCase()] = new OpenApiString(value.Id.ToString());

        @object[nameof(value.StrikePrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.StrikePrice);

        @object[nameof(value.ImpliedVolatility)
                .ToCamelCase()] = new OpenApiDouble((double)value.ImpliedVolatility);

        @object[nameof(value.SettlementDate)
                .ToCamelCase()] = new OpenApiDate(new DateTime(value.SettlementDate, TimeOnly.MinValue));

        @object[nameof(value.Name)
                .ToCamelCase()] = new OpenApiString(value.Name);

        @object[nameof(value.Ticker)
                .ToCamelCase()] = new OpenApiString(value.Ticker);

        @object[nameof(value.OptionType)
                .ToCamelCase()] = new OpenApiString(value.OptionType.ToString());

        @object[nameof(value.HighPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.HighPrice);

        @object[nameof(value.LowPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.LowPrice);

        @object[nameof(value.Volume)
                .ToCamelCase()] = new OpenApiDouble((double)value.Volume);

        @object[nameof(value.PriceChangeInInterval)
                .ToCamelCase()] = new OpenApiDouble((double)value.PriceChangeInInterval);

        @object[nameof(value.PriceChangePercentInInterval)
                .ToCamelCase()] = new OpenApiDouble((double)value.PriceChangePercentInInterval);

        @object[nameof(value.AskPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.AskPrice);

        @object[nameof(value.BidPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.BidPrice);

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

    public static OpenApiObject ToOpenApiObject(this OptionSimpleResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this OptionSimpleResponse value, OpenApiObject @object)
    {
        @object[nameof(value.Id)
                .ToCamelCase()] = new OpenApiString(value.Id.ToString());

        @object[nameof(value.StrikePrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.StrikePrice);

        @object[nameof(value.ImpliedVolatility)
                .ToCamelCase()] = new OpenApiDouble((double)value.ImpliedVolatility);

        @object[nameof(value.SettlementDate)
                .ToCamelCase()] = new OpenApiDate(new DateTime(value.SettlementDate, TimeOnly.MinValue));

        @object[nameof(value.Name)
                .ToCamelCase()] = new OpenApiString(value.Name);

        @object[nameof(value.Ticker)
                .ToCamelCase()] = new OpenApiString(value.Ticker);

        @object[nameof(value.OptionType)
                .ToCamelCase()] = new OpenApiString(value.OptionType.ToString());

        @object[nameof(value.HighPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.HighPrice);

        @object[nameof(value.LowPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.LowPrice);

        @object[nameof(value.Volume)
                .ToCamelCase()] = new OpenApiDouble((double)value.Volume);

        @object[nameof(value.PriceChange)
                .ToCamelCase()] = new OpenApiDouble((double)value.PriceChange);

        @object[nameof(value.PriceChangeInInterval)
                .ToCamelCase()] = new OpenApiDouble((double)value.PriceChangeInInterval);

        @object[nameof(value.PriceChangePercentInInterval)
                .ToCamelCase()] = new OpenApiDouble((double)value.PriceChangePercentInInterval);

        @object[nameof(value.AskPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.AskPrice);

        @object[nameof(value.BidPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.BidPrice);

        @object[nameof(value.CreatedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.CreatedAt);

        @object[nameof(value.ModifiedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.ModifiedAt);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this OptionDailyResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this OptionDailyResponse value, OpenApiObject @object)
    {
        @object[nameof(value.Id)
                .ToCamelCase()] = new OpenApiString(value.Id.ToString());

        @object[nameof(value.StrikePrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.StrikePrice);

        @object[nameof(value.ImpliedVolatility)
                .ToCamelCase()] = new OpenApiDouble((double)value.ImpliedVolatility);

        @object[nameof(value.SettlementDate)
                .ToCamelCase()] = new OpenApiDate(new DateTime(value.SettlementDate, TimeOnly.MinValue));

        @object[nameof(value.Name)
                .ToCamelCase()] = new OpenApiString(value.Name);

        @object[nameof(value.Ticker)
                .ToCamelCase()] = new OpenApiString(value.Ticker);

        @object[nameof(value.OptionType)
                .ToCamelCase()] = new OpenApiString(value.OptionType.ToString());

        @object[nameof(value.HighPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.HighPrice);

        @object[nameof(value.LowPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.LowPrice);

        @object[nameof(value.OpeningPrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.OpeningPrice);

        @object[nameof(value.ClosePrice)
                .ToCamelCase()] = new OpenApiDouble((double)value.ClosePrice);

        @object[nameof(value.Volume)
                .ToCamelCase()] = new OpenApiDouble((double)value.Volume);

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
