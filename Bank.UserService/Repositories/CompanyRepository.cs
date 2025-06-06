using System.Linq.Expressions;

using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.Database.Core;
using Bank.UserService.Database;
using Bank.UserService.Models;

using EFCore.BulkExtensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Bank.UserService.Repositories;

public interface ICompanyRepository
{
    Task<Page<Company>> FindAll(CompanyFilterQuery companyFilterQuery, Pageable pageable);

    Task<Company?> FindById(Guid id);

    Task<Company> Add(Company company);

    Task<bool> AddRange(IEnumerable<Company> companies);

    Task<Company> Update(Company company);

    Task<bool> ExistsByUniqueConstrains(string registrationNumber, string taxIdentificationNumber);

    Task<bool> Exists(Guid id);

    Task<bool> IsEmpty();
}

public class CompanyRepository(IDatabaseContextFactory<ApplicationContext> contextFactory) : ICompanyRepository
{
    private readonly IDatabaseContextFactory<ApplicationContext> m_ContextFactory = contextFactory;

    public async Task<Page<Company>> FindAll(CompanyFilterQuery companyFilterQuery, Pageable pageable)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var companyQueue = context.Companies.IncludeAll()
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
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Companies.IncludeAll()
                            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Company> Add(Company company)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var addedCompany = await context.Companies.AddAsync(company);

        await context.SaveChangesAsync();

        return addedCompany.Entity;
    }

    public async Task<bool> AddRange(IEnumerable<Company> companies)
    {
        await using var context = await m_ContextFactory.CreateContext;

        await context.BulkInsertAsync(companies, config => config.BatchSize = 2000);

        return true;
    }

    public async Task<Company> Update(Company company)
    {
        await using var context = await m_ContextFactory.CreateContext;

        await context.Companies.Where(dbCompany => dbCompany.Id == company.Id)
                     .ExecuteUpdateAsync(setters => setters.SetProperty(dbCompany => dbCompany.Address, company.Address)
                                                           .SetProperty(dbCompany => dbCompany.Name,            company.Name)
                                                           .SetProperty(dbCompany => dbCompany.ActivityCode,    company.ActivityCode)
                                                           .SetProperty(dbCompany => dbCompany.MajorityOwnerId, company.MajorityOwnerId));

        return company;
    }

    public async Task<bool> ExistsByUniqueConstrains(string registrationNumber, string taxIdentificationNumber)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Companies.AnyAsync(company => company.RegistrationNumber == registrationNumber || company.TaxIdentificationNumber == taxIdentificationNumber);
    }

    public async Task<bool> Exists(Guid id)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Companies.AnyAsync(company => company.Id == id);
    }

    public async Task<bool> IsEmpty()
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Companies.AnyAsync() is not true;
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
                                                                                 Expression<Func<TEntity, Company?>> navigationExpression, params string[] excludeProperties)
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
