using Bank.Application.Domain;

using Microsoft.AspNetCore.Authorization;

namespace Bank.UserService.Security;

public class PermissionRequirement : IAuthorizationRequirement
{
    public PermissionRequirement(Permission[] permissions)
    {
        Permissions = permissions;
    }

    public Permission[] Permissions { get; }
}

public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var permissionClaim = context.User.FindFirst("permission");

        if (permissionClaim is not null && long.TryParse(permissionClaim.Value, out var permissionValue))
        {
            var userPermissions = new AuthPermission(permissionValue);

            if (requirement.Permissions.Any(userPermissions.HasPermission))
                context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
