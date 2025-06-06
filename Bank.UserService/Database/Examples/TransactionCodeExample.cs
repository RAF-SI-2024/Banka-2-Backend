using Bank.Application.Responses;

namespace Bank.UserService.Database.Examples;

public static partial class Example
{
    public static class TransactionCode
    {
        public static readonly TransactionCodeResponse Response = new()
                                                                  {
                                                                      Id         = Guid.Parse("9a25d56c-5244-4b5a-b39d-d07b0e1be150"),
                                                                      Code       = "289",
                                                                      Name       = "Platna transakcija",
                                                                      CreatedAt  = DateTime.UtcNow,
                                                                      ModifiedAt = DateTime.UtcNow,
                                                                  };
    }
}
