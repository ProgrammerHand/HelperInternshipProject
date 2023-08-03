using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Helper.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecurityController : ControllerBase
    {
        [HttpPost("authorize")]
        public async Task<ActionResult> AuthorizeUser()
        {
            return Ok();//await _authorization.authorize());
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser()
        {
            return Ok();// await _registration.register());
        }
    }
}
