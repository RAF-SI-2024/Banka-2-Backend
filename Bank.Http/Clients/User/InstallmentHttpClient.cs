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
    public async Task<Page<InstallmentResponse>> GetAllInstallmentsForLoan(Guid loanId, Pageable pageable)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.Installment.GetAllByLoan.Replace("{id:guid}", loanId.ToString());
        var query  = pageable.ToQuery();

        var response = await httpClient.GetAsync($"{domain}?{query}");

        if (!response.IsSuccessStatusCode)
            return new Page<InstallmentResponse>([], 0, 0, 0);

        var responsePage = await response.Content.ReadFromJsonAsync<Page<InstallmentResponse>>();

        return responsePage ?? new Page<InstallmentResponse>([], 0, 0, 0);
    }

    public async Task<InstallmentResponse?> GetOneInstallment(Guid installmentId)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.Installment.GetOne.Replace("{id:guid}", installmentId.ToString());

        var response = await httpClient.GetAsync(domain);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<InstallmentResponse>();
    }

    public async Task<InstallmentResponse?> CreateInstallment(InstallmentCreateRequest createRequest)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain  = Endpoints.Installment.Create;
        var content = createRequest.ToContent();

        var response = await httpClient.PostAsync(domain, content);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<InstallmentResponse>();
    }

    public async Task<InstallmentResponse?> UpdateInstallment(Guid installmentId, InstallmentUpdateRequest updateRequest)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain  = Endpoints.Installment.Update.Replace("{id:guid}", installmentId.ToString());
        var content = updateRequest.ToContent();

        var response = await httpClient.PutAsync(domain, content);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<InstallmentResponse>();
    }
}
