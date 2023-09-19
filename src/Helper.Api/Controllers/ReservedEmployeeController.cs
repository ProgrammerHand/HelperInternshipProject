using Helper.Application.Abstraction.Commands;
using Helper.Application.Abstraction.Queries;
using Helper.Application.ReservedEmployeeTime.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Helper.Api.Controllers
{
    [ApiController]
        [Route("api/[controller]")]
        public class ReservedEmployeeController : ControllerBase
        {
            private readonly ICommandDispatcher _commandDispatcher;
            private readonly IQueryDispatcher _queryDispatcher;


            public ReservedEmployeeController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
            {
                _commandDispatcher = commandDispatcher;
                _queryDispatcher = queryDispatcher;

            }
            [HttpPost("free")]
            public async Task<ActionResult> GetAvailableEmployee(GetAvailableEmployee querry)
            {
                return Ok(await _queryDispatcher.QueryAsync(querry));
            }
        }
}
