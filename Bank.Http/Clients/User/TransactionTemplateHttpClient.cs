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
    public async Task<Page<TransactionTemplateResponse>> GetAllTransactionTemplates(Pageable pageable)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.TransactionTemplate.GetAll;
        var query  = pageable.ToQuery();

        var response = await httpClient.GetAsync($"{domain}?{query}");

        if (!response.IsSuccessStatusCode)
            return new Page<TransactionTemplateResponse>([], 0, 0, 0);

        var responsePage = await response.Content.ReadFromJsonAsync<Page<TransactionTemplateResponse>>();

        return responsePage ?? new Page<TransactionTemplateResponse>([], 0, 0, 0);
    }

    public async Task<TransactionTemplateResponse?> GetOneTransactionTemplate(Guid transactionTemplateId)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.TransactionTemplate.GetOne.Replace("{id:guid}", transactionTemplateId.ToString());

        var response = await httpClient.GetAsync(domain);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<TransactionTemplateResponse>();
    }

    public async Task<TransactionTemplateResponse?> CreateTransactionTemplate(TransactionTemplateCreateRequest createRequest)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain  = Endpoints.TransactionTemplate.Create;
        var content = createRequest.ToContent();

        var response = await httpClient.PostAsync(domain, content);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<TransactionTemplateResponse>();
    }

    public async Task<TransactionTemplateResponse?> UpdateTransactionTemplate(Guid transactionTemplateId, TransactionTemplateUpdateRequest updateRequest)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain  = Endpoints.TransactionTemplate.Update.Replace("{id:guid}", transactionTemplateId.ToString());
        var content = updateRequest.ToContent();

        var response = await httpClient.PutAsync(domain, content);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<TransactionTemplateResponse>();
    }
}
