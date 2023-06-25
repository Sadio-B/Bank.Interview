using Bank.Interview.ApiTest.Common;
using Bank.Interview.Application.Features.Operations.Commands.DepositIntoAccount;
using FluentAssertions;
using System.Text;
using System.Text.Json;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace Bank.Interview.ApiTest.Steps.DepositIntoAccount
{
    [Binding]
    [Collection("DatabaseFixture")]
    public class DepositIntoAccountStepDefinitions
    {

        private long actual;
        private long deposit;
        private readonly HttpClient _client;
        private readonly DatabaseFixture<DepositeSeeder> _fixture;

        public DepositIntoAccountStepDefinitions(DatabaseFixture<DepositeSeeder> databaseFixture)
        {
            _fixture = databaseFixture;
            _client = databaseFixture.client;
        }

        [Given(@"The following deposite")]
        public async Task GivenTheAmountDepositeIs(Table table)
        {
            var jsonDepositIntoAccountCommand = JsonSerializer.Serialize(table.CreateInstance<DepositIntoAccountCommand>());
            var httpContent = new StringContent(jsonDepositIntoAccountCommand, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/deposits/add", httpContent);
            var responseString = await response.Content.ReadAsStringAsync();
            actual = JsonSerializer.Deserialize<long>(responseString);
        }

        [When(@"the account balance is increased by ""([^""]*)""")]
        public void WhenTheAccountBalanceIsIncreasedBy(string deposit)
        {
            this.deposit = long.Parse(deposit);
        }

        [Then(@"the acount balance which was ""(.*)"" is equal to ""(.*)""")]
        public void ThenTheAcountBalanceWhichWasIsEqualTo(string initialblanceAccount, string expected)
        {
            actual.Should().Be(long.Parse(initialblanceAccount) + deposit);
            actual.Should().Be(long.Parse(expected));
        }
    }
}
