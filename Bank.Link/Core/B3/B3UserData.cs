using System.Net.Http.Json;

using Bank.Application.Domain;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.Link.Endpoints;
using Bank.Link.Mapper.B3.Content;
using Bank.Link.Mapper.B3.Query;
using Bank.Link.Queries;
using Bank.Link.Responses;
using Bank.Link.Service;

namespace Bank.Link.Core.B3;

internal class B3UserDataLink(BankData bankData, IHttpClientFactory httpClientFactory, IDataService dataService) : IExternalUserDataLink
{
    private readonly IHttpClientFactory m_HttpClientFactory = httpClientFactory;
    private readonly IDataService       m_DataService       = dataService;

    public BankData BankData { get; } = bankData;

    public async Task<AccountResponse?> GetAccount(string accountNumber)
    {
        var httpClient = m_HttpClientFactory.CreateClient(BankData.Code);

        var filter   = new Query.B3.AccountFilter(accountNumber: accountNumber);
        var pageable = Pageable.Create(0, 1);

        var domain = Endpoint.B3.Account.GetAll;

        var query = filter.ToQuery()
                          .Map(pageable);

        var response = await httpClient.GetAsync($"{domain}?{query}");

        if (!response.IsSuccessStatusCode)
            return null;

        var responseList = await response.Content.ReadFromJsonAsync<Response.B3.Page<Response.B3.AccountResponse>>();

        if (responseList is null || responseList.Content.Count == 0)
            return null;

        var accountResponse  = responseList.Content.First();
        var currencyResponse = m_DataService.GetCurrencyByCode(accountResponse.CurrencyCode);

        return currencyResponse is null ? null : accountResponse.ToNative(currencyResponse, m_DataService.GetAccountType(accountNumber)!);
    }

    public async Task<object?> CreateTransaction(TransactionCreateRequest createRequest)
    {
        var httpClient = m_HttpClientFactory.CreateClient(BankData.Code);

        var domain      = Endpoint.B3.Transaction.Create;
        var requestData = createRequest.ToB3(m_DataService.GetTransactionCodeById(createRequest.CodeId)!.Code);

        var response = await httpClient.PostAsync(domain, requestData.ToContent());

        if (!response.IsSuccessStatusCode)
            return null;

        var responseData = await response.Content.ReadFromJsonAsync<Response.B3.TransactionResponse>();

        return responseData?.Id;
    }

    public async Task<bool> NotifyTransactionStatus(TransactionNotifyStatusRequest notifyStatusRequest)
    {
        var httpClient = m_HttpClientFactory.CreateClient(BankData.Code);

        var domain      = Endpoint.B3.Transaction.PutStatus.Replace("{id:long}", notifyStatusRequest.TransactionId?.ToString());
        var requestData = notifyStatusRequest.ToB3();

        var response = await httpClient.PutAsync(domain, requestData.ToContent());

        return response.IsSuccessStatusCode;
    }
}
