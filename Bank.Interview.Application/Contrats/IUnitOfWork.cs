using Bank.Interview.Application.Contrats.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Interview.Application.Contrats
{
    public interface IUnitOfWork
    {
        public IAccountRepository AccountRepository { get; }

        public ITransactionRepository TransactionRepository { get; }

        public IOverdraftRepository OverdraftRepository { get; }

        public IUserRepository UserRepository { get; }

        public Task SaveAllAsync();
    }
}
