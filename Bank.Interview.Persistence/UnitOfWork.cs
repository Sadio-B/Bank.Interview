using AutoMapper;
using Bank.Interview.Application.Contrats;
using Bank.Interview.Application.Contrats.Repositories;
using Bank.Interview.Persistence.Repositories;

namespace Bank.Interview.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BankContext _bankContext;
        private readonly IMapper _mapper;

        public IAccountRepository AccountRepository => new AccountRepository(_bankContext, _mapper);

        public ITransactionRepository TransactionRepository => new TransactionRepository(_bankContext, _mapper);

        public IOverdraftRepository OverdraftRepository => new OverdraftRepository(_bankContext, _mapper);

        public IUserRepository UserRepository => new UserRepository(_bankContext, _mapper);


        public UnitOfWork(BankContext bankContext, IMapper mapper)
        {
            this._bankContext = bankContext;
            this._mapper = mapper;
        }

        public async Task SaveAllAsync()
        {
            await _bankContext.SaveChangesAsync();
        }
    }
}
