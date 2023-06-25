namespace Bank.Interview.Domain.Entities
{
    public class User
    {
        public long Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public ICollection<Account>? Accounts { get; set; }
    }
}
