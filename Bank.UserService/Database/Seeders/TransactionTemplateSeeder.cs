using Bank.UserService.Models;

namespace Bank.UserService.Database.Seeders;

using TransactionTemplateModel = TransactionTemplate;

public static partial class Seeder
{
    public static class TransactionTemplate
    {
        public static readonly TransactionTemplateModel TransactionTemplate01 = new()
                                                                                {
                                                                                    Id            = Guid.Parse("6b700aed-6f16-40ce-a4b9-bdf9715b9c2e"),
                                                                                    ClientId      = Client.Client01.Id,
                                                                                    Name          = $"{Client.Client02.FirstName} {Currency.SerbianDinar.Code}",
                                                                                    AccountNumber = "12345",
                                                                                    Deleted       = false,
                                                                                    CreatedAt     = DateTime.UtcNow,
                                                                                    ModifiedAt    = DateTime.UtcNow
                                                                                };

        public static readonly TransactionTemplateModel TransactionTemplate02 = new()
                                                                                {
                                                                                    Id            = Guid.Parse("0b8d3099-7b5b-41ee-be26-11c2bc76320f"),
                                                                                    ClientId      = Client.Client02.Id,
                                                                                    Name          = $"{Client.Client03.FirstName} {Currency.Euro.Code}",
                                                                                    AccountNumber = "54321",
                                                                                    Deleted       = false,
                                                                                    CreatedAt     = DateTime.UtcNow,
                                                                                    ModifiedAt    = DateTime.UtcNow
                                                                                };
    }
}

public static class TransactionTemplateSeederExtension
{
    public static async Task SeedTransactionTemplate(this ApplicationContext context)
    {
        if (context.TransactionTemplates.Any())
            return;

        await context.TransactionTemplates.AddRangeAsync([
                                                             Seeder.TransactionTemplate.TransactionTemplate01, Seeder.TransactionTemplate.TransactionTemplate02
                                                         ]);

        await context.SaveChangesAsync();
    }
}
