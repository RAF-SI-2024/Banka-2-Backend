using Bank.Application.Requests;
using Bank.UserService.Database.Seeders;

namespace Bank.UserService.Database.Sample;

public static partial class Sample
{
    public static class Company
    {
        public static readonly CompanyCreateRequest CreateRequest = new()
                                                                    {
                                                                        Name                    = "Innovate Tech",
                                                                        RegistrationNumber      = "11345678",
                                                                        TaxIdentificationNumber = "88654321",
                                                                        ActivityCode            = "1234",
                                                                        Address                 = "123 Tech Street, Innovate City",
                                                                        MajorityOwnerId         = Seeder.Client.Client05.Id
                                                                    };

        public static readonly CompanyUpdateRequest UpdateRequest = new()
                                                                    {
                                                                        Name            = "Updated Company Name",
                                                                        ActivityCode    = "4321",
                                                                        Address         = "456 Updated Street, New City",
                                                                        MajorityOwnerId = Seeder.Client.Client01.Id
                                                                    };
    }
}
