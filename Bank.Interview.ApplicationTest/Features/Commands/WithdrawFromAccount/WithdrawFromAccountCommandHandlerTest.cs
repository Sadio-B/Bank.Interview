using Bank.Interview.Application.Contrats;
using Bank.Interview.Application.Exceptions;
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
        public async Task WithdrawFromAccount_ShouldThrowValidationException_WhenAmountEquals0()
        {
            var withdrawFromAccountCommand = new WithdrawFromAccountCommand { AccountId = 1, Amount = 0, Currency = Currency.Euros };
            var withdrawFromAccountCommandHandler = new WithdrawFromAccountCommandHandler(unitOfWorkMock.Object);

            var actual = await Assert.ThrowsAnyAsync<ValidationException>(() => withdrawFromAccountCommandHandler.Handle(withdrawFromAccountCommand, CancellationToken.None));

            actual.ErrorMessages.Count.Should().Be(1);
            actual.ErrorMessages.First().Should().Be("Amount must be greater than 0");
        }

        [Fact]
        public async Task WithdrawFromAccount_ShouldThrowException_WhenAccountDoesNotExists()
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
        public async Task WithdrawFromAccount_ShouldThrowException_WhenBalanceEquals100WithdrawAmountEquals1000AndOverdraftEquals100()
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
        public async Task WithdrawFromAccount_ShouldReturn0_WhenBalanceEquals50WithdrawAmountEquals50AndOverDraftEquals0()
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
