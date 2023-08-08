using Humanizer.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace Helper.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecurityController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public SecurityController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

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

        [HttpGet("appinfo")]
        public async Task<ActionResult> GetAppInfo()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            //var app = AppDomain.CurrentDomain.FriendlyName;
            var app = _configuration.GetValue<string>("app:name");
            return Ok($"Envoirment: {env}; App name: {app}");
        }
    }
}
