using Bank.Application.Requests;
using Bank.Application.Responses;

namespace Bank.OpenApi.Examples;

public static partial class Example
{
    public static class AccountCurrency
    {
        public static readonly AccountCurrencyCreateRequest DefaultCreateRequest = new()
                                                                                   {
                                                                                       EmployeeId   = Constant.Id,
                                                                                       CurrencyId   = Constant.Id,
                                                                                       AccountId    = Constant.Id,
                                                                                       DailyLimit   = Constant.DailyLimit,
                                                                                       MonthlyLimit = Constant.MonthlyLimit,
                                                                                   };

        public static readonly AccountCurrencyClientUpdateRequest DefaultClientUpdateRequest = new()
                                                                                               {
                                                                                                   DailyLimit   = Constant.DailyLimit,
                                                                                                   MonthlyLimit = Constant.MonthlyLimit,
                                                                                               };

        public static readonly AccountCurrencyResponse DefaultResponse = new()
                                                                         {
                                                                             Id               = Constant.Id,
                                                                             Account          = Account.DefaultSimpleResponse,
                                                                             Employee         = Employee.DefaultSimpleResponse,
                                                                             Currency         = Currency.DefaultResponse,
                                                                             Balance          = Constant.Balance,
                                                                             AvailableBalance = Constant.Balance,
                                                                             DailyLimit       = Constant.DailyLimit,
                                                                             MonthlyLimit     = Constant.MonthlyLimit,
                                                                             CreatedAt        = Constant.CreatedAt,
                                                                             ModifiedAt       = Constant.ModifiedAt,
                                                                         };
    }
}
