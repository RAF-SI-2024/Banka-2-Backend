using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Models;
using Bank.UserService.Utils;

namespace Bank.UserService.Mappers;

public static class CardMapper
{
    public static CardTypeResponse ToResponse(this CardType cardType)
    {
        return new CardTypeResponse
               {
                   Id         = cardType.Id,
                   Name       = cardType.Name,
                   CreatedAt  = cardType.CreatedAt,
                   ModifiedAt = cardType.ModifiedAt
               };
    }

    public static CardResponse ToResponse(this Card card)
    {
        return new CardResponse
               {
                   Id         = card.Id,
                   Name       = card.Name,
                   CreatedAt  = card.CreatedAt,
                   ModifiedAt = card.ModifiedAt,
                   CVV        = card.CVV,
                   Status     = card.Status,
                   Type       = card.Type!.ToResponse(),
                   ExpiresAt  = card.ExpiresAt,
                   Account    = card.Account!.ToResponse(),
                   Limit      = card.Limit,
                   Number     = card.Number
               };
    }

    public static Card ToCard(this CardCreateRequest card, CardType type, Account account)
    {
        var (cardNumber, cvv) = CardNumberGeneratorUtil.GenerateCardDetails(type.Name);

        return new Card
               {
                   Id         = Guid.NewGuid(),
                   TypeId     = card.CardTypeId,
                   Name       = card.Name,
                   Limit      = card.Limit,
                   Status     = card.Status,
                   CreatedAt  = DateTime.UtcNow,
                   ModifiedAt = DateTime.UtcNow,
                   Number     = cardNumber,
                   CVV        = cvv,
                   ExpiresAt  = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(1)),
                   AccountId  = account.Id
               };
    }

    public static Card Update(this Card card, CardUpdateStatusRequest updateRequest)
    {
        card.Status     = updateRequest.Status;
        card.ModifiedAt = DateTime.UtcNow;

        return card;
    }

    public static Card Update(this Card card, CardUpdateLimitRequest updateRequest)
    {
        card.Limit      = updateRequest.Limit;
        card.ModifiedAt = DateTime.UtcNow;

        return card;
    }
}
