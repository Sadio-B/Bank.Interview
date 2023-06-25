using Bank.Interview.Application.Common;
using Bank.Interview.Application.Dtos;
using Bank.Interview.Application.Features.Operations.Queries.GetTransactions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Interview.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{accountId}", Name = "GetTransactions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TransactionsPaginatedDto>> GetTransactions([FromRoute] long accountId, [FromQuery] int? pageIndex, [FromQuery] int? pageSize)
        {
            var defaultPaginationRequest = new PaginationRequest();

            var transactionsPaginated = await _mediator.Send(new GetTransactionsQuery
            {
                AccountId = accountId,
                PaginationRequest = { PageIndex = pageIndex ?? defaultPaginationRequest.PageIndex, PageSize = pageSize ?? defaultPaginationRequest.PageSize }

            });

            return Ok(transactionsPaginated);
        }

    }
}
