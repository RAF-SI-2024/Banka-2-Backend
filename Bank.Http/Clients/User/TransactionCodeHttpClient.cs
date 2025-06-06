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
    public async Task<Page<TransactionCodeResponse>> GetAllTransactionCodes(TransactionCodeFilterQuery filter, Pageable pageable)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.TransactionCode.GetAll;

        var query = filter.ToQuery()
                          .Map(pageable);

        var response = await httpClient.GetAsync($"{domain}?{query}");

        if (!response.IsSuccessStatusCode)
            return new Page<TransactionCodeResponse>([], 0, 0, 0);

        var responsePage = await response.Content.ReadFromJsonAsync<Page<TransactionCodeResponse>>();

        return responsePage ?? new Page<TransactionCodeResponse>([], 0, 0, 0);
    }

    public async Task<TransactionCodeResponse?> GetOneTransactionCode(Guid transactionCodeId)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.TransactionCode.GetOne.Replace("{id:guid}", transactionCodeId.ToString());

        var response = await httpClient.GetAsync(domain);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<TransactionCodeResponse>();
    }
}
