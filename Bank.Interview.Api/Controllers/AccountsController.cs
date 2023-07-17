using Bank.Interview.Application.Common;
using Bank.Interview.Application.Dtos;
using Bank.Interview.Application.Features.Operations.Commands.DepositIntoAccount;
using Bank.Interview.Application.Features.Operations.Commands.WithdrawFromAccount;
using Bank.Interview.Application.Features.Operations.Queries.GetTransactions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Interview.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{accountId}/transactions", Name = "GetTransactions")]
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

        [HttpPost("deposit", Name = "Deposit")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<long>> DepositeIntoAccount([FromBody] DepositIntoAccountCommand depositIntoAccountCommand)
        {
            var newBalanceAccount = await _mediator.Send(depositIntoAccountCommand);

            return Ok(newBalanceAccount);
        }

        [HttpPost("withdraw", Name = "Withdraw")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<long>> WithdrawFromAccount([FromBody] WithdrawFromAccountCommand withdrawFromAccountCommand)
        {
            var newBalanceAccount = await _mediator.Send(withdrawFromAccountCommand);

            return Ok(newBalanceAccount);
        }
    }
}
