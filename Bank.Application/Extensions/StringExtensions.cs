namespace Bank.Application.Extensions;

public static class StringExtensions
{
    public static bool TryParseUINToDate(this string value, out DateOnly date)
    {
        var day       = value.Substring(0, 2);
        var month     = value.Substring(2, 2);
        var year      = value.Substring(4, 3);
        var yearDigit = year[0] == '9' ? "1" : "2";

        var result = DateOnly.TryParse($"{yearDigit}{year}-{month}-{day}", out var dateOnly);

        date = dateOnly;

        return result;
    }

    public static string OrDefault(this string? value, string defaultValue = "")
    {
        return value ?? defaultValue;
    }

    public static int ParseIntOrDefault(this string? value, int defaultValue = 0)
    {
        return int.TryParse(value, out var result) ? result : defaultValue;
    }

    public static bool ParseBoolOrDefault(this string? value, bool defaultValue = false)
    {
        return bool.TryParse(value, out var result) ? result : defaultValue;
    }

    public static string ToCamelCase(this string value)
    {
        return string.IsNullOrEmpty(value) ? value : char.ToLower(value[0]) + value[1..];
    }
}
