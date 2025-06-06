using Bank.Application.Extensions;
using Bank.Application.Utilities;

namespace Bank.Permissions.Configurations;

public static partial class Configuration
{
    public static class Jwt
    {
        public static readonly string SecretKey               = EnvironmentUtilities.GetStringVariable("BANK_PERMISSIONS_JWT_SECRET_KEY");
        public static readonly int    ExpirationTimeInMinutes = EnvironmentUtilities.GetIntVariable("BANK_PERMISSIONS_JWT_EXPIRATION_TIME_IN_MINUTES");

        // @formatter:off
        public static class Payload
        {
            public static readonly string Id          = nameof(Id).ToCamelCase();
            public static readonly string Permissions = nameof(Permissions).ToCamelCase();
        }
    }
}
