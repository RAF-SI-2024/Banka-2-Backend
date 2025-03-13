using Bank.Application.Requests;
using Bank.Application.Utilities;

using FluentValidation;

namespace Bank.Application.Validators;

public class AccountTypeValidator
{
    public class Create : AbstractValidator<AccountTypeCreateRequest>
    {
        public Create()
        {
            RuleFor(accountTypeRequest => accountTypeRequest.Name)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("Name"))
            .MaximumLength(64)
            .WithMessage(ValidationErrorMessage.Global.TextTooLong("Name", 64))
            .Must(ValidatorUtilities.UserService.ValidateAccountName)
            .WithMessage(ValidationErrorMessage.Global.FieldIsInvalid("Name"));

            RuleFor(accountRequest => accountRequest.Code)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("Code"))
            .Must(ValidatorUtilities.UserService.ValidateAccountTypeCode)
            .WithMessage(ValidationErrorMessage.Global.FieldIsInvalid("Code"));
        }
    }

    public class Update : AbstractValidator<AccountTypeUpdateRequest>
    {
        public Update()
        {
            RuleFor(accountTypeRequest => accountTypeRequest.Name)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("Required"))
            .MaximumLength(64)
            .WithMessage(ValidationErrorMessage.Global.TextTooLong("Name", 64))
            .Must(ValidatorUtilities.UserService.ValidateAccountName)
            .WithMessage(ValidationErrorMessage.Global.FieldIsInvalid("Name"));

            RuleFor(accountRequest => accountRequest.Code)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("Code"))
            .Must(ValidatorUtilities.UserService.ValidateAccountTypeCode)
            .WithMessage(ValidationErrorMessage.Global.FieldIsInvalid("Code"));
        }
    }
}
