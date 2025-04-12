using Bank.Application.Requests;
using Bank.UserService.Database.Sample;
using Bank.UserService.Database.Seeders;

namespace Bank.UserService.Test.Examples.Entities;

public static partial class Example
{
    public static partial class Entity
    {
        public static class Account
        {
            public static readonly AccountCreateRequest CreateRequest = Sample.Account.CreateRequest;

            public static readonly AccountUpdateClientRequest UpdateClientRequest = Sample.Account.UpdateClientRequest;

            public static readonly AccountUpdateEmployeeRequest UpdateEmployeeRequest = Sample.Account.UpdateEmployeeRequest;

            public static readonly Guid AccountId = Seeder.Account.ForeignAccount02.Id;
        }
    }
}
