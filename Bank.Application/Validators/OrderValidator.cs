﻿using Bank.Application.Domain;
using Bank.Application.Requests;

using FluentValidation;

namespace Bank.Application.Validators;

public static class OrderValidator
{
    public class Create : AbstractValidator<OrderCreateRequest>
    {
        public Create()
        {
            RuleFor(request => request.ActuaryId)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("ActuaryId"));

            RuleFor(request => request.OrderType)
            .NotEqual(OrderType.Invalid)
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("OrderType"));

            RuleFor(request => request.Quantity)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("Quantity"))
            .GreaterThan(0)
            .WithMessage(ValidationErrorMessage.Global.FieldIsInvalid("Quantity"));

            RuleFor(request => request.ContractCount)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("Contract Count"))
            .GreaterThan(0)
            .WithMessage(ValidationErrorMessage.Global.FieldIsInvalid("Contract Count"));

            RuleFor(request => request.LimitPrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage(ValidationErrorMessage.Global.FieldIsInvalid("Limit Price"));

            RuleFor(request => request.StopPrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage(ValidationErrorMessage.Global.FieldIsInvalid("Stop Price"));

            RuleFor(request => request.Direction)
            .NotEqual(Direction.Invalid)
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("Direction"));

            RuleFor(request => request.SupervisorId)
            .NotEmpty()
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("SupervisorId"));
        }
    }

    public class Update : AbstractValidator<OrderUpdateRequest>
    {
        public Update()
        {
            RuleFor(request => request.Status)
            .NotEqual(OrderStatus.Invalid)
            .WithMessage(ValidationErrorMessage.Global.FieldIsRequired("Status"));
        }
    }
}
