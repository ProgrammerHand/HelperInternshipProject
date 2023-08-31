using Helper.Application.Abstraction.Commands;
using Helper.Application.Abstraction.Events;
using Helper.Application.Inquiry.Events;
using Helper.Application.Integrations;
using Helper.Core.Inquiry;

namespace Helper.Application.Inquiry.Commands.Handlers
{
    public sealed class AcceptInquiryHandler : ICommandHandler<AcceptInquiry>
    {
        private readonly IInquiryRepository _inquiryRepo;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IMailSendingClient _mailclient;

        public AcceptInquiryHandler(IInquiryRepository inquiryRepo, IEventDispatcher eventDispatcher, IMailSendingClient mailclient)
        {
            _inquiryRepo = inquiryRepo;
            _eventDispatcher = eventDispatcher;
            _mailclient = mailclient;
        }

        public async Task HandleAsync(AcceptInquiry command)
        {
            var inquiry = await _inquiryRepo.GetByIdAsync(command.InquiriId);
            inquiry.AcceptInquiry();
            inquiry.SetRowVersion(command.RowVersion);
            await _inquiryRepo.UpdateAsync(inquiry);
            var mailData = new MailDto
            {
                ReciverEmail = inquiry.Author.Email,
                ReciverName = "user", // name from User?
                Subject = $"Inquiry {inquiry.Id.Value}",
                Content = "Your Inquiry was acceped, please review offer in your account"
            };
            await _mailclient.SendMailAsync(mailData);
            await _eventDispatcher.PublishAsync(new InquiryAccepted(inquiry.Id));
        }
    }
}
