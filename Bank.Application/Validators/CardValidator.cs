using Bank.Application.Requests;

using FluentValidation;

namespace Bank.Application.Validators
{
    public class CardValidator
    {
        public class Create : AbstractValidator<CardCreateRequest>
        {
            public Create()
            {
                RuleFor(request => request.CardTypeId)
                .NotEmpty()
                .WithMessage("Card type ID is required.");

                RuleFor(request => request.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(64)
                .WithMessage("Name must be at most 64 characters long.")
                .Matches(@"^[A-Za-zÀ-Žšž\s\-,']+(?: [A-Za-zÀ-Žšž\s\-,']+)*$")
                .WithMessage("Name contains invalid characters.");

                RuleFor(request => request.Limit)
                .GreaterThan(0)
                .WithMessage("Limit must be a positive number greater than 0.");

                RuleFor(request => request.Status)
                .NotNull()
                .WithMessage("Status is required.");
            }
        }
    }
}
