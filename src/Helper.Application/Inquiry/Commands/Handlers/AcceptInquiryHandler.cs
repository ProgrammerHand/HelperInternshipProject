using Helper.Application.Abstraction.Commands;
using Helper.Application.Abstraction.Events;
using Helper.Application.Exceptions;
using Helper.Application.Inquiry.Commands;
using Helper.Application.Inquiry.Events;
using Helper.Application.Offer.Events;
using Helper.Core.Inquiry;

namespace Helper.Application.Inquiry.Commands.Handlers
{
    public sealed class AcceptInquiryHandler : ICommandHandler<AcceptInquiry>
    {
        private readonly IInquiryRepository _inquiryRepo;
        private readonly IEventDispatcher _eventDispatcher;

        public AcceptInquiryHandler(IInquiryRepository inquiryRepo, IEventDispatcher eventDispatcher)
        {
            _inquiryRepo = inquiryRepo;
            _eventDispatcher = eventDispatcher;
        }

        public async Task HandleAsync(AcceptInquiry command)
        {
            var inquiry = await _inquiryRepo.GetByIdAsync(command.InquiriId);
            if (command.RowVersion == inquiry.RowVersion)
                throw new WrongRowVersionException();
            inquiry.AcceptInquiry();
            await _inquiryRepo.UpdateAsync(inquiry);
            await _eventDispatcher.PublishAsync(new IquiryAccepted(inquiry.Id));// zmienuic na inquiryid
        }
    }
}
