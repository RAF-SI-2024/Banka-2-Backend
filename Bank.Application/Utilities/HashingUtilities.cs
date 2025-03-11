using System.Security.Cryptography;
using System.Text;

namespace Bank.Application.Utilities;

public static class HashingUtilities
{
    public static string Hash(byte[] value)
    {
        return Convert.ToHexString(SHA256.HashData(value));
    }

    public static string Hash(string value)
    {
        return Hash(Encoding.UTF8.GetBytes(value));
    }

    public static string HashPassword(string password, Guid salt)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var saltBytes     = salt.ToByteArray();

        return Hash(passwordBytes.Concat(saltBytes)
                                 .ToArray());
    }
}
