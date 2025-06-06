using Bank.Application.Domain;

using Microsoft.AspNetCore.Authorization;

namespace Bank.Permissions.Core;

public class PermissionRequirements(bool needsAuthentication, params Permission[] permissions) : IAuthorizationRequirement
{
    public Permission[] Permissions         { get; } = permissions;
    public bool         NeedsAuthentication { get; } = needsAuthentication;
}
