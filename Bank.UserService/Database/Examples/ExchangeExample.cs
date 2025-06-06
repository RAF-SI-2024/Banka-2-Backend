using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Database.Seeders;

namespace Bank.UserService.Database.Examples;

public static partial class Example
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

        public static readonly ExchangeResponse Response = new()
                                                           {
                                                               Id           = Guid.Parse("dba783b1-38b9-4537-8806-d2b00864019a"),
                                                               CurrencyFrom = null!,
                                                               CurrencyTo   = null!,
                                                               Commission   = UpdateRequest.Commission,
                                                               Rate         = 0.85m,
                                                               InverseRate  = 1.18m,
                                                               CreatedAt    = DateTime.UtcNow,
                                                               ModifiedAt   = DateTime.UtcNow
                                                           };
    }
}
