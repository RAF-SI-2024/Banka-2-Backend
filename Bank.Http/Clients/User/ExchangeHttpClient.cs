using System.Net.Http.Json;

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
    public async Task<List<ExchangeResponse>> GetAllExchanges(ExchangeFilterQuery filter)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.Exchange.GetAll;
        var query  = filter.ToQuery();

        var response = await httpClient.GetAsync($"{domain}?{query}");

        if (!response.IsSuccessStatusCode)
            return [];

        return await response.Content.ReadFromJsonAsync<List<ExchangeResponse>>() ?? [];
    }

    public async Task<ExchangeResponse?> GetOneExchange(Guid exchangeId)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.Exchange.GetOne.Replace("{id:guid}", exchangeId.ToString());

        var response = await httpClient.GetAsync(domain);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<ExchangeResponse>();
    }

    public async Task<ExchangeResponse?> GetExchangeByCurrencies(ExchangeBetweenQuery query)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain      = Endpoints.Exchange.GetByCurrencies;
        var queryString = query.ToQuery();

        var response = await httpClient.GetAsync($"{domain}?{queryString}");

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<ExchangeResponse>();
    }

    public async Task<ExchangeResponse?> MakeExchange(ExchangeMakeExchangeRequest makeExchangeRequest)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain  = Endpoints.Exchange.MakeExchange;
        var content = makeExchangeRequest.ToContent();

        var response = await httpClient.PostAsync(domain, content);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<ExchangeResponse>();
    }

    public async Task<ExchangeResponse?> UpdateExchange(Guid exchangeId, ExchangeUpdateRequest updateRequest)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain  = Endpoints.Exchange.Update.Replace("{id:guid}", exchangeId.ToString());
        var content = updateRequest.ToContent();

        var response = await httpClient.PutAsync(domain, content);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<ExchangeResponse>();
    }
}
