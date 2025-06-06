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
    public async Task<Page<CompanyResponse>> GetAllCompanies(CompanyFilterQuery filter, Pageable pageable)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.Company.GetAll;

        var query = filter.ToQuery()
                          .Map(pageable);

        var response = await httpClient.GetAsync($"{domain}?{query}");

        if (!response.IsSuccessStatusCode)
            return new Page<CompanyResponse>([], 0, 0, 0);

        var responsePage = await response.Content.ReadFromJsonAsync<Page<CompanyResponse>>();

        return responsePage ?? new Page<CompanyResponse>([], 0, 0, 0);
    }

    public async Task<CompanyResponse?> GetOneCompany(Guid companyId)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.Company.GetOne.Replace("{id:guid}", companyId.ToString());

        var response = await httpClient.GetAsync(domain);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<CompanyResponse>();
    }

    public async Task<CompanyResponse?> CreateCompany(CompanyCreateRequest createRequest)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain  = Endpoints.Company.Create;
        var content = createRequest.ToContent();

        var response = await httpClient.PostAsync(domain, content);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<CompanyResponse>();
    }

    public async Task<CompanyResponse?> UpdateCompany(Guid companyId, CompanyUpdateRequest updateRequest)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain  = Endpoints.Company.Update.Replace("{id:guid}", companyId.ToString());
        var content = updateRequest.ToContent();

        var response = await httpClient.PutAsync(domain, content);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<CompanyResponse>();
    }
}
