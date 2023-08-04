using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Helper.Application.Commands;
using Helper.Application.Abstractions;
using Helper.Core.Inquiry.ValueObjects;
using Helper.Application.Queries;
using Helper.Application.DTO;

namespace Helper.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InquiriesController : ControllerBase
    {
        private readonly ICommandHandler<RejectInquiry> _rejectInquirysHandler;
        private readonly ICommandHandler<AcceptInquiry> _acceptInquirysHandler;
        private readonly ICommandHandler<CreateInquiry> _createInquiryHandler;
        private readonly ICommandHandler<SetFeasibilityNote> _setFeasibilityHandler;
        private readonly IQueryHandler<GetInquiry, InquiryDto> _getInquiry;
        private readonly IQueryHandler<GetInquiries, List<InquiryDto>> _getInquiries;
        private readonly IQueryHandler<GetInquirySolutionVariants, InquirySolutionVariantsDto> _getInquirySolutionVariants;

        public InquiriesController(ICommandHandler<RejectInquiry> rejectInquirysHandler, ICommandHandler<AcceptInquiry> acceptInquirysHandler,
            ICommandHandler<CreateInquiry> createInquiryHandler,ICommandHandler<SetFeasibilityNote> setFeasibilityHandler,
            IQueryHandler<GetInquiry, InquiryDto> getInquiry, IQueryHandler<GetInquiries, List<InquiryDto>> getInquiries,
            IQueryHandler<GetInquirySolutionVariants, InquirySolutionVariantsDto> getInquirySolutionVariants)
        {
            _rejectInquirysHandler = rejectInquirysHandler;
            _acceptInquirysHandler = acceptInquirysHandler;
            _createInquiryHandler = createInquiryHandler;
            _setFeasibilityHandler = setFeasibilityHandler;
            _getInquiry = getInquiry;
            _getInquiries = getInquiries;
            _getInquirySolutionVariants = getInquirySolutionVariants;
        }

        [HttpPost(""), AllowAnonymous]
        public async Task<ActionResult> CreateInquiry(CreateInquiry command)
        {
            await _createInquiryHandler.HandleAsync(command);
            return Ok();
        }

        [HttpPut("feasibility-note/{inquiryId}")]
        public async Task<ActionResult> SetFeasibility(Guid inquiryId, SetFeasibilityNote command)
        {
            await _setFeasibilityHandler.HandleAsync(command with { InquiriId = inquiryId });
            return Ok();
        }


        [HttpPut("accepted/{inquiryId}")]
        public async Task<ActionResult> AcceptInquiry(int inquiryId)
        {
            await _acceptInquirysHandler.HandleAsync(new AcceptInquiry(inquiryId));
            return Ok();
        }

        [HttpPut("rejected/{inquiryId}")]
        public async Task<ActionResult> RejectInquiry(int inquiryId)
        {
            await _rejectInquirysHandler.HandleAsync(new RejectInquiry(inquiryId));
            return Ok();
        }

        [HttpGet(""), Authorize]
        public async Task<ActionResult> GetInquiries()
        {
             
            return Ok(await _getInquiries.HandleAsync(new GetInquiries()));
        }

        [HttpGet("{inquiriId}"), Authorize]
        public async Task<ActionResult> GetInquiry(GetInquiry querry)
        {
            
            return Ok(await _getInquiry.HandleAsync(querry));
        }

        [HttpGet("solutions-variants")]
        public async Task<ActionResult> GetInquirySolutionVariants()
        {
            return Ok(await _getInquirySolutionVariants.HandleAsync(new GetInquirySolutionVariants()));
        }
    }
}
