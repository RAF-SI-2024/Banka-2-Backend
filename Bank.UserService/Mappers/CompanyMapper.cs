using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Models;

namespace Bank.UserService.Mappers;

public static class CompanyMapper
{
    public static CompanyResponse ToResponse(this Company company)
    {
        return new CompanyResponse
               {
                   Id                      = company.Id,
                   Name                    = company.Name,
                   RegistrationNumber      = company.RegistrationNumber,
                   TaxIdentificationNumber = company.TaxIdentificationNumber,
                   ActivityCode            = company.ActivityCode,
                   Address                 = company.Address,
                   MajorityOwner           = company.MajorityOwner?.ToSimpleResponse()
               };
    }

    public static CompanySimpleResponse ToSimpleResponse(this Company company)
    {
        return new CompanySimpleResponse
               {
                   Id                      = company.Id,
                   Name                    = company.Name,
                   RegistrationNumber      = company.RegistrationNumber,
                   TaxIdentificationNumber = company.TaxIdentificationNumber,
                   ActivityCode            = company.ActivityCode,
                   Address                 = company.Address
               };
    }

    public static Company ToCompany(this CompanyCreateRequest companyCreateRequest, User majorityOwner)
    {
        return new Company
               {
                   Id                      = Guid.NewGuid(),
                   Name                    = companyCreateRequest.Name,
                   RegistrationNumber      = companyCreateRequest.RegistrationNumber,
                   TaxIdentificationNumber = companyCreateRequest.TaxIdentificationNumber,
                   ActivityCode            = companyCreateRequest.ActivityCode,
                   Address                 = companyCreateRequest.Address,
                   MajorityOwnerId         = companyCreateRequest.MajorityOwnerId,
                   MajorityOwner           = majorityOwner,
               };
    }

    public static Company ToCompany(this CompanyUpdateRequest companyUpdateRequest, Company oldCompany, User majorityOwner)
    {
        return new Company
               {
                   Id                      = oldCompany.Id,
                   Name                    = companyUpdateRequest.Name,
                   RegistrationNumber      = oldCompany.RegistrationNumber,
                   TaxIdentificationNumber = oldCompany.TaxIdentificationNumber,
                   ActivityCode            = companyUpdateRequest.ActivityCode,
                   Address                 = companyUpdateRequest.Address,
                   MajorityOwnerId         = companyUpdateRequest.MajorityOwnerId,
                   MajorityOwner           = majorityOwner
               };
    }
}
