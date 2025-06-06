using System.Net.Http.Json;
using System.Web;

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
    public async Task<Page<UserResponse>> GetAllUsers(UserFilterQuery filter, Pageable pageable)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.User.GetAll;

        var query = filter.ToQuery()
                          .Map(pageable);

        var response = await httpClient.GetAsync($"{domain}?{query}");

        if (!response.IsSuccessStatusCode)
            return new Page<UserResponse>([], 0, 0, 0);

        var responsePage = await response.Content.ReadFromJsonAsync<Page<UserResponse>>();

        return responsePage ?? new Page<UserResponse>([], 0, 0, 0);
    }

    public async Task<UserResponse?> GetOneUser(Guid userId)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.User.GetOne.Replace("{id:guid}", userId.ToString());

        var response = await httpClient.GetAsync(domain);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<UserResponse>();
    }

    public async Task<UserLoginResponse?> Login(UserLoginRequest loginRequest)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain  = Endpoints.User.Login;
        var content = loginRequest.ToContent();

        var response = await httpClient.PostAsync(domain, content);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<UserLoginResponse>();
    }

    public async Task Activate(UserActivationRequest activationRequest, string token)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain = Endpoints.User.Activate;
        var query  = HttpUtility.ParseQueryString(string.Empty);
        query[nameof(token)] = token;

        var content = activationRequest.ToContent();

        await httpClient.PostAsync($"{domain}?{query}", content);
    }

    public async Task RequestPasswordReset(UserRequestPasswordResetRequest passwordResetRequest)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain  = Endpoints.User.RequestPasswordReset;
        var content = passwordResetRequest.ToContent();

        await httpClient.PostAsync(domain, content);
    }

    public async Task PasswordReset(UserPasswordResetRequest passwordResetRequest, string token)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain  = Endpoints.User.PasswordReset;
        var content = passwordResetRequest.ToContent();

        await httpClient.PostAsync(domain, content);
    }

    public async Task UpdateUserPermission(Guid userId, UserUpdatePermissionRequest updatePermissionRequest)
    {
        using var httpClient = m_HttpClientFactory.CreateClient(Configuration.Client.Name.UserService);

        var domain  = Endpoints.User.PasswordReset;
        var content = updatePermissionRequest.ToContent();

        await httpClient.PutAsync(domain, content);
    }
}
