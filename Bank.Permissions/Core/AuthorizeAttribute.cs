using Bank.Application.Domain;

namespace Bank.Permissions.Core;

using OriginalAuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute(params Permission[] permissions) : OriginalAuthorizeAttribute
{
    public Permission[] Permissions { get; } = permissions;
}
