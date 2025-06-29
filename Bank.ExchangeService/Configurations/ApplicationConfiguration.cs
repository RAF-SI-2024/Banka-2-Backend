using Bank.Application.Domain;
using Bank.Application.Utilities;

namespace Bank.ExchangeService.Configurations;

public static partial class Configuration
{
    public static class Application
    {
        public static readonly Profile  Profile     = EnvironmentUtilities.GetEnumVariable("BANK_APPLICATION_PROFILE", Profile.Development);
        public static readonly string[] CorsOrigins = EnvironmentUtilities.GetStringArrayVariable("BANK_APPLICATION_CORS_ORIGINS");
    }
}
