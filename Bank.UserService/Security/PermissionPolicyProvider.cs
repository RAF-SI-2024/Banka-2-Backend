using Bank.Application.Domain;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Bank.UserService.Security;

public class PermissionPolicyProvider : DefaultAuthorizationPolicyProvider
{
    public PermissionPolicyProvider(IOptions<AuthorizationOptions> options) : base(options) { }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if (!policyName.StartsWith("Permission."))
            return await base.GetPolicyAsync(policyName);

        var permissionsString = policyName.Substring("Permission.".Length);

        var permissionValues = permissionsString.Split('_')
                                                .Select(p => p.Trim())
                                                .Where(p => !string.IsNullOrEmpty(p));

        var permissions = new List<Permission>();

        foreach (var permValue in permissionValues)
            if (Enum.TryParse<Permission>(permValue, out var permission))
                permissions.Add(permission);

        var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                     .AddRequirements(new PermissionRequirement(permissions.ToArray()))
                                                     .Build();

        return policy;
    }
}
