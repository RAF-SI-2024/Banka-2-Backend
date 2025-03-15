using Bank.Application.Requests;

using FluentValidation;

using ValidatorUtilities = Bank.Application.Utilities.ValidatorUtilities.UserService;

namespace Bank.Application.Validators;

public class AccountValidator
{
    public class Create : AbstractValidator<AccountCreateRequest>
    {
        public Create()
        {
            RuleFor(accountRequest => accountRequest.DailyLimit)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("Daily limit"))
            .GreaterThan(0)
            .WithMessage(ValidationErrorMessage.Global.FieldIsInvalid("Daily limit"));

            RuleFor(accountRequest => accountRequest.MonthlyLimit)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("Monthly limit"))
            .GreaterThan(0)
            .WithMessage(ValidationErrorMessage.Global.FieldIsInvalid("Monthly limit"));

            RuleFor(accountRequest => accountRequest.Name)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("Name"))
            .MaximumLength(64)
            .WithMessage(ValidationErrorMessage.Global.TextTooLong("Name", 64))
            .Must(ValidatorUtilities.ValidateAccountName)
            .WithMessage(ValidationErrorMessage.Global.FieldIsInvalid("Name"));

            RuleFor(accountRequest => accountRequest.ClientId)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("ClientId"));

            RuleFor(accountRequest => accountRequest.CurrencyId)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("CurrencyId"));

            RuleFor(accountRequest => accountRequest.AccountTypeId)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("AccountTypeId"));

            RuleFor(accountRequest => accountRequest.Status)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("Status"));

            RuleFor(accountRequest => accountRequest.Balance)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("Balance"))
            .GreaterThanOrEqualTo(0)
            .WithMessage(ValidationErrorMessage.Global.FieldIsInvalid("Balance"));
        }
    }

    public class UpdateClient : AbstractValidator<AccountUpdateClientRequest>
    {
        public UpdateClient()
        {
            RuleFor(accountRequest => accountRequest.DailyLimit)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("Daily limit"))
            .GreaterThan(0)
            .WithMessage(ValidationErrorMessage.Global.FieldIsInvalid("Daily limit"));

            RuleFor(accountRequest => accountRequest.MonthlyLimit)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("Monthly limit"))
            .GreaterThan(0)
            .WithMessage(ValidationErrorMessage.Global.FieldIsInvalid("Monthly limit"));

            RuleFor(accountRequest => accountRequest.Name)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("Name"))
            .MaximumLength(64)
            .WithMessage(ValidationErrorMessage.Global.TextTooLong("Name", 64))
            .Must(ValidatorUtilities.ValidateUsername)
            .WithMessage(ValidationErrorMessage.Global.FieldIsInvalid("Name"));
        }
    }
}
