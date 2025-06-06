using Bank.Application.Domain;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Database.Seeders;

namespace Bank.UserService.Database.Examples;

file static class Values
{
    public static readonly Guid    Id         = Guid.Parse("ddee4d61-3fef-44a9-b186-6e81a87c63a1");
    public const           decimal FromAmount = 1000.00m;
    public const           decimal ToAmount   = 950.00m;
}

public static partial class Example
{
    public static class Transaction
    {
        public static readonly TransactionCreateRequest CreateRequest = new()
                                                                        {
                                                                            FromAccountNumber = "222000000000000111", //Seeder.Account.DomesticAccount01.AccountNumber,
                                                                            FromCurrencyId    = Seeder.Currency.SerbianDinar.Id,
                                                                            ToAccountNumber   = "222000000000000331", //Seeder.Account.ForeignAccount01.Number,
                                                                            ToCurrencyId      = Seeder.Currency.Euro.Id,
                                                                            Amount            = 10.05m,
                                                                            CodeId            = Seeder.TransactionCode.TransactionCode289.Id,
                                                                            ReferenceNumber   = "2345454333",
                                                                            Purpose           = "Placanje fakture"
                                                                        };

        public static readonly TransactionUpdateRequest UpdateRequest = new()
                                                                        {
                                                                            Status = TransactionStatus.Failed
                                                                        };

        public static readonly TransactionResponse Response = new()
                                                              {
                                                                  Id              = Values.Id,
                                                                  FromAccount     = null!,
                                                                  ToAccount       = null!,
                                                                  FromAmount      = Values.FromAmount,
                                                                  ToAmount        = Values.ToAmount,
                                                                  Code            = null!,
                                                                  ReferenceNumber = CreateRequest.ReferenceNumber!,
                                                                  Purpose         = CreateRequest.Purpose,
                                                                  Status          = UpdateRequest.Status,
                                                                  CreatedAt       = DateTime.UtcNow,
                                                                  ModifiedAt      = DateTime.UtcNow,
                                                                  FromCurrency    = null!,
                                                                  ToCurrency      = null!
                                                              };

        public static readonly TransactionCreateResponse CreateResponse = new()
                                                                          {
                                                                              Id              = Values.Id,
                                                                              FromAmount      = Values.FromAmount,
                                                                              Code            = null!,
                                                                              ReferenceNumber = CreateRequest.ReferenceNumber!,
                                                                              Purpose         = CreateRequest.Purpose,
                                                                              Status          = UpdateRequest.Status,
                                                                              CreatedAt       = DateTime.UtcNow,
                                                                              ModifiedAt      = DateTime.UtcNow
                                                                          };
    }
}
