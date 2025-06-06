using Bank.Http.Clients.User;
using Bank.Http.Configurations;

using Microsoft.Extensions.DependencyInjection;

namespace Bank.Http;

public static class HttpExtensions
{
    public static IServiceCollection AddUserServiceHttpClient(this IServiceCollection services)
    {
        services.AddHttpClient(Configuration.Client.Name.UserService, httpClient =>
                                                                      {
                                                                          httpClient.BaseAddress = new Uri($"{Configuration.Client.BaseUrl.UserService}");
                                                                          httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Configuration.Jwt.ServiceToken}");
                                                                      });

        services.AddSingleton<IUserServiceHttpClient, UserServiceHttpClient>();

        return services;
    }
}
