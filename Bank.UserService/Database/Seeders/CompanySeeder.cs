using Bank.UserService.Models;

namespace Bank.UserService.Database.Seeders;

using CompanyModel = Company;

public static partial class Seeder
{
    public static class Company
    {
        public static readonly CompanyModel Company01 = new()
                                                        {
                                                            Id                      = Guid.Parse("c40849dd-2eec-46ab-a4ba-fddbfa8907fd"),
                                                            Name                    = "Tech Innovators",
                                                            RegistrationNumber      = "12345678",
                                                            TaxIdentificationNumber = "987654321",
                                                            ActivityCode            = "1234",
                                                            Address                 = "123 Tech Street, Innovate City",
                                                            MajorityOwnerId         = Client.Client01.Id,
                                                        };

        public static readonly CompanyModel Company02 = new()
                                                        {
                                                            Id                      = Guid.Parse("f47f1dd2-1ded-4726-b18d-af0d9f5cbb0c"),
                                                            Name                    = "Global Solutions",
                                                            RegistrationNumber      = "23456789",
                                                            TaxIdentificationNumber = "876543210",
                                                            ActivityCode            = "5678",
                                                            Address                 = "456 Global Ave, Solution Town",
                                                            MajorityOwnerId         = Client.Client02.Id,
                                                        };

        public static readonly CompanyModel Company03 = new()
                                                        {
                                                            Id                      = Guid.Parse("6bfecf13-b357-4f7f-991f-f40548274d8b"),
                                                            Name                    = "Creative Ventures",
                                                            RegistrationNumber      = "34567890",
                                                            TaxIdentificationNumber = "765432109",
                                                            ActivityCode            = "2345",
                                                            Address                 = "789 Creative Blvd, Dream City",
                                                            MajorityOwnerId         = Client.Client03.Id,
                                                        };

        public static readonly CompanyModel Company04 = new()
                                                        {
                                                            Id                      = Guid.Parse("dce6e329-dbad-434e-91f0-5da8c3013da6"),
                                                            Name                    = "Future Enterprises",
                                                            RegistrationNumber      = "45678901",
                                                            TaxIdentificationNumber = "654321098",
                                                            ActivityCode            = "3456",
                                                            Address                 = "101 Future Rd, Progress Town",
                                                            MajorityOwnerId         = Client.Client04.Id,
                                                        };
    }
}

public static class CompanySeederExtension
{
    public static async Task SeedCompany(this ApplicationContext context)
    {
        if (context.Companies.Any())
            return;

        await context.Companies.AddRangeAsync([
                                                  Seeder.Company.Company01, Seeder.Company.Company02, Seeder.Company.Company03, Seeder.Company.Company04
                                              ]);

        await context.SaveChangesAsync();
    }
}
