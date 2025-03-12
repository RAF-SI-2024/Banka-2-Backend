using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Models;

namespace Bank.UserService.Mappers;

public static class AccountMapper
{
    public static AccountResponse ToResponse(this Account account)
    {
        return new AccountResponse
               {
                   Id            = account.Id,
                   AccountNumber = account.Number,
                   Name          = account.Name,
                   Client = account.Client?.ToClient()
                                   .ToSimpleResponse()!,
                   Balance          = account.Balance,
                   AvailableBalance = account.AvailableBalance,
                   Employee = account.Employee?.ToEmployee()
                                     .ToSimpleResponse()!,
                   Currency          = account.Currency?.ToResponse()!,
                   Type              = account.Type?.ToResponse()!,
                   AccountCurrencies = MapAccountCurrencies(account.AccountCurrencies),
                   DailyLimit        = 0,
                   MonthlyLimit      = 0,
                   CreationDate      = account.CreationDate,
                   ExpirationDate    = account.ExpirationDate,
                   Status            = account.Status,
                   CreatedAt         = account.CreatedAt,
                   ModifiedAt        = account.ModifiedAt
               };
    }

    private static List<AccountCurrencyResponse> MapAccountCurrencies(List<AccountCurrency> accountCurrencies) =>
    accountCurrencies.Select(accountCurrency => accountCurrency.ToResponse())
                     .ToList();

    public static AccountSimpleResponse ToSimpleResponse(this Account account)
    {
        return new AccountSimpleResponse
               {
                   Id            = account.Id,
                   AccountNumber = account.Number
               };
    }

    public static Account ToAccount(this AccountCreateRequest accountCreateRequest, User employee, User client, Currency currency, AccountType accountType)
    {
        var account = new Account
                      {
                          Id                = Guid.NewGuid(),
                          EmployeeId        = employee.Id,
                          Employee          = employee,
                          CurrencyId        = currency.Id,
                          Currency          = currency,
                          Balance           = accountCreateRequest.Balance,
                          AvailableBalance  = accountCreateRequest.Balance,
                          DailyLimit        = accountCreateRequest.DailyLimit,
                          MonthlyLimit      = accountCreateRequest.MonthlyLimit,
                          CreatedAt         = DateTime.UtcNow,
                          ModifiedAt        = DateTime.UtcNow,
                          Client            = client,
                          ClientId          = client.Id,
                          Name              = accountCreateRequest.Name,
                          Number            = GenerateAccountNumber(),
                          Type              = accountType,
                          AccountTypeId     = accountType.Id,
                          AccountCurrencies = [],
                          CreationDate      = DateOnly.FromDateTime(DateTime.UtcNow),
                          ExpirationDate    = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(5)),
                          Status            = accountCreateRequest.Status
                      };

        return account;
    }

    public static Account ToAccount(this AccountUpdateClientRequest accountUpdate, Account oldAccount)
    {
        return new Account
               {
                   Id                = oldAccount.Id,
                   CreatedAt         = oldAccount.CreatedAt,
                   ModifiedAt        = DateTime.UtcNow,
                   Employee          = oldAccount.Employee,
                   EmployeeId        = oldAccount.EmployeeId,
                   Currency          = oldAccount.Currency,
                   CurrencyId        = oldAccount.CurrencyId,
                   Balance           = oldAccount.Balance,
                   AvailableBalance  = oldAccount.AvailableBalance,
                   Client            = oldAccount.Client,
                   ClientId          = oldAccount.ClientId,
                   Number            = oldAccount.Number,
                   Name              = accountUpdate.Name,
                   Type              = oldAccount.Type,
                   AccountTypeId     = oldAccount.AccountTypeId,
                   AccountCurrencies = oldAccount.AccountCurrencies,
                   CreationDate      = oldAccount.CreationDate,
                   ExpirationDate    = oldAccount.ExpirationDate,
                   DailyLimit        = accountUpdate.DailyLimit,
                   MonthlyLimit      = accountUpdate.MonthlyLimit,
                   Status            = oldAccount.Status
               };
    }

    public static Account ToAccount(this AccountUpdateEmployeeRequest accountUpdate, Account oldAccount)
    {
        oldAccount.Status = accountUpdate.Status;

        return new Account
               {
                   Id                = oldAccount.Id,
                   CreatedAt         = oldAccount.CreatedAt,
                   ModifiedAt        = DateTime.UtcNow,
                   Employee          = oldAccount.Employee,
                   EmployeeId        = oldAccount.EmployeeId,
                   Currency          = oldAccount.Currency,
                   CurrencyId        = oldAccount.CurrencyId,
                   Balance           = oldAccount.Balance,
                   AvailableBalance  = oldAccount.AvailableBalance,
                   Client            = oldAccount.Client,
                   ClientId          = oldAccount.ClientId,
                   Number            = oldAccount.Number,
                   Name              = oldAccount.Name,
                   Type              = oldAccount.Type,
                   AccountTypeId     = oldAccount.AccountTypeId,
                   AccountCurrencies = oldAccount.AccountCurrencies,
                   CreationDate      = oldAccount.CreationDate,
                   ExpirationDate    = oldAccount.ExpirationDate,
                   DailyLimit        = oldAccount.DailyLimit,
                   MonthlyLimit      = oldAccount.MonthlyLimit,
                   Status            = accountUpdate.Status
               };
    }

    private static string GenerateAccountNumber()
    {
        Random random       = new Random();
        string randomDigits = "";

        for (int i = 0; i < 9; i++)
            randomDigits += random.Next(0, 10)
                                  .ToString();

        return randomDigits;
    }
}
