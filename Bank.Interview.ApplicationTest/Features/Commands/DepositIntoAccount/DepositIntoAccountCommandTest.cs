using Bank.Interview.Application.Contrats;
using Bank.Interview.Application.Exceptions;
using Bank.Interview.Application.Features.Operations.Commands.DepositIntoAccount;
using Bank.Interview.Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;

namespace Bank.Interview.ApplicationTest.Features.Commands.DepositIntoAccount
{
    public class DepositIntoAccountCommandTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();

        [Fact]
        public async Task DepositIntoAccount_ShouldThrowValidationException_WhenAmountEqual0()
        {
            var depositIntoAccountCommand = new DepositIntoAccountCommand { AccountId = 1, Amount = 0, Currency = Currency.Euros };
            var depositIntoAccountCommandHandler = new DepositIntoAccountCommandHandler(_unitOfWorkMock.Object);

            var actual = await Assert.ThrowsAnyAsync<ValidationException>(() => depositIntoAccountCommandHandler.Handle(depositIntoAccountCommand, CancellationToken.None));

            actual.ErrorMessages.Count.Should().Be(1);
            actual.ErrorMessages.First().Should().Be("Amount must be greater than 0");
        }

        [Fact]
        public async Task DepositIntoAccount_ShouldThrowValidationException_WhenAccountDoesNotExists()
        {
            _unitOfWorkMock
                .Setup(service => service.AccountRepository.GetByIdAsync(It.IsAny<long>()))
                .ReturnsAsync((Account)null);

            var depositIntoAccountCommand = new DepositIntoAccountCommand { AccountId = 1, Amount = 50, Currency = Currency.Euros };
            var depositIntoAccountCommandHandler = new DepositIntoAccountCommandHandler(_unitOfWorkMock.Object);

            var exception = await Assert.ThrowsAsync<Exception>(() => depositIntoAccountCommandHandler.Handle(depositIntoAccountCommand, CancellationToken.None));

            exception.Message.Should().BeSameAs("Account not found");
        }

        [Fact]
        public async Task DepositIntoAccount_ShouldReturn200_WhenBalanceAccountEquals100AndAmountDepositEqals100()
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
