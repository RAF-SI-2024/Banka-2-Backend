using Bank.Application.Requests;
using Bank.UserService.Database.Seeders;

namespace Bank.UserService.Database.Sample;

public static partial class Sample
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

        public static readonly CardUpdateStatusRequest StatusUpdateRequest = new()
                                                                             {
                                                                                 Status = false
                                                                             };

        public static readonly CardUpdateLimitRequest LimitUpdateRequest = new()
                                                                           {
                                                                               Limit = 10000.00m
                                                                           };
    }
}
