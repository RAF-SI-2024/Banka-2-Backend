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
    public async Task<Page<LoanResponse>> GetAllLoans(LoanFilterQuery filter, Pageable pageable)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.Loan.GetAll;

        var query = filter.ToQuery()
                          .Map(pageable);

        var response = await httpClient.GetAsync($"{domain}?{query}");

        if (!response.IsSuccessStatusCode)
            return new Page<LoanResponse>([], 0, 0, 0);

        var responsePage = await response.Content.ReadFromJsonAsync<Page<LoanResponse>>();

        return responsePage ?? new Page<LoanResponse>([], 0, 0, 0);
    }

    public async Task<LoanResponse?> GetOneLoan(Guid loanId)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.Loan.GetOne.Replace("{id:guid}", loanId.ToString());

        var response = await httpClient.GetAsync(domain);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<LoanResponse>();
    }

    public async Task<Page<LoanResponse>> GetAllLoansForClient(Guid clientId, Pageable pageable)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.Loan.GetAllForClient.Replace("{clientId:guid}", clientId.ToString());
        var query  = pageable.ToQuery();

        var response = await httpClient.GetAsync($"{domain}?{query}");

        if (!response.IsSuccessStatusCode)
            return new Page<LoanResponse>([], 0, 0, 0);

        var responsePage = await response.Content.ReadFromJsonAsync<Page<LoanResponse>>();

        return responsePage ?? new Page<LoanResponse>([], 0, 0, 0);
    }

    public async Task<LoanResponse?> CreateLoan(LoanCreateRequest createRequest)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain  = Endpoints.Loan.Create;
        var content = createRequest.ToContent();

        var response = await httpClient.PostAsync(domain, content);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<LoanResponse>();
    }

    public async Task<LoanResponse?> UpdateLoan(Guid loanId, LoanUpdateRequest updateRequest)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain  = Endpoints.Loan.Update.Replace("{id:guid}", loanId.ToString());
        var content = updateRequest.ToContent();

        var response = await httpClient.PutAsync(domain, content);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<LoanResponse>();
    }
}
