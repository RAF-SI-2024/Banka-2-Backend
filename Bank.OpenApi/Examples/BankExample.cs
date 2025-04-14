using Bank.Application.Responses;

namespace Bank.OpenApi.Examples;

public static partial class Example
{
    public class Bank
    {
        public static readonly BankResponse DefaultResponse = new()
                                                              {
                                                                  Id         = Constant.Id,
                                                                  Name       = Constant.BankName,
                                                                  Code       = Constant.BankCode,
                                                                  BaseUrl    = Constant.BankBaseUrl,
                                                                  CreatedAt  = Constant.CreatedAt,
                                                                  ModifiedAt = Constant.ModifiedAt,
                                                              };
    }
}
