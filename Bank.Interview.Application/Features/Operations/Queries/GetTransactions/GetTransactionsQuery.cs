using Bank.Interview.Application.Common;
using Bank.Interview.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Bank.Interview.Application.Features.Operations.Queries.GetTransactions
{
    public class GetTransactionsQuery : IRequest<TransactionsPaginatedDto>
    {
        public long AccountId { get; set; }

        public PaginationRequest PaginationRequest { get; set; } = new PaginationRequest();
    }
}
