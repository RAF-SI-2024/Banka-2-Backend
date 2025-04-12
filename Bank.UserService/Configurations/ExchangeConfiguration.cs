namespace Bank.UserService.Configurations;

public static partial class Configuration
{
    public static class Exchange
    {
        public const string DefaultCurrencyCode = "RSD";
        public const string ApiUrlTemplate      = $"https://www.floatrates.com/daily/{DefaultCurrencyCode}.json";
    }
}
