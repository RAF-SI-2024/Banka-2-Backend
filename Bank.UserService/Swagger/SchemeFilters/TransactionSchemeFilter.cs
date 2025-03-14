using Bank.Application.Domain;
using Bank.Application.Extensions;
using Bank.Application.Requests;
using Bank.Application.Responses;

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bank.UserService.Swagger.SchemeFilters;

file static class Example
{
    public static class Transaction
    {
        public static readonly Guid              Id              = Guid.Parse("ddee4d61-3fef-44a9-b186-6e81a87c63a1");
        public static readonly Guid              FromAccountId   = Guid.Parse("c61ff9cf-3632-470c-abdd-a45c2f264bee");
        public static readonly Guid              FromCurrencyId  = Guid.Parse("e9b82215-f306-4df9-96f5-0331d0bccf6b");
        public static readonly Guid              ToCurrencyId    = Guid.Parse("5ab96b3c-fce8-4951-9542-fbbd5c09ea58");
        public const           string            ToAccountNumber = "222000100000123411";
        public const           decimal           Amount          = 100.00M;
        public const           decimal           FromAmount      = 1000.00m;
        public const           decimal           ToAmount        = 950.00m;
        public static readonly Guid              CodeId          = Guid.Parse("18c3d509-cac1-4a7c-b845-b25db0a8cc56");
        public const           string            ReferenceNumber = "117.6926";
        public const           string            Purpose         = "Plaćanje fakture";
        public const           TransactionStatus Status          = TransactionStatus.Completed;
        public static readonly DateTime          CreatedAt       = new(2024, 3, 5, 10, 30, 0);
        public static readonly DateTime          ModifiedAt      = new(2025, 3, 5, 12, 45, 0);

        public static readonly TransactionCreateRequest CreateRequest = new()
                                                                        {
                                                                            FromAccountId   = FromAccountId,
                                                                            FromCurrencyId  = FromCurrencyId,
                                                                            ToAccountNumber = ToAccountNumber,
                                                                            ToCurrencyId    = ToCurrencyId,
                                                                            Amount          = Amount,
                                                                            CodeId          = CodeId,
                                                                            ReferenceNumber = ReferenceNumber,
                                                                            Purpose         = Purpose
                                                                        };

        public static readonly TransactionUpdateRequest UpdateRequest = new()
                                                                        {
                                                                            Status = Status
                                                                        };

        public static readonly TransactionResponse Response = new()
                                                              {
                                                                  Id              = Id,
                                                                  FromAccount     = null!,
                                                                  ToAccount       = null!,
                                                                  FromAmount      = FromAmount,
                                                                  ToAmount        = ToAmount,
                                                                  Code            = null!,
                                                                  ReferenceNumber = ReferenceNumber,
                                                                  Purpose         = Purpose,
                                                                  Status          = Status,
                                                                  CreatedAt       = CreatedAt,
                                                                  ModifiedAt      = ModifiedAt
                                                              };

        public static readonly TransactionCreateResponse CreateResponse = new()
                                                                          {
                                                                              Id              = Id,
                                                                              FromAmount      = FromAmount,
                                                                              Code            = null!,
                                                                              ReferenceNumber = ReferenceNumber,
                                                                              Purpose         = Purpose,
                                                                              Status          = Status,
                                                                              CreatedAt       = CreatedAt,
                                                                              ModifiedAt      = ModifiedAt
                                                                          };
    }
}

