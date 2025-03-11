using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Models;

namespace Bank.UserService.Mappers;

public static class AccountCurrencyMapper
{
    public static AccountCurrencyResponse ToResponse(this AccountCurrency accountCurrency)
    {
        return new AccountCurrencyResponse
               {
                   Id      = accountCurrency.Id,
                   Account = accountCurrency.Account.ToSimpleResponse(),
                   Employee = accountCurrency.Employee.ToEmployee()
                                             .ToSimpleResponse(),
                   Currency         = accountCurrency.Currency.ToResponse(),
                   Balance          = accountCurrency.Balance,
                   AvailableBalance = accountCurrency.AvailableBalance,
                   DailyLimit       = accountCurrency.DailyLimit,
                   MonthlyLimit     = accountCurrency.MonthlyLimit,
                   CreatedAt        = accountCurrency.CreatedAt,
                   ModifiedAt       = accountCurrency.ModifiedAt
               };
    }

    public static AccountCurrency ToAccountCurrency(this AccountCurrencyCreateRequest accountCurrencyCreateRequest, User employee, Currency currency, Account account)
    {
        return new AccountCurrency
               {
                   Id               = Guid.NewGuid(),
                   AccountId        = account.Id,
                   Account          = account,
                   EmployeeId       = employee.Id,
                   Employee         = employee,
                   CurrencyId       = currency.Id,
                   Currency         = currency,
                   Balance          = 0,
                   AvailableBalance = 0,
                   DailyLimit       = accountCurrencyCreateRequest.DailyLimit,
                   MonthlyLimit     = accountCurrencyCreateRequest.MonthlyLimit,
                   CreatedAt        = DateTime.UtcNow,
                   ModifiedAt       = DateTime.UtcNow
               };
    }

    public static AccountCurrency ToAccountCurrency(this AccountCurrencyClientUpdateRequest accountCurrencyUpdate, AccountCurrency oldAccountCurrency)
    {
        return new AccountCurrency
               {
                   Id               = oldAccountCurrency.Id,
                   DailyLimit       = accountCurrencyUpdate.DailyLimit,
                   MonthlyLimit     = accountCurrencyUpdate.MonthlyLimit,
                   CreatedAt        = oldAccountCurrency.CreatedAt,
                   ModifiedAt       = DateTime.UtcNow,
                   Account          = oldAccountCurrency.Account,
                   AccountId        = oldAccountCurrency.AccountId,
                   Employee         = oldAccountCurrency.Employee,
                   EmployeeId       = oldAccountCurrency.EmployeeId,
                   Currency         = oldAccountCurrency.Currency,
                   CurrencyId       = oldAccountCurrency.CurrencyId,
                   Balance          = oldAccountCurrency.Balance,
                   AvailableBalance = oldAccountCurrency.AvailableBalance
               };
    }
}
