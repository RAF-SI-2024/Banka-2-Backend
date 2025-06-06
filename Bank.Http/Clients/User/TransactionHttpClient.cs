using System.Net.Http.Json;

using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.Http.Configurations;
using Bank.Http.Mapper.Content;
using Bank.Http.Mapper.Query;

namespace Bank.Http.Clients.User;

internal partial class UserServiceHttpClient
{
    public async Task<Page<TransactionResponse>> GetAllTransactions(TransactionFilterQuery filter, Pageable pageable)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.Transaction.GetAll;

        var query = filter.ToQuery()
                          .Map(pageable);

        var response = await httpClient.GetAsync($"{domain}?{query}");

        if (!response.IsSuccessStatusCode)
            return new Page<TransactionResponse>([], 0, 0, 0);

        var responsePage = await response.Content.ReadFromJsonAsync<Page<TransactionResponse>>();

        return responsePage ?? new Page<TransactionResponse>([], 0, 0, 0);
    }

    public async Task<Page<TransactionResponse>> GetAllTransactionsForAccount(Guid accountId, TransactionFilterQuery filter, Pageable pageable)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.Transaction.GetAllForAccount.Replace("{accountId:guid}", accountId.ToString());

        var query = filter.ToQuery()
                          .Map(pageable);

        var response = await httpClient.GetAsync($"{domain}?{query}");

        if (!response.IsSuccessStatusCode)
            return new Page<TransactionResponse>([], 0, 0, 0);

        var responsePage = await response.Content.ReadFromJsonAsync<Page<TransactionResponse>>();

        return responsePage ?? new Page<TransactionResponse>([], 0, 0, 0);
    }

    public async Task<TransactionResponse?> GetOneTransaction(Guid transactionId)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.Transaction.GetOne.Replace("{id:guid}", transactionId.ToString());

        var response = await httpClient.GetAsync(domain);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<TransactionResponse>();
    }

    public async Task<TransactionCreateResponse?> CreateTransaction(TransactionCreateRequest createRequest)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain  = Endpoints.Transaction.Create;
        var content = createRequest.ToContent();

        var response = await httpClient.PostAsync(domain, content);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<TransactionCreateResponse>();
    }

    public async Task<TransactionResponse?> UpdateTransaction(Guid transactionId, TransactionUpdateRequest updateRequest)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain  = Endpoints.Transaction.Update.Replace("{id:guid}", transactionId.ToString());
        var content = updateRequest.ToContent();

        var response = await httpClient.PutAsync(domain, content);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<TransactionResponse>();
    }
}
