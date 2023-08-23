using Helper.Application.Abstraction.Commands;
using Helper.Application.Abstraction.Queries;
using Helper.Application.Inquiry.Commands;
using Helper.Application.Inquiry.Queries;
using Helper.Application.Offer.Commands;
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

        [HttpPost("{inquiryId}/verify"), Authorize]
        public async Task<ActionResult> Verify(VerifyOffer command)
        {
            await _commandDispatcher.SendAsync(command);
            return Ok();
        }

        [HttpPost("{inquiryId}/price"), Authorize]
        public async Task<ActionResult> SpecifyPrice(SpecifyOfferPrice command)
        {
            await _commandDispatcher.SendAsync(command);
            return Ok();
        }

        [HttpPost("/accept"), Authorize]
        public async Task<ActionResult> Accept(AcceptOffer command)
        {
            await _commandDispatcher.SendAsync(command);
            return Ok();
        }

        [HttpPost("/reject"), Authorize]
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

        [HttpGet("{inquiryId}")]
        public async Task<ActionResult> GetOffer([FromRoute(Name = "offerId")] Guid offerId)
        {

            return Ok(await _queryDispatcher.QueryAsync(new GetOffer(offerId)));
        }

    }
}
