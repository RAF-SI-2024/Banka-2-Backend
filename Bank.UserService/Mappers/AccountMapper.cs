using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Database.Seeders;
using Bank.UserService.Models;

namespace Bank.UserService.Mappers;

public static class AccountMapper
{
    public static AccountResponse ToResponse(this Account account)
    {
        return new AccountResponse
               {
                   Id            = account.Id,
                   AccountNumber = account.AccountNumber,
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
                   DailyLimit        = account.DailyLimit,
                   MonthlyLimit      = account.MonthlyLimit,
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
                   AccountNumber = account.AccountNumber
               };
    }

    public static Account ToAccount(this AccountCreateRequest accountCreateRequest, Guid employeeId)
    {
        var account = new Account
                      {
                          Id               = Guid.NewGuid(),
                          EmployeeId       = employeeId,
                          CurrencyId       = accountCreateRequest.CurrencyId,
                          Balance          = accountCreateRequest.Balance,
                          AvailableBalance = accountCreateRequest.Balance,
                          DailyLimit       = accountCreateRequest.DailyLimit,
                          MonthlyLimit     = accountCreateRequest.MonthlyLimit,
                          CreatedAt        = DateTime.UtcNow,
                          ModifiedAt       = DateTime.UtcNow,
                          ClientId         = accountCreateRequest.ClientId,
                          Name             = accountCreateRequest.Name,
                          Number           = GenerateAccountNumber(),
                          AccountTypeId    = accountCreateRequest.AccountTypeId,
                          CreationDate     = DateOnly.FromDateTime(DateTime.UtcNow),
                          ExpirationDate   = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(5)),
                          Status           = accountCreateRequest.Status
                      };

        return account;
    }

    public static Account ToAccount(this Account account, AccountUpdateClientRequest accountUpdate)
    {
        account.ModifiedAt   = DateTime.UtcNow;
        account.Name         = accountUpdate.Name;
        account.DailyLimit   = accountUpdate.DailyLimit;
        account.MonthlyLimit = accountUpdate.MonthlyLimit;

        return account;
    }

    public static Account ToAccount(this Account account, AccountUpdateEmployeeRequest accountUpdate)
    {
        account.Status     = accountUpdate.Status;
        account.ModifiedAt = DateTime.UtcNow;

        return account;
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

    public static Account ToAccount(this AccountResponse response, Guid clientId)
    {
        return new Account
               {
                   Id               = Guid.NewGuid(),
                   ClientId         = clientId,
                   Name             = response.Name,
                   Number           = response.AccountNumber[7..16],
                   Balance          = 0,
                   AvailableBalance = 0,
                   EmployeeId       = clientId,
                   CurrencyId       = response.Currency.Id,
                   AccountTypeId    = Seeder.AccountType.CheckingAccount.Id,
                   DailyLimit       = 0,
                   MonthlyLimit     = 0,
                   CreationDate     = DateOnly.FromDateTime(DateTime.UtcNow),
                   ExpirationDate   = DateOnly.FromDateTime(DateTime.UtcNow),
                   Status           = false,
                   CreatedAt        = DateTime.UtcNow,
                   ModifiedAt       = DateTime.UtcNow,
               };
    }
}
