using Bank.Application.Utilities;

namespace Bank.ExchangeService.Configurations;

public static partial class Configuration
{
    public static class Frontend
    {
        public static readonly string BaseUrl = EnvironmentUtilities.GetStringVariable("BANK_USER_FRONTEND_BASE_URL");
    }
}
