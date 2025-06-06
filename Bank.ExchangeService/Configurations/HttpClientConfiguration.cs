using Bank.Application.Utilities;

namespace Bank.ExchangeService.Configurations;

public static partial class Configuration
{
    public static class HttpClient
    {
        public static class BaseUrl
        {
            public static readonly string UserService = EnvironmentUtilities.GetStringVariable("BANK_EXCHANGE_USER_SERVICE_BASE_URL");
        }

        public static class Name
        {
            public const string UserService    = nameof(UserService);
            public const string GetOneUser     = nameof(GetOneUser);
            public const string GetOneClient   = nameof(GetOneClient);
            public const string GetOneEmployee = nameof(GetOneEmployee);
        }

        public const string GetLatestStocks = nameof(GetLatestStocks);
    }
}
