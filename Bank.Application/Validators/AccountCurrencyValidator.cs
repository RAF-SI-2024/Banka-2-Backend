using Bank.Application.Requests;

using FluentValidation;

namespace Bank.Application.Validators;

public class AccountCurrencyValidator
{
    public class Create : AbstractValidator<AccountCurrencyCreateRequest>
    {
        public Create()
        {
            RuleFor(accountCurrencyRequest => accountCurrencyRequest.EmployeeId)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("EmployeeId"));

            RuleFor(accountCurrencyRequest => accountCurrencyRequest.CurrencyId)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("CurrencyId"));

            RuleFor(accountCurrencyRequest => accountCurrencyRequest.AccountId)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("AccountId"));

            RuleFor(accountCurrencyRequest => accountCurrencyRequest.DailyLimit)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("Dailylimit"))
            .GreaterThan(0)
            .WithMessage(ValidationErrorMessage.Global.FieldIsInvalid("Dailylimit"));

            RuleFor(accountCurrencyCreateRequest => accountCurrencyCreateRequest.MonthlyLimit)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("Monthlylimit"))
            .GreaterThan(0)
            .WithMessage(ValidationErrorMessage.Global.FieldIsInvalid("Monthlylimit"));
        }
    }

    public class Update : AbstractValidator<AccountCurrencyClientUpdateRequest>
    {
        public Update()
        {
            RuleFor(accountCurrencyRequest => accountCurrencyRequest.DailyLimit)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("Dailylimit"))
            .GreaterThan(0)
            .WithMessage(ValidationErrorMessage.Global.FieldIsInvalid("Dailylimit"));

            RuleFor(accountCurrencyCreateRequest => accountCurrencyCreateRequest.MonthlyLimit)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("Monthlylimit"))
            .GreaterThan(0)
            .WithMessage(ValidationErrorMessage.Global.FieldIsInvalid("Monthlylimit"));
        }
    }
}
