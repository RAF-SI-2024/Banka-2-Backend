using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Bank.Permissions.Core;

public class PermissionPolicyProvider(IOptions<AuthorizationOptions> options, IHttpContextAccessor httpContextAccessor) : IAuthorizationPolicyProvider
{
    private readonly DefaultAuthorizationPolicyProvider m_FallbackPolicyProvider = new(options);
    private readonly IHttpContextAccessor               m_HttpContextAccessor    = httpContextAccessor;

    public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        return m_FallbackPolicyProvider.GetPolicyAsync(policyName);
    }

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
    {
        var endpoint = m_HttpContextAccessor.HttpContext?.GetEndpoint();

        if (endpoint is null)
            return m_FallbackPolicyProvider.GetDefaultPolicyAsync();

        var authorizeAttributes = endpoint.Metadata.GetOrderedMetadata<AuthorizeAttribute>();

        var permissions = authorizeAttributes.SelectMany(authorizeAttribute => authorizeAttribute.Permissions)
                                             .Distinct()
                                             .ToArray();

        return Task.FromResult(new AuthorizationPolicyBuilder().AddRequirements(new PermissionRequirements(authorizeAttributes.Any(), permissions))
                                                               .Build());
    }

    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
    {
        return m_FallbackPolicyProvider.GetFallbackPolicyAsync();
    }
}
