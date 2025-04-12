using Bank.Application.Domain;
using Bank.UserService.Models;

namespace Bank.UserService.Database.Seeders;

using LoanModel = Loan;

public static partial class Seeder
{
    public static class Loan
    {
        public static readonly LoanModel PersonalLoan1 = new()
                                                         {
                                                             Id           = Guid.Parse("f5a74113-8f10-42a3-b130-54c5c691ba8e"),
                                                             TypeId       = Guid.Parse("4632f907-4f31-47f2-8fab-5c8a717aef55"),
                                                             AccountId    = Account.DomesticAccount01.Id,
                                                             Amount       = 10000.00m,
                                                             Period       = 24,
                                                             CreationDate = DateTime.UtcNow,
                                                             MaturityDate = DateTime.UtcNow.AddMonths(24),
                                                             CurrencyId   = Currency.SerbianDinar.Id,
                                                             Status       = LoanStatus.Active,
                                                             InterestType = InterestType.Fixed,
                                                             CreatedAt    = DateTime.UtcNow,
                                                             ModifiedAt   = DateTime.UtcNow
                                                         };

        public static readonly LoanModel MortgageLoan1 = new()
                                                         {
                                                             Id           = Guid.Parse("47782a0e-e03c-4b91-8228-d6fd5a6906f1"),
                                                             TypeId       = Guid.Parse("6302513e-9bf2-4de0-abdf-93bad3e20a35"),
                                                             AccountId    = Account.DomesticAccount01.Id,
                                                             Amount       = 75000.00m,
                                                             Period       = 240,
                                                             CreationDate = DateTime.UtcNow,
                                                             MaturityDate = DateTime.UtcNow.AddMonths(240),
                                                             CurrencyId   = Currency.Euro.Id,
                                                             Status       = LoanStatus.Active,
                                                             InterestType = InterestType.Variable,
                                                             CreatedAt    = DateTime.UtcNow,
                                                             ModifiedAt   = DateTime.UtcNow
                                                         };

        public static readonly LoanModel AutoLoan1 = new()
                                                     {
                                                         Id           = Guid.Parse("b99e8f17-2b07-4742-906f-df62092652d3"),
                                                         TypeId       = Guid.Parse("a5c27bad-e370-48bd-b54d-638a4f450025"),
                                                         AccountId    = Account.DomesticAccount02.Id,
                                                         Amount       = 15000.00m,
                                                         Period       = 60,
                                                         CreationDate = DateTime.UtcNow,
                                                         MaturityDate = DateTime.UtcNow.AddMonths(60),
                                                         CurrencyId   = Currency.SerbianDinar.Id,
                                                         Status       = LoanStatus.Active,
                                                         InterestType = InterestType.Fixed,
                                                         CreatedAt    = DateTime.UtcNow,
                                                         ModifiedAt   = DateTime.UtcNow
                                                     };

        public static readonly LoanModel BusinessLoan1 = new()
                                                         {
                                                             Id           = Guid.Parse("3e37c493-2efb-4312-b76a-c160636b3dc8"),
                                                             TypeId       = Guid.Parse("25ae0a98-8b18-4b3c-be08-2ee97059a305"),
                                                             AccountId    = Account.ForeignAccount02.Id,
                                                             Amount       = 50000.00m,
                                                             Period       = 36,
                                                             CreationDate = DateTime.UtcNow,
                                                             MaturityDate = DateTime.UtcNow.AddMonths(36),
                                                             CurrencyId   = Currency.USDollar.Id,
                                                             Status       = LoanStatus.Active,
                                                             InterestType = InterestType.Variable,
                                                             CreatedAt    = DateTime.UtcNow,
                                                             ModifiedAt   = DateTime.UtcNow
                                                         };

        public static readonly LoanModel StudentLoan1 = new()
                                                        {
                                                            Id           = Guid.Parse("c3e68735-957f-4e06-ac74-fd8f2fac466b"),
                                                            TypeId       = Guid.Parse("cf7caa28-4c66-4e20-a97c-157edf86d8db"),
                                                            AccountId    = Account.ForeignAccount01.Id,
                                                            Amount       = 8000.00m,
                                                            Period       = 84, // 7 years in months
                                                            CreationDate = DateTime.UtcNow,
                                                            MaturityDate = DateTime.UtcNow.AddMonths(84),
                                                            CurrencyId   = Currency.Euro.Id,
                                                            Status       = LoanStatus.Active,
                                                            InterestType = InterestType.Fixed,
                                                            CreatedAt    = DateTime.UtcNow,
                                                            ModifiedAt   = DateTime.UtcNow
                                                        };
    }
}

public static class LoanSeederExtension
{
    public static async Task SeedLoans(this ApplicationContext context)
    {
        if (context.Loans.Any())
            return;

        await context.Loans.AddRangeAsync(Seeder.Loan.PersonalLoan1, Seeder.Loan.MortgageLoan1, Seeder.Loan.AutoLoan1, Seeder.Loan.BusinessLoan1, Seeder.Loan.StudentLoan1);

        await context.SaveChangesAsync();
    }
}
