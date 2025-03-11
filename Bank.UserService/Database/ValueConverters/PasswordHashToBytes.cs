using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bank.UserService.Database.ValueConverters;

public class PasswordHashToBytes() : ValueConverter<string, byte[]>(stringHash => Convert.FromHexString(stringHash), bytesHash => Convert.ToHexString(bytesHash)) { }
