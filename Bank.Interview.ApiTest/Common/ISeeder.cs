using Bank.Interview.Persistence;

namespace Bank.Interview.ApiTest.Common
{
    public interface ISeeder
    {
        public void Seed(BankContext bankContext);
    }
}
