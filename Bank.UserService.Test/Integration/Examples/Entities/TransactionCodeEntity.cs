using Bank.UserService.Database.Seeders;
using Bank.UserService.Models;

namespace Bank.UserService.Test.Examples.Entities;

using TransactionCodeModel = TransactionCode;

public static partial class Example
{
    public static partial class Entity
    {
        public static class TransactionCode
        {
            public static readonly TransactionCodeModel GetTransactionCode = new()
                                                                             {
                                                                                 Id         = Seeder.TransactionCode.TransactionCode244.Id,
                                                                                 Code       = Seeder.TransactionCode.TransactionCode244.Code,
                                                                                 Name       = Seeder.TransactionCode.TransactionCode244.Name,
                                                                                 CreatedAt  = Seeder.TransactionCode.TransactionCode244.CreatedAt,
                                                                                 ModifiedAt = Seeder.TransactionCode.TransactionCode244.ModifiedAt
                                                                             };
        }
    }
}
