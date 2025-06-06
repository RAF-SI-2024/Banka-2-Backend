using Bank.Application.Requests;
using Bank.Application.Responses;

namespace Bank.UserService.Database.Examples;

public static partial class Example
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

        public static readonly TransactionTemplateResponse Response = new()
                                                                      {
                                                                          Id            = Guid.Parse("44ca0bef-1783-40b9-9b41-51e196d4f6b3"),
                                                                          Client        = null!,
                                                                          Name          = CreateRequest.Name,
                                                                          AccountNumber = CreateRequest.AccountNumber,
                                                                          Deleted       = UpdateRequest.Deleted,
                                                                          CreatedAt     = DateTime.UtcNow,
                                                                          ModifiedAt    = DateTime.UtcNow
                                                                      };

        public static readonly TransactionTemplateSimpleResponse SimpleResponse = new()
                                                                                  {
                                                                                      Id            = Guid.Parse("44ca0bef-1783-40b9-9b41-51e196d4f6b3"),
                                                                                      Name          = CreateRequest.Name,
                                                                                      AccountNumber = CreateRequest.AccountNumber,
                                                                                      Deleted       = UpdateRequest.Deleted,
                                                                                  };
    }
}
