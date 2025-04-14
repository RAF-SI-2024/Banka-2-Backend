using Bank.Application.Requests;
using Bank.Application.Responses;

namespace Bank.OpenApi.Examples;

public static partial class Example
{
    public static class Loan
    {
        public static readonly LoanCreateRequest DefaultCreateRequest = new()
                                                                        {
                                                                            TypeId       = Constant.Id,
                                                                            AccountId    = Constant.Id,
                                                                            Amount       = Constant.Amount,
                                                                            Period       = Constant.Period,
                                                                            CurrencyId   = Constant.Id,
                                                                            InterestType = Constant.InterestType,
                                                                        };

        public static readonly LoanUpdateRequest DefaultUpdateRequest = new()
                                                                        {
                                                                            Status       = Constant.LoanStatus,
                                                                            MaturityDate = Constant.CreatedAt,
                                                                        };

        public static readonly LoanResponse DefaultResponse = new()
                                                              {
                                                                  Id           = Constant.Id,
                                                                  Type         = LoanType.DefaultResponse,
                                                                  Account      = Account.DefaultResponse,
                                                                  Amount       = Constant.Amount,
                                                                  Period       = Constant.Period,
                                                                  CreationDate = Constant.CreationDate,
                                                                  MaturityDate = Constant.CreationDate,
                                                                  Currency     = Currency.DefaultResponse,
                                                                  Status       = Constant.LoanStatus,
                                                                  InterestType = Constant.InterestType,
                                                                  CreatedAt    = Constant.CreatedAt,
                                                                  ModifiedAt   = Constant.ModifiedAt,
                                                              };
    }
}
