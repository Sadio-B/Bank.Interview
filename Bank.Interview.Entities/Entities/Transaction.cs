namespace Bank.Interview.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }

        public long Amount { get; set; }

        public Currency Currency { get; set; }

        public DateTime MadeOn { get; set; }

        public TransactionType TransactionType { get; set; }

        public Account? Account { get; set; }

        public long AccountId { get; set; }
    }
}
