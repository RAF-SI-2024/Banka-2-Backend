using System.Linq.Expressions;

using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.UserService.Database;
using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Bank.UserService.Repositories;

public interface ICardRepository
{
    Task<Page<Card>> FindAll(CardFilterQuery cardFilterQuery, Pageable pageable);

    Task<Page<Card>> FindAllByAccountId(Guid accountId, Pageable pageable);

    Task<Card?> FindById(Guid id);

    Task<Card?> FindByNumber(string number);

    Task<Card> Add(Card card);

    Task<Card> Update(Card card);

    Task<List<Card>> FindAllByClientId(Guid clientId);
}

public class CardRepository(ApplicationContext context) : ICardRepository
{
    private readonly ApplicationContext m_Context = context;

    public async Task<Page<Card>> FindAll(CardFilterQuery cardFilterQuery, Pageable pageable)
    {
        var cardQuery = m_Context.Cards.IncludeAll()
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
        return await m_Context.Cards.IncludeAll()
                              .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Page<Card>> FindAllByAccountId(Guid accountId, Pageable pageable)
    {
        var cards = await m_Context.Cards.IncludeAll()
                                   .Where(card => card.Account!.Id == accountId)
                                   .Skip((pageable.Page - 1) * pageable.Size)
                                   .Take(pageable.Size)
                                   .ToListAsync();

        var totalElements = await m_Context.Cards.CountAsync();

        return new Page<Card>(cards, pageable.Page, pageable.Size, totalElements);
    }

    public async Task<Card?> FindByNumber(string number)
    {
        return await m_Context.Cards.IncludeAll()
                              .FirstOrDefaultAsync(card => card.Number == number);
    }

    public async Task<Card> Add(Card card)
    {
        var addedCard = await m_Context.Cards.AddAsync(card);

        await m_Context.SaveChangesAsync();

        return addedCard.Entity;
    }

    public async Task<Card> Update(Card card)
    {
        await m_Context.Cards.Where(dbCard => dbCard.Id == card.Id)
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
        return await m_Context.Cards.IncludeAll()
                              .Where(c => c.Account != null && c.Account.ClientId == clientId)
                              .ToListAsync();
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
                                                                                 Expression<Func<TEntity, List<Card>>>          navigationExpression, params string[] excludeProperties)
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
