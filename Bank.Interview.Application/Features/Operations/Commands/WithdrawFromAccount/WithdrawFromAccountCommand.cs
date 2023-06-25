using Bank.Interview.Domain.Entities;
using MediatR;

namespace Bank.Interview.Application.Features.Operations.Commands.WithdrawFromAccount
{
    public class WithdrawFromAccountCommand : IRequest<long>
    {
        public long AccountId { get; set; }

        public long Amount { get; set; }

        public Currency Currency { get; set; }
    }
}
