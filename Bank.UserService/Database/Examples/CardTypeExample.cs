using Bank.Application.Responses;

namespace Bank.UserService.Database.Examples;

public static partial class Example
{
    public static class CardType
    {
        public static readonly CardTypeResponse Response = new()
                                                           {
                                                               Id         = Guid.Parse("123e4567-e89b-12d3-a456-426614174000"),
                                                               Name       = "Credit Card",
                                                               CreatedAt  = DateTime.UtcNow,
                                                               ModifiedAt = DateTime.UtcNow,
                                                           };
    }
}
