using Bank.Application.Utilities;

namespace Bank.Link.Configurations;

public partial class Configuration
{
    public static class ExternalBank
    {
        public static class Bank3
        {
            public static readonly string BankServiceBaseUrl = EnvironmentUtilities.GetStringVariable("BANK_LINK_EXTERNAL_BANK_BANK_3_BANK_SERVICE_BASE_URL");
        }
    }
}
