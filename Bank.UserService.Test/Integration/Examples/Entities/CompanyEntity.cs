using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Database.Sample;
using Bank.UserService.Mappers;

using Seeder = Bank.UserService.Database.Seeders.Seeder;

namespace Bank.UserService.Test.Examples.Entities;

public static partial class Example
{
    public static partial class Entity
    {
        public static class Company
        {
            public static readonly CompanyCreateRequest CreateRequest = Sample.Company.CreateRequest;

            public static readonly CompanyResponse CompanyResponse = new()
                                                                     {
                                                                         Id                      = Seeder.Company.Company01.Id,
                                                                         Name                    = Seeder.Company.Company01.Name,
                                                                         RegistrationNumber      = Seeder.Company.Company01.RegistrationNumber,
                                                                         TaxIdentificationNumber = Seeder.Company.Company01.TaxIdentificationNumber,
                                                                         ActivityCode            = Seeder.Company.Company01.ActivityCode,
                                                                         Address                 = Seeder.Company.Company01.Address,
                                                                         MajorityOwner           = Seeder.Company.Company01.MajorityOwner?.ToSimpleResponse()
                                                                     };

            public static readonly CompanyResponse CompanyResponse2 = new()
                                                                      {
                                                                          Id                      = Seeder.Company.Company02.Id,
                                                                          Name                    = Seeder.Company.Company02.Name,
                                                                          RegistrationNumber      = Seeder.Company.Company02.RegistrationNumber,
                                                                          TaxIdentificationNumber = Seeder.Company.Company02.TaxIdentificationNumber,
                                                                          ActivityCode            = Seeder.Company.Company02.ActivityCode,
                                                                          Address                 = Seeder.Company.Company02.Address,
                                                                          MajorityOwner           = Seeder.Company.Company02.MajorityOwner?.ToSimpleResponse()
                                                                      };

            public static readonly CompanyUpdateRequest UpdateRequest = Sample.Company.UpdateRequest;

            public static readonly CompanyFilterQuery CompanyFilterQuery = new()
                                                                           {
                                                                               Name                    = "",
                                                                               RegistrationNumber      = "",
                                                                               TaxIdentificationNumber = ""
                                                                           };

            public static readonly Pageable Pageable = new()
                                                       {
                                                           Page = 1,
                                                           Size = 10
                                                       };
        }
    }
}
