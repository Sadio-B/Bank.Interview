using Bank.Interview.Domain.Entities;

namespace Bank.Interview.Application.Dtos
{
    public class TransactionDto
    {
        public int Id { get; set; }

        public long Amount { get; set; }

        public Currency Currency { get; set; }

        public DateTime MadeOn { get; set; }

        public TransactionType TransactionType { get; set; }
    }
}
