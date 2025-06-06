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
    public async Task<Page<LoanTypeResponse>> GetAllLoanTypes(Pageable pageable)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.LoanType.GetAll;
        var query  = pageable.ToQuery();

        var response = await httpClient.GetAsync($"{domain}?{query}");

        if (!response.IsSuccessStatusCode)
            return new Page<LoanTypeResponse>([], 0, 0, 0);

        var responsePage = await response.Content.ReadFromJsonAsync<Page<LoanTypeResponse>>();

        return responsePage ?? new Page<LoanTypeResponse>([], 0, 0, 0);
    }

    public async Task<LoanTypeResponse?> GetOneLoanType(Guid loanTypeId)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.LoanType.GetOne.Replace("{id:guid}", loanTypeId.ToString());

        var response = await httpClient.GetAsync(domain);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<LoanTypeResponse>();
    }

    public async Task<LoanTypeResponse?> CreateLoanType(LoanTypeCreateRequest createRequest)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain  = Endpoints.LoanType.Create;
        var content = createRequest.ToContent();

        var response = await httpClient.PostAsync(domain, content);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<LoanTypeResponse>();
    }

    public async Task<LoanTypeResponse?> UpdateLoanType(Guid loanTypeId, LoanTypeUpdateRequest updateRequest)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain  = Endpoints.LoanType.Update.Replace("{id:guid}", loanTypeId.ToString());
        var content = updateRequest.ToContent();

        var response = await httpClient.PutAsync(domain, content);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<LoanTypeResponse>();
    }
}
