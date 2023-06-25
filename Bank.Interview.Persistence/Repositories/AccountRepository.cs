using AutoMapper;
using Bank.Interview.Application.Contrats.Repositories;
using Bank.Interview.Domain.Entities;
using Bank.Interview.Persistence.Repositories.Common;

namespace Bank.Interview.Persistence.Repositories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository(BankContext bankContext, IMapper mapper) : base(bankContext, mapper)
        {

        }
    }
}
