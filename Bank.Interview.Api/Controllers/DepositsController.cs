using Bank.Interview.Application.Features.Operations.Commands.DepositIntoAccount;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Interview.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class DepositsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DepositsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add", Name = "DepositIntoAccount")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<long>> DepositeIntoAccount([FromBody] DepositIntoAccountCommand depositIntoAccountCommand)
        {
            var newBalanceAccount = await _mediator.Send(depositIntoAccountCommand);

            return Ok(newBalanceAccount);
        }
    }
}
