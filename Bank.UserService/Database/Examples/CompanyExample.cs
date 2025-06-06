using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Database.Seeders;

namespace Bank.UserService.Database.Examples;

public static partial class Example
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

        public static readonly CompanyResponse Response = new()
                                                          {
                                                              Id                      = Guid.Parse("8a7b9f2d-4c8a-4b1d-b1cd-987654321abc"),
                                                              Name                    = CreateRequest.Name,
                                                              RegistrationNumber      = CreateRequest.RegistrationNumber,
                                                              TaxIdentificationNumber = CreateRequest.TaxIdentificationNumber,
                                                              ActivityCode            = CreateRequest.ActivityCode,
                                                              Address                 = CreateRequest.Address,
                                                              MajorityOwner           = null!
                                                          };

        public static readonly CompanySimpleResponse SimpleResponse = new()
                                                                      {
                                                                          Id                      = Guid.Parse("8a7b9f2d-4c8a-4b1d-b1cd-987654321abc"),
                                                                          Name                    = CreateRequest.Name,
                                                                          RegistrationNumber      = CreateRequest.RegistrationNumber,
                                                                          TaxIdentificationNumber = CreateRequest.TaxIdentificationNumber,
                                                                          ActivityCode            = CreateRequest.ActivityCode,
                                                                          Address                 = CreateRequest.Address
                                                                      };
    }
}
