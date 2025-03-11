using Bank.Application.Utilities;

namespace Bank.UserService.Configurations;

public static partial class Configuration
{
    public static class Email
    {
        public static readonly string Address  = EnvironmentUtilities.GetStringVariable("BANK_USER_EMAIL_ADDRESS");
        public static readonly string Password = EnvironmentUtilities.GetStringVariable("BANK_USER_EMAIL_PASSWORD");
        public static readonly string Server   = EnvironmentUtilities.GetStringVariable("BANK_USER_EMAIL_SERVER");
        public static readonly int    Port     = EnvironmentUtilities.GetIntVariable("BANK_USER_EMAIL_PORT");
    }
}
