using Bank.Interview.Application.Contrats;
using Bank.Interview.Application.Features.Operations.Commands.WithdrawFromAccount;
using Bank.Interview.Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;

namespace Bank.Interview.ApplicationTest.Features.Commands.WithdrawFromAccount
{
    public class WithdrawFromAccountCommandHandlerTest
    {
        private readonly Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();

        [Fact]
        public async Task WithdrawFromAccount_When_Account_Does_Not_Exists_Throw()
        {
            unitOfWorkMock
                .Setup(service => service.AccountRepository.GetByIdAsync(It.IsAny<long>()))
                .ReturnsAsync((Account)null);

            var withdrawFromAccountCommand = new WithdrawFromAccountCommand { AccountId = 1, Amount = 50, Currency = Currency.Euros };
            var withdrawFromAccountCommandHandler = new WithdrawFromAccountCommandHandler(unitOfWorkMock.Object);

            var exception = await Assert.ThrowsAsync<Exception>(() => withdrawFromAccountCommandHandler.Handle(withdrawFromAccountCommand, CancellationToken.None));

            exception.Message.Should().BeSameAs("Account not found");
        }

        [Fact]
        public async Task WithdrawFromAccount_1000_When_Balance_Equal_100_And_Overdraft_Equal_100_Return_Exception()
        {
            unitOfWorkMock
                .Setup(service => service.AccountRepository.GetByIdAsync(It.IsAny<long>()))
                .ReturnsAsync(new Account { Balance = 100 });

            unitOfWorkMock
                .Setup(service => service.OverdraftRepository.GetApplicabletOverdraftByAccountIdAndDate(It.IsAny<long>(), It.IsAny<DateTime>()))
                .ReturnsAsync(new Overdraft { Amount = 10 });

            var withdrawFromAccountCommand = new WithdrawFromAccountCommand { AccountId = 1, Amount = 1000, Currency = Currency.Euros };
            var withdrawFromAccountCommandHandler = new WithdrawFromAccountCommandHandler(unitOfWorkMock.Object);

            var exception = await Assert.ThrowsAsync<Exception>(() => withdrawFromAccountCommandHandler.Handle(withdrawFromAccountCommand, CancellationToken.None));

            exception.Message.Should().BeSameAs("Overdraft reached");
        }

        [Fact]
        public async Task WithdrawFromAccount_50_When_Balance_Equal_50_And_OverDraft_Equal_0_Return_0()
        {
            unitOfWorkMock
                .Setup(service => service.AccountRepository.GetByIdAsync(It.IsAny<long>()))
                .ReturnsAsync(new Account { Balance = 50 });

            unitOfWorkMock
                .Setup(service => service.OverdraftRepository.GetApplicabletOverdraftByAccountIdAndDate(It.IsAny<long>(), It.IsAny<DateTime>()))
                .ReturnsAsync((Overdraft)null);

            unitOfWorkMock
                .Setup(service => service.TransactionRepository.AddAsync(It.IsAny<Domain.Entities.Transaction>()));

            unitOfWorkMock
                .Setup(service => service.SaveAllAsync());

            var withdrawFromAccountCommand = new WithdrawFromAccountCommand { AccountId = 1, Amount = 50, Currency = Currency.Euros };
            var withdrawFromAccountCommandHandler = new WithdrawFromAccountCommandHandler(unitOfWorkMock.Object);

            var actual = await withdrawFromAccountCommandHandler.Handle(withdrawFromAccountCommand, CancellationToken.None);

            unitOfWorkMock.Verify(service => service.TransactionRepository.AddAsync(It.IsAny<Domain.Entities.Transaction>()), Times.Once);
            unitOfWorkMock.Verify(service => service.SaveAllAsync(), Times.Once);

            actual.Should().Be(0);

        }

        [Fact]
        public async Task WithdrawFromAccount_50_When_Balance_Equal_100_And_OverDraft_Equal10_Return_50()
        {
            unitOfWorkMock
                .Setup(service => service.AccountRepository.GetByIdAsync(It.IsAny<long>()))
                .ReturnsAsync(new Account { Balance = 100 });

            unitOfWorkMock
                .Setup(service => service.OverdraftRepository.GetApplicabletOverdraftByAccountIdAndDate(It.IsAny<long>(), It.IsAny<DateTime>()))
                .ReturnsAsync(new Overdraft { Amount = 10 });

            unitOfWorkMock
                .Setup(service => service.TransactionRepository.AddAsync(It.IsAny<Domain.Entities.Transaction>()));

            unitOfWorkMock
                .Setup(service => service.SaveAllAsync());

            var withdrawFromAccountCommand = new WithdrawFromAccountCommand { AccountId = 1, Amount = 50, Currency = Currency.Euros };
            var withdrawFromAccountCommandHandler = new WithdrawFromAccountCommandHandler(unitOfWorkMock.Object);

            var actual = await withdrawFromAccountCommandHandler.Handle(withdrawFromAccountCommand, CancellationToken.None);

            unitOfWorkMock.Verify(service => service.TransactionRepository.AddAsync(It.IsAny<Domain.Entities.Transaction>()), Times.Once);
            unitOfWorkMock.Verify(service => service.SaveAllAsync(), Times.Once);

            actual.Should().Be(50);
        }
    }
}
