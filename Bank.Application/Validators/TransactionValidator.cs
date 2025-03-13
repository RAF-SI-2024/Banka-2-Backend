﻿using Bank.Application.Domain;
using Bank.Application.Requests;
using Bank.Application.Utilities;

using FluentValidation;

namespace Bank.Application.Validators;

public static class TransactionValidator
{
    public class Create : AbstractValidator<TransactionCreateRequest>
    {
        public Create()
        {
            RuleFor(request => request.FromAccountId)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("FromAccountId"));

            RuleFor(request => request.FromCurrencyId)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("FromCurrencyId"));

            RuleFor(request => request.ToCurrencyId)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("ToCurrencyId"));

            RuleFor(request => request.CodeId)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("CodeId"));

            RuleFor(request => request.ToAccountNumber)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("ToAccountNumber"))
            .Length(18)
            .WithMessage(ValidationErrorMessage.Global.TextFixedLength("ToAccountNumber", 18))
            .Must(ValidatorUtilities.Global.ContainsOnlyNumbers)
            .WithMessage(ValidationErrorMessage.Global.FieldIsInvalid("ToAccountNumber"));

            RuleFor(request => request.Amount)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("Amount"))
            .GreaterThan(0)
            .WithMessage(ValidationErrorMessage.Global.FieldIsInvalid("Amount"));

            RuleFor(request => request.ReferenceNumber)
            .MaximumLength(20)
            .WithMessage(ValidationErrorMessage.Global.TextFixedLength("ReferenceNumber", 20));

            RuleFor(request => request.Purpose)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("Purpose"))
            .MaximumLength(1024)
            .WithMessage(ValidationErrorMessage.Global.TextFixedLength("Purpose", 1024));
        }
    }

    public class Update : AbstractValidator<TransactionUpdateRequest>
    {
        public Update()
        {
            RuleFor(request => request.Status)
            .NotEqual(TransactionStatus.Invalid)
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("Status"));
        }
    }
}
