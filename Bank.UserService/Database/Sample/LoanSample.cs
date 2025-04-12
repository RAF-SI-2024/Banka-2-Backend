using Bank.Application.Domain;
using Bank.Application.Requests;

using Seeder = Bank.UserService.Database.Seeders.Seeder;

namespace Bank.UserService.Database.Sample;

public static partial class Sample
{
    public static class Loan
    {
        public static readonly LoanCreateRequest Request = new()
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
    }
}
