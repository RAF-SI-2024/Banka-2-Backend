using Bank.Application.Requests;
using Bank.Application.Responses;

namespace Bank.Link.Core;

public interface IExternalUserData
{
    Task<AccountResponse?> GetAccount(string accountNumber);

    Task<object?> CreateTransaction(TransactionCreateRequest createRequest);

    Task<bool> NotifyTransactionStatus(TransactionNotifyStatusRequest notifyStatusRequest);
}

internal interface IExternalUserDataLink : IExternalUserData
{
    internal BankData BankData { get; }
}

internal class ExternalUserData(IEnumerable<IExternalUserDataLink> userDataLinks) : IExternalUserData
{
    private readonly Dictionary<string, IExternalUserDataLink> m_DataLinkDictionary = userDataLinks.ToDictionary(link => link.BankData.Code, link => link);

    public async Task<AccountResponse?> GetAccount(string accountNumber)
    {
        var bankCode = accountNumber[..3];

        if (!m_DataLinkDictionary.TryGetValue(bankCode, out var dataLink))
            return null;

        return await dataLink.GetAccount(accountNumber);
    }

    public async Task<object?> CreateTransaction(TransactionCreateRequest createRequest)
    {
        var bankCode = createRequest.ToAccountNumber![..3];

        if (!m_DataLinkDictionary.TryGetValue(bankCode, out var dataLink))
            return null;

        return await dataLink.CreateTransaction(createRequest);
    }

    public async Task<bool> NotifyTransactionStatus(TransactionNotifyStatusRequest notifyStatusRequest)
    {
        var bankCode = notifyStatusRequest.AccountNumber![..3];

        if (!m_DataLinkDictionary.TryGetValue(bankCode, out var dataLink))
            return false;

        return await dataLink.NotifyTransactionStatus(notifyStatusRequest);
    }
}
