using Helper.Application.Abstraction.Commands;
using Helper.Application.Integrations;
using Helper.Core.Inquiry;
using Helper.Core.Offer;

namespace Helper.Application.Offer.Commands.Handlers
{
    public class SendOfferHandler : ICommandHandler<SendOffer>
    {
        public readonly IOfferRepository _offerRepo;
        private readonly IInquiryRepository _inquiryRepo;
        private readonly IMailSendingClient _mailClient;
        private readonly IPdfGenerator _pdfGenerator;

        public SendOfferHandler(IOfferRepository offerRepo, IInquiryRepository inquiryRepo, IMailSendingClient mailClient, IPdfGenerator pdfGenerator)
        {
            _offerRepo = offerRepo;
            _inquiryRepo = inquiryRepo;
            _mailClient = mailClient;
            _pdfGenerator = pdfGenerator;
        }

        public async Task HandleAsync(SendOffer command) 
        {
            var offer = await _offerRepo.GetByIdAsync(command.OfferId); 
            var inquiry = await _inquiryRepo.GetByIdAsync(offer.InquiryId);
            offer.FinalizeDraft();
            var documment = _pdfGenerator.GenerateOffer(inquiry, offer);
            var mailData = new MailDto
            {
                ReciverEmail = inquiry.Author.Email,
                ReciverName = "User",
                Subject = $"Inquiry {inquiry.Id.Value}", // TODO add file
                Content = "Offer Content"
            };
            //await _mailClient.SendMailAsync(mailData);
            await _offerRepo.UpdateAsync(offer);
        }
        
    }
}
