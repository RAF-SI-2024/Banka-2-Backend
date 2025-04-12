using Bank.Application.Domain;
using Bank.Application.Requests;
using Bank.UserService.Database.Seeders;

namespace Bank.UserService.Database.Sample;

public static partial class Sample
{
    public static class Transaction
    {
        public static readonly TransactionCreateRequest CreateRequest = new()
                                                                        {
                                                                            FromAccountNumber = Seeder.Account.DomesticAccount01.AccountNumber,
                                                                            FromCurrencyId    = Seeder.Currency.SerbianDinar.Id,
                                                                            ToAccountNumber   = Seeder.Account.ForeignAccount01.Number,
                                                                            ToCurrencyId      = Seeder.Currency.Euro.Id,
                                                                            Amount            = 10.05m,
                                                                            CodeId            = Seeder.TransactionCode.TransactionCode224.Id,
                                                                            ReferenceNumber   = "2345454333",
                                                                            Purpose           = "Placanje fakture"
                                                                        };

        public static readonly TransactionUpdateRequest UpdateRequest = new()
                                                                        {
                                                                            Status = TransactionStatus.Failed
                                                                        };
    }
}
