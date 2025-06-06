using DatabaseConfiguration = Bank.Database.Configurations.Configuration;

namespace Bank.ExchangeService.Configurations;

public static partial class Configuration
{
    public static class Database
    {
        public static readonly bool CreateDrop = DatabaseConfiguration.Database.Persistent.CreateDrop;
    }
}
