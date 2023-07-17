using Bank.Interview.ApiTest.Common;
using Bank.Interview.Application.Features.Operations.Commands.WithdrawFromAccount;
using Bank.Interview.Domain.Entities;
using FluentAssertions;
using System.Text;
using System.Text.Json;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace Bank.Interview.ApiTest.Steps.WithdrawFromAccount
{
    [Binding]
    [Collection("DatabaseFixture")]
    public class WithdrawFromAccountStepDefinitions
    {
        private readonly HttpClient _client;
        private readonly DatabaseFixture<WithdrawSeeder> _fixture;
        private long actual;
        private long withdrawAmount;
        public WithdrawFromAccountStepDefinitions(DatabaseFixture<WithdrawSeeder> databaseFixture)
        {
            _fixture = databaseFixture;
            _client = databaseFixture.client;
        }

        [Given(@"The following withdraw")]
        public async Task GivenTheFollowingWithdraw(Table table)
        {
            var withdrawFromAccountCommand = table.CreateInstance<WithdrawFromAccountCommand>();
            var jsonWithdrawFromAccountCommand = JsonSerializer.Serialize(withdrawFromAccountCommand);
            var httpContent = new StringContent(jsonWithdrawFromAccountCommand, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"api/Accounts/withdraw", httpContent);
            var responseString = await response.Content.ReadAsStringAsync();

            actual = JsonSerializer.Deserialize<long>(responseString);
        }

        [When(@"the account balance is reduced by ""([^""]*)""")]
        public void WhenTheAccountBalanceIsReducedBy(string withdrawAmount)
        {
            this.withdrawAmount = long.Parse(withdrawAmount);
        }

        [Then(@"the new bank balance is ""([^""]*)"" compared to ""([^""]*)"" previously")]
        public void ThenTheNewBankBalanceIsComparedToPreviously(string expected, string initialblanceAccount)
        {
            actual.Should().Be(long.Parse(initialblanceAccount) - withdrawAmount);
            actual.Should().Be(long.Parse(expected));
        }

    }
}
