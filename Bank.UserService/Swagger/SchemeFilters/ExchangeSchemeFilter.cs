using Bank.Application.Extensions;
using Bank.Application.Requests;
using Bank.Application.Responses;

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bank.UserService.Swagger.SchemeFilters;

file static class Example
{
    public static class Exchange
    {
        public static readonly Guid     Id               = Guid.Parse("dba783b1-38b9-4537-8806-d2b00864019a");
        public const           string   CurrencyFromCode = "USD";
        public const           string   CurrencyToCode   = "EUR";
        public static readonly Guid     CurrencyFromId   = Guid.Parse("1874a160-fdb6-47d1-80cf-8f586d48474b");
        public static readonly Guid     CurrencyToId     = Guid.Parse("31446197-7e28-4915-b7ad-09a9d1ad4d3b");
        public const           decimal  Amount           = 1000.00m;
        public const           decimal  Commission       = 5.00m;
        public const           decimal  Rate             = 0.85m;
        public const           decimal  InverseRate      = 1.18m;
        public static readonly Guid     AccountId        = Guid.Parse("0c868018-1ff0-4b06-b715-f209e2f1d632");
        public static readonly DateTime CreatedAt        = new(2024, 3, 5, 10, 30, 0);
        public static readonly DateTime ModifiedAt       = new(2025, 3, 5, 12, 45, 0);

        public static readonly ExchangeMakeExchangeRequest MakeExchangeRequest = new()
                                                                                 {
                                                                                     CurrencyFromId = CurrencyFromId,
                                                                                     CurrencyToId   = CurrencyToId,
                                                                                     Amount         = Amount,
                                                                                     AccountId      = AccountId
                                                                                 };

        public static readonly ExchangeUpdateRequest UpdateRequest = new()
                                                                     {
                                                                         Commission = Commission
                                                                     };

        public static readonly ExchangeBetweenRequest BetweenRequest = new()
                                                                       {
                                                                           CurrencyFromCode = CurrencyFromCode,
                                                                           CurrencyToCode   = CurrencyToCode
                                                                       };

        public static readonly ExchangeResponse Response = new()
                                                           {
                                                               Id           = Id,
                                                               CurrencyFrom = null!,
                                                               CurrencyTo   = null!,
                                                               Commission   = Commission,
                                                               Rate         = Rate,
                                                               InverseRate  = InverseRate,
                                                               CreatedAt    = CreatedAt,
                                                               ModifiedAt   = ModifiedAt
                                                           };
    }
}

public static partial class SwaggerSchemaFilter
{
    public static class Exchange
    {
        public class MakeExchangeRequest() : SwaggerSchemaFilter<ExchangeMakeExchangeRequest>(SchemeFilters.Example.Exchange.MakeExchangeRequest)
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

        public class UpdateRequest() : SwaggerSchemaFilter<ExchangeUpdateRequest>(SchemeFilters.Example.Exchange.UpdateRequest)
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

        public class BetweenRequest() : SwaggerSchemaFilter<ExchangeBetweenRequest>(SchemeFilters.Example.Exchange.BetweenRequest)
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
