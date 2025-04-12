using Bank.Application.Requests;
using Bank.UserService.Database.Sample;
using Bank.UserService.Database.Seeders;

namespace Bank.UserService.Test.Examples.Entities;

public static partial class Example
{
    public static partial class Entity
    {
        public static class Client
        {
            public static readonly ClientCreateRequest CreateRequest = Sample.Client.CreateRequest;

            public static readonly ClientUpdateRequest UpdateRequest = Sample.Client.UpdateRequest;

            public static readonly Guid Id1 = Seeder.Client.Bank.Id;

            public static readonly Guid Id2 = Seeder.Client.Client01.Id;
        }
    }
}
