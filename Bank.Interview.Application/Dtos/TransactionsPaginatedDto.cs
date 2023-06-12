using Bank.Interview.Application.Common;
using Bank.Interview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Interview.Application.Dtos
{
    public class TransactionsPaginatedDto
    {
        public Pagination Pagination { get; set; } = new Pagination();

        public IEnumerable<TransactionDto> Transactions { get; set; } = new List<TransactionDto>();
    }
}
