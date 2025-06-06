using Bank.Application.Extensions;
using Bank.Application.Responses;
using Bank.OpenApi.Extensions;

using Microsoft.OpenApi.Any;

namespace Bank.OpenApi.Mappers;

internal static class FutureContractOpenApiMapper
{
    public static OpenApiObject ToOpenApiObject(this FutureContractResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this FutureContractResponse value, OpenApiObject @object)
    {
        @object[nameof(value.Id)
                .ToCamelCase()] = new OpenApiString(value.Id.ToString());

        @object[nameof(value.ContractSize)
                .ToCamelCase()] = new OpenApiInteger(value.ContractSize);

        @object[nameof(value.ContractUnit)
                .ToCamelCase()] = new OpenApiString(value.ContractUnit.ToString());

        @object[nameof(value.SettlementDate)
                .ToCamelCase()] = new OpenApiDate(new DateTime(value.SettlementDate, TimeOnly.MinValue));

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

    public static OpenApiObject ToOpenApiObject(this FutureContractSimpleResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this FutureContractSimpleResponse value, OpenApiObject @object)
    {
        @object[nameof(value.Id)
                .ToCamelCase()] = new OpenApiString(value.Id.ToString());

        @object[nameof(value.ContractSize)
                .ToCamelCase()] = new OpenApiInteger(value.ContractSize);

        @object[nameof(value.ContractUnit)
                .ToCamelCase()] = new OpenApiString(value.ContractUnit.ToString());

        @object[nameof(value.SettlementDate)
                .ToCamelCase()] = new OpenApiDate(new DateTime(value.SettlementDate, TimeOnly.MinValue));

        @object[nameof(value.Name)
                .ToCamelCase()] = new OpenApiString(value.Name);

        @object[nameof(value.Ticker)
                .ToCamelCase()] = new OpenApiString(value.Ticker);

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

        @object[nameof(value.CreatedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.CreatedAt);

        @object[nameof(value.ModifiedAt)
                .ToCamelCase()] = new OpenApiDateTime(value.ModifiedAt);

        return @object;
    }

    public static OpenApiObject ToOpenApiObject(this FutureContractDailyResponse value)
    {
        return MapOpenApiObject(value, new OpenApiObject());
    }

    public static OpenApiObject MapOpenApiObject(this FutureContractDailyResponse value, OpenApiObject @object)
    {
        @object[nameof(value.Id)
                .ToCamelCase()] = new OpenApiString(value.Id.ToString());

        @object[nameof(value.ContractSize)
                .ToCamelCase()] = new OpenApiInteger(value.ContractSize);

        @object[nameof(value.ContractUnit)
                .ToCamelCase()] = new OpenApiString(value.ContractUnit.ToString());

        @object[nameof(value.SettlementDate)
                .ToCamelCase()] = new OpenApiDate(new DateTime(value.SettlementDate, TimeOnly.MinValue));

        @object[nameof(value.Name)
                .ToCamelCase()] = new OpenApiString(value.Name);

        @object[nameof(value.Ticker)
                .ToCamelCase()] = new OpenApiString(value.Ticker);

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
