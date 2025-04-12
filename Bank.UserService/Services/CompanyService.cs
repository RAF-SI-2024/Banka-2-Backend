using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Mappers;
using Bank.UserService.Repositories;

namespace Bank.UserService.Services;

public interface ICompanyService
{
    Task<Result<Page<CompanyResponse>>> GetAll(CompanyFilterQuery companyFilterQuery, Pageable pageable);

    Task<Result<CompanyResponse>> GetOne(Guid id);

    Task<Result<CompanyResponse>> Create(CompanyCreateRequest companyCreateRequest);

    Task<Result<CompanyResponse>> Update(CompanyUpdateRequest companyUpdateRequest, Guid id);
}

public class CompanyService(ICompanyRepository companyRepository, IUserRepository userRepository) : ICompanyService
{
    private readonly ICompanyRepository m_CompanyRepository = companyRepository;
    private readonly IUserRepository    m_UserRepository    = userRepository;

    public async Task<Result<Page<CompanyResponse>>> GetAll(CompanyFilterQuery companyFilterQuery, Pageable pageable)
    {
        var page = await m_CompanyRepository.FindAll(companyFilterQuery, pageable);

        var companyResponse = page.Items.Select(company => company.ToResponse())
                                  .ToList();

        return Result.Ok(new Page<CompanyResponse>(companyResponse, page.PageNumber, page.PageSize, page.TotalElements));
    }

    public async Task<Result<CompanyResponse>> GetOne(Guid id)
    {
        var company = await m_CompanyRepository.FindById(id);

        if (company is null)
            return Result.NotFound<CompanyResponse>($"No Company found with Id: {id}");

        return Result.Ok(company.ToResponse());
    }

    public async Task<Result<CompanyResponse>> Create(CompanyCreateRequest companyCreateRequest)
    {
        var user = await m_UserRepository.FindById(companyCreateRequest.MajorityOwnerId);

        if (user is null)
            return Result.NotFound<CompanyResponse>($"No User found with Id: {companyCreateRequest.MajorityOwnerId}");

        if (await m_CompanyRepository.ExistsByUniqueConstrains(companyCreateRequest.RegistrationNumber, companyCreateRequest.TaxIdentificationNumber))
            return Result.BadRequest<CompanyResponse>($"Already Company with either "                                                                     +
                                                      $"{nameof(companyCreateRequest.RegistrationNumber)}: {companyCreateRequest.RegistrationNumber} or " +
                                                      $"{nameof(companyCreateRequest.TaxIdentificationNumber)}: {companyCreateRequest.TaxIdentificationNumber}");

        var company = await m_CompanyRepository.Add(companyCreateRequest.ToCompany());

        return Result.Ok(company.ToResponse());
    }

    public async Task<Result<CompanyResponse>> Update(CompanyUpdateRequest companyUpdateRequest, Guid id)
    {
        var dbCompany = await m_CompanyRepository.FindById(id);

        if (dbCompany is null)
            return Result.NotFound<CompanyResponse>($"No Company found with Id: {id}");

        var userMajorityOwner = await m_UserRepository.FindById(companyUpdateRequest.MajorityOwnerId);

        if (userMajorityOwner is null)
            return Result.NotFound<CompanyResponse>($"No User found with Id: {companyUpdateRequest.MajorityOwnerId}");

        var company = await m_CompanyRepository.Update(dbCompany.Update(companyUpdateRequest));

        return Result.Ok(company.ToResponse());
    }
}
