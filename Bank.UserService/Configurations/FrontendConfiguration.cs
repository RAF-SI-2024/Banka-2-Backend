using Bank.Application.Utilities;

namespace Bank.UserService.Configurations;

public static partial class Configuration
{
    public static class Frontend
    {
        public static readonly string BaseUrl = EnvironmentUtilities.GetStringVariable("BANK_USER_FRONTEND_BASE_URL");

        public static class Route
        {
            public static readonly string Activate      = EnvironmentUtilities.GetStringVariable("BANK_USER_FRONTEND_ROUTE_ACTIVATE");
            public static readonly string ResetPassword = EnvironmentUtilities.GetStringVariable("BANK_USER_FRONTEND_ROUTE_RESET_PASSWORD");
        }
    }
}
