using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Helper.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InquiriesController : ControllerBase
    {
        //private readonly ICommandHandler<InquiriStatus> _changeInquiriStatusHandler;
        //private readonly ICommandHandler<CreateInquiri> _createInquiriHandler;
        //private readonly ICommandHandler<FeasibilityNote> _setFeasibilityHandler;

        [HttpPost(""), AllowAnonymous]
        public async Task<ActionResult> CreateInquiry()//CreateInquiri command)
        {
            //await _createInquiriHandler.HandleAsync(command);
            return Ok();
        }

        [HttpPut("feasibility-note/{inquiryId}"), Authorize]
        public async Task<ActionResult> SetFeasibility()//int inquiryId, FeasibilityNote command)
        {
            //await _setFeasibilityHandler.HandleAsync(command with { inquiriId = inquiryId });
            return Ok();
        }


        [HttpPut("accepted/{inquiryId}")]
        public async Task<ActionResult> AcceptInquiry(int inquiryId)
        {
            //await _changeInquiriStatusHandler.HandleAsync(new InquiriStatus(inquiryId, true));
            return Ok();
        }

        [HttpPut("rejected/{inquiryId}")]
        public async Task<ActionResult> RejectInquiry(int inquiryId)
        {
            //await _changeInquiriStatusHandler.HandleAsync(new InquiriStatus(inquiryId, false)); ;
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
