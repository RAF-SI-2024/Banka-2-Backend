using Bank.Application.Extensions;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Database.Sample;

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bank.UserService.Swagger.SchemeFilters;

file static class Example
{
    public static class Card
    {
        public static readonly CardResponse Response = new()
                                                       {
                                                           Id         = Guid.Parse("987e6543-e21b-45d3-a123-654321abcdef"),
                                                           Number     = "1234-5678-9876-5437",
                                                           Type       = null!,
                                                           Name       = Sample.Card.CreateRequest.Name,
                                                           ExpiresAt  = new(2028, 5, 31),
                                                           Account    = null!,
                                                           CVV        = "123",
                                                           Limit      = Sample.Card.CreateRequest.Limit,
                                                           Status     = Sample.Card.CreateRequest.Status,
                                                           CreatedAt  = DateTime.UtcNow,
                                                           ModifiedAt = DateTime.UtcNow
                                                       };
    }
}

public static partial class SwaggerSchemaFilter
{
    public static class Card
    {
        public class CreateRequest() : SwaggerSchemaFilter<CardCreateRequest>(Sample.Card.CreateRequest)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.CardTypeId)
                            .ToCamelCase()] = new OpenApiString(Example.CardTypeId.ToString()),
                           [nameof(Example.AccountId)
                            .ToCamelCase()] = new OpenApiString(Example.AccountId.ToString()),
                           [nameof(Example.Name)
                            .ToCamelCase()] = new OpenApiString(Example.Name),
                           [nameof(Example.Limit)
                            .ToCamelCase()] = new OpenApiDouble((double)Example.Limit),
                           [nameof(Example.Status)
                            .ToCamelCase()] = new OpenApiBoolean(Example.Status)
                       };
            }
        }

        public class StatusUpdateRequest() : SwaggerSchemaFilter<CardUpdateStatusRequest>(Sample.Card.StatusUpdateRequest)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.Status)
                            .ToCamelCase()] = new OpenApiBoolean(Example.Status)
                       };
            }
        }

        public class LimitUpdateRequest() : SwaggerSchemaFilter<CardUpdateLimitRequest>(Sample.Card.LimitUpdateRequest)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.Limit)
                            .ToCamelCase()] = new OpenApiDouble((double)Example.Limit)
                       };
            }
        }

        public class Response() : SwaggerSchemaFilter<CardResponse>(SchemeFilters.Example.Card.Response)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                var account  = context.SchemaRepository.Schemas[nameof(AccountResponse)].Example;
                var cardType = context.SchemaRepository.Schemas[nameof(CardTypeResponse)].Example;

                return new OpenApiObject()
                       {
                           [nameof(Example.Id)
                            .ToCamelCase()] = new OpenApiString(Example.Id.ToString()),
                           [nameof(Example.Number)
                            .ToCamelCase()] = new OpenApiString(Example.Number),
                           [nameof(Example.Type)
                            .ToCamelCase()] = cardType,
                           [nameof(Example.Name)
                            .ToCamelCase()] = new OpenApiString(Example.Name),
                           [nameof(Example.ExpiresAt)
                            .ToCamelCase()] = new OpenApiDate(new DateTime(Example.ExpiresAt, TimeOnly.MinValue)),
                           [nameof(Example.Account)
                            .ToCamelCase()] = account,
                           [nameof(Example.CVV)
                            .ToCamelCase()] = new OpenApiString(Example.CVV),
                           [nameof(Example.Limit)
                            .ToCamelCase()] = new OpenApiDouble((double)Example.Limit),
                           [nameof(Example.Status)
                            .ToCamelCase()] = new OpenApiBoolean(Example.Status),
                           [nameof(Example.CreatedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.CreatedAt),
                           [nameof(Example.ModifiedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.ModifiedAt)
                       };
            }
        }
    }
}
