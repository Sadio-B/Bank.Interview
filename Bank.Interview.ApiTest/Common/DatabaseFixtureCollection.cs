using Xunit;

namespace Bank.Interview.ApiTest.Common
{
    public class DatabaseFixtureCollection<TSeeder> : ICollectionFixture<DatabaseFixture<TSeeder>> where TSeeder : ISeeder, new()
    {
    }
}
