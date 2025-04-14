using Bank.Application.Requests;
using Bank.Application.Responses;

namespace Bank.OpenApi.Examples;

public static partial class Example
{
    public static class Company
    {
        public static readonly CompanyCreateRequest DefaultCreateRequest = new()
                                                                           {
                                                                               Name                    = Constant.CompanyName,
                                                                               RegistrationNumber      = Constant.UniqueIdentificationNumber,
                                                                               TaxIdentificationNumber = Constant.UniqueIdentificationNumber,
                                                                               ActivityCode            = Constant.CompanyActivityCode,
                                                                               Address                 = Constant.Address,
                                                                               MajorityOwnerId         = Constant.Id,
                                                                           };

        public static readonly CompanyUpdateRequest DefaultUpdateRequest = new()
                                                                           {
                                                                               Name            = Constant.CompanyName,
                                                                               ActivityCode    = Constant.CompanyActivityCode,
                                                                               Address         = Constant.Address,
                                                                               MajorityOwnerId = Constant.Id,
                                                                           };

        public static readonly CompanyResponse DefaultResponse = new()
                                                                 {
                                                                     Id                      = Constant.Id,
                                                                     Name                    = Constant.CompanyName,
                                                                     RegistrationNumber      = Constant.UniqueIdentificationNumber,
                                                                     TaxIdentificationNumber = Constant.UniqueIdentificationNumber,
                                                                     ActivityCode            = Constant.CompanyActivityCode,
                                                                     Address                 = Constant.Address,
                                                                     MajorityOwner           = User.DefaultSimpleResponse,
                                                                 };

        public static readonly CompanySimpleResponse DefaultSimpleResponse = new()
                                                                             {
                                                                                 Id                      = Constant.Id,
                                                                                 Name                    = Constant.CompanyName,
                                                                                 RegistrationNumber      = Constant.UniqueIdentificationNumber,
                                                                                 TaxIdentificationNumber = Constant.UniqueIdentificationNumber,
                                                                                 ActivityCode            = Constant.CompanyActivityCode,
                                                                                 Address                 = Constant.Address,
                                                                             };
    }
}
