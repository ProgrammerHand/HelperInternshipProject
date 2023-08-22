using Helper.Application.Abstraction.Commands;
using Helper.Application.Exceptions;
using Helper.Core.Inquiry;

namespace Helper.Application.Inquiry.Commands.Handlers
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
            inquiry.SetRowVersion(command.RowVersion);
            inquiry.SetFeasibilityNote(command.Value);
            await _inquiryRepo.UpdateAsync(inquiry);
        }
    }
}
