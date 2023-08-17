using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Helper.Application.Commands;
using Helper.Application.Abstractions;
using Helper.Application.Queries;
using Helper.Application.DTO;
using System.Security.Claims;
using Helper.Infrastructure.JWT;
using Helper.Application.Commands.Handlers;
using Helper.Application.Abstraction;

namespace Helper.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InquiriesController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryHandler<GetInquiry, InquiryDto> _getInquiry;
        private readonly IQueryHandler<GetInquiries, List<InquiryDto>> _getInquiries;
        private readonly IQueryHandler<GetInquirySolutionVariants, InquirySolutionVariantsDto> _getInquirySolutionVariants;

        public InquiriesController(IQueryHandler<GetInquiry, InquiryDto> getInquiry,
            IQueryHandler<GetInquiries, List<InquiryDto>> getInquiries, IQueryHandler<GetInquirySolutionVariants, InquirySolutionVariantsDto> getInquirySolutionVariants,
            ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _getInquiry = getInquiry;
            _getInquiries = getInquiries;
            _getInquirySolutionVariants = getInquirySolutionVariants;
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
        public async Task<ActionResult> AcceptInquiry([FromRoute(Name = "inquiryId")] Guid inquiryId)
        {
            await _commandDispatcher.SendAsync(new AcceptInquiry(inquiryId));
            return Ok();
        }


        [HttpPut("rejected/{inquiryId}")]
        [Authorize(Policy = Policies.IsWorker)]
        public async Task<ActionResult> RejectInquiry([FromRoute(Name = "inquiryId")] Guid inquiryId)
        {
            await _commandDispatcher.SendAsync(new RejectInquiry(inquiryId));
            return Ok();
        }

        [HttpGet("")]
        [Authorize(Policy = Policies.IsWorker)]
        public async Task<ActionResult> GetInquiries()
        {
             
            return Ok(await _getInquiries.HandleAsync(new GetInquiries()));
        }

        [HttpGet("{inquiryId}")]
        public async Task<ActionResult> GetInquiry([FromRoute(Name = "inquiryId")] Guid inquiryId)
        {
            
            return Ok(await _getInquiry.HandleAsync(new GetInquiry(inquiryId)));
        }

        [HttpGet("solutions-variants")]
        public async Task<ActionResult> GetInquirySolutionVariants()
        {
            return Ok(await _getInquirySolutionVariants.HandleAsync(new GetInquirySolutionVariants()));
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
 