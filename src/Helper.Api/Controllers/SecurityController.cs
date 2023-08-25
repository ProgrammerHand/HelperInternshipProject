using Helper.Application.Abstraction.Commands;
using Helper.Application.Abstraction.Queries;
using Helper.Application.Security;
using Helper.Application.User.Commands;
using Helper.Infrastructure.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Helper.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecurityController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly ITokenStorageHttpContext _tokenStorage;

        public SecurityController(IConfiguration configuration, ITokenStorageHttpContext tokenStorage, ICommandDispatcher commandDispatcher)
        {
            _configuration = configuration;
            _tokenStorage = tokenStorage;
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost("authorise")]
        public async Task<ActionResult> AuthoriseUser(AuthoriseUser command)
        {
            await _commandDispatcher.SendAsync(command);
            return Ok(_tokenStorage.GetToken());
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser(RegisterUser command)
        {
            await _commandDispatcher.SendAsync(command);
            return Ok();
        }

        [HttpGet("appinfo"), Authorize]
        public async Task<ActionResult> GetAppInfo()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            //var app = AppDomain.CurrentDomain.FriendlyName;
            var app = _configuration.GetValue<string>("app:name");
            return Ok($"Envoirment: {env}; App name: {app}");
        }

        [HttpDelete("delete/{userId}"), Authorize(Policy = Policies.IsAdmin)]
        public async Task<ActionResult> DeleteUser([FromRoute(Name = "userId")] Guid userId)
        {
            await _commandDispatcher.SendAsync(new DeleteUser(userId));
            return Ok();
        }
    }
}
