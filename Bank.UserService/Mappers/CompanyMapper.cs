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

    public static Company ToCompany(this CompanyCreateRequest companyCreateRequest)
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
               };
    }

    public static Company Update(this Company company, CompanyUpdateRequest updateRequest)
    {
        company.Name            = updateRequest.Name;
        company.ActivityCode    = updateRequest.ActivityCode;
        company.Address         = updateRequest.Address;
        company.MajorityOwnerId = updateRequest.MajorityOwnerId;

        return company;
    }
}
