using Helper.Application.Abstraction.Commands;
using Helper.Application.Abstraction.Queries;
using Helper.Application.Integrations;
using Helper.Application.Offer.Commands;
using Helper.Application.Offer.Queries;
using Helper.Application.ReservedEmployeeTime;
using Helper.Application.Solution.Commands;
using Helper.Application.Solution.Queries;
using Helper.Infrastructure.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Helper.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SolutionController : ControllerBase
    {
        private readonly IGoogleDriveClient _gdriveclient;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly IEmployeeReservation _employeeReservation;

        public SolutionController(IGoogleDriveClient gdriveclient, IQueryDispatcher queryDispatcher,
            ICommandDispatcher commandDispatcher, IEmployeeReservation employeeReservation)
        {
            _gdriveclient = gdriveclient;
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
            _employeeReservation = employeeReservation;
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

        [HttpGet("{solutionId}")]
        [Authorize(Policy = Policies.IsWorker)]
        public async Task<ActionResult> GetSolution([FromRoute(Name = "solutionId")] Guid solutionId)
        {
            return Ok(await _queryDispatcher.QueryAsync(new GetSolution(solutionId)));
        }

        [HttpGet("availableEmployee/{solutionId}")]
        [Authorize(Policy = Policies.IsWorker)]
        public async Task<ActionResult> GetAvailableEmployee([FromRoute(Name = "solutionId")] Guid solutionId)
        {
            return Ok(await _employeeReservation.GetAvailableEmployee(solutionId));
        }

        [HttpGet("owned")]
        [Authorize(Policy = Policies.IsWorker)]
        public async Task<ActionResult> GetOwnedSolutions()
        {
            return Ok(await _queryDispatcher.QueryAsync(new GetOwnedSolutions(Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value))));
        }

    }
}
