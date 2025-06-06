using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Permissions.Services;

public interface IAuthorizationServiceFactory
{
    IAuthorizationService AuthorizationService { get; }
}

internal class AuthorizationServiceFactory(IServiceScopeFactory serviceScopeFactory) : IAuthorizationServiceFactory
{
    private readonly IServiceScopeFactory m_ServiceScopeFactory = serviceScopeFactory;

    public IAuthorizationService AuthorizationService => CreateAuthorizationService();

    private AuthorizationService CreateAuthorizationService()
    {
        var contextAccessor = m_ServiceScopeFactory.CreateScope()
                                                   .ServiceProvider.GetRequiredService<IHttpContextAccessor>();

        var authService = new AuthorizationService(contextAccessor);

        return authService;
    }
}
