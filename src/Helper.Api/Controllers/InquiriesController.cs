using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Helper.Infrastructure.JWT;
using Helper.Application.Inquiry.Commands;
using Helper.Application.Inquiry.Queries;
using Helper.Application.Abstraction.Commands;
using Helper.Application.Abstraction.Queries;

namespace Helper.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InquiriesController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public InquiriesController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpPost(""), Authorize]
        public async Task<ActionResult> CreateInquiry(CreateInquiry command)
        {
            await _commandDispatcher.SendAsync(command with {AuthorId = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)});
            return Ok();
        }

        [HttpPut("feasibility-note/{inquiryId}")]
        [Authorize(Policy = Policies.IsWorker)]
        public async Task<ActionResult> SetFeasibility([FromRoute(Name = "inquiryId")] Guid inquiryId, SetFeasibilityNote command)
        {
            await _commandDispatcher.SendAsync(command with {InquiriId = inquiryId});
            return Ok();
        }


        [HttpPut("accepted/{inquiryId}")]
        [Authorize(Policy = Policies.IsWorker)]
        public async Task<ActionResult> AcceptInquiry([FromRoute(Name = "inquiryId")] Guid inquiryId, AcceptInquiry command)
        {
            await _commandDispatcher.SendAsync(command with { InquiriId = inquiryId });
            return Ok();
        }


        [HttpPut("rejected/{inquiryId}")]
        [Authorize(Policy = Policies.IsWorker)]
        public async Task<ActionResult> RejectInquiry([FromRoute(Name = "inquiryId")] Guid inquiryId, RejectInquiry command)
        {
            await _commandDispatcher.SendAsync(command with { InquiriId = inquiryId });
            return Ok();
        }

        [HttpGet("")]
        [Authorize(Policy = Policies.IsWorker)]
        public async Task<ActionResult> GetInquiries()
        {
             
            return Ok(await _queryDispatcher.QueryAsync(new GetInquiries()));
        }

        [HttpGet("{inquiryId}")]
        public async Task<ActionResult> GetInquiry([FromRoute(Name = "inquiryId")] Guid inquiryId)
        {
            
            return Ok(await _queryDispatcher.QueryAsync(new GetInquiry(inquiryId)));
        }

        [HttpGet("solutions-variants")]
        public async Task<ActionResult> GetInquirySolutionVariants()
        {
            return Ok(await _queryDispatcher.QueryAsync(new GetInquirySolutionVariants()));
        }

        [HttpPut("Author/{inquiryId}")]
        [Authorize(Policy = Policies.IsWorker)]
        public async Task<ActionResult> ChangeAuthorInquiry([FromRoute(Name = "inquiryId")] Guid inquiryId, ChangeAuthor command)
        {
            await _commandDispatcher.SendAsync(command with {InquiryId = inquiryId});
            return Ok();
        }
    }
}
 