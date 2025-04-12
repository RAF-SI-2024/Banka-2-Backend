using Bank.Application.Requests;

namespace Bank.UserService.Database.Sample;

public static partial class Sample
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
    }
}
