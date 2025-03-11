using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.UserService.Mappers;
using Bank.UserService.Repositories;

namespace Bank.UserService.Services;

public interface ICardTypeService
{
    Task<Result<CardTypeResponse>> GetCardTypeById(Guid id);

    Task<Result<Page<CardTypeResponse>>> FindAll(CardTypeFilterQuery userFilterQuery, Pageable pageable);
}

public class CardTypeService(ICardTypeRepository repository) : ICardTypeService
{
    private readonly ICardTypeRepository m_CardTypeRepository = repository;

    public async Task<Result<CardTypeResponse>> GetCardTypeById(Guid id)
    {
        var cardType = await m_CardTypeRepository.FindById(id);

        if (cardType == null)
            return Result.NotFound<CardTypeResponse>($"No CardType found with Id: {id}");

        var response = cardType.ToResponse();
        return Result.Ok(response);
    }

    public async Task<Result<Page<CardTypeResponse>>> FindAll(CardTypeFilterQuery userFilterQuery, Pageable pageable)
    {
        var cardTypes = await m_CardTypeRepository.FindAll(userFilterQuery, pageable);

        var responses = cardTypes.Items.Select(ct => ct.ToResponse())
                                 .ToList();

        return Result.Ok(new Page<CardTypeResponse>(responses, cardTypes.PageNumber, cardTypes.PageSize, cardTypes.TotalElements));
    }
}
