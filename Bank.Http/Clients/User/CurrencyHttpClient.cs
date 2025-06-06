using System.Net.Http.Json;

using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.Http.Configurations;
using Bank.Http.Mapper.Query;

namespace Bank.Http.Clients.User;

internal partial class UserServiceHttpClient
{
    public async Task<List<CurrencyResponse>> GetAllCurrencies(CurrencyFilterQuery filter)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.Currency.GetAll;
        var query  = filter.ToQuery();

        var response = await httpClient.GetAsync($"{domain}?{query}");

        if (!response.IsSuccessStatusCode)
            return [];

        return await response.Content.ReadFromJsonAsync<List<CurrencyResponse>>() ?? [];
    }

    public async Task<List<CurrencySimpleResponse>> GetAllSimpleCurrencies(CurrencyFilterQuery filter)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.Currency.GetAllSimple;
        var query  = filter.ToQuery();

        var response = await httpClient.GetAsync($"{domain}?{query}");

        if (!response.IsSuccessStatusCode)
            return [];

        return await response.Content.ReadFromJsonAsync<List<CurrencySimpleResponse>>() ?? [];
    }

    public async Task<CurrencyResponse?> GetOneCurrency(Guid currencyId)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.Currency.GetOne.Replace("{id:guid}", currencyId.ToString());

        var response = await httpClient.GetAsync(domain);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<CurrencyResponse>();
    }

    public async Task<CurrencySimpleResponse?> GetOneSimpleCurrency(Guid currencyId)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.Currency.GetOneSimple.Replace("{id:guid}", currencyId.ToString());

        var response = await httpClient.GetAsync(domain);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<CurrencySimpleResponse>();
    }
}
