using Bank.Interview.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Interview.Application.Features.Operations.Commands.DepositIntoAccount
{
    public class DepositIntoAccountCommand : IRequest<long>
    {
        public long AccountId { get; set; }
        
        public long Amount { get; set; }

        public Currency  Currency { get; set; }

        public TransactionType TransactionType { get; set; }
    }
}
