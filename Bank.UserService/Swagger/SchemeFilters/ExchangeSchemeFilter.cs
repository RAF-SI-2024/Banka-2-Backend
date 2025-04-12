using Bank.Application.Extensions;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Database.Sample;

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bank.UserService.Swagger.SchemeFilters;

file static class Example
{
    public static class Exchange
    {
        public static readonly ExchangeResponse Response = new()
                                                           {
                                                               Id           = Guid.Parse("dba783b1-38b9-4537-8806-d2b00864019a"),
                                                               CurrencyFrom = null!,
                                                               CurrencyTo   = null!,
                                                               Commission   = Sample.Exchange.UpdateRequest.Commission,
                                                               Rate         = 0.85m,
                                                               InverseRate  = 1.18m,
                                                               CreatedAt    = DateTime.UtcNow,
                                                               ModifiedAt   = DateTime.UtcNow
                                                           };
    }
}

public static partial class SwaggerSchemaFilter
{
    public static class Exchange
    {
        public class MakeExchangeRequest() : SwaggerSchemaFilter<ExchangeMakeExchangeRequest>(Sample.Exchange.MakeExchangeRequest)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.CurrencyFromId)
                            .ToCamelCase()] = new OpenApiString(Example.CurrencyFromId.ToString()),
                           [nameof(Example.CurrencyToId)
                            .ToCamelCase()] = new OpenApiString(Example.CurrencyToId.ToString()),
                           [nameof(Example.Amount)
                            .ToCamelCase()] = new OpenApiDouble((double)Example.Amount),
                           [nameof(Example.AccountId)
                            .ToCamelCase()] = new OpenApiString(Example.AccountId.ToString())
                       };
            }
        }

        public class UpdateRequest() : SwaggerSchemaFilter<ExchangeUpdateRequest>(Sample.Exchange.UpdateRequest)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.Commission)
                            .ToCamelCase()] = new OpenApiDouble((double)Example.Commission)
                       };
            }
        }

        public class BetweenRequest() : SwaggerSchemaFilter<ExchangeBetweenQuery>(Sample.Exchange.BetweenQuery)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.CurrencyFromCode)
                            .ToCamelCase()] = new OpenApiString(Example.CurrencyFromCode),
                           [nameof(Example.CurrencyToCode)
                            .ToCamelCase()] = new OpenApiString(Example.CurrencyToCode)
                       };
            }
        }

        public class Response() : SwaggerSchemaFilter<ExchangeResponse>(SchemeFilters.Example.Exchange.Response)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                var currencyFrom = context.SchemaRepository.Schemas[nameof(CurrencySimpleResponse)].Example;
                var currencyTo   = context.SchemaRepository.Schemas[nameof(CurrencySimpleResponse)].Example;

                return new OpenApiObject()
                       {
                           [nameof(Example.Id)
                            .ToCamelCase()] = new OpenApiString(Example.Id.ToString()),
                           [nameof(Example.CurrencyFrom)
                            .ToCamelCase()] = currencyFrom,
                           [nameof(Example.CurrencyTo)
                            .ToCamelCase()] = currencyTo,
                           [nameof(Example.Commission)
                            .ToCamelCase()] = new OpenApiDouble((double)Example.Commission),
                           [nameof(Example.Rate)
                            .ToCamelCase()] = new OpenApiDouble((double)Example.Rate),
                           [nameof(Example.InverseRate)
                            .ToCamelCase()] = new OpenApiDouble((double)Example.InverseRate),
                           [nameof(Example.CreatedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.CreatedAt),
                           [nameof(Example.ModifiedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.ModifiedAt)
                       };
            }
        }
    }
}
