using Bank.Interview.Application.Common;

namespace Bank.Interview.Application.Dtos
{
    public class TransactionsPaginatedDto
    {
        public Pagination Pagination { get; set; } = new Pagination();

        public IEnumerable<TransactionDto> Transactions { get; set; } = new List<TransactionDto>();
    }
}
