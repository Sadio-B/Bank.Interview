using FluentValidation;

namespace Bank.Interview.Application.Features.Operations.Commands.DepositIntoAccount
{
    public class DepositIntoAccountCommandValidator : AbstractValidator<DepositIntoAccountCommand>
    {
        public DepositIntoAccountCommandValidator()
        {
            RuleFor(deposit => deposit.AccountId)
                .NotNull().WithMessage("{PropertyName} must be greater than 0")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

            RuleFor(deposit => deposit.Amount)
                .NotNull().WithMessage("{PropertyName} is required")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

            RuleFor(deposit => deposit.Currency)
                .NotNull().WithMessage("{PropertyName} must be not null");
        }
    }
}
