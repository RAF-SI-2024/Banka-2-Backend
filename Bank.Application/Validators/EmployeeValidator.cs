using Bank.Application.Domain;
using Bank.Application.Extensions;
using Bank.Application.Requests;

using FluentValidation;

using ValidatorUtilities = Bank.Application.Utilities.ValidatorUtilities.UserService;

namespace Bank.Application.Validators;

public class EmployeeValidator
{
    public class Create : AbstractValidator<EmployeeCreateRequest>
    {
        private DateOnly m_UINDate;
        private Gender   m_Gender;

        public Create()
        {
            RuleFor(employeeRequest => employeeRequest.FirstName)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FirstNameRequired)
            .MaximumLength(32)
            .WithMessage(ValidationErrorMessage.Global.FirstNameLength)
            .Must(ValidatorUtilities.ValidateName)
            .WithMessage(ValidationErrorMessage.Global.FirstNameInvalid);

            RuleFor(employeeRequest => employeeRequest.LastName)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.LastNameRequired)
            .MaximumLength(32)
            .WithMessage(ValidationErrorMessage.Global.LastNameLength)
            .Must(ValidatorUtilities.ValidateName)
            .WithMessage(ValidationErrorMessage.Global.LastNameInvalid);

            RuleFor(employeeRequest => employeeRequest.Gender)
            .NotEqual(Gender.Invalid)
            .WithMessage(ValidationErrorMessage.Global.GenderRequired);

            RuleFor(employeeRequest => employeeRequest.UniqueIdentificationNumber)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.UINRequired)
            .Must(ValidatorUtilities.MatchUniqueIdentificationNumberRule)
            .WithMessage(ValidationErrorMessage.Global.UINLength)
            .Must(ValidateUniqueIdentificationNumberDate)
            .WithMessage(ValidationErrorMessage.Global.UINInvalidDate)
            .Must(ValidateGender)
            .WithMessage(ValidationErrorMessage.Global.UINInvalidGender)
            .Must(ValidatorUtilities.ValidateControlNumber)
            .WithMessage(ValidationErrorMessage.Global.UINInvalidControlDigit);

            RuleFor(employeeRequest => employeeRequest.DateOfBirth)
            .NotEqual(DateOnly.MinValue)
            .WithMessage(ValidationErrorMessage.Global.DOBRequired)
            .Must(ValidatorUtilities.ValidateDateOfBirth)
            .WithMessage(ValidationErrorMessage.Global.DOBInvalid)
            .Must(DatesMatch)
            .WithMessage(ValidationErrorMessage.Global.DOBMismatch);

            RuleFor(employeeRequest => employeeRequest.Email)
            .EmailAddress()
            .WithMessage(ValidationErrorMessage.Global.EmailInvalid);

            RuleFor(employeeRequest => employeeRequest.Username)
            .Must(ValidatorUtilities.ValidateUsername)
            .WithMessage(ValidationErrorMessage.Global.UsernameInvalid);

            RuleFor(employeeRequest => employeeRequest.PhoneNumber)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.PhoneNumberRequired)
            .Must(ValidatorUtilities.ValidatePhoneNumber)
            .WithMessage(ValidationErrorMessage.Global.PhoneNumberInvalid)
            .MinimumLength(12)
            .WithMessage(ValidationErrorMessage.Global.PhoneNumberMinLength)
            .MaximumLength(13)
            .WithMessage(ValidationErrorMessage.Global.PhoneNumberMaxLength);

            RuleFor(employeeRequest => employeeRequest.Address)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.AddressRequired)
            .Must(ValidatorUtilities.ValidateNameWithNumbers)
            .WithMessage(ValidationErrorMessage.Global.AddressInvalid);

            RuleFor(employeeRequest => employeeRequest.Role)
            .NotEqual(Role.Invalid)
            .WithMessage(ValidationErrorMessage.Global.RoleRequired);

            RuleFor(employeeRequest => employeeRequest.Department)
            .Must(ValidatorUtilities.ValidateNameWithNumbers)
            .WithMessage(ValidationErrorMessage.Global.DepartmentInvalid);
        }

        private bool ValidateUniqueIdentificationNumberDate(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            var result = value.Substring(0, 7)
                              .TryParseUINToDate(out var date);

            m_UINDate = date;

            return result;
        }

        private bool DatesMatch(DateOnly dateOfBirth)
        {
            return m_UINDate == dateOfBirth;
        }

        private bool ValidateGender(string value)
        {
            return !(value[9] - '0' < 5 ^ m_Gender == 0);
        }
    }

    public class Update : AbstractValidator<EmployeeUpdateRequest>
    {
        public Update()
        {
            RuleFor(employeeRequest => employeeRequest.FirstName)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FirstNameRequired)
            .MaximumLength(32)
            .WithMessage(ValidationErrorMessage.Global.FirstNameLength)
            .Must(ValidatorUtilities.ValidateName)
            .WithMessage(ValidationErrorMessage.Global.FirstNameInvalid);

            RuleFor(employeeRequest => employeeRequest.LastName)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.LastNameRequired)
            .MaximumLength(32)
            .WithMessage(ValidationErrorMessage.Global.LastNameLength)
            .Must(ValidatorUtilities.ValidateName)
            .WithMessage(ValidationErrorMessage.Global.LastNameInvalid);

            RuleFor(employeeRequest => employeeRequest.Username)
            .Must(ValidatorUtilities.ValidateUsername)
            .WithMessage(ValidationErrorMessage.Global.UsernameInvalid);

            RuleFor(employeeRequest => employeeRequest.PhoneNumber)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.PhoneNumberRequired)
            .Must(ValidatorUtilities.ValidatePhoneNumber)
            .WithMessage(ValidationErrorMessage.Global.PhoneNumberInvalid)
            .MinimumLength(12)
            .WithMessage(ValidationErrorMessage.Global.PhoneNumberMinLength)
            .MaximumLength(13)
            .WithMessage(ValidationErrorMessage.Global.PhoneNumberMaxLength);

            RuleFor(employeeRequest => employeeRequest.Address)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.AddressRequired)
            .Must(ValidatorUtilities.ValidateNameWithNumbers)
            .WithMessage(ValidationErrorMessage.Global.AddressInvalid);

            RuleFor(employeeRequest => employeeRequest.Department)
            .Must(ValidatorUtilities.ValidateNameWithNumbers)
            .WithMessage(ValidationErrorMessage.Global.DepartmentInvalid);
        }
    }
}
