using Bank.Application.Requests;
using Bank.Application.Utilities;

using FluentValidation;

namespace Bank.Application.Validators;

public static class UserValidator
{
    public class Login : AbstractValidator<UserLoginRequest>
    {
        public Login()
        {
            RuleFor(userRequest => userRequest.Email)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.EmailRequired)
            .EmailAddress()
            .WithMessage(ValidationErrorMessage.Global.EmailInvalid);
        }
    }

    public class Activation : AbstractValidator<UserActivationRequest>
    {
        public Activation()
        {
            RuleFor(request => request.Password)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.PasswordRequired)
            .MinimumLength(8)
            .WithMessage(ValidationErrorMessage.Global.PasswordTooShort)
            .MaximumLength(32)
            .WithMessage(ValidationErrorMessage.Global.PasswordTooLong)
            .Must(ValidatorUtilities.UserService.ContainAtLeastTwoDigits)
            .WithMessage(ValidationErrorMessage.Global.PasswordTwoDigitsRequired)
            .Must(ValidatorUtilities.UserService.ContainAtLeastOneLowercaseCharacter)
            .WithMessage(ValidationErrorMessage.Global.PasswordLowercaseCharacterRequired)
            .Must(ValidatorUtilities.UserService.ContainAtLeastOneUppercaseCharacter)
            .WithMessage(ValidationErrorMessage.Global.PasswordUppercaseCharacterRequired);
        }
    }

    public class PasswordReset : AbstractValidator<UserPasswordResetRequest>
    {
        public PasswordReset()
        {
            RuleFor(request => request.Password)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.PasswordRequired)
            .MinimumLength(8)
            .WithMessage(ValidationErrorMessage.Global.PasswordTooShort)
            .MaximumLength(32)
            .WithMessage(ValidationErrorMessage.Global.PasswordTooLong)
            .Must(ValidatorUtilities.UserService.ContainAtLeastTwoDigits)
            .WithMessage(ValidationErrorMessage.Global.PasswordTwoDigitsRequired)
            .Must(ValidatorUtilities.UserService.ContainAtLeastOneLowercaseCharacter)
            .WithMessage(ValidationErrorMessage.Global.PasswordLowercaseCharacterRequired)
            .Must(ValidatorUtilities.UserService.ContainAtLeastOneUppercaseCharacter)
            .WithMessage(ValidationErrorMessage.Global.PasswordUppercaseCharacterRequired);
        }
    }
}
