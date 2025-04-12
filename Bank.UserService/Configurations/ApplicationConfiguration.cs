using Bank.Application.Domain;
using Bank.Application.Utilities;

namespace Bank.UserService.Configurations;

public static partial class Configuration
{
    public static class Application
    {
        public static readonly Profile Profile = EnvironmentUtilities.GetEnumVariable("BANK_USER_APPLICATION_PROFILE", Profile.Development);
    }
}
