using Bank.Application.Utilities;

namespace Bank.ExchangeService.Configurations;

public static partial class Configuration
{
    public static class Jwt
    {
        public static readonly string SecretKey               = EnvironmentUtilities.GetStringVariable("BANK_USER_JWT_SECRET_KEY");
        public static readonly int    ExpirationTimeInMinutes = EnvironmentUtilities.GetIntVariable("BANK_USER_JWT_EXPIRATION_TIME_IN_MINUTES");
    }
}
