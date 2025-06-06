using Bank.Application.Utilities;

namespace Bank.Http.Configurations;

public static partial class Configuration
{
    public static class Jwt
    {
        public static readonly string ServiceToken = EnvironmentUtilities.GetStringVariable("BANK_HTTP_JWT_SERVICE_TOKEN");
    }
}
