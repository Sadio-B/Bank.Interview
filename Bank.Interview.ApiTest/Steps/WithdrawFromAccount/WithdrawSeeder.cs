using Bank.Interview.ApiTest.Common;
using Bank.Interview.Domain.Entities;
using Bank.Interview.Persistence;

namespace Bank.Interview.ApiTest.Steps.WithdrawFromAccount
{
    public class WithdrawSeeder : ISeeder
    {
        public void Seed(BankContext bankContext)
        {
            var user = InitializeUser();
            bankContext.Users.Add(user);
            bankContext.SaveChanges();
        }

        private static User InitializeUser()
        {
            return new User
            {
                FirstName = "John",
                LastName = "Doe",
                Accounts = new List<Account> {
                    new Account
                    {
                        Balance = 500L,
                        currency = Currency.Euros,
                        Transactions = new List<Transaction>
                        {
                            new Transaction
                            {
                                Amount = 200L,
                                Currency = Currency.Euros,
                                MadeOn = DateTime.Now,
                                TransactionType = TransactionType.Deposit,
                            },
                            new Transaction
                            {
                                Amount = 300L,
                                Currency = Currency.Euros,
                                MadeOn = DateTime.Now,
                                TransactionType = TransactionType.Deposit,
                            },
                        },

                    }
                }
            };
        }
    }
}
