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

public interface ICardRepository
{
    Task<Page<Card>> FindAll(CardFilterQuery cardFilterQuery, Pageable pageable);

    Task<Page<Card>> FindAllByAccountId(Guid accountId, Pageable pageable);

    Task<Page<Card>> FindAllByClientId(Guid clientId, Pageable pageable);

    Task<Card?> FindById(Guid id);

    Task<Card?> FindByNumber(string number);

    Task<Card> Add(Card card);

    Task<bool> AddRange(IEnumerable<Card> cards);

    Task<Card> Update(Card card);

    Task<List<Card>> FindAllByClientId(Guid clientId);

    Task<bool> Exists(Guid id);

    Task<bool> IsEmpty();
}

public class CardRepository(IDatabaseContextFactory<ApplicationContext> contextFactory) : ICardRepository
{
    private readonly IDatabaseContextFactory<ApplicationContext> m_ContextFactory = contextFactory;

    public async Task<Page<Card>> FindAll(CardFilterQuery cardFilterQuery, Pageable pageable)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var cardQuery = context.Cards.IncludeAll()
                               .AsQueryable();

        if (!string.IsNullOrEmpty(cardFilterQuery.Number))
            cardQuery = cardQuery.Where(card => EF.Functions.ILike(card.Number, $"%{cardFilterQuery.Number}%"));

        if (!string.IsNullOrEmpty(cardFilterQuery.Name))
            cardQuery = cardQuery.Where(card => EF.Functions.ILike(card.Name, $"%{cardFilterQuery.Name}%"));

        int totalElements = await cardQuery.CountAsync();

        var cards = await cardQuery.Skip((pageable.Page - 1) * pageable.Size)
                                   .Take(pageable.Size)
                                   .ToListAsync();

        return new Page<Card>(cards, pageable.Page, pageable.Size, totalElements);
    }

    public async Task<Card?> FindById(Guid id)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Cards.IncludeAll()
                            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Page<Card>> FindAllByAccountId(Guid accountId, Pageable pageable)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var cards = await context.Cards.IncludeAll()
                                 .Where(card => card.Account!.Id == accountId)
                                 .Skip((pageable.Page - 1) * pageable.Size)
                                 .Take(pageable.Size)
                                 .ToListAsync();

        var totalElements = await context.Cards.CountAsync();

        return new Page<Card>(cards, pageable.Page, pageable.Size, totalElements);
    }

    public async Task<Page<Card>> FindAllByClientId(Guid clientId, Pageable pageable)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var cards = await context.Cards.IncludeAll()
                                 .Where(card => card.Account!.Client!.Id == clientId)
                                 .Skip((pageable.Page - 1) * pageable.Size)
                                 .Take(pageable.Size)
                                 .ToListAsync();

        var totalElements = await context.Cards.CountAsync();

        return new Page<Card>(cards, pageable.Page, pageable.Size, totalElements);
    }

    public async Task<Card?> FindByNumber(string number)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Cards.IncludeAll()
                            .FirstOrDefaultAsync(card => card.Number == number);
    }

    public async Task<Card> Add(Card card)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var addedCard = await context.Cards.AddAsync(card);

        await context.SaveChangesAsync();

        return addedCard.Entity;
    }

    public async Task<bool> AddRange(IEnumerable<Card> cards)
    {
        await using var context = await m_ContextFactory.CreateContext;

        await context.BulkInsertAsync(cards, config => config.BatchSize = 2000);

        return true;
    }

    public async Task<Card> Update(Card card)
    {
        await using var context = await m_ContextFactory.CreateContext;

        await context.Cards.Where(dbCard => dbCard.Id == card.Id)
                     .ExecuteUpdateAsync(setters => setters.SetProperty(dbCard => dbCard.TypeId, card.TypeId)
                                                           .SetProperty(dbCard => dbCard.Name,       card.Name)
                                                           .SetProperty(dbCard => dbCard.Limit,      card.Limit)
                                                           .SetProperty(dbCard => dbCard.Status,     card.Status)
                                                           .SetProperty(dbCard => dbCard.ModifiedAt, card.ModifiedAt)
                                                           .SetProperty(dbCard => dbCard.Number,     card.Number)
                                                           .SetProperty(dbCard => dbCard.CVV,        card.CVV)
                                                           .SetProperty(dbCard => dbCard.ExpiresAt,  card.ExpiresAt)
                                                           .SetProperty(dbCard => dbCard.AccountId,  card.AccountId));

        return card;
    }

    public async Task<List<Card>> FindAllByClientId(Guid clientId)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Cards.IncludeAll()
                            .Where(c => c.Account != null && c.Account.ClientId == clientId)
                            .ToListAsync();
    }

    public async Task<bool> Exists(Guid id)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Cards.AnyAsync(card => card.Id == id);
    }

    public async Task<bool> IsEmpty()
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Cards.AnyAsync() is not true;
    }
}

public static partial class RepositoryExtensions
{
    public static IIncludableQueryable<Card, object?> IncludeAll(this DbSet<Card> set)
    {
        return set.Include(card => card.Account)
                  .ThenIncludeAll(card => card.Account)
                  .Include(card => card.Type)
                  .ThenIncludeAll(card => card.Type);
    }

    public static IIncludableQueryable<TEntity, object?> ThenIncludeAll<TEntity>(this IIncludableQueryable<TEntity, Card?> value,
                                                                                 Expression<Func<TEntity, Card?>>          navigationExpression, params string[] excludeProperties)
    where TEntity : class
    {
        IIncludableQueryable<TEntity, object?> query = value;

        if (!excludeProperties.Contains(nameof(Card.Account)))
            query = query.Include(navigationExpression)
                         .ThenInclude(card => card!.Account);

        if (!excludeProperties.Contains(nameof(Card.Type)))
            query = query.Include(navigationExpression)
                         .ThenInclude(card => card!.Type);

        return query;
    }

    public static IIncludableQueryable<TEntity, object?> ThenIncludeAll<TEntity>(this IIncludableQueryable<TEntity, List<Card>> value,
                                                                                 Expression<Func<TEntity, List<Card>>> navigationExpression, params string[] excludeProperties)
    where TEntity : class
    {
        IIncludableQueryable<TEntity, object?> query = value;

        if (!excludeProperties.Contains(nameof(Card.Account)))
            query = query.Include(navigationExpression)
                         .ThenInclude(card => card.Account);

        if (!excludeProperties.Contains(nameof(Card.Type)))
            query = query.Include(navigationExpression)
                         .ThenInclude(card => card.Type);

        return query;
    }
}
