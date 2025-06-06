using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.Link.Mapper.B3.Content;
using Bank.Link.Responses;
using Bank.Link.Service;

namespace Bank.Link.Core.Test;

internal class TestExternalUserDataLink(BankData bankData, IDataService dataService) : IExternalUserDataLink
{
    private readonly IDataService m_DataService = dataService;

    public BankData BankData { get; } = bankData;

    public Task<AccountResponse?> GetAccount(string accountNumber)
    {
        var owner = new Response.B3.AccountClientResponse()
                    {
                        Id        = 0,
                        FirstName = "FirstName",
                        LastName  = "LastName",
                        Email     = "email@email.com",
                    };

        return Task.FromResult(new Response.B3.AccountResponse
                               {
                                   Name                = "Zazer",
                                   AccountNumber       = accountNumber,
                                   ClientId            = 0,
                                   CompanyId           = 0,
                                   CreatedByEmployeeId = 0,
                                   CreationDate        = null!,
                                   ExpirationDate      = null!,
                                   CurrencyCode        = null!,
                                   Status              = null!,
                                   Balance             = 0,
                                   AvailableBalance    = 0,
                                   DailyLimit          = 0,
                                   MonthlyLimit        = 0,
                                   DailySpending       = 0,
                                   MonthlySpending     = 0,
                                   OwnershipType       = null!,
                                   Owner               = owner,
                                   AccountCategory     = null!
                               }.ToNative(m_DataService.GetCurrencies()
                                                       .First(), m_DataService.GetAccountType(accountNumber)!))!;
    }

    public Task<object?> CreateTransaction(TransactionCreateRequest createRequest)
    {
        return Task.FromResult<object?>(1L);
    }

    public Task<bool> NotifyTransactionStatus(TransactionNotifyStatusRequest notifyStatusRequest)
    {
        return Task.FromResult(true);
    }
}
