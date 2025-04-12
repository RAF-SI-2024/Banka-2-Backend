using Bank.Application.Domain;

namespace Bank.ExchangeService.Services;

public interface IAuthorizationService
{
    public Guid UserId { get; }
    public Role Role   { get; }
}

public class AuthorizationService : IAuthorizationService
{
    public Guid UserId { get; }
    public Role Role   { get; }

    public AuthorizationService(IHttpContextAccessor httpContextAccessor)
    {
        var userId = httpContextAccessor.HttpContext?.User.FindFirst("id");
        var role   = httpContextAccessor.HttpContext?.User.FindFirst("role");

        UserId = userId != null ? Guid.Parse(userId.Value) : Guid.Empty;
        Role   = role   != null ? Enum.TryParse(role.Value, out Role myRole) ? myRole : Role.Invalid : Role.Invalid;
    }
}
