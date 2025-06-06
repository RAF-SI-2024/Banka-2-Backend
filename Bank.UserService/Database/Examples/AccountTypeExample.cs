using Bank.Application.Requests;
using Bank.Application.Responses;

namespace Bank.UserService.Database.Examples;

public static partial class Example
{
    public static class AccountType
    {
        public static readonly AccountTypeCreateRequest CreateRequest = new()
                                                                        {
                                                                            Name = "Štedni račun",
                                                                            Code = "SAV"
                                                                        };

        public static readonly AccountTypeUpdateRequest UpdateRequest = new()
                                                                        {
                                                                            Name = "Štedni račun",
                                                                            Code = "SAV"
                                                                        };

        public static readonly AccountTypeResponse Response = new()
                                                              {
                                                                  Id         = Guid.Parse("c3f7a5d4-e6b8-4d2a-a678-123456789abc"),
                                                                  Name       = CreateRequest.Name,
                                                                  Code       = CreateRequest.Code,
                                                                  CreatedAt  = DateTime.UtcNow,
                                                                  ModifiedAt = DateTime.UtcNow
                                                              };
    }
}
