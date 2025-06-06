using Bank.Application.Requests;
using Bank.UserService.Database.Seeders;

namespace Bank.UserService.Test.Examples.Entities;

public static partial class Example
{
    public static partial class Entity
    {
        public static class AccountCurrency
        {
            public static readonly AccountCurrencyCreateRequest CreateRequest = Database.Examples.Example.AccountCurrency.CreateRequest;

            public static readonly AccountCurrencyClientUpdateRequest ClientUpdateRequest = Database.Examples.Example.AccountCurrency.ClientUpdateRequest;

            public static readonly Guid AccountCurrencyId = Seeder.AccountCurrency.BankEuro.Id;

            public static readonly Guid EmployeeId = Seeder.Employee.Employee01.Id;

            public static readonly Permissions.Domain.Permissions Permissions = Seeder.Employee.Employee01.Permissions;
        }
    }
}
