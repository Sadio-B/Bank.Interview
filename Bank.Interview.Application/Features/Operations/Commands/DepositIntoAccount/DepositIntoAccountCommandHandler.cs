using Bank.Interview.Application.Contrats;
using Bank.Interview.Application.Exceptions;
using Bank.Interview.Domain.Entities;
using FluentValidation.Results;
using MediatR;

namespace Bank.Interview.Application.Features.Operations.Commands.DepositIntoAccount
{
    public class DepositIntoAccountCommandHandler : IRequestHandler<DepositIntoAccountCommand, long>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepositIntoAccountCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<long> Handle(DepositIntoAccountCommand request, CancellationToken cancellationToken)
        {
            ValidateRequest(request);

            var account = await _unitOfWork.AccountRepository.GetByIdAsync(request.AccountId);

            // TODO Create  custom Exception
            if (account is null)
                throw new Exception("Account not found");


            var depositToMake = new Transaction
            {
                AccountId = request.AccountId,
                Amount = request.Amount,
                Currency = request.Currency,
                MadeOn = DateTime.Now,
                TransactionType = TransactionType.Deposit,
            };

            account.Balance += request.Amount;

            _unitOfWork.AccountRepository.Edit(account);
            await _unitOfWork.TransactionRepository.AddAsync(depositToMake);
            await _unitOfWork.SaveAllAsync();

            return account.Balance;
        }

        private static  void ValidateRequest(DepositIntoAccountCommand request)
        {
            var validator = new DepositIntoAccountCommandValidator();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);
        }
    }
}
