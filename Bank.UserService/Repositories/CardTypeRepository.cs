using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.UserService.Database;
using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;

namespace Bank.UserService.Repositories;

public interface ICardTypeRepository
{
    Task<Page<CardType>> FindAll(CardTypeFilterQuery query, Pageable pageable);

    Task<CardType?> FindById(Guid id);

    Task<CardType?> FindByName(string name);

    Task<CardType> Add(CardType cardType);

    Task<CardType> Update(CardType oldCardType, CardType cardType);
}

public class CardTypeRepository(ApplicationContext context) : ICardTypeRepository
{
    private readonly ApplicationContext m_Context = context;

    public async Task<Page<CardType>> FindAll(CardTypeFilterQuery query, Pageable pageable)
    {
        var cardTypeQuery = m_Context.CardTypes.AsQueryable();

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
        return await m_Context.CardTypes.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<CardType?> FindByName(string name)
    {
        return await m_Context.CardTypes.FirstOrDefaultAsync(ct => ct.Name == name);
    }

    public async Task<CardType> Add(CardType cardType)
    {
        var addedCardType = await m_Context.CardTypes.AddAsync(cardType);
        await m_Context.SaveChangesAsync();
        return addedCardType.Entity;
    }

    public async Task<CardType> Update(CardType oldCardType, CardType cardType)
    {
        m_Context.CardTypes.Entry(oldCardType)
                 .State = EntityState.Detached;

        var updatedCardType = m_Context.CardTypes.Update(cardType);
        await m_Context.SaveChangesAsync();
        return updatedCardType.Entity;
    }
}
