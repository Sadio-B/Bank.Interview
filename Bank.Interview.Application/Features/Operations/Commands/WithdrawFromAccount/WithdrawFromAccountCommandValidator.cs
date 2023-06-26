using FluentValidation;

namespace Bank.Interview.Application.Features.Operations.Commands.WithdrawFromAccount
{
    public class WithdrawFromAccountCommandValidator : AbstractValidator<WithdrawFromAccountCommand>
    {
        public WithdrawFromAccountCommandValidator()
        {
            RuleFor(withdraw => withdraw.AccountId)
                .NotNull().WithMessage("{PropertyName} must be greater than 0")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

            RuleFor(withdraw => withdraw.Amount)
                .NotNull().WithMessage("{PropertyName} is required")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

            RuleFor(withdraw => withdraw.Currency)
                .NotNull().WithMessage("{PropertyName} must be not null");
        }
    }
}
