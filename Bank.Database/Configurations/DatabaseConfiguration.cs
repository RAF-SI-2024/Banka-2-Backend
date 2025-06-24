using Bank.Application.Utilities;

namespace Bank.Database.Configurations;

public static partial class Configuration
{
    public static class Database
    {
        public static class Persistent
        {
            public static readonly string Host              = EnvironmentUtilities.GetStringVariable("BANK_DATABASE_HOST");
            public static readonly int    Port              = EnvironmentUtilities.GetIntVariable("BANK_DATABASE_PORT");
            public static readonly string Scheme            = EnvironmentUtilities.GetStringVariable("BANK_DATABASE_SCHEME");
            public static readonly string Username          = EnvironmentUtilities.GetStringVariable("BANK_DATABASE_USERNAME");
            public static readonly string Password          = EnvironmentUtilities.GetStringVariable("BANK_DATABASE_PASSWORD");
            public static readonly bool   CreateDrop        = EnvironmentUtilities.GetBoolVariable("BANK_DATABASE_CREATE_DROP");
            public static readonly int    MinConnections    = EnvironmentUtilities.GetIntVariable("BANK_DATABASE_MIN_CONNECTIONS",    0);
            public static readonly int    MaxConnections    = EnvironmentUtilities.GetIntVariable("BANK_DATABASE_MAX_CONNECTIONS",    100);
            public static readonly int    ConnectionTimeout = EnvironmentUtilities.GetIntVariable("BANK_DATABASE_CONNECTION_TIMEOUT", 300);
            public static readonly int    ReadBufferSize    = EnvironmentUtilities.GetIntVariable("BANK_DATABASE_READ_BUFFER_SIZE",   8192);
            public static readonly int    WriteBufferSize   = EnvironmentUtilities.GetIntVariable("BANK_DATABASE_WRITE_BUFFER_SIZE",  8192);

            public static string GetConnectionString()
            {
                return $"Host={Host};Port={Port};Database={Scheme};Username={Username};Password={Password};Connection Idle Lifetime={ConnectionTimeout};Minimum Pool Size={
                    MinConnections};Maximum Pool Size={MaxConnections};Read Buffer Size={ReadBufferSize};Write Buffer Size={WriteBufferSize}";
            }
        }

        public static class InMemory
        {
            public static readonly string Host            = EnvironmentUtilities.GetStringVariable("BANK_DATABASE_IN_MEMORY_HOST", "localhost");
            public static readonly int    Port            = EnvironmentUtilities.GetIntVariable("BANK_DATABASE_IN_MEMORY_PORT", 6379);
            public static readonly string Scheme          = EnvironmentUtilities.GetStringVariable("BANK_DATABASE_IN_MEMORY_SCHEME");
            public static readonly string Password        = EnvironmentUtilities.GetStringVariable("BANK_DATABASE_IN_MEMORY_PASSWORD");
            public static readonly bool   AbortConnection = EnvironmentUtilities.GetBoolVariable("BANK_DATABASE_IN_MEMORY_ABORT_CONNECTION");

            public static string GetConnectionString()
            {
                return $"{Host}:{Port},name={Scheme},password={Password},abortConnect={AbortConnection}";
            }
        }
    }
}
