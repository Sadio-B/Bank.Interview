using AutoMapper;
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
    public class OverdraftRepository : GenericRepository<Overdraft>, IOverdraftRepository
    {
        public OverdraftRepository(BankContext bankContext, IMapper mapper) : base(bankContext, mapper)
        {
        }

        public async Task<Overdraft?> GetApplicabletOverdraftByAccountIdAndDate(long accountId, DateTime date)
        {
            return await _bankContext.Overdrafts
                .Where(overdraft => overdraft.AccountId == accountId && overdraft.StartDate <= date && (date <= overdraft.EndDate || overdraft.EndDate == null))
                .FirstOrDefaultAsync();
        }
    }
}
