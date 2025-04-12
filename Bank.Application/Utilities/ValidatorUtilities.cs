using System.Text.RegularExpressions;

namespace Bank.Application.Utilities;

public static partial class ValidatorUtilities
{
    public static class UserService
    {
        public static bool ValidateName(string value)
        {
            return !string.IsNullOrEmpty(value) && Regex.IsMatch(value, "^[A-Za-zčćđšžČĆĐŠŽ]+( [A-Za-zčćđšžČĆĐŠŽ]+)*$");
        }

        public static bool ValidateUsername(string value)
        {
            return !string.IsNullOrEmpty(value) && Regex.IsMatch(value, @"^(?!.*\.\.)(?!.*\.$)[a-zA-ZčćđšžČĆĐŠŽ0-9._]{3,32}$");
        }

        public static bool ValidateNameWithNumbers(string value)
        {
            return Regex.IsMatch(value, "^[A-Za-zčćđšžČĆĐŠŽ]+( [0-9A-Za-zčćđšžČĆĐŠŽ]+)*$");
        }

        public static bool ValidatePhoneNumber(string value)
        {
            return Regex.IsMatch(value, @"^\+\d+$");
        }

        public static bool MatchUniqueIdentificationNumberRule(string value)
        {
            return Regex.IsMatch(value, @"^\d{13}$");
        }

        public static bool ValidateDateOfBirth(DateOnly value)
        {
            return value < DateOnly.FromDateTime(DateTime.Today);
        }

        public static bool ValidateControlNumber(string value)
        {
            int sum = 0;

            for (int i = 0; i < 12; i++)
                sum += (value[i] - '0') * (7 - i % 6);

            int controlNumber = 11 - sum % 11;

            if (controlNumber > 9)
                controlNumber = 0;

            return controlNumber == value[12] - '0';
        }

        public static bool ContainAtLeastTwoDigits(string password)
        {
            return password.Count(char.IsDigit) >= 2;
        }

        public static bool ContainAtLeastOneLowercaseCharacter(string password)
        {
            return password.Any(char.IsLower);
        }

        public static bool ContainAtLeastOneUppercaseCharacter(string password)
        {
            return password.Any(char.IsUpper);
        }

        public static bool ValidateActivityCode(string activityCode)
        {
            return Regex.IsMatch(activityCode, @"^\d+(\.\d+)?$");
        }

        public static bool ValidateAccountName(string value)
        {
            return Regex.IsMatch(value, @"^[A-Za-zČĆĐŠŽčćđšž\s'-]+$");
        }

        public static bool ValidateAccountTypeCode(string value)
        {
            return Regex.IsMatch(value, @"^\d{2}$");
        }
    }

    public static class Global
    {
        public static bool ContainsOnlyNumbers(string value)
        {
            return Regex.IsMatch(value, @"^\d+$");
        }

        public static bool ContainsOnlyLetters(string value)
        {
            return Regex.IsMatch(value, @"^[a-zA-Z]+$");
        }
    }
}
