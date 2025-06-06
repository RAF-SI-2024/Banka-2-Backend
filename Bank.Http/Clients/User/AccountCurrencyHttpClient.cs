using System.Net.Http.Json;

using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.Http.Configurations;
using Bank.Http.Mapper.Content;
using Bank.Http.Mapper.Query;

namespace Bank.Http.Clients.User;

internal partial class UserServiceHttpClient
{
    public async Task<Page<AccountCurrencyResponse>> GetAllAccountCurrencies(Pageable pageable)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.AccountCurrency.GetAll;
        var query  = pageable.ToQuery();

        var response = await httpClient.GetAsync($"{domain}?{query}");

        if (!response.IsSuccessStatusCode)
            return new Page<AccountCurrencyResponse>([], 0, 0, 0);

        var responsePage = await response.Content.ReadFromJsonAsync<Page<AccountCurrencyResponse>>();

        return responsePage ?? new Page<AccountCurrencyResponse>([], 0, 0, 0);
    }

    public async Task<AccountCurrencyResponse?> GetOneAccountCurrency(Guid accountCurrencyId)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.AccountCurrency.GetOne.Replace("{id:guid}", accountCurrencyId.ToString());

        var response = await httpClient.GetAsync(domain);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<AccountCurrencyResponse>();
    }

    public async Task<AccountCurrencyResponse?> CreateAccountCurrency(AccountCurrencyCreateRequest createRequest)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain  = Endpoints.AccountCurrency.Create;
        var content = createRequest.ToContent();

        var response = await httpClient.PostAsync(domain, content);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<AccountCurrencyResponse>();
    }

    public async Task<AccountCurrencyResponse?> UpdateAccountCurrency(Guid accountCurrencyId, AccountCurrencyClientUpdateRequest updateRequest)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain  = Endpoints.AccountCurrency.UpdateClient.Replace("{id:guid}", accountCurrencyId.ToString());
        var content = updateRequest.ToContent();

        var response = await httpClient.PutAsync(domain, content);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<AccountCurrencyResponse>();
    }
}
