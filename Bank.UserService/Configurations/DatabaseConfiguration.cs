using DatabaseConfiguration = Bank.Database.Configurations.Configuration;

namespace Bank.UserService.Configurations;

public static partial class Configuration
{
    public static class Database
    {
        public static readonly bool CreateDrop = DatabaseConfiguration.Database.CreateDrop;
    }
}
