using Helper.Application.Abstraction.Commands;
using Helper.Application.Inquiry.Commands;
using Helper.Core.Inquiry;

namespace Helper.Application.Inquiry.Commands.Handlers
{
    public sealed class AcceptInquiryHandler : ICommandHandler<AcceptInquiry>
    {
        private readonly IInquiryRepository _inquiryRepo;

        public AcceptInquiryHandler(IInquiryRepository inquiryRepo)
        {
            _inquiryRepo = inquiryRepo;
        }

        public async Task HandleAsync(AcceptInquiry command)
        {
            var inquiry = await _inquiryRepo.GetByIdAsync(command.InquiriId);
            inquiry.AcceptInquiry();
            await _inquiryRepo.UpdateAsync(inquiry);
        }
    }
}
