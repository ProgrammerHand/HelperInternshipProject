using Helper.Application.Abstractions;
using Helper.Core.Inquiry;

namespace Helper.Application.Commands.Handlers
{
    public sealed class SetFeasibilitNoteHandler : ICommandHandler<SetFeasibilityNote>
    {
        private readonly IInquiryRepository _inquiryRepo;

        public SetFeasibilitNoteHandler(IInquiryRepository inquiryRepo)
        {
            _inquiryRepo = inquiryRepo;
        }
        public async Task HandleAsync(SetFeasibilityNote command)
        {
            var inquiry = await _inquiryRepo.GetByIdAsync(command.InquiriId);
            inquiry.SetFeasibilityNote(command.Value);
            await _inquiryRepo.UpdateAsync(inquiry);
        }
    }
}
