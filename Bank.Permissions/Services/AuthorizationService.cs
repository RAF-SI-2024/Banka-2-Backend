using System.Security.Claims;
using System.Text;

using Bank.Permissions.Configurations;

using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

using OtpNet;

namespace Bank.Permissions.Services;

using Permissions = Domain.Permissions;

public interface IAuthorizationService
{
    public Guid        UserId      { get; }
    public Permissions Permissions { get; }

    public string RegenerateToken();

    public string ConfirmationCode();

    public string GenerateTokenFor(Guid userId, Permissions permissions);

    public bool IsConfirmationCodeValid(string? confirmationCode);
}

internal class AuthorizationService : IAuthorizationService
{
    public Permissions Permissions { get; }
    public Guid        UserId      { get; }

    public AuthorizationService(IHttpContextAccessor httpContextAccessor)
    {
        var userIdClaim      = httpContextAccessor.HttpContext?.User.FindFirst(Configuration.Jwt.Payload.Id);
        var permissionsClaim = httpContextAccessor.HttpContext?.User.FindFirst(Configuration.Jwt.Payload.Permissions);

        UserId      = Guid.TryParse(userIdClaim?.Value, out var userId) ? userId : Guid.Empty;
        Permissions = Permissions.TryParse(permissionsClaim?.Value, out var permissions) ? permissions : new Permissions();
    }

    public string RegenerateToken()
    {
        return GenerateToken(UserId, Permissions);
    }

    public string GenerateTokenFor(Guid userId, Permissions permissions)
    {
        return GenerateToken(userId, permissions);
    }

    public static string GenerateToken(Guid userId, Permissions permission)
    {
        var expirationInMinutes = Configuration.Jwt.ExpirationTimeInMinutes;
        var securityKey         = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.Jwt.SecretKey));
        var credentials         = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
                     {
                         new(Configuration.Jwt.Payload.Id, userId.ToString()),
                         new(Configuration.Jwt.Payload.Permissions, permission.ToString()),
                     };

        var tokenDescriptor = new SecurityTokenDescriptor
                              {
                                  Subject            = new ClaimsIdentity(claims),
                                  Expires            = DateTime.Now.AddMinutes(expirationInMinutes),
                                  SigningCredentials = credentials
                              };

        return new JsonWebTokenHandler().CreateToken(tokenDescriptor);
    }

    public bool IsConfirmationCodeValid(string? confirmationCode)
    {
        if (confirmationCode is null)
            return false;

        var totp = new Totp(UserId.ToByteArray(), mode: OtpHashMode.Sha256, timeCorrection: TimeCorrection.UncorrectedInstance);

        return totp.VerifyTotp(DateTime.UtcNow, confirmationCode, out _);
    }

    public string ConfirmationCode()
    {
        var totp = new Totp(UserId.ToByteArray(), mode: OtpHashMode.Sha256, timeCorrection: TimeCorrection.UncorrectedInstance);

        return totp.ComputeTotp();
    }
}
