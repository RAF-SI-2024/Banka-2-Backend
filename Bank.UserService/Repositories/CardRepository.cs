using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.UserService.Database;
using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;

namespace Bank.UserService.Repositories;

public interface ICardRepository
{
    Task<Page<Card>> FindAll(CardFilterQuery cardFilterQuery, Pageable pageable);

    Task<Card?> FindById(Guid id);

    Task<List<Card>> FindByAccountId(Guid accountId);

    Task<Card?> FindByNumber(string number);

    Task<Card> Add(Card card);

    Task<Card> Update(Card oldCard, Card card);

    Task<Card> UpdateStatus(Guid id, bool status);

    Task<Card> UpdateLimit(Guid id, decimal limit);
}

public class CardRepository(ApplicationContext context) : ICardRepository
{
    private readonly ApplicationContext m_Context = context;

    public async Task<Page<Card>> FindAll(CardFilterQuery cardFilterQuery, Pageable pageable)
    {
        var cardQuery = m_Context.Cards.Include(card => card.Type)
                                 .Include(card => card.Account)
                                 .Include(card => card.Account!.Client)
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
        return await m_Context.Cards.Include(card => card.Type)
                              .Include(card => card.Account)
                              .Include(card => card.Account!.Client)
                              .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Card>> FindByAccountId(Guid accountId)
    {
        return await m_Context.Cards.Include(card => card.Type)
                              .Include(card => card.Account)
                              .Include(card => card.Account!.Client)
                              .Where(card => card.Account!.Id == accountId)
                              .ToListAsync();
    }

    public async Task<Card?> FindByNumber(string number)
    {
        return await m_Context.Cards.Include(card => card.Type)
                              .Include(card => card.Account)
                              .FirstOrDefaultAsync(card => card.Number == number);
    }

    public async Task<Card> Add(Card card)
    {
        var addedCard = await m_Context.Cards.AddAsync(card);

        await m_Context.SaveChangesAsync();

        return addedCard.Entity;
    }

    public async Task<Card> Update(Card oldCard, Card card)
    {
        m_Context.Cards.Entry(oldCard)
                 .State = EntityState.Detached;

        var updatedCard = m_Context.Cards.Update(card);
        await m_Context.SaveChangesAsync();
        return updatedCard.Entity;
    }

    public async Task<Card> UpdateStatus(Guid id, bool status)
    {
        var card = await FindById(id);

        if (card == null)
            throw new Exception("Card not found.");

        card.Status     = status;
        card.ModifiedAt = DateTime.UtcNow;

        await m_Context.SaveChangesAsync();
        return card;
    }

    public async Task<Card> UpdateLimit(Guid id, decimal limit)
    {
        var card = await FindById(id);

        if (card == null)
            throw new Exception("Card not found.");

        card.Limit      = limit;
        card.ModifiedAt = DateTime.UtcNow;

        await m_Context.SaveChangesAsync();
        return card;
    }
}
