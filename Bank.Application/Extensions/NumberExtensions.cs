namespace Bank.Application.Extensions;

public static class NumberExtensions
{
    public static string EncodeToBase64(this int value)
    {
        byte[] bytes = BitConverter.GetBytes(value);

        return Convert.ToBase64String(bytes)
                      .TrimEnd('=');
    }

    public static int DecodeBase64ToInt(this string base64)
    {
        int mod4 = base64.Length % 4;

        if (mod4 > 0)
            base64 += new string('=', 4 - mod4);

        byte[] bytes = Convert.FromBase64String(base64);

        return BitConverter.ToInt32(bytes);
    }
}
