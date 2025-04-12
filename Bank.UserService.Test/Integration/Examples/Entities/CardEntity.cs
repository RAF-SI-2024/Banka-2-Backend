using Bank.Application.Requests;
using Bank.UserService.Database.Sample;
using Bank.UserService.Database.Seeders;

namespace Bank.UserService.Test.Examples.Entities;

public static partial class Example
{
    public static partial class Entity
    {
        public static class Card
        {
            public static readonly CardCreateRequest CreateRequest = Sample.Card.CreateRequest;

            public static readonly CardUpdateStatusRequest StatusUpdateRequest = Sample.Card.StatusUpdateRequest;

            public static readonly CardUpdateLimitRequest LimitUpdateRequest = Sample.Card.LimitUpdateRequest;

            public static readonly Guid Id = Seeder.Card.Card03.Id;
        }
    }
}
