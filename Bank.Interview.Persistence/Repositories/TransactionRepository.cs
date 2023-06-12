using AutoMapper;
using Bank.Interview.Application.Common;
using Bank.Interview.Application.Contrats.Repositories;
using Bank.Interview.Domain.Entities;
using Bank.Interview.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Interview.Persistence.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(BankContext bankContext, IMapper mapper) : base(bankContext, mapper)
        {
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsPaginatedByAccountId(long accountId, PaginationRequest paginationRequest)
        {
           return await _bankContext.Transactions
                .Where(transaction => transaction.AccountId.Equals(accountId))
                .Skip((paginationRequest.PageIndex - 1) * paginationRequest.PageSize)
                .Take(paginationRequest.PageSize)
                .ToListAsync();
        }
    }
}
