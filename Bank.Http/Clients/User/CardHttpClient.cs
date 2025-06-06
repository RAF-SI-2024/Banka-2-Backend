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
    public async Task<Page<CardResponse>> GetAllCards(CardFilterQuery filter, Pageable pageable)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.Card.GetAll;

        var query = filter.ToQuery()
                          .Map(pageable);

        var response = await httpClient.GetAsync($"{domain}?{query}");

        if (!response.IsSuccessStatusCode)
            return new Page<CardResponse>([], 0, 0, 0);

        var responsePage = await response.Content.ReadFromJsonAsync<Page<CardResponse>>();

        return responsePage ?? new Page<CardResponse>([], 0, 0, 0);
    }

    public async Task<Page<CardResponse>> GetAllCardsForAccount(Guid accountId, CardFilterQuery filter, Pageable pageable)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.Card.GetAllForAccount.Replace("{accountId:guid}", accountId.ToString());

        var query = filter.ToQuery()
                          .Map(pageable);

        var response = await httpClient.GetAsync($"{domain}?{query}");

        if (!response.IsSuccessStatusCode)
            return new Page<CardResponse>([], 0, 0, 0);

        var responsePage = await response.Content.ReadFromJsonAsync<Page<CardResponse>>();

        return responsePage ?? new Page<CardResponse>([], 0, 0, 0);
    }

    public async Task<Page<CardResponse>> GetAllCardsForClient(Guid clientId)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.Card.GetAllForClient.Replace("{clientId:guid}", clientId.ToString());

        var response = await httpClient.GetAsync(domain);

        if (!response.IsSuccessStatusCode)
            return new Page<CardResponse>([], 0, 0, 0);

        var responsePage = await response.Content.ReadFromJsonAsync<Page<CardResponse>>();

        return responsePage ?? new Page<CardResponse>([], 0, 0, 0);
    }

    public async Task<CardResponse?> GetOneCard(Guid cardId)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.Card.GetOne.Replace("{id:guid}", cardId.ToString());

        var response = await httpClient.GetAsync(domain);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<CardResponse>();
    }

    public async Task<CardResponse?> CreateCard(CardCreateRequest createRequest)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain  = Endpoints.Card.Create;
        var content = createRequest.ToContent();

        var response = await httpClient.PostAsync(domain, content);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<CardResponse>();
    }

    public async Task<CardResponse?> UpdateCard(Guid cardId, CardUpdateStatusRequest updateRequest)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain  = Endpoints.Card.UpdateStatus.Replace("{id:guid}", cardId.ToString());
        var content = updateRequest.ToContent();

        var response = await httpClient.PutAsync(domain, content);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<CardResponse>();
    }

    public async Task<CardResponse?> UpdateCard(Guid cardId, CardUpdateLimitRequest updateRequest)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain  = Endpoints.Card.UpdateLimit.Replace("{id:guid}", cardId.ToString());
        var content = updateRequest.ToContent();

        var response = await httpClient.PutAsync(domain, content);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<CardResponse>();
    }
}
