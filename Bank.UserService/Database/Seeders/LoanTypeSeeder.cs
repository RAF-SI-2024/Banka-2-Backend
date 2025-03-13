using Bank.UserService.Database;
using Bank.UserService.Models;

namespace Bank.LoanService.Database.Seeders;

using LoanTypeModel = LoanType;

public static partial class Seeder
{
    public static class LoanType
    {
        public static readonly LoanTypeModel Personal = new()
                                                        {
                                                            Id         = Guid.Parse("4632f907-4f31-47f2-8fab-5c8a717aef55"),
                                                            Name       = "Personal Loan",
                                                            Margin     = 3.5m,
                                                            CreatedAt  = DateTime.UtcNow,
                                                            ModifiedAt = DateTime.UtcNow,
                                                        };

        public static readonly LoanTypeModel Mortgage = new()
                                                        {
                                                            Id         = Guid.Parse("6302513e-9bf2-4de0-abdf-93bad3e20a35"),
                                                            Name       = "Mortgage Loan",
                                                            Margin     = 2.0m,
                                                            CreatedAt  = DateTime.UtcNow,
                                                            ModifiedAt = DateTime.UtcNow,
                                                        };

        public static readonly LoanTypeModel AutoLoan = new()
                                                        {
                                                            Id         = Guid.Parse("a5c27bad-e370-48bd-b54d-638a4f450025"),
                                                            Name       = "Auto Loan",
                                                            Margin     = 4.25m,
                                                            CreatedAt  = DateTime.UtcNow,
                                                            ModifiedAt = DateTime.UtcNow,
                                                        };

        public static readonly LoanTypeModel StudentLoan = new()
                                                           {
                                                               Id         = Guid.Parse("cf7caa28-4c66-4e20-a97c-157edf86d8db"),
                                                               Name       = "Student Loan",
                                                               Margin     = 1.75m,
                                                               CreatedAt  = DateTime.UtcNow,
                                                               ModifiedAt = DateTime.UtcNow,
                                                           };

        public static readonly LoanTypeModel BusinessLoan = new()
                                                            {
                                                                Id         = Guid.Parse("25ae0a98-8b18-4b3c-be08-2ee97059a305"),
                                                                Name       = "Business Loan",
                                                                Margin     = 5.0m,
                                                                CreatedAt  = DateTime.UtcNow,
                                                                ModifiedAt = DateTime.UtcNow,
                                                            };
    }
}

public static class LoanTypeSeederExtension
{
    public static async Task SeedLoanTypes(this ApplicationContext context)
    {
        if (context.LoanTypes.Any())
            return;

        await context.LoanTypes.AddRangeAsync([
                                                  Seeder.LoanType.Personal,
                                                  Seeder.LoanType.Mortgage,
                                                  Seeder.LoanType.AutoLoan,
                                                  Seeder.LoanType.StudentLoan,
                                                  Seeder.LoanType.BusinessLoan
                                              ]);

        await context.SaveChangesAsync();
    }
}
