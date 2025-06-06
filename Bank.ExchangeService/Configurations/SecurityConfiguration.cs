using System.Globalization;

using Bank.Application.Utilities;

namespace Bank.ExchangeService.Configurations;

public static partial class Configuration
{
    public static class Security
    {
        public static class Global
        {
            public static readonly int HistoryTimeFrameInMinutes = EnvironmentUtilities.GetIntVariable("BANK_EXCHANGE_HISTORY_TIME_FRAME_IN_MINUTES");
            public static readonly int LatestTimeFrameInMinutes  = EnvironmentUtilities.GetIntVariable("BANK_EXCHANGE_LATEST_TIME_FRAME_IN_MINUTES");
        }

        public static class Stock
        {
            public const string GetAllApi     = "https://paper-api.alpaca.markets/v2/assets";
            public const string GetHistoryApi = "https://data.alpaca.markets/v2/stocks/bars";
            public const string GetLatest     = "https://data.alpaca.markets/v2/stocks/snapshots";

            public static readonly string FromDateTime = EnvironmentUtilities.GetStringVariable("BANK_EXCHANGE_SECURITY_STOCK_FROM_DATE");

            // public static readonly string ToDateTime =
            // EnvironmentUtilities.GetStringVariable("BANK_EXCHANGE_SECURITY_STOCK_TO_DATE", DateTime.Today.ToString(CultureInfo.InvariantCulture));
        }

        public static class ForexPair
        {
            public const string GetDataApi = "https://www.alphavantage.co/query";
        }

        public static class Option
        {
            public const string OptionChainApi = "https://data.alpaca.markets/v1beta1/options/snapshots";

            public static readonly string ToDateTime =
            EnvironmentUtilities.GetStringVariable("BANK_EXCHANGE_SECURITY_OPTION_TO_DATE", DateTime.MaxValue.ToString(CultureInfo.InvariantCulture));
        }

        public static class Keys
        {
            private static          int      s_ApiKeyAndSecretIndex = 0;
            private static readonly Lock     s_ApiKeyAndSecretLock  = new();
            private static readonly string[] s_ApiAlpacaKeys        = EnvironmentUtilities.GetStringArrayVariable("BANK_EXCHANGE_SECURITY_ALPACA_API_KEY");
            private static readonly string[] s_ApiAlpacaSecrets     = EnvironmentUtilities.GetStringArrayVariable("BANK_EXCHANGE_SECURITY_ALPACA_API_SECRET");

            // Forex
            public static readonly string ApiKeyForex = EnvironmentUtilities.GetStringVariable("BANK_EXCHANGE_SECURITY_FOREX_PAIR_API_KEY");

            public static (string key, string secret) AlpacaApiKeyAndSecret => FindKeyAndSecret();

            private static (string key, string secret) FindKeyAndSecret()
            {
                lock (s_ApiKeyAndSecretLock)
                {
                    s_ApiKeyAndSecretIndex = (s_ApiKeyAndSecretIndex + 1) % s_ApiAlpacaKeys.Length;
                }

                return (s_ApiAlpacaKeys[s_ApiKeyAndSecretIndex], s_ApiAlpacaSecrets[s_ApiKeyAndSecretIndex]);
            }
        }
    }
}
