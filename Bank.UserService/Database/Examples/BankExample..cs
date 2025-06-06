using Bank.Application.Responses;

namespace Bank.UserService.Database.Examples;

public static partial class Example
{
    public static class Bank
    {
        public static readonly BankResponse Response = new()
                                                       {
                                                           Id         = Guid.Parse("ff2d66fe-f9fe-46e5-971a-71aaef01afa9"),
                                                           Name       = "Bank 5",
                                                           Code       = "555",
                                                           BaseUrl    = "null",
                                                           CreatedAt  = DateTime.UtcNow,
                                                           ModifiedAt = DateTime.UtcNow
                                                       };
    }
}
