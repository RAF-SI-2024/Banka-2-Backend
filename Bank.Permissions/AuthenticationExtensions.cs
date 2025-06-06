using System.IdentityModel.Tokens.Jwt;
using System.Text;

using Bank.Permissions.Configurations;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Bank.Permissions;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddAuthenticationServices(this IServiceCollection services)
    {
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwtOptions => jwtOptions.TokenValidationParameters = new TokenValidationParameters
                                                                                   {
                                                                                       IssuerSigningKey =
                                                                                       new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration
                                                                                                                                       .Jwt
                                                                                                                                       .SecretKey)),
                                                                                       ValidateIssuerSigningKey = true,
                                                                                       ValidateLifetime         = true,
                                                                                       ValidateIssuer           = false,
                                                                                       ValidateAudience         = false,
                                                                                       ClockSkew                = TimeSpan.Zero
                                                                                   });

        return services;
    }
}
