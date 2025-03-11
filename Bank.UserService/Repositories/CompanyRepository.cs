using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.UserService.Database;
using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;

namespace Bank.UserService.Repositories;

public interface ICompanyRepository
{
    Task<Page<Company>> FindAll(CompanyFilterQuery companyFilterQuery, Pageable pageable);

    Task<Company?> FindById(Guid id);

    Task<Company> Add(Company company);

    Task<Company> Update(Company oldCompany, Company company);

    Task<bool> ExistsByUniqueConstrains(string registrationNumber, string taxIdentificationNumber);
}

public class CompanyRepository(ApplicationContext context) : ICompanyRepository
{
    private readonly ApplicationContext m_Context = context;

    public async Task<Page<Company>> FindAll(CompanyFilterQuery companyFilterQuery, Pageable pageable)
    {
        var companyQueue = m_Context.Companies.Include(company => company.MajorityOwner)
                                    .AsQueryable();

        if (!string.IsNullOrEmpty(companyFilterQuery.Name))
            companyQueue = companyQueue.Where(company => EF.Functions.ILike(company.Name, $"%{companyFilterQuery.Name}%"));

        if (!string.IsNullOrEmpty(companyFilterQuery.TaxIdentificationNumber))
            companyQueue = companyQueue.Where(company => EF.Functions.ILike(company.TaxIdentificationNumber, $"%{companyFilterQuery.TaxIdentificationNumber}%"));

        if (!string.IsNullOrEmpty(companyFilterQuery.RegistrationNumber))
            companyQueue = companyQueue.Where(company => EF.Functions.ILike(company.RegistrationNumber, $"%{companyFilterQuery.RegistrationNumber}%"));

        int totalElements = await companyQueue.CountAsync();

        var companies = await companyQueue.Skip((pageable.Page - 1) * pageable.Size)
                                          .Take(pageable.Size)
                                          .ToListAsync();

        return new Page<Company>(companies, pageable.Page, pageable.Size, totalElements);
    }

    public async Task<Company?> FindById(Guid id)
    {
        return await m_Context.Companies.Include(company => company.MajorityOwner)
                              .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Company> Add(Company company)
    {
        var addedCompany = await m_Context.Companies.AddAsync(company);

        await m_Context.SaveChangesAsync();

        return addedCompany.Entity;
    }

    public async Task<Company> Update(Company oldCompany, Company company)
    {
        m_Context.Companies.Entry(oldCompany)
                 .State = EntityState.Detached;

        var updatedCompany = m_Context.Companies.Update(company);

        await m_Context.SaveChangesAsync();

        return updatedCompany.Entity;
    }

    public async Task<bool> ExistsByUniqueConstrains(string registrationNumber, string taxIdentificationNumber)
    {
        return await m_Context.Companies.AnyAsync(company => company.RegistrationNumber == registrationNumber || company.TaxIdentificationNumber == taxIdentificationNumber);
    }
}
