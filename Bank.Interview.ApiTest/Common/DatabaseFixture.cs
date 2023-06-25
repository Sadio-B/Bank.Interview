using Bank.Interview.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Interview.ApiTest.Common
{
    public class DatabaseFixture<TSeeder> : IDisposable where TSeeder : ISeeder, new()
    {
        public readonly HttpClient client;
        public readonly BankContext bankContext;
        private readonly SqliteConnection _connection;
        private readonly TSeeder seeder = new TSeeder();
        private readonly WebApplicationFactory<Program> _factory;

        public DatabaseFixture()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var bankContextOptions = new DbContextOptionsBuilder<BankContext>()
                .UseSqlite(_connection)
                .Options;

            bankContext = new BankContext(bankContextOptions);
            bankContext.Database.EnsureDeleted();
            bankContext.Database.EnsureCreated();
            seeder.Seed(bankContext);

            _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton<DbContextOptions<BankContext>>(bankContextOptions);
                });
            });

            client = _factory.CreateDefaultClient();
            client.DefaultRequestHeaders.Add("Environment", "IntegrationTests");

        }

        public void Dispose()
        {
            _connection.Close();
            _connection.Dispose();
            _factory.Dispose();
        }
    }
}