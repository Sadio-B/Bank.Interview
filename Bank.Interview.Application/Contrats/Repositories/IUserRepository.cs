using Bank.Interview.Application.Contrats.Common;
using Bank.Interview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Interview.Application.Contrats.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
    }
}
