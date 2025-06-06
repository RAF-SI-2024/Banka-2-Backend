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
    public async Task<Page<AccountResponse>> GetAllAccounts(AccountFilterQuery filter, Pageable pageable)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.Account.GetAll;

        var query = filter.ToQuery()
                          .Map(pageable);

        var response = await httpClient.GetAsync($"{domain}?{query}");

        if (!response.IsSuccessStatusCode)
            return new Page<AccountResponse>([], 0, 0, 0);

        var responsePage = await response.Content.ReadFromJsonAsync<Page<AccountResponse>>();

        return responsePage ?? new Page<AccountResponse>([], 0, 0, 0);
    }

    public async Task<Page<AccountResponse>> GetAllAccountsForClient(Guid clientId, AccountFilterQuery filter, Pageable pageable)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.Account.GetAllForClient.Replace("{clientId:guid}", clientId.ToString());

        var query = filter.ToQuery()
                          .Map(pageable);

        var response = await httpClient.GetAsync($"{domain}?{query}");

        if (!response.IsSuccessStatusCode)
            return new Page<AccountResponse>([], 0, 0, 0);

        var responsePage = await response.Content.ReadFromJsonAsync<Page<AccountResponse>>();

        return responsePage ?? new Page<AccountResponse>([], 0, 0, 0);
    }

    public async Task<AccountResponse?> GetOneAccount(Guid accountId)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.Account.GetOne.Replace("{id:guid}", accountId.ToString());

        var response = await httpClient.GetAsync(domain);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<AccountResponse>();
    }

    public async Task<AccountResponse?> CreateAccount(AccountCreateRequest createRequest)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain  = Endpoints.Account.Create;
        var content = createRequest.ToContent();

        var response = await httpClient.PostAsync(domain, content);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<AccountResponse>();
    }

    public async Task<AccountResponse?> UpdateAccount(Guid accountId, AccountUpdateClientRequest updateRequest)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain  = Endpoints.Account.UpdateClient.Replace("{id:guid}", accountId.ToString());
        var content = updateRequest.ToContent();

        var response = await httpClient.PostAsync(domain, content);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<AccountResponse>();
    }

    public async Task<AccountResponse?> UpdateAccount(Guid accountId, AccountUpdateEmployeeRequest updateRequest)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain  = Endpoints.Account.UpdateEmployee.Replace("{id:guid}", accountId.ToString());
        var content = updateRequest.ToContent();

        var response = await httpClient.PostAsync(domain, content);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<AccountResponse>();
    }
}
