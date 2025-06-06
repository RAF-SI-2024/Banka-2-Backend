using System.Security.Claims;
using System.Text;

using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Bank.ExchangeService.Test.Services;

public static class JwtTestHelper
{
    public static string GenerateMockJwtToken(Guid userId, Permissions.Domain.Permissions permissions)
    {
        var secretKey   = "test-secret-key-123456789012345678901234567890"; // 256-bit key
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
                     {
                         new("id", userId.ToString()),
                         new("permissions", permissions.ToString())
                     };

        var tokenDescriptor = new SecurityTokenDescriptor
                              {
                                  Subject            = new ClaimsIdentity(claims),
                                  Expires            = DateTime.UtcNow.AddMinutes(30),
                                  SigningCredentials = credentials
                              };

        return new JsonWebTokenHandler().CreateToken(tokenDescriptor);
    }
}
