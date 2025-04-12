using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.UserService.Database.Seeders;

namespace Bank.UserService.Database.Sample;

public static partial class Sample
{
    public static class Exchange
    {
        public static readonly ExchangeMakeExchangeRequest MakeExchangeRequest = new()
                                                                                 {
                                                                                     CurrencyFromId = Seeder.Currency.SerbianDinar.Id,
                                                                                     CurrencyToId   = Seeder.Currency.Euro.Id,
                                                                                     Amount         = 100,
                                                                                     AccountId      = Seeder.Account.DomesticAccount01.Id
                                                                                 };

        public static readonly ExchangeUpdateRequest UpdateRequest = new()
                                                                     {
                                                                         Commission = 0.07m,
                                                                     };

        public static readonly ExchangeBetweenQuery BetweenQuery = new()
                                                                   {
                                                                       CurrencyFromCode = "DIN",
                                                                       CurrencyToCode   = "EUR"
                                                                   };
    }
}
