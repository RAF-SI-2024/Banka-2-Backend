using Bank.Application.Utilities;

namespace Bank.Link.Configurations;

public static partial class Configuration
{
    public static class Jwt
    {
        public static readonly string B3Token = EnvironmentUtilities.GetStringVariable("BANK_LINK_JWT_B3_TOKEN");
    }
}
