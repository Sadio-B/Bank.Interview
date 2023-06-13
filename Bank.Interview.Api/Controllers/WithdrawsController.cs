using Bank.Interview.Application.Features.Operations.Commands.WithdrawFromAccount;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Interview.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class WithdrawsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WithdrawsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add", Name = "WithdrawFromAccount")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<long>> WithdrawFromAccount([FromBody] WithdrawFromAccountCommand withdrawFromAccountCommand)
        {
            var newBalanceAccount = await _mediator.Send(withdrawFromAccountCommand);

            return Ok(newBalanceAccount);
        }
    }
}
