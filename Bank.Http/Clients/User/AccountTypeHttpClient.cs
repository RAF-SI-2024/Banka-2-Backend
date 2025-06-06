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
    public async Task<Page<AccountTypeResponse>> GetAllAccountTypes(AccountTypeFilterQuery filter, Pageable pageable)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.AccountType.GetAll;

        var query = filter.ToQuery()
                          .Map(pageable);

        var response = await httpClient.GetAsync($"{domain}?{query}");

        if (!response.IsSuccessStatusCode)
            return new Page<AccountTypeResponse>([], 0, 0, 0);

        var responsePage = await response.Content.ReadFromJsonAsync<Page<AccountTypeResponse>>();

        return responsePage ?? new Page<AccountTypeResponse>([], 0, 0, 0);
    }

    public async Task<AccountTypeResponse?> GetOneAccountType(Guid accountTypeId)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.AccountType.GetOne.Replace("{id:guid}", accountTypeId.ToString());

        var response = await httpClient.GetAsync(domain);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<AccountTypeResponse>();
    }
}
