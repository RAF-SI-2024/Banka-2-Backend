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

public interface ICardTypeRepository
{
    Task<Page<CardType>> FindAll(CardTypeFilterQuery query, Pageable pageable);

    Task<CardType?> FindById(Guid id);

    Task<CardType?> FindByName(string name);

    Task<CardType> Add(CardType cardType);

    Task<bool> AddRange(IEnumerable<CardType> cardTypes);

    Task<CardType> Update(CardType oldCardType, CardType cardType);

    Task<bool> Exists(Guid id);

    Task<bool> IsEmpty();
}

public class CardTypeRepository(IDatabaseContextFactory<ApplicationContext> contextFactory) : ICardTypeRepository
{
    private readonly IDatabaseContextFactory<ApplicationContext> m_ContextFactory = contextFactory;

    public async Task<Page<CardType>> FindAll(CardTypeFilterQuery query, Pageable pageable)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var cardTypeQuery = context.CardTypes.AsQueryable();

        if (!string.IsNullOrEmpty(query.Name))
            cardTypeQuery = cardTypeQuery.Where(cardType => EF.Functions.ILike(cardType.Name, $"%{query.Name}%"));

        int totalElements = await cardTypeQuery.CountAsync();

        var cardTypes = await cardTypeQuery.Skip((pageable.Page - 1) * pageable.Size)
                                           .Take(pageable.Size)
                                           .ToListAsync();

        return new Page<CardType>(cardTypes, pageable.Page, pageable.Size, totalElements);
    }

    public async Task<CardType?> FindById(Guid id)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.CardTypes.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<CardType?> FindByName(string name)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.CardTypes.FirstOrDefaultAsync(ct => ct.Name == name);
    }

    public async Task<CardType> Add(CardType cardType)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var addedCardType = await context.CardTypes.AddAsync(cardType);
        await context.SaveChangesAsync();
        return addedCardType.Entity;
    }

    public async Task<bool> AddRange(IEnumerable<CardType> cardTypes)
    {
        await using var context = await m_ContextFactory.CreateContext;

        await context.BulkInsertAsync(cardTypes, config => config.BatchSize = 2000);

        return true;
    }

    public async Task<CardType> Update(CardType oldCardType, CardType cardType)
    {
        await using var context = await m_ContextFactory.CreateContext;

        context.CardTypes.Entry(oldCardType)
               .State = EntityState.Detached;

        var updatedCardType = context.CardTypes.Update(cardType);
        await context.SaveChangesAsync();
        return updatedCardType.Entity;
    }

    public async Task<bool> Exists(Guid id)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.CardTypes.AnyAsync(cardType => cardType.Id == id);
    }

    public async Task<bool> IsEmpty()
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.CardTypes.AnyAsync() is not true;
    }
}

public static partial class RepositoryExtensions
{
    [Obsolete("This method does not have implementation.", true)]
    public static IIncludableQueryable<CardType, object?> IncludeAll(this DbSet<CardType> set)
    {
        return set.Include(cardType => cardType);
    }

    public static IIncludableQueryable<TEntity, object?> ThenIncludeAll<TEntity>(this IIncludableQueryable<TEntity, CardType?> query,
                                                                                 Expression<Func<TEntity, CardType?>> navigationExpression, params string[] excludeProperties)
    where TEntity : class
    {
        return query;
    }

    public static IIncludableQueryable<TEntity, object?> ThenIncludeAll<TEntity>(this IIncludableQueryable<TEntity, List<CardType>> query,
                                                                                 Expression<Func<TEntity, List<CardType>>> navigationExpression, params string[] excludeProperties)
    where TEntity : class
    {
        return query;
    }
}
