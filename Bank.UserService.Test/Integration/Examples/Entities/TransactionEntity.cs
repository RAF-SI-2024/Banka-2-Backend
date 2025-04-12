using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.UserService.Database.Sample;
using Bank.UserService.Database.Seeders;

namespace Bank.UserService.Test.Examples.Entities;

public static partial class Example
{
    public static partial class Entity
    {
        public static class Transaction
        {
            public static readonly TransactionCreateRequest CreateRequest = Sample.Transaction.CreateRequest;

            public static readonly TransactionUpdateRequest UpdateRequest = Sample.Transaction.UpdateRequest;

            public static readonly Guid TransactionId = Seeder.Transaction.Transaction01.Id;

            public static readonly Guid AccountId = Seeder.Account.DomesticAccount01.Id;

            public static readonly TransactionFilterQuery FilterQuery = new() { };

            public static readonly Pageable Pageable = new()
                                                       {
                                                           Page = 1,
                                                           Size = 10
                                                       };
        }
    }
}
