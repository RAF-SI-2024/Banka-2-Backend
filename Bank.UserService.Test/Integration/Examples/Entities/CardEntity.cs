using Bank.Application.Requests;
using Bank.UserService.Database.Seeders;

namespace Bank.UserService.Test.Examples.Entities;

public static partial class Example
{
    public static partial class Entity
    {
        public static class Card
        {
            public static readonly CardCreateRequest CreateRequest = Database.Examples.Example.Card.CreateRequest;

            public static readonly CardUpdateStatusRequest StatusUpdateRequest = Database.Examples.Example.Card.UpdateStatusRequest;

            public static readonly CardUpdateLimitRequest LimitUpdateRequest = Database.Examples.Example.Card.UpdateLimitRequest;

            public static readonly Guid Id = Seeder.Card.Card03.Id;
        }
    }
}
