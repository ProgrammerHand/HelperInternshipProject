using Helper.Application.Abstraction.Commands;
using Helper.Core.Inquiry;
using Helper.Application.Exceptions;

namespace Helper.Application.Inquiry.Commands.Handlers
{
    public sealed class DeleteInquiryHandler : ICommandHandler<DeleteInquiry>
    {
        private readonly IInquiryRepository _inquiryRepo;

        public DeleteInquiryHandler(IInquiryRepository inquiryRepo)
        {
            _inquiryRepo = inquiryRepo;
        }


        public async Task HandleAsync(DeleteInquiry command)
        {
            var inquiry = await _inquiryRepo.GetByIdAsync(command.InquiryId) ?? throw new InquiryDoesntExistException();
            await _inquiryRepo.DeleteInquiry(inquiry);
        }
    }
}
