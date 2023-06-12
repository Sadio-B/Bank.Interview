using Bank.Interview.Application.Common;
using Bank.Interview.Application.Contrats.Common;
using Bank.Interview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Interview.Application.Contrats.Repositories
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        public Task<IEnumerable<Transaction>> GetTransactionsPaginatedByAccountId(long  accountId, PaginationRequest paginationRequest);
    }
}