public static partial class SwaggerSchemaFilter
{
    public static class Transaction
    {
        public class CreateRequest() : SwaggerSchemaFilter<TransactionCreateRequest>(SchemeFilters.Example.Transaction.CreateRequest)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.FromAccountId)
                            .ToCamelCase()] = new OpenApiString(Example.FromAccountId.ToString()),
                           [nameof(Example.FromCurrencyId)
                            .ToCamelCase()] = new OpenApiString(Example.FromCurrencyId.ToString()),
                           [nameof(Example.ToAccountNumber)
                            .ToCamelCase()] = new OpenApiString(Example.ToAccountNumber),
                           [nameof(Example.ToCurrencyId)
                            .ToCamelCase()] = new OpenApiString(Example.ToCurrencyId.ToString()),
                           [nameof(Example.Amount)
                            .ToCamelCase()] = new OpenApiDouble((double)Example.Amount),
                           [nameof(Example.CodeId)
                            .ToCamelCase()] = new OpenApiString(Example.CodeId.ToString()),
                           [nameof(Example.ReferenceNumber)
                            .ToCamelCase()] = new OpenApiString(Example.ReferenceNumber),
                           [nameof(Example.Purpose)
                            .ToCamelCase()] = new OpenApiString(Example.Purpose)
                       };
            }
        }

        public class UpdateRequest() : SwaggerSchemaFilter<TransactionUpdateRequest>(SchemeFilters.Example.Transaction.UpdateRequest)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.Status)
                            .ToCamelCase()] = new OpenApiString(Example.Status.ToString())
                       };
            }
        }

        public class Response() : SwaggerSchemaFilter<TransactionResponse>(SchemeFilters.Example.Transaction.Response)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                var fromAccount = context.SchemaRepository.Schemas[nameof(AccountSimpleResponse)].Example;
                var toAccount   = context.SchemaRepository.Schemas[nameof(AccountSimpleResponse)].Example;
                var code        = context.SchemaRepository.Schemas[nameof(TransactionCodeResponse)].Example;

                return new OpenApiObject()
                       {
                           [nameof(Example.Id)
                            .ToCamelCase()] = new OpenApiString(Example.Id.ToString()),
                           [nameof(Example.FromAccount)
                            .ToCamelCase()] = fromAccount,
                           [nameof(Example.ToAccount)
                            .ToCamelCase()] = toAccount,
                           [nameof(Example.FromAmount)
                            .ToCamelCase()] = new OpenApiDouble((double)Example.FromAmount),
                           [nameof(Example.ToAmount)
                            .ToCamelCase()] = new OpenApiDouble((double)Example.ToAmount),
                           [nameof(Example.Code)
                            .ToCamelCase()] = code,
                           [nameof(Example.ReferenceNumber)
                            .ToCamelCase()] = new OpenApiString(Example.ReferenceNumber),
                           [nameof(Example.Purpose)
                            .ToCamelCase()] = new OpenApiString(Example.Purpose),
                           [nameof(Example.Status)
                            .ToCamelCase()] = new OpenApiString(Example.Status.ToString()),
                           [nameof(Example.CreatedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.CreatedAt),
                           [nameof(Example.ModifiedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.ModifiedAt)
                       };
            }
        }

        public class CreateResponse() : SwaggerSchemaFilter<TransactionCreateResponse>(SchemeFilters.Example.Transaction.CreateResponse)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                var code = context.SchemaRepository.Schemas[nameof(TransactionCodeResponse)].Example;

                return new OpenApiObject()
                       {
                           [nameof(Example.Id)
                            .ToCamelCase()] = new OpenApiString(Example.Id.ToString()),
                           [nameof(Example.FromAmount)
                            .ToCamelCase()] = new OpenApiDouble((double)Example.FromAmount),
                           [nameof(Example.Code)
                            .ToCamelCase()] = code,
                           [nameof(Example.ReferenceNumber)
                            .ToCamelCase()] = new OpenApiString(Example.ReferenceNumber),
                           [nameof(Example.Purpose)
                            .ToCamelCase()] = new OpenApiString(Example.Purpose),
                           [nameof(Example.Status)
                            .ToCamelCase()] = new OpenApiString(Example.Status.ToString()),
                           [nameof(Example.CreatedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.CreatedAt),
                           [nameof(Example.ModifiedAt)
                            .ToCamelCase()] = new OpenApiDateTime(Example.ModifiedAt)
                       };
            }
        }
    }
}
