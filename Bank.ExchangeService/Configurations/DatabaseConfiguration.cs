﻿using Bank.Application.Utilities;

namespace Bank.ExchangeService.Configurations;

public static partial class Configuration
{
    public static class Database
    {
        public static readonly string Host       = EnvironmentUtilities.GetStringVariable("BANK_EXCHANGE_DATABASE_HOST");
        public static readonly int    Port       = EnvironmentUtilities.GetIntVariable("BANK_EXCHANGE_DATABASE_PORT");
        public static readonly string Scheme     = EnvironmentUtilities.GetStringVariable("BANK_EXCHANGE_DATABASE_SCHEME");
        public static readonly string Username   = EnvironmentUtilities.GetStringVariable("BANK_EXCHANGE_DATABASE_USERNAME");
        public static readonly string Password   = EnvironmentUtilities.GetStringVariable("BANK_EXCHANGE_DATABASE_PASSWORD");
        public static readonly bool   CreateDrop = EnvironmentUtilities.GetBoolVariable("BANK_EXCHANGE_DATABASE_CREATE_DROP");

        public static string GetConnectionString()
        {
            return $"Host={Host};Port={Port};Database={Scheme};Username={Username};Password={Password}";
        }
    }
}
