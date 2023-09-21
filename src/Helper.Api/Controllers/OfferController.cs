using Helper.Application.Abstraction.Commands;
using Helper.Application.Abstraction.Queries;
using Helper.Application.Offer.Commands;
using Helper.Application.Offer.Queries;
using Helper.Infrastructure.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Helper.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class OfferController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        public OfferController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpPatch("paymentDateSet/{offerId}")]
        [Authorize(Policy = Policies.IsWorker)]
        public async Task<ActionResult> SetPaymentDate([FromRoute(Name = "offerId")] Guid offerId, SetOfferPaymentDate command)
        {
            await _commandDispatcher.SendAsync(command with {OfferId = offerId});
            return Ok();
        }

        [HttpPatch("priceSet/{offerId}")]
        [Authorize(Policy = Policies.IsWorker)]
        public async Task<ActionResult> SpecifyPrice([FromRoute(Name = "offerId")] Guid offerId, SpecifyOfferPrice command)
        {
            await _commandDispatcher.SendAsync(command with {OfferId = offerId});
            return Ok();
        }

        [HttpPost("sent/{offerId}")]
        [Authorize(Policy = Policies.IsWorker)]
        public async Task<ActionResult> SendToClient([FromRoute(Name = "offerId")] Guid offerId)
        {
            await _commandDispatcher.SendAsync(new SendOffer(offerId));
            return Ok();
        }

        [HttpPatch("accepted"), Authorize]
        public async Task<ActionResult> Accept(AcceptOffer command)
        {
            await _commandDispatcher.SendAsync(command);
            return Ok();
        }

        [HttpPatch("rejected"), Authorize]
        public async Task<ActionResult> Reject(RejectOffer command)
        {
            await _commandDispatcher.SendAsync(command);
            return Ok();
        }

        [HttpGet("")]
        [Authorize(Policy = Policies.IsWorker)]
        public async Task<ActionResult> GetOffers()
        {
            return Ok(await _queryDispatcher.QueryAsync(new GetOffers()));
        }

        [HttpGet("owned"), Authorize]
        public async Task<ActionResult> GetOwnedOffers()
        {
            return Ok(await _queryDispatcher.QueryAsync(new GetOwnedOffers(Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value))));
        }

        [HttpGet("{offerId}"), Authorize]
        public async Task<ActionResult> GetOffer([FromRoute(Name = "offerId")] Guid offerId)
        {
            return Ok(await _queryDispatcher.QueryAsync(new GetOffer(offerId)));
        }

    }
}
