using Bank.Application.Requests;
using Bank.Application.Responses;

namespace Bank.OpenApi.Examples;

public static partial class Example
{
    public static class Account
    {
        public static readonly AccountCreateRequest DefaultCreateRequest = new()
                                                                           {
                                                                               Name          = Constant.AccountName,
                                                                               DailyLimit    = Constant.DailyLimit,
                                                                               MonthlyLimit  = Constant.MonthlyLimit,
                                                                               ClientId      = Constant.Id,
                                                                               Balance       = Constant.Balance,
                                                                               CurrencyId    = Constant.Id,
                                                                               AccountTypeId = Constant.Id,
                                                                               Status        = Constant.Boolean
                                                                           };

        public static readonly AccountUpdateClientRequest DefaultUpdateClientRequest = new()
                                                                                       {
                                                                                           Name         = Constant.AccountName,
                                                                                           DailyLimit   = Constant.DailyLimit,
                                                                                           MonthlyLimit = Constant.MonthlyLimit,
                                                                                       };

        public static readonly AccountUpdateEmployeeRequest DefaultUpdateEmployeeRequest = new()
                                                                                           {
                                                                                               Status = Constant.Boolean
                                                                                           };

        public static readonly AccountResponse DefaultResponse = new()
                                                                 {
                                                                     Id                = Constant.Id,
                                                                     AccountNumber     = Constant.AccountNumber,
                                                                     Office            = Constant.Office,
                                                                     Name              = Constant.AccountName,
                                                                     Balance           = Constant.Balance,
                                                                     AvailableBalance  = Constant.Balance,
                                                                     Type              = AccountType.DefaultResponse,
                                                                     Currency          = Currency.DefaultResponse,
                                                                     Employee          = Employee.DefaultSimpleResponse,
                                                                     Client            = Client.DefaultSimpleResponse,
                                                                     AccountCurrencies = [AccountCurrency.DefaultResponse],
                                                                     DailyLimit        = Constant.DailyLimit,
                                                                     MonthlyLimit      = Constant.MonthlyLimit,
                                                                     CreationDate      = Constant.CreationDate,
                                                                     ExpirationDate    = Constant.CreationDate,
                                                                     Status            = Constant.Boolean,
                                                                     CreatedAt         = Constant.CreatedAt,
                                                                     ModifiedAt        = Constant.ModifiedAt
                                                                 };

        public static readonly AccountSimpleResponse DefaultSimpleResponse = new()
                                                                             {
                                                                                 Id            = Constant.Id,
                                                                                 AccountNumber = Constant.AccountNumber,
                                                                             };
    }
}
