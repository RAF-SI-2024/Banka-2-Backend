using Bank.Application.Utilities;

namespace Bank.Http.Configurations;

public static partial class Configuration
{
    public static class Client
    {
        public static class BaseUrl
        {
            public static readonly string UserService = EnvironmentUtilities.GetStringVariable("BANK_HTTP_CLIENT_BASE_URL_USER_SERVICE");
        }

        public static class Name
        {
            public const string UserService = nameof(UserService);
        }
    }
}
