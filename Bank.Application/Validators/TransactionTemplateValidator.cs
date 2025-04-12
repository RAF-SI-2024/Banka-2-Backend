using Bank.Application.Requests;
using Bank.Application.Utilities;

using FluentValidation;

namespace Bank.Application.Validators;

public static class TransactionTemplateValidator
{
    public class Create : AbstractValidator<TransactionTemplateCreateRequest>
    {
        public Create()
        {
            RuleFor(request => request.Name)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("Name"))
            .MaximumLength(32)
            .WithMessage(ValidationErrorMessage.Global.TextTooLong("Name", 32))
            .Must(ValidatorUtilities.UserService.ValidateNameWithNumbers)
            .WithMessage(ValidationErrorMessage.Global.FieldIsInvalid("Name"));

            RuleFor(request => request.AccountNumber)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("Account Number"))
            .Length(18)
            .WithMessage(ValidationErrorMessage.Global.TextFixedLength("Account Number", 18))
            .Must(ValidatorUtilities.Global.ContainsOnlyNumbers)
            .WithMessage(ValidationErrorMessage.Global.FieldIsInvalid("Account Number"));
        }
    }

    public class Update : AbstractValidator<TransactionTemplateUpdateRequest>
    {
        public Update()
        {
            RuleFor(request => request.Name)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("Name"))
            .MaximumLength(32)
            .WithMessage(ValidationErrorMessage.Global.TextTooLong("Name", 32))
            .Must(ValidatorUtilities.UserService.ValidateNameWithNumbers)
            .WithMessage(ValidationErrorMessage.Global.FieldIsInvalid("Name"));

            RuleFor(request => request.AccountNumber)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("Account Number"))
            .Length(18)
            .WithMessage(ValidationErrorMessage.Global.TextFixedLength("Account Number", 18))
            .Must(ValidatorUtilities.Global.ContainsOnlyNumbers)
            .WithMessage(ValidationErrorMessage.Global.FieldIsInvalid("Account Number"));
        }
    }
}
