using AutoMapper;
using Bank.Interview.Application.Common;
using Bank.Interview.Application.Contrats;
using Bank.Interview.Application.Features.Operations.Queries.GetTransactions;
using Bank.Interview.Application.Profiles;
using Bank.Interview.Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;

namespace Bank.Interview.ApplicationTest.Features.Queries.GetTransactions
{
    public class GetTransactionsQueryHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();

        public GetTransactionsQueryHandlerTest()
        {
            var mapperConfiguration = new MapperConfiguration(configurations =>
            {
                configurations.AddProfile<MappingProfiles>();
            });
            _mapper = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task GetTransactions_ShouldReturnATotalEquals10_WhenElementCountEquals20PageSizeEquals10AndPageInde_Equals1()
        {
            var accountId = 1L;

            var paginationRequest = new PaginationRequest { PageIndex = 1, PageSize = 10 };

            _unitOfWorkMock
                .Setup(service => service.TransactionRepository.CountAsync())
                .ReturnsAsync(20);

            _unitOfWorkMock
                .Setup(service => service.TransactionRepository.GetTransactionsPaginatedByAccountId(It.IsAny<long>(), paginationRequest))
                .ReturnsAsync(new List<Transaction>
                {
                    new Transaction { AccountId = 1 }, new Transaction { AccountId = 2 },
                    new Transaction { AccountId = 3 }, new Transaction { AccountId = 4 },
                    new Transaction { AccountId = 5 }, new Transaction { AccountId = 6 },
                    new Transaction { AccountId = 7 }, new Transaction { AccountId = 8 },
                    new Transaction { AccountId = 9 }, new Transaction { AccountId = 10 },
                });

            var getTransactionsQuery = new GetTransactionsQuery { AccountId = accountId, PaginationRequest = paginationRequest };
            var getTransactionsQueryHandler = new GetTransactionsQueryHandler(_unitOfWorkMock.Object, _mapper);

            var actual = await getTransactionsQueryHandler.Handle(getTransactionsQuery, CancellationToken.None);

            actual.Pagination.Should().BeEquivalentTo(new Pagination { ElementCount = 20, PageCount = 2, PageIndex = 1, PageSize = 10 });
            actual.Transactions.Count().Should().Be(10);
        }
    }
}
