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
                   Account = accountCurrency.Account?.ToSimpleResponse()!,
                   Employee = accountCurrency.Employee?.ToEmployee()
                                             .ToSimpleResponse()!,
                   Currency         = accountCurrency.Currency?.ToResponse()!,
                   Balance          = accountCurrency.Balance,
                   AvailableBalance = accountCurrency.AvailableBalance,
                   DailyLimit       = accountCurrency.DailyLimit,
                   MonthlyLimit     = accountCurrency.MonthlyLimit,
                   CreatedAt        = accountCurrency.CreatedAt,
                   ModifiedAt       = accountCurrency.ModifiedAt
               };
    }

    public static AccountCurrency ToAccountCurrency(this AccountCurrencyCreateRequest createRequest)
    {
        return new AccountCurrency
               {
                   Id               = Guid.NewGuid(),
                   AccountId        = createRequest.AccountId,
                   EmployeeId       = createRequest.EmployeeId,
                   CurrencyId       = createRequest.CurrencyId,
                   Balance          = 0,
                   AvailableBalance = 0,
                   DailyLimit       = createRequest.DailyLimit,
                   MonthlyLimit     = createRequest.MonthlyLimit,
                   CreatedAt        = DateTime.UtcNow,
                   ModifiedAt       = DateTime.UtcNow
               };
    }

    public static AccountCurrency ToAccountCurrency(this AccountCurrency accountCurrency, AccountCurrencyClientUpdateRequest accountCurrencyUpdate)
    {
        accountCurrency.DailyLimit   = accountCurrencyUpdate.DailyLimit;
        accountCurrency.MonthlyLimit = accountCurrencyUpdate.MonthlyLimit;
        accountCurrency.ModifiedAt   = DateTime.UtcNow;

        return accountCurrency;
        
    }
}
