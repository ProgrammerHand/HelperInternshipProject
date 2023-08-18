﻿using Helper.Application.Abstraction.Commands;
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

            inquiry.SetFeasibilityNote(command.Value);
            inquiry.SetRowVersion(command.RowVersion);
            await _inquiryRepo.UpdateAsync(inquiry);
        }
    }
}
