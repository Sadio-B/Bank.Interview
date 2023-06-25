using Bank.Interview.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bank.Interview.Persistence
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Overdraft> Overdrafts { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BankContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}
