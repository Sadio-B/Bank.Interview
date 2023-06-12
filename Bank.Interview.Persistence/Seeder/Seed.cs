using Bank.Interview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Interview.Persistence.Seeder
{
    public static class Seed
    {
        public static async Task InitializeData(BankContext bankContext)
        {
            var user = CreateUser();

            bankContext.Users.Add(user);

            await bankContext.SaveChangesAsync();
        }

        private static User CreateUser()
        {
            User user =  new()
            {
                FirstName = "John",
                LastName = "Doe",
                Accounts = new List<Account>()
                {
                    new Account()
                    {
                        Balance = 0,
                        currency = Currency.Euros,
                        Overdrafts = new List<Overdraft>
                    {
                       new Overdraft()
                       {
                           EndDate = DateTime.Now.AddYears(1),
                           StartDate = DateTime.Now.AddYears(-1),
                       },
                    },
                        Transactions = new List<Transaction>()
                        {
                            new Transaction()
                            {
                                Amount = 1,
                                Currency = Currency.Euros,
                                MadeOn = DateTime.Now,
                                TransactionType = TransactionType.Deposit,
                            },
                            new Transaction()
                            {
                                Amount = 2,
                                Currency = Currency.Euros,
                                MadeOn = DateTime.Now,
                                TransactionType = TransactionType.Deposit,
                            },
                            new Transaction()
                            {
                                Amount = 3,
                                Currency = Currency.Euros,
                                MadeOn = DateTime.Now,
                                TransactionType = TransactionType.Deposit,
                            },
                            new Transaction()
                            {
                                Amount = 4,
                                Currency = Currency.Euros,
                                MadeOn = DateTime.Now,
                                TransactionType = TransactionType.Deposit,
                            },
                            new Transaction()
                            {
                                Amount = 5,
                                Currency = Currency.Euros,
                                MadeOn = DateTime.Now,
                                TransactionType = TransactionType.Deposit,
                            },
                            new Transaction()
                            {
                                Amount = 6,
                                Currency = Currency.Euros,
                                MadeOn = DateTime.Now,
                                TransactionType = TransactionType.Deposit,
                            },
                            new Transaction()
                            {
                                Amount = 7,
                                Currency = Currency.Euros,
                                MadeOn = DateTime.Now,
                                TransactionType = TransactionType.Deposit,
                            },
                            new Transaction()
                            {
                                Amount = 8,
                                Currency = Currency.Euros,
                                MadeOn = DateTime.Now,
                                TransactionType = TransactionType.Deposit,
                            },
                            new Transaction()
                            {
                                Amount = 9,
                                Currency = Currency.Euros,
                                MadeOn = DateTime.Now,
                                TransactionType = TransactionType.Deposit,
                            },
                            new Transaction()
                            {
                                Amount = 10,
                                Currency = Currency.Euros,
                                MadeOn = DateTime.Now,
                                TransactionType = TransactionType.Deposit,
                            },
                            new Transaction()
                            {
                                Amount = 100,
                                Currency = Currency.Euros,
                                MadeOn = DateTime.Now,
                                TransactionType = TransactionType.Deposit,
                            },
                            new Transaction()
                            {
                                Amount = 200,
                                Currency = Currency.Euros,
                                MadeOn = DateTime.Now,
                                TransactionType = TransactionType.Deposit,
                            },
                            new Transaction()
                            {
                                Amount = 300,
                                Currency = Currency.Euros,
                                MadeOn = DateTime.Now,
                                TransactionType = TransactionType.Deposit,
                            },
                            new Transaction()
                            {
                                Amount = 400,
                                Currency = Currency.Euros,
                                MadeOn = DateTime.Now,
                                TransactionType = TransactionType.Deposit,
                            },
                            new Transaction()
                            {
                                Amount = 500,
                                Currency = Currency.Euros,
                                MadeOn = DateTime.Now,
                                TransactionType = TransactionType.Deposit,
                            },
                            new Transaction()
                            {
                                Amount = 600,
                                Currency = Currency.Euros,
                                MadeOn = DateTime.Now,
                                TransactionType = TransactionType.Deposit,
                            },
                            new Transaction()
                            {
                                Amount = 700,
                                Currency = Currency.Euros,
                                MadeOn = DateTime.Now,
                                TransactionType = TransactionType.Deposit,
                            },
                            new Transaction()
                            {
                                Amount = 800,
                                Currency = Currency.Euros,
                                MadeOn = DateTime.Now,
                                TransactionType = TransactionType.Deposit,
                            },
                            new Transaction()
                            {
                                Amount = 900,
                                Currency = Currency.Euros,
                                MadeOn = DateTime.Now,
                                TransactionType = TransactionType.Deposit,
                            },
                            new Transaction()
                            {
                                Amount = 1000,
                                Currency = Currency.Euros,
                                MadeOn = DateTime.Now,
                                TransactionType = TransactionType.Deposit,
                            },
                        }
                    }
                }
            };

            foreach (var acount in user.Accounts)
            {
                acount.Balance = CalculateBalanceAccount(acount);
            }

            return user;
        }

        private static long CalculateBalanceAccount(Account account)
        {
            long balance = account.Transactions?.Sum(transaction => transaction.Amount) ?? 0;

            return balance;
        }
    }
}
