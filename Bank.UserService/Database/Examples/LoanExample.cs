using Bank.Application.Domain;
using Bank.Application.Requests;
using Bank.Application.Responses;

using Seeder = Bank.UserService.Database.Seeders.Seeder;

namespace Bank.UserService.Database.Examples;

public static partial class Example
{
    public static class Loan
    {
        public static readonly LoanCreateRequest CreateRequest = new()
                                                                 {
                                                                     TypeId       = Seeder.LoanType.Personal.Id,
                                                                     AccountId    = Seeder.Account.DomesticAccount02.Id,
                                                                     Amount       = 50000.00m,
                                                                     Period       = 60,
                                                                     CurrencyId   = Seeder.Currency.SerbianDinar.Id,
                                                                     InterestType = InterestType.Mixed
                                                                 };

        public static readonly LoanUpdateRequest UpdateRequest = new()
                                                                 {
                                                                     Status       = LoanStatus.Closed,
                                                                     MaturityDate = new(2029, 3, 5)
                                                                 };

        public static readonly LoanResponse Response = new()
                                                       {
                                                           Id           = Guid.Parse("90a10f93-85cc-491a-8624-07c485a2b431"),
                                                           Type         = null!,
                                                           Account      = null!,
                                                           Amount       = CreateRequest.Amount,
                                                           Period       = CreateRequest.Period,
                                                           CreationDate = new(2024, 3, 5),
                                                           MaturityDate = new(2029, 3, 5),
                                                           Currency     = null!,
                                                           Status       = LoanStatus.Active,
                                                           InterestType = CreateRequest.InterestType,
                                                           CreatedAt    = DateTime.UtcNow,
                                                           ModifiedAt   = DateTime.UtcNow
                                                       };
    }
}
