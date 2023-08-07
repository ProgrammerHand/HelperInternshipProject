using Helper.Application.Abstractions;
using Helper.Core.Inquiry;

namespace Helper.Application.Commands.Handlers
{
    public sealed class RejectInquiryHandler : ICommandHandler<RejectInquiry>
    {
        private readonly IInquiryRepository _inquiryRepo;

        public RejectInquiryHandler(IInquiryRepository inquiryRepo)
        {
            _inquiryRepo = inquiryRepo;
        }
        public async Task HandleAsync(RejectInquiry command)
        {
            var inquiry = await _inquiryRepo.GetByIdAsync(command.InquiriId);
            inquiry.RejectInquiry();
            await _inquiryRepo.UpdateAsync(inquiry);
        }
    }
}
