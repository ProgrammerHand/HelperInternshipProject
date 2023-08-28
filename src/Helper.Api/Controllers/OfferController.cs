using Helper.Application.Abstraction.Commands;
using Helper.Application.Abstraction.Queries;
using Helper.Application.Offer.Queries;
using Helper.Application.Offer.Commands;
using Helper.Infrastructure.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Helper.Infrastructure.Integrations;

namespace Helper.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class OfferController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly IGoogleDriveClient _gdriveclient;

        public OfferController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, IGoogleDriveClient gdriveclient)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
            _gdriveclient = gdriveclient;
        }

        [HttpPatch("setPaymentDate/{offerId}")]
        [Authorize(Policy = Policies.IsWorker)]
        public async Task<ActionResult> SetPaymentDate([FromRoute(Name = "offerId")] Guid offerId, SetOfferPaymentDate command)
        {
            await _commandDispatcher.SendAsync(command with {OfferId = offerId});
            return Ok();
        }

        [HttpPatch("price/{offerId}")]
        [Authorize(Policy = Policies.IsWorker)]
        public async Task<ActionResult> SpecifyPrice([FromRoute(Name = "offerId")] Guid offerId, SpecifyOfferPrice command)
        {
            await _commandDispatcher.SendAsync(command with {OfferId = offerId});
            return Ok();
        }

        [HttpPatch("/accept"), Authorize]
        public async Task<ActionResult> Accept(AcceptOffer command)
        {
            await _commandDispatcher.SendAsync(command);
            return Ok();
        }

        [HttpPut("/reject"), Authorize]
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

        [HttpGet("{offerId}"), Authorize]
        public async Task<ActionResult> GetOffer([FromRoute(Name = "offerId")] Guid offerId)
        {
            return Ok(await _queryDispatcher.QueryAsync(new GetOffer(offerId)));
        }

        [HttpPost("/folder")]
        public async Task<ActionResult> CreateFolder(string name)
        {
            await _gdriveclient.CreateFolder(name);
            return Ok();
        }
        

    }
}
