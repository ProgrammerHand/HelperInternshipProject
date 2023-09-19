using Helper.Application.Abstraction.Commands;
using Helper.Application.Abstraction.Queries;
using Helper.Application.Integrations;
using Helper.Application.Offer.Commands;
using Helper.Application.Offer.Queries;
using Helper.Application.Solution.Commands;
using Helper.Application.Solution.Queries;
using Helper.Infrastructure.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Helper.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SolutionController : ControllerBase
    {
        private readonly IGoogleDriveClient _gdriveclient;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public SolutionController(IGoogleDriveClient gdriveclient, IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _gdriveclient = gdriveclient;
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost("/folder")]
        public async Task<ActionResult> CreateFolder(string name)
        {

            return Ok(await _gdriveclient.CreateFolder(name));
        }

        [HttpPatch("assign"), Authorize]
        [Authorize(Policy = Policies.IsWorker)]
        public async Task<ActionResult> AssingConsultant(AssignConsultant command)
        {
            await _commandDispatcher.SendAsync(command);
            return Ok();
        }

        [HttpGet("")]
        [Authorize(Policy = Policies.IsWorker)]
        public async Task<ActionResult> GetSolutions()
        {
            return Ok(await _queryDispatcher.QueryAsync(new GetSolutions()));
        }

    }
}
