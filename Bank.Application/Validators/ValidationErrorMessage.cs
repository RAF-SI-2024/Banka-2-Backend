namespace Bank.Application.Validators;

public static partial class ValidationErrorMessage
{
    public static class User
    {
        public static class Login { }

        public static class PasswordReset { }

        public static class ActivateAccount { }
    }
}

public static partial class ValidationErrorMessage
{
    public static class Employee { }
}

public static partial class ValidationErrorMessage
{
    public static class Client { }
}

public static partial class ValidationErrorMessage
{
    public static class Global
    {
        public const string FirstNameRequired = "First name is required.";
        public const string FirstNameLength   = "First name must be at most 32 characters long.";
        public const string FirstNameInvalid  = "First name is not valid.";

        public const string LastNameRequired = "Last name is required.";
        public const string LastNameLength   = "Last name must be at most 32 characters long.";
        public const string LastNameInvalid  = "Last name is not valid.";

        public const string GenderRequired = "Gender is required.";

        public const string UINRequired            = "Unique identification number is required.";
        public const string UINLength              = "Unique identification number must be 13 digits.";
        public const string UINInvalidDate         = "Unique identification number does not contain a valid date.";
        public const string UINInvalidGender       = "Unique identification number does not contain a valid gender.";
        public const string UINInvalidControlDigit = "Unique identification number does not have a valid control digit.";

        public const string PasswordRequired                   = "Password is required.";
        public const string PasswordTooShort                   = "Password must be at least 8 charachers long.";
        public const string PasswordTooLong                    = "Password must be at most 32 characters long.";
        public const string PasswordTwoDigitsRequired          = "Password must contain at least two digits.";
        public const string PasswordLowercaseCharacterRequired = "Password must contain at least one lowercase character.";
        public const string PasswordUppercaseCharacterRequired = "Password must contain at least one uppercase character.";

        public const string DOBRequired = "Date of birth is required.";
        public const string DOBInvalid  = "Date of birth is not valid.";
        public const string DOBMismatch = "Date of birth doesn't match date of unique identification number.";

        public const string EmailRequired = "Email is required.";
        public const string EmailInvalid  = "Email is not a valid email address.";

        public const string UsernameInvalid = "Invalid username.";

        public const string PhoneNumberRequired  = "Phone number is required.";
        public const string PhoneNumberInvalid   = "Phone number is not valid.";
        public const string PhoneNumberMinLength = "Phone number does not have enough digits.";
        public const string PhoneNumberMaxLength = "Phone number has more than 13 digits.";

        public const string AddressRequired = "Address is required.";
        public const string AddressInvalid  = "Address is not valid.";

        public const string RoleRequired = "Role is required.";

        public const string DepartmentInvalid = "Department name is not valid.";

        public const string AmountInvalid = "Amount for exchange can't be 0.";

        public static string FieldIsInvalid(string fieldName) => $"{fieldName} is not valid.";

        public static string FieldIsRequired(string fieldName) => $"{fieldName} is required.";

        public static string TextTooLong(string fieldName, int maxLength) => $"{fieldName} must be at most {maxLength} characters long.";

        public static string TextTooShort(string fieldName, int minLength) => $"{fieldName} must be at least {minLength} characters long.";

        public static string TextFixedLength(string fieldName, int length) => $"{fieldName} must be exactly {length} characters long.";
    }
}

public static partial class ValidationErrorMessage
{
    public static class Company
    {
        public const string NameNull   = "Name can't be null.";
        public const string NameEmpty  = "Name can't be empty.";
        public const string NameLength = "Name must be at most 32 characters long.";

        public const string RegistrationNumberNull        = "Registration Number can't be null.";
        public const string RegistrationNumberLength      = "Registration Number must be exactly 8 characters long.";
        public const string RegistrationNumberTextInvalid = "Registration Number must contains only numbers.";

        public const string TINNull        = "Tax Identification Number can't be null.";
        public const string TINLength      = "Tax Identification Number must be exactly 9 characters long.";
        public const string TINTextInvalid = "Tax Identification Number must contains only numbers.";

        public const string ActivityCodeNull        = "Activity Code can't be null.";
        public const string ActivityCodeLength      = "Activity Code must be exactly 4 or 5 characters long.";
        public const string ActivityCodeTextInvalid = "Activity Code must contain only . and numbers.";
    }
}

public static partial class ValidationErrorMessage
{
    public static class Currency
    {
        public const string CodeEmpty   = "Code can't be empty.";
        public const string CodeLenght  = "Code must be exactly 3 characters long.";
        public const string CodeInvalid = "Code must be contain only letters.";

        public const string IdNull  = "Currency id can't be null.";
        public const string IdEmpty = "Currency id can't be empty.";
    }
}

public static partial class ValidationErrorMessage
{
    public static class Account
    {
        public const string IdNull  = "Account id can't be null.";
        public const string IdEmpty = "Account id can't be empty.";
    }
}
