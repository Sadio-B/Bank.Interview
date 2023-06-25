using Bank.Interview.Application.Common;
using Bank.Interview.Application.Contrats.Common;
using Bank.Interview.Domain.Entities;

namespace Bank.Interview.Application.Contrats.Repositories
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        public Task<IEnumerable<Transaction>> GetTransactionsPaginatedByAccountId(long accountId, PaginationRequest paginationRequest);
    }
}
