using Bank.Application.Domain;
using Bank.Application.Extensions;
using Bank.Application.Requests;

using FluentValidation;

using ValidatorUtilities = Bank.Application.Utilities.ValidatorUtilities.UserService;

namespace Bank.Application.Validators;

public class ClientValidator
{
    public class Create : AbstractValidator<ClientCreateRequest>
    {
        private DateOnly m_UINDate;
        private Gender   m_Gender;

        public Create()
        {
            RuleFor(clientRequest => clientRequest.FirstName)
            .NotEmpty()
            .WithMessage("First name is required.")
            .MaximumLength(32)
            .WithMessage("First name must be at most 32 characters long.")
            .Must(ValidatorUtilities.ValidateName)
            .WithMessage("First name is not valid.");

            RuleFor(clientRequest => clientRequest.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.")
            .MaximumLength(32)
            .WithMessage("Last name must be at most 32 characters long.")
            .Must(ValidatorUtilities.ValidateName)
            .WithMessage("Last name is not valid.");

            RuleFor(clientRequest => clientRequest.Gender)
            .NotEqual(Gender.Invalid)
            .WithMessage("Gender is required.");

            RuleFor(clientRequest => clientRequest.UniqueIdentificationNumber)
            .NotEmpty()
            .WithMessage("Unique identification number is required.")
            .Must(ValidatorUtilities.MatchUniqueIdentificationNumberRule)
            .WithMessage("Unique identification number must be 13 digits.")
            .Must(ValidateUniqueIdentificationNumberDate)
            .WithMessage("Unique identification number does not contain a valid date.")
            .Must(ValidateGender)
            .WithMessage("Unique identification number does not contain a valid gender.")
            .Must(ValidatorUtilities.ValidateControlNumber)
            .WithMessage("Unique identification number does not have a valid control digit.");

            RuleFor(clientRequest => clientRequest.DateOfBirth)
            .NotEqual(DateOnly.MinValue)
            .WithMessage("Date of birth is required.")
            .Must(ValidatorUtilities.ValidateDateOfBirth)
            .WithMessage("Date of birth is not valid.")
            .Must(DatesMatch)
            .WithMessage("Date of birth doesn't match date of unique identification number.");

            RuleFor(clientRequest => clientRequest.Email)
            .EmailAddress()
            .WithMessage("Email is not a valid email address.");

            RuleFor(clientRequest => clientRequest.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required.")
            .Must(ValidatorUtilities.ValidatePhoneNumber)
            .WithMessage("Phone number is not valid.")
            .MinimumLength(12)
            .WithMessage("Phone number does not have enough digits.")
            .MaximumLength(13)
            .WithMessage("Phone number has more than 13 digits.");

            RuleFor(clientRequest => clientRequest.Address)
            .NotEmpty()
            .WithMessage("Address is required.")
            .Must(ValidatorUtilities.ValidateNameWithNumbers)
            .WithMessage("Address is not valid.");
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

    public class Update : AbstractValidator<ClientUpdateRequest>
    {
        public Update()
        {
            RuleFor(clientRequest => clientRequest.FirstName)
            .NotEmpty()
            .WithMessage("First name is required.")
            .MaximumLength(32)
            .WithMessage("First name must be at most 32 characters long.")
            .Must(ValidatorUtilities.ValidateName)
            .WithMessage("First name is not valid.");

            RuleFor(clientRequest => clientRequest.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.")
            .MaximumLength(32)
            .WithMessage("Last name must be at most 32 characters long.")
            .Must(ValidatorUtilities.ValidateName)
            .WithMessage("Last name is not valid.");

            RuleFor(clientRequest => clientRequest.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required.")
            .Must(ValidatorUtilities.ValidatePhoneNumber)
            .WithMessage("Phone number is not valid.")
            .MinimumLength(12)
            .WithMessage("Phone number does not have enough digits.")
            .MaximumLength(13)
            .WithMessage("Phone number has more than 13 digits.");

            RuleFor(clientRequest => clientRequest.Address)
            .NotEmpty()
            .WithMessage("Address is required.")
            .Must(ValidatorUtilities.ValidateNameWithNumbers)
            .WithMessage("Address is not valid.");
        }
    }
}
