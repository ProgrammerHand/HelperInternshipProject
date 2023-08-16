using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Helper.Application.Commands;
using Helper.Application.Abstractions;
using Helper.Application.Queries;
using Helper.Application.DTO;
using System.Security.Claims;
using Helper.Infrastructure.JWT;
using Helper.Application.Commands.Handlers;

namespace Helper.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InquiriesController : ControllerBase
    {
        private readonly ICommandHandler<RejectInquiry> _rejectInquiryHandler;
        private readonly ICommandHandler<AcceptInquiry> _acceptInquiryHandler;
        private readonly ICommandHandler<CreateInquiry> _createInquiryHandler;
        private readonly ICommandHandler<SetFeasibilityNote> _setFeasibilityHandler;
        private readonly ICommandHandler<ChangeAuthor> _changeAuthorHandler;
        private readonly IQueryHandler<GetInquiry, InquiryDto> _getInquiry;
        private readonly IQueryHandler<GetInquiries, List<InquiryDto>> _getInquiries;
        private readonly IQueryHandler<GetInquirySolutionVariants, InquirySolutionVariantsDto> _getInquirySolutionVariants;

        public InquiriesController(ICommandHandler<RejectInquiry> rejectInquiryHandler, ICommandHandler<AcceptInquiry> acceptInquiryHandler,
            ICommandHandler<CreateInquiry> createInquiryHandler,ICommandHandler<SetFeasibilityNote> setFeasibilityHandler,
            ICommandHandler<ChangeAuthor> changeAuthorHandler, IQueryHandler<GetInquiry, InquiryDto> getInquiry,
            IQueryHandler<GetInquiries, List<InquiryDto>> getInquiries, IQueryHandler<GetInquirySolutionVariants, InquirySolutionVariantsDto> getInquirySolutionVariants)
        {
            _rejectInquiryHandler = rejectInquiryHandler;
            _acceptInquiryHandler = acceptInquiryHandler;
            _createInquiryHandler = createInquiryHandler;
            _setFeasibilityHandler = setFeasibilityHandler;
            _getInquiry = getInquiry;
            _getInquiries = getInquiries;
            _getInquirySolutionVariants = getInquirySolutionVariants;
            _changeAuthorHandler = changeAuthorHandler;
        }

        [HttpPost(""), Authorize]
        public async Task<ActionResult> CreateInquiry(CreateInquiry command)
        {
            await _createInquiryHandler.HandleAsync(command with {AuthorId = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)});
            return Ok();
        }

        [HttpPut("feasibility-note/{inquiryId}")]
        [Authorize(Policy = Policies.IsWorker)]
        public async Task<ActionResult> SetFeasibility([FromRoute(Name = "inquiryId")] Guid inquiryId, SetFeasibilityNote command)
        {
            await _setFeasibilityHandler.HandleAsync(command with {InquiriId = inquiryId});
            return Ok();
        }


        [HttpPut("accepted/{inquiryId}")]
        [Authorize(Policy = Policies.IsWorker)]
        public async Task<ActionResult> AcceptInquiry([FromRoute(Name = "inquiryId")] Guid inquiryId)
        {
            await _acceptInquiryHandler.HandleAsync(new AcceptInquiry(inquiryId));
            return Ok();
        }


        [HttpPut("rejected/{inquiryId}")]
        [Authorize(Policy = Policies.IsWorker)]
        public async Task<ActionResult> RejectInquiry([FromRoute(Name = "inquiryId")] Guid inquiryId)
        {
            await _rejectInquiryHandler.HandleAsync(new RejectInquiry(inquiryId));
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
            await _changeAuthorHandler.HandleAsync(command with {InquiryId = inquiryId});
            return Ok();
        }
    }
}
 