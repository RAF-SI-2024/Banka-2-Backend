using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Models;

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
                   ModifiedAt = cardType.ModifiedAt,
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
                   Type       = card.Type.ToResponse(),
                   ExpiresAt  = card.ExpiresAt,
                   Account    = card.Account.ToResponse(),
                   Limit      = card.Limit,
                   Number     = card.Number,
               };
    }

    public static Card ToCard(this CardCreateRequest card, CardType type, Account account)
    {
        return new Card
               {
                   Id         = Guid.NewGuid(),
                   TypeId     = card.CardTypeId,
                   Name       = card.Name,
                   Limit      = card.Limit,
                   Status     = card.Status,
                   CreatedAt  = DateTime.UtcNow,
                   ModifiedAt = DateTime.UtcNow,
                   Number     = GenerateDummyCardNumber(),
                   CVV        = GenerateRandomCVV(),
                   ExpiresAt  = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(1)),
                   Account    = account, // TODO: ADD USER TO REQUEST 
                   Type       = type,
                   AccountId  = account.Id,
               };
    }

    public static Card ToCard(this CardStatusUpdateRequest card, Card oldCard)
    {
        return new Card
               {
                   Id         = oldCard.Id,
                   Name       = oldCard.Name,
                   CreatedAt  = oldCard.CreatedAt,
                   ModifiedAt = oldCard.ModifiedAt,
                   CVV        = oldCard.CVV,
                   Status     = card.Status,
                   Type       = oldCard.Type,
                   TypeId     = oldCard.Type.Id,
                   ExpiresAt  = oldCard.ExpiresAt,
                   Account    = oldCard.Account,
                   AccountId  = oldCard.Account.Id,
                   Limit      = oldCard.Limit,
                   Number     = oldCard.Number
               };
    }

    public static Card ToCard(this CardLimitUpdateRequest card, Card oldCard)
    {
        return new Card
               {
                   Id         = oldCard.Id,
                   Name       = oldCard.Name,
                   CreatedAt  = oldCard.CreatedAt,
                   ModifiedAt = oldCard.ModifiedAt,
                   CVV        = oldCard.CVV,
                   Status     = oldCard.Status,
                   Type       = oldCard.Type,
                   TypeId     = oldCard.Type.Id,
                   ExpiresAt  = oldCard.ExpiresAt,
                   Account    = oldCard.Account,
                   AccountId  = oldCard.Account.Id,
                   Limit      = card.Limit,
                   Number     = oldCard.Number
               };
    }

    private static string GenerateDummyCardNumber()
    {
        Random random = new Random();

        // Common card prefix for testing (Visa format)
        string prefix = "4";

        string middle = "";

        for (int i = 0; i < 14; i++)
        {
            middle += random.Next(0, 10)
                            .ToString();
        }

        string number = prefix + middle;

        int  sum       = 0;
        bool alternate = false;

        for (int i = number.Length - 1; i >= 0; i--)
        {
            int digit = int.Parse(number[i]
                                  .ToString());

            if (alternate)
            {
                digit *= 2;

                if (digit > 9)
                {
                    digit -= 9;
                }
            }

            sum       += digit;
            alternate =  !alternate;
        }

        // Calculate check digit
        int checkDigit = (10 - (sum % 10)) % 10;

        return number + checkDigit;
    }

    public static string GenerateRandomCVV()
    {
        Random random = new Random();

        return random.Next(100, 1000)
                     .ToString();
    }
}
