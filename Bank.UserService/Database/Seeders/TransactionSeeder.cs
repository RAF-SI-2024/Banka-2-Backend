using System.Collections.Immutable;

using Bank.Application.Domain;
using Bank.UserService.Models;

namespace Bank.UserService.Database.Seeders;

using TransactionModel = Transaction;

public static partial class Seeder
{
    public static class Transaction
    {
        public static readonly TransactionModel Transaction01 = new()
                                                                {
                                                                    Id              = Guid.Parse("08fcb416-23f9-43e4-87fd-c0e8efc4e98e"),
                                                                    FromAccountId   = Account.DomesticAccount01.Id,
                                                                    FromCurrencyId  = Currency.SerbianDinar.Id,
                                                                    FromAmount      = 3000,
                                                                    ToAccountId     = Account.DomesticAccount02.Id,
                                                                    ToCurrencyId    = Currency.SerbianDinar.Id,
                                                                    ToAmount        = 3000,
                                                                    CodeId          = TransactionCode.TransactionCode289.Id,
                                                                    ReferenceNumber = "12345678",
                                                                    Purpose         = "I want more money",
                                                                    Status          = TransactionStatus.Completed,
                                                                    CreatedAt       = DateTime.UtcNow,
                                                                    ModifiedAt      = DateTime.UtcNow,
                                                                };

        public static readonly TransactionModel Transaction02 = new()
                                                                {
                                                                    Id              = Guid.Parse("69b5d21f-3a95-488b-be32-79b48f6c0791"),
                                                                    FromAccountId   = Account.ForeignAccount01.Id,
                                                                    FromCurrencyId  = Currency.Euro.Id,
                                                                    FromAmount      = 40,
                                                                    ToAccountId     = Account.ForeignAccount02.Id,
                                                                    ToCurrencyId    = Currency.Euro.Id,
                                                                    ToAmount        = 40,
                                                                    CodeId          = TransactionCode.TransactionCode244.Id,
                                                                    ReferenceNumber = "1234567",
                                                                    Purpose         = "I want more and more money",
                                                                    Status          = TransactionStatus.Completed,
                                                                    CreatedAt       = DateTime.UtcNow,
                                                                    ModifiedAt      = DateTime.UtcNow,
                                                                };

        public static readonly ImmutableArray<TransactionModel> All =
        [
            Transaction01, Transaction02
        ];
    }
}
