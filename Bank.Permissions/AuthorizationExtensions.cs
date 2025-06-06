using Bank.Permissions.Core;
using Bank.Permissions.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Permissions;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddAuthorizationServices(this IServiceCollection services)
    {
        services.AddAuthorization();

        services.AddAuthorizationBuilder()
                .SetDefaultPolicy(new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                                  .Build());

        services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
        services.AddSingleton<IAuthorizationServiceFactory, AuthorizationServiceFactory>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

        return services;
    }
}
