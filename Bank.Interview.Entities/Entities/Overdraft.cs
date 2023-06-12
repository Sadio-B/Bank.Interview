using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Interview.Domain.Entities
{
    public  class Overdraft
    {
        public int Id { get; set; }

        public long Amount { get; set; }

        public DateTime StartDate { get; set; }    

        public DateTime? EndDate { get; set; }

        public Account? Account { get; set; }

        public long AccountId { get; set; }
    }
}
