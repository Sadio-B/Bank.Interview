using Bank.Interview.Application.Contrats.Common;
using Bank.Interview.Domain.Entities;

namespace Bank.Interview.Application.Contrats.Repositories
{
    public interface IOverdraftRepository : IGenericRepository<Overdraft>
    {
        public Task<Overdraft?> GetApplicabletOverdraftByAccountIdAndDate(long accountId, DateTime date);
    }
}
