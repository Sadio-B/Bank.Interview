namespace Bank.Interview.Domain.Entities
{
    public class Account
    {
        public long Id { get; set; }

        public long Balance { get; set; }

        public Currency currency { get; set; }

        public User? User { get; set; }
        public long UserId { get; set; }

        public ICollection<Transaction>? Transactions { get; set; }

        public ICollection<Overdraft>? Overdrafts { get; set; }
    }
}
