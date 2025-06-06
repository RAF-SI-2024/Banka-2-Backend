using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Database.Seeders;

namespace Bank.UserService.Database.Examples;

public static partial class Example
{
    public static class Card
    {
        public static readonly CardCreateRequest CreateRequest = new()
                                                                 {
                                                                     CardTypeId = Seeder.CardType.VisaDebitCard.Id,
                                                                     AccountId  = Seeder.Account.DomesticAccount02.Id,
                                                                     Name       = "Credit Card",
                                                                     Limit      = 5000.00m,
                                                                     Status     = true
                                                                 };

        public static readonly CardUpdateStatusRequest UpdateStatusRequest = new()
                                                                             {
                                                                                 Status = false
                                                                             };

        public static readonly CardUpdateLimitRequest UpdateLimitRequest = new()
                                                                           {
                                                                               Limit = 10000.00m
                                                                           };

        public static readonly CardResponse Response = new()
                                                       {
                                                           Id         = Guid.Parse("987e6543-e21b-45d3-a123-654321abcdef"),
                                                           Number     = "1234-5678-9876-5437",
                                                           Type       = null!,
                                                           Name       = CreateRequest.Name,
                                                           ExpiresAt  = new(2028, 5, 31),
                                                           Account    = null!,
                                                           CVV        = "123",
                                                           Limit      = CreateRequest.Limit,
                                                           Status     = CreateRequest.Status,
                                                           CreatedAt  = DateTime.UtcNow,
                                                           ModifiedAt = DateTime.UtcNow
                                                       };
    }
}
