using Helper.Application.Abstractions;
using Helper.Application.Commands;
using Helper.Application.Commands.Handlers;
using Helper.Application.Security;
using Microsoft.AspNetCore.Mvc;

namespace Helper.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecurityController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ICommandHandler<RegisterUser> _registerUserHandler;
        private readonly ICommandHandler<AuthoriseUser> _authoriseUserHandler;
        private readonly ITokenManager _tokenManager;

        public SecurityController(IConfiguration configuration, ICommandHandler<RegisterUser> registerUserHandler,
            ICommandHandler<AuthoriseUser> authoriseUserHandler)
        {
            _configuration = configuration;
            _registerUserHandler = registerUserHandler;
            _authoriseUserHandler = authoriseUserHandler;
        }

        [HttpPost("authorise")]
        public async Task<ActionResult> AuthoriseUser(AuthoriseUser command)
        {
            await _authoriseUserHandler.HandleAsync(command);
            return Ok();
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser(RegisterUser command)
        {
            await _registerUserHandler.HandleAsync(command);
            return Ok();
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
