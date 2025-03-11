using Bank.Application.Requests;
using Bank.Application.Utilities;

using FluentValidation;

namespace Bank.Application.Validators;

public static class CompanyValidator
{
    public class Create : AbstractValidator<CompanyCreateRequest>
    {
        public Create()
        {
            RuleFor(companyRequest => companyRequest.Name)
            .NotNull()
            .WithMessage(ValidationErrorMessage.Company.NameNull)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Company.NameEmpty)
            .MaximumLength(32)
            .WithMessage(ValidationErrorMessage.Company.NameLength);

            RuleFor(companyRequest => companyRequest.RegistrationNumber)
            .NotNull()
            .WithMessage(ValidationErrorMessage.Company.RegistrationNumberNull)
            .Length(8)
            .WithMessage(ValidationErrorMessage.Company.RegistrationNumberLength)
            .Must(ValidatorUtilities.Global.ContainsOnlyNumbers)
            .WithMessage(ValidationErrorMessage.Company.RegistrationNumberTextInvalid);

            RuleFor(companyRequest => companyRequest.TaxIdentificationNumber)
            .NotNull()
            .WithMessage(ValidationErrorMessage.Company.TINNull)
            .Length(9)
            .WithMessage(ValidationErrorMessage.Company.TINLength)
            .Must(ValidatorUtilities.Global.ContainsOnlyNumbers)
            .WithMessage(ValidationErrorMessage.Company.TINTextInvalid);

            RuleFor(companyRequest => companyRequest.ActivityCode)
            .NotNull()
            .WithMessage(ValidationErrorMessage.Company.ActivityCodeNull)
            .Length(4, 5)
            .WithMessage(ValidationErrorMessage.Company.ActivityCodeLength)
            .Must(ValidatorUtilities.UserService.ValidateActivityCode)
            .WithMessage(ValidationErrorMessage.Company.ActivityCodeTextInvalid);

            RuleFor(companyRequest => companyRequest.Address)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.AddressRequired)
            .Must(ValidatorUtilities.UserService.ValidateNameWithNumbers)
            .WithMessage(ValidationErrorMessage.Global.AddressInvalid);
        }
    }

    public class Update : AbstractValidator<CompanyUpdateRequest>
    {
        public Update()
        {
            RuleFor(companyRequest => companyRequest.Name)
            .NotNull()
            .WithMessage(ValidationErrorMessage.Company.NameNull)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Company.NameEmpty)
            .MaximumLength(32)
            .WithMessage(ValidationErrorMessage.Company.NameLength);

            RuleFor(companyRequest => companyRequest.ActivityCode)
            .NotNull()
            .WithMessage(ValidationErrorMessage.Company.ActivityCodeNull)
            .Length(4, 5)
            .WithMessage(ValidationErrorMessage.Company.ActivityCodeLength)
            .Must(ValidatorUtilities.UserService.ValidateActivityCode)
            .WithMessage(ValidationErrorMessage.Company.ActivityCodeTextInvalid);

            RuleFor(companyRequest => companyRequest.Address)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.AddressRequired)
            .Must(ValidatorUtilities.UserService.ValidateNameWithNumbers)
            .WithMessage(ValidationErrorMessage.Global.AddressInvalid);
        }
    }
}
