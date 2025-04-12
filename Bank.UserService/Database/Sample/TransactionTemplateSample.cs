using Bank.Application.Requests;

namespace Bank.UserService.Database.Sample;

public static partial class Sample
{
    public static class TransactionTemplate
    {
        public static readonly TransactionTemplateCreateRequest CreateRequest = new()
                                                                                {
                                                                                    AccountNumber = "11111",
                                                                                    Name          = "Slavko RSD"
                                                                                };

        public static readonly TransactionTemplateUpdateRequest UpdateRequest = new()
                                                                                {
                                                                                    AccountNumber = "22222",
                                                                                    Deleted       = false,
                                                                                    Name          = "Mirko EUR"
                                                                                };
    }
}
