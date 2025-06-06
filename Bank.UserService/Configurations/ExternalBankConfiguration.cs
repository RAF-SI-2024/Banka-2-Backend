using Bank.Application.Utilities;

namespace Bank.UserService.Configurations;

public partial class Configuration
{
    public static class ExternalBank
    {
        public static readonly string Bank1BaseUrl = EnvironmentUtilities.GetStringVariable("BANK_EXTERNAL_BANK_BASE_URL_BANK_1");
        public static readonly string Bank3BaseUrl = EnvironmentUtilities.GetStringVariable("BANK_EXTERNAL_BANK_BASE_URL_BANK_3");
        public static readonly string Bank4BaseUrl = EnvironmentUtilities.GetStringVariable("BANK_EXTERNAL_BANK_BASE_URL_BANK_4");
    }
}
