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
    public static class Transaction
    {
        public static readonly Guid    Id         = Guid.Parse("ddee4d61-3fef-44a9-b186-6e81a87c63a1");
        public const           decimal FromAmount = 1000.00m;
        public const           decimal ToAmount   = 950.00m;

        public static readonly TransactionResponse Response = new()
                                                              {
                                                                  Id              = Guid.Parse("ddee4d61-3fef-44a9-b186-6e81a87c63a1"),
                                                                  FromAccount     = null!,
                                                                  ToAccount       = null!,
                                                                  FromAmount      = FromAmount,
                                                                  ToAmount        = ToAmount,
                                                                  Code            = null!,
                                                                  ReferenceNumber = Sample.Transaction.CreateRequest.ReferenceNumber!,
                                                                  Purpose         = Sample.Transaction.CreateRequest.Purpose,
                                                                  Status          = Sample.Transaction.UpdateRequest.Status,
                                                                  CreatedAt       = DateTime.UtcNow,
                                                                  ModifiedAt      = DateTime.UtcNow,
                                                                  FromCurrency    = null!,
                                                                  ToCurrency      = null!
                                                              };

        public static readonly TransactionCreateResponse CreateResponse = new()
                                                                          {
                                                                              Id              = Guid.Parse("ddee4d61-3fef-44a9-b186-6e81a87c63a1"),
                                                                              FromAmount      = FromAmount,
                                                                              Code            = null!,
                                                                              ReferenceNumber = Sample.Transaction.CreateRequest.ReferenceNumber!,
                                                                              Purpose         = Sample.Transaction.CreateRequest.Purpose,
                                                                              Status          = Sample.Transaction.UpdateRequest.Status,
                                                                              CreatedAt       = DateTime.UtcNow,
                                                                              ModifiedAt      = DateTime.UtcNow
                                                                          };
    }
}

public static partial class SwaggerSchemaFilter
{
    public static class Transaction
    {
        public class CreateRequest() : SwaggerSchemaFilter<TransactionCreateRequest>(Sample.Transaction.CreateRequest)
        {
            protected override IOpenApiAny CreateExample(OpenApiSchema schema, SchemaFilterContext context)
            {
                return new OpenApiObject()
                       {
                           [nameof(Example.FromAccountNumber)
                            .ToCamelCase()] = new OpenApiString(Example.FromAccountNumber),
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

        public class UpdateRequest() : SwaggerSchemaFilter<TransactionUpdateRequest>(Sample.Transaction.UpdateRequest)
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
