using Helper.Application.Abstractions;
using Helper.Application.Commands;
using Helper.Application.Commands.Handlers;
using Helper.Core.User;
using Microsoft.AspNetCore.Mvc;

namespace Helper.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecurityController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ICommandHandler<RegisterUser> _registerUserHandler;

        public SecurityController(IConfiguration configuration, ICommandHandler<RegisterUser> registerUserHandler)
        {
            _configuration = configuration;
            _registerUserHandler = registerUserHandler;
        }

        [HttpPost("authorize")]
        public async Task<ActionResult> AuthorizeUser()
        {
            return Ok();//await _authorization.authorize());
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser(RegisterUser command)
        {
            _registerUserHandler.HandleAsync(command);
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
