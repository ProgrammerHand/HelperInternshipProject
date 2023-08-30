using Helper.Application.Abstraction.Commands;
using Helper.Application.Exceptions;
using Helper.Application.Inquiry.Commands;
using Helper.Application.Integrations;
using Helper.Core.Inquiry;

namespace Helper.Application.Inquiry.Commands.Handlers
{
    public sealed class RejectInquiryHandler : ICommandHandler<RejectInquiry>
    {
        private readonly IInquiryRepository _inquiryRepo;
        private readonly IMailSendingClient _mailclient;

        public RejectInquiryHandler(IInquiryRepository inquiryRepo, IMailSendingClient mailclient)
        {
            _inquiryRepo = inquiryRepo;
            _mailclient = mailclient;
        }
        public async Task HandleAsync(RejectInquiry command)
        {
            var inquiry = await _inquiryRepo.GetByIdAsync(command.InquiryId);
            inquiry.SetRowVersion(command.RowVersion);
            inquiry.RejectInquiry();
            await _inquiryRepo.UpdateAsync(inquiry);
            var mailData = new MailDto
            {
                ReciverEmail = inquiry.Author.Email,
                ReciverName = "user", // name from User?
                Subject = $"Inquiry {inquiry.Id.Value}",
                Content = "Your Inquiry was rejected, please contact support for details"
            };
            await _mailclient.SendMailAsync(mailData);
        }
    }
}
