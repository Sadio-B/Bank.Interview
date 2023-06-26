using Bank.Interview.Application.Contrats;
using Bank.Interview.Application.Exceptions;
using Bank.Interview.Application.Features.Operations.Commands.DepositIntoAccount;
using Bank.Interview.Domain.Entities;
using MediatR;

namespace Bank.Interview.Application.Features.Operations.Commands.WithdrawFromAccount
{
    public class WithdrawFromAccountCommandHandler : IRequestHandler<WithdrawFromAccountCommand, long>
    {
        private readonly IUnitOfWork _unitOfWork;

        public WithdrawFromAccountCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<long> Handle(WithdrawFromAccountCommand request, CancellationToken cancellationToken)
        {
            ValidateRequest(request);

            var account = await _unitOfWork.AccountRepository.GetByIdAsync(request.AccountId);

            // TODO Create  custom Exception
            if (account == null)
                throw new Exception("Account not found");

            var overdraft = await _unitOfWork.OverdraftRepository.GetApplicabletOverdraftByAccountIdAndDate(request.AccountId, DateTime.Now);

            var balanceMinAuthorized = -1 * (overdraft?.Amount ?? 0);
            var newBalance = account.Balance - request.Amount;

            // TODO Create  custom Exception
            if (newBalance < balanceMinAuthorized)
                throw new Exception("Overdraft reached");

            var withdrawToMake = new Transaction
            {
                AccountId = request.AccountId,
                Amount = newBalance,
                Currency = request.Currency,
                MadeOn = DateTime.Now,
                TransactionType = TransactionType.withdrawal,
            };

            account.Balance -= request.Amount;

            _unitOfWork.AccountRepository.Edit(account);
            await _unitOfWork.TransactionRepository.AddAsync(withdrawToMake);
            await _unitOfWork.SaveAllAsync();

            return account.Balance;
        }

        private static void ValidateRequest(WithdrawFromAccountCommand request)
        {
            var validator = new WithdrawFromAccountCommandValidator();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);
        }
    }
}
