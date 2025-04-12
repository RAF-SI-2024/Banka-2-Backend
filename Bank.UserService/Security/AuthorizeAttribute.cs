using Bank.Application.Domain;

namespace Bank.UserService.Security;

public class AuthorizeAttribute : Microsoft.AspNetCore.Authorization.AuthorizeAttribute
{
    private Permission[] _permissions;

    public AuthorizeAttribute() { }

    public AuthorizeAttribute(Permission firstPermission, params Permission[] additionalPermissions)
    {
        var allPermissions = new[] { firstPermission }.Concat(additionalPermissions ?? Array.Empty<Permission>());
        Policy = $"Permission.{string.Join("_", allPermissions)}";
    }

    public Permission[] Permissions
    {
        get => _permissions;
        set
        {
            _permissions = value;

            if (_permissions != null && _permissions.Length > 0)
                Policy = $"Permission.{string.Join("_", _permissions)}";
            else
                throw new ArgumentException("At least one permission must be specified", nameof(Permissions));
        }
    }
}
