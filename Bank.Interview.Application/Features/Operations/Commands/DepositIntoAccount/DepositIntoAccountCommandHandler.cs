using Bank.Interview.Application.Contrats;
using Bank.Interview.Application.Contrats.Repositories;
using Bank.Interview.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                TransactionType = request.TransactionType,
            };

            account.Balance += request.Amount;

            _unitOfWork.AccountRepository.Edit(account);
            await _unitOfWork.TransactionRepository.AddAsync(depositToMake);
            await _unitOfWork.SaveAllAsync();

            return account.Balance;
        }
    }
}
