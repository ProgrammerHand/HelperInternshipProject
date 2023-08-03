using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Helper.Application.Commands;
using Helper.Application.Abstractions;
using Helper.Core.ValueObjects;

namespace Helper.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InquiriesController : ControllerBase
    {
        private readonly ICommandHandler<AcceptInquiry> _rejectInquirysHandler;
        private readonly ICommandHandler<AcceptInquiry> _acceptInquirysHandler;
        private readonly ICommandHandler<CreateInquiry> _createInquiryHandler;
        private readonly ICommandHandler<FeasibilityNote> _setFeasibilityHandler;

        public InquiriesController(ICommandHandler<AcceptInquiry> rejectInquirysHandler, ICommandHandler<AcceptInquiry> acceptInquirysHandler,
            ICommandHandler<CreateInquiry> createInquiryHandler,ICommandHandler<FeasibilityNote> setFeasibilityHandler)
        {
            _rejectInquirysHandler = rejectInquirysHandler;
            _acceptInquirysHandler = acceptInquirysHandler;
            _createInquiryHandler = createInquiryHandler;
            _setFeasibilityHandler = setFeasibilityHandler;
        }

        [HttpPost(""), AllowAnonymous]
        public async Task<ActionResult> CreateInquiry(CreateInquiry command)
        {
            await _createInquiryHandler.HandleAsync(command);
            return Ok();
        }

        [HttpPut("feasibility-note/{inquiryId}"), Authorize]
        public async Task<ActionResult> SetFeasibility(int inquiryId, FeasibilityNote command)
        {
            await _setFeasibilityHandler.HandleAsync(command with { InquiriId = inquiryId });
            return Ok();
        }


        [HttpPut("accepted/{inquiryId}")]
        public async Task<ActionResult> AcceptInquiry(int inquiryId)
        {
            await _acceptInquirysHandler.HandleAsync(new AcceptInquiry(inquiryId, Status.accepted));
            return Ok();
        }

        [HttpPut("rejected/{inquiryId}")]
        public async Task<ActionResult> RejectInquiry(int inquiryId)
        {
            await _rejectInquirysHandler.HandleAsync(new AcceptInquiry(inquiryId, Status.rejected));
            return Ok();
        }

        [HttpGet(""), Authorize]
        public async Task<ActionResult> GetInquiries()
        {
            return Ok();// await _getInquiries.GetAll());
        }

        [HttpGet("{inquiriId}"), Authorize]
        public async Task<ActionResult> GetInquiry()
        {
            return Ok();// await _getInquiries.GetInquiri(inquiriId));
        }

        [HttpGet("solutions-variants"), Authorize]
        public async Task<ActionResult> GetInquirySolutionVariants(int inquiriId)
        {
            return Ok();// await _getInquiries.GetInquiriSolutionVariants(inquiriId));
        }
    }
}
