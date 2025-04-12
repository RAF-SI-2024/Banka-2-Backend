using System.Linq.Expressions;

using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.UserService.Database;
using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Bank.UserService.Repositories;

public interface ICompanyRepository
{
    Task<Page<Company>> FindAll(CompanyFilterQuery companyFilterQuery, Pageable pageable);

    Task<Company?> FindById(Guid id);

    Task<Company> Add(Company company);

    Task<Company> Update(Company company);

    Task<bool> ExistsByUniqueConstrains(string registrationNumber, string taxIdentificationNumber);
}

public class CompanyRepository(ApplicationContext context) : ICompanyRepository
{
    private readonly ApplicationContext m_Context = context;

    public async Task<Page<Company>> FindAll(CompanyFilterQuery companyFilterQuery, Pageable pageable)
    {
        var companyQueue = m_Context.Companies.IncludeAll()
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
        return await m_Context.Companies.IncludeAll()
                              .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Company> Add(Company company)
    {
        var addedCompany = await m_Context.Companies.AddAsync(company);

        await m_Context.SaveChangesAsync();

        return addedCompany.Entity;
    }

    public async Task<Company> Update(Company company)
    {
        await m_Context.Companies.Where(dbCompany => dbCompany.Id == company.Id)
                       .ExecuteUpdateAsync(setters => setters.SetProperty(dbCompany => dbCompany.Address, company.Address)
                                                             .SetProperty(dbCompany => dbCompany.Name,            company.Name)
                                                             .SetProperty(dbCompany => dbCompany.ActivityCode,    company.ActivityCode)
                                                             .SetProperty(dbCompany => dbCompany.MajorityOwnerId, company.MajorityOwnerId));

        return company;
    }

    public async Task<bool> ExistsByUniqueConstrains(string registrationNumber, string taxIdentificationNumber)
    {
        return await m_Context.Companies.AnyAsync(company => company.RegistrationNumber == registrationNumber || company.TaxIdentificationNumber == taxIdentificationNumber);
    }
}

public static partial class RepositoryExtensions
{
    public static IIncludableQueryable<Company, object?> IncludeAll(this DbSet<Company> set)
    {
        return set.Include(company => company.MajorityOwner)
                  .ThenIncludeAll(company => company.MajorityOwner);
    }

    public static IIncludableQueryable<TEntity, object?> ThenIncludeAll<TEntity>(this IIncludableQueryable<TEntity, Company?> value,
                                                                                 Expression<Func<TEntity, Company?>>          navigationExpression, params string[] excludeProperties)
    where TEntity : class
    {
        IIncludableQueryable<TEntity, object?> query = value;

        if (!excludeProperties.Contains(nameof(Company.MajorityOwner)))
            query = query.Include(navigationExpression)
                         .ThenInclude(company => company!.MajorityOwner);

        return query;
    }

    public static IIncludableQueryable<TEntity, object?> ThenIncludeAll<TEntity>(this IIncludableQueryable<TEntity, List<Company>> value,
                                                                                 Expression<Func<TEntity, List<Company>>> navigationExpression, params string[] excludeProperties)
    where TEntity : class
    {
        IIncludableQueryable<TEntity, object?> query = value;

        if (!excludeProperties.Contains(nameof(Company.MajorityOwner)))
            query = query.Include(navigationExpression)
                         .ThenInclude(company => company!.MajorityOwner);

        return query;
    }
}
