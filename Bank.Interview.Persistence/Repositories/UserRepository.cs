using AutoMapper;
using Bank.Interview.Application.Contrats.Repositories;
using Bank.Interview.Domain.Entities;
using Bank.Interview.Persistence.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Interview.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(BankContext bankContext, IMapper mapper) : base(bankContext, mapper)
        {
        }
    }
}
