using Helper.Application.Integrations;
using Microsoft.AspNetCore.Mvc;

namespace Helper.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SolutionController : ControllerBase
    {
        private readonly IGoogleDriveClient _gdriveclient;

        public SolutionController(IGoogleDriveClient gdriveclient)
        {
            _gdriveclient = gdriveclient;
        }

        [HttpPost("/folder")]
        public async Task<ActionResult> CreateFolder(string name)
        {

            return Ok(await _gdriveclient.CreateFolder(name));
        }
    }
}
