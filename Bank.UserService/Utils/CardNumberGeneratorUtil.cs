using System.Security.Cryptography;

namespace Bank.UserService.Utils;

public static class CardNumberGeneratorUtil
{
    private static readonly Dictionary<string, string> s_CardPrefixes = new()
                                                                        {
                                                                            { "Visa Debit", "4" },
                                                                            { "Visa Business", "4" },
                                                                            { "MasterCard Gold", "52" },
                                                                            { "DinaCard Standard", "9891" },
                                                                            { "American Express Platinum", "37" }
                                                                        };

    private static readonly Dictionary<string, int> s_CardLengths = new()
                                                                    {
                                                                        { "Visa Debit", 16 },
                                                                        { "Visa Business", 16 },
                                                                        { "MasterCard Gold", 16 },
                                                                        { "DinaCard Standard", 16 },
                                                                        { "American Express Platinum", 15 }
                                                                    };

    private static readonly Random s_Random = new();

    public static (string CardNumber, string Cvv) GenerateCardDetails(string cardTypeName)
    {
        var cardNumber = GenerateCardNumber(cardTypeName);
        var cvv        = GenerateCvv(cardTypeName);
        return (cardNumber, cvv);
    }

    public static string GenerateCardNumber(string cardTypeName)
    {
        if (!s_CardPrefixes.TryGetValue(cardTypeName, out var prefix))
            throw new ArgumentException($"Unknown card type: {cardTypeName}");

        var cardLength = s_CardLengths[cardTypeName];

        var partialNumber    = prefix;
        var digitsToGenerate = cardLength - prefix.Length - 1;

        for (var i = 0; i < digitsToGenerate; i++)
            partialNumber += s_Random.Next(0, 10)
                                     .ToString();

        var checkDigit = CalculateLuhnCheckDigit(partialNumber);
        var cardNumber = partialNumber + checkDigit;

        if (!ValidateLuhn(cardNumber))
            return GenerateCardNumber(cardTypeName);

        return cardNumber;
    }

    public static string GenerateCvv(string cardTypeName)
    {
        var       cvvLength = cardTypeName == "American Express Platinum" ? 4 : 3;
        using var rng       = RandomNumberGenerator.Create();
        var       data      = new byte[cvvLength];
        rng.GetBytes(data);

        return string.Concat(data.Select(b => (b % 10).ToString()));
    }

    private static int CalculateLuhnCheckDigit(string partialCardNumber)
    {
        var sum           = 0;
        var isSecondDigit = true;

        for (var i = partialCardNumber.Length - 1; i >= 0; i--)
        {
            var digit = int.Parse(partialCardNumber[i]
                                  .ToString());

            if (isSecondDigit)
            {
                digit *= 2;

                if (digit > 9)
                    digit -= 9;
            }

            sum           += digit;
            isSecondDigit =  !isSecondDigit;
        }

        return (10 - sum % 10) % 10;
    }

    public static bool ValidateLuhn(string cardNumber)
    {
        var sum           = 0;
        var isSecondDigit = false;

        for (var i = cardNumber.Length - 1; i >= 0; i--)
        {
            var digit = int.Parse(cardNumber[i]
                                  .ToString());

            if (isSecondDigit)
            {
                digit *= 2;

                if (digit > 9)
                    digit -= 9;
            }

            sum           += digit;
            isSecondDigit =  !isSecondDigit;
        }

        return sum % 10 == 0;
    }
}
