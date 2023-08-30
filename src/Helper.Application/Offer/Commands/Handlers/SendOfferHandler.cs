using Helper.Application.Integrations;
using Helper.Core.Inquiry;
using Helper.Core.Offer;
using Helper.Core.User;

namespace Helper.Application.Offer.Commands.Handlers
{
    public class SendOfferHandler
    {
        public readonly IOfferRepository _offerRepo;
        private readonly IInquiryRepository _inquiryRepo;
        private readonly IMailSendingClient _mailClient;

        public SendOfferHandler(IOfferRepository offerRepo, IInquiryRepository inquiryRepo, IMailSendingClient mailClient)
        {
            _offerRepo = offerRepo;
            _inquiryRepo = inquiryRepo;
            _mailClient = mailClient;
        }

        public async Task HandleAsync(SendOffer command) 
        {
            var offer = await _offerRepo.GetByIdAsync(command.Offerid);
            var inquiry = await _inquiryRepo.GetByIdAsync(offer.InquiryId);
            offer.FinalizeDraft();
            var mailData = new MailDto
            {
                ReciverEmail = inquiry.Author.Email,
                ReciverName = "user", // name from User?
                Subject = $"Inquiry {inquiry.Id.Value}",
                Content = "Offer Content"
            };
            await _mailClient.SendMailAsync(mailData);
            //TODO: mailsending
        }
        
    }
}
