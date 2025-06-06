using Microsoft.AspNetCore.Authorization;

namespace Bank.Permissions.Core;

using Permissions = Domain.Permissions;

public class PermissionHandler : AuthorizationHandler<PermissionRequirements>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirements requirement)
    {
        if (requirement.Permissions.Length == 0)
        {
            if (!requirement.NeedsAuthentication || context.User.Identity?.IsAuthenticated is true)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }

        var permissionsClaim = context.User.FindFirst(claim => claim.Type == Permissions.Identifier);

        if (permissionsClaim is null || Permissions.TryParse(permissionsClaim.Value, out var userPermissions) is false)
            return Task.CompletedTask;

        if (requirement.Permissions.Any(requiredPermission => userPermissions.HasPermission(requiredPermission)))
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
