using Bank.Application.Extensions;
using Bank.Application.Requests;
using Bank.Application.Responses;

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bank.UserService.Swagger.SchemeFilters;

file static class Example
{
    public static class Card
    {
        public static readonly Guid     Id         = Guid.Parse("987e6543-e21b-45d3-a123-654321abcdef");
        public static readonly Guid     CardTypeId = Guid.Parse("c3d4e5f6-a7b8-1234-5678-9abcdef01234");
        public static readonly Guid     AccountId  = Guid.Parse("d4e5f6a7-b8c9-1234-5678-abcdef012345");
        public const           string   Number     = "1234-5678-9876-5437";
        public const           string   Name       = "Credit Card";
        public static readonly DateOnly ExpiresAt  = new(2028, 5, 31);
        public const           string   Cvv        = "123";
        public const           decimal  Limit      = 5000.00m;
        public const           bool     Status     = true;
        public static readonly DateTime CreatedAt  = new(2024, 5, 2, 14, 12, 23);
        public static readonly DateTime ModifiedAt = new(2025, 1, 3, 11, 45, 0);

        public static readonly CardResponse Response = new()
                                                       {
                                                           Id         = Id,
                                                           Number     = Number,
                                                           Type       = null!,
                                                           Name       = Name,
                                                           ExpiresAt  = ExpiresAt,
                                                           Account    = null!,
                                                           CVV        = Cvv,
                                                           Limit      = Limit,
                                                           Status     = Status,
                                                           CreatedAt  = CreatedAt,
                                                           ModifiedAt = ModifiedAt
                                                       };

        public static readonly CardCreateRequest CreateRequest = new()
                                                                 {
                                                                     CardTypeId = CardTypeId,
                                                                     AccountId  = AccountId,
                                                                     Name       = Name,
                                                                     Limit      = Limit,
                                                                     Status     = Status
                                                                 };

        public static readonly CardStatusUpdateRequest StatusUpdateRequest = new()
                                                                             {
                                                                                 Status = Status
                                                                             };

        public static readonly CardLimitUpdateRequest LimitUpdateRequest = new()
                                                                           {
                                                                               Limit = Limit
                                                                           };
    }
}

public static partial class SwaggerSchemaFilter
{
    public static class Card
    {
        public class CreateRequest() : SwaggerSchemaFilter<CardCreateRequest>(SchemeFilters.Example.Card.CreateRequest)
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

        public class StatusUpdateRequest() : SwaggerSchemaFilter<CardStatusUpdateRequest>(SchemeFilters.Example.Card.StatusUpdateRequest)
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

        public class LimitUpdateRequest() : SwaggerSchemaFilter<CardLimitUpdateRequest>(SchemeFilters.Example.Card.LimitUpdateRequest)
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
