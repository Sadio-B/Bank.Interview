using Bank.Interview.Application.Contrats;
using Bank.Interview.Application.Features.Operations.Commands.DepositIntoAccount;
using Bank.Interview.Application.Features.Operations.Commands.WithdrawFromAccount;
using Bank.Interview.Domain.Entities;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Interview.ApplicationTest.Features.Commands.DepositIntoAccount
{
    [TestClass]
    public class DepositIntoAccountCommandTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();

        [TestMethod]
        public async Task DepositIntoAccount_When_Account_Does_Not_Exists()
        {
            _unitOfWorkMock
                .Setup(service => service.AccountRepository.GetByIdAsync(It.IsAny<long>()))
                .ReturnsAsync((Account)null);

            var depositIntoAccountCommand = new DepositIntoAccountCommand { AccountId = 1, Amount = 50, Currency = Currency.Euros };
            var depositIntoAccountCommandHandler = new DepositIntoAccountCommandHandler(_unitOfWorkMock.Object);

            var exception = await Assert.ThrowsExceptionAsync<Exception>(() => depositIntoAccountCommandHandler.Handle(depositIntoAccountCommand, CancellationToken.None));

            exception.Message.Should().BeSameAs("Account not found");
        }

        [TestMethod]
        public async Task DepositIntoAccount_100_When_BalanceAccount_100_Return_200()
        {
            _unitOfWorkMock
                .Setup(service => service.AccountRepository.GetByIdAsync(It.IsAny<long>()))
                .ReturnsAsync(new Account { Balance = 100 });

            _unitOfWorkMock
                .Setup(service => service.TransactionRepository.AddAsync(It.IsAny<Domain.Entities.Transaction>()));

            _unitOfWorkMock
                .Setup(service => service.SaveAllAsync());

            var depositIntoAccountCommand = new DepositIntoAccountCommand { AccountId = 1, Amount = 100, Currency = Currency.Euros };
            var depositIntoAccountCommandHandler = new DepositIntoAccountCommandHandler(_unitOfWorkMock.Object);

            var actual = await depositIntoAccountCommandHandler.Handle(depositIntoAccountCommand, CancellationToken.None);

            actual.Should().Be(200);
        }
    }
}
