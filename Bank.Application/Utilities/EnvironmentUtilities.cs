using Bank.Application.Extensions;

namespace Bank.Application.Utilities;

public static class EnvironmentUtilities
{
    public static string GetStringVariable(string variableName, string defaultValue = "")
    {
        return Environment.GetEnvironmentVariable(variableName)
                          .OrDefault(defaultValue);
    }

    public static int GetIntVariable(string variableName, int defaultValue = 0)
    {
        return Environment.GetEnvironmentVariable(variableName)
                          .ParseIntOrDefault(defaultValue);
    }

    public static bool GetBoolVariable(string variableName, bool defaultValue = false)
    {
        return Environment.GetEnvironmentVariable(variableName)
                          .ParseBoolOrDefault(defaultValue);
    }

    public static TEnum GetEnumVariable<TEnum>(string variableName, TEnum defaultValue = default) where TEnum : struct, Enum
    {
        return Enum.TryParse(Environment.GetEnvironmentVariable(variableName), out TEnum result) ? result : defaultValue;
    }
}
