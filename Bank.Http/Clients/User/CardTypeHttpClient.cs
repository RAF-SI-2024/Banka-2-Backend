using System.Net.Http.Json;

using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.Http.Configurations;
using Bank.Http.Mapper.Query;

namespace Bank.Http.Clients.User;

internal partial class UserServiceHttpClient
{
    public async Task<Page<CardTypeResponse>> GetAllCardTypes(CardTypeFilterQuery filter, Pageable pageable)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.CardType.GetAll;

        var query = filter.ToQuery()
                          .Map(pageable);

        var response = await httpClient.GetAsync($"{domain}?{query}");

        if (!response.IsSuccessStatusCode)
            return new Page<CardTypeResponse>([], 0, 0, 0);

        var responsePage = await response.Content.ReadFromJsonAsync<Page<CardTypeResponse>>();

        return responsePage ?? new Page<CardTypeResponse>([], 0, 0, 0);
    }

    public async Task<CardTypeResponse?> GetOneCardType(Guid cardTypeId)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.CardType.GetOne.Replace("{id:guid}", cardTypeId.ToString());

        var response = await httpClient.GetAsync(domain);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<CardTypeResponse>();
    }
}
