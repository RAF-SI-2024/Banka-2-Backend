using Bank.Application.Requests;
using Bank.UserService.Database.Seeders;

namespace Bank.UserService.Test.Examples.Entities;

public static partial class Example
{
    public static partial class Entity
    {
        public static class LoanType
        {
            public static readonly LoanTypeCreateRequest Request       = Database.Examples.Example.LoanType.CreateRequest;
            public static readonly LoanTypeUpdateRequest UpdateRequest = Database.Examples.Example.LoanType.UpdateRequest;
            public static readonly Guid                  Id            = Seeder.LoanType.Personal.Id;
        }
    }
}
