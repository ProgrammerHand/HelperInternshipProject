using Helper.Application.Abstraction.Events;
using Helper.Application.Inquiry.Events;
using Helper.Core.Inquiry;
using Helper.Core.Offer;

namespace Helper.Application.Offer.Events.Handlers
{
    public class CreateOfferHandler : IEventHandler<InquiryAccepted>
    {
        private readonly IInquiryRepository _inquiryRepo;
        private readonly IOfferRepository _offerRepo;

        public CreateOfferHandler(IInquiryRepository inquiryRepo, IOfferRepository offerRepo)
        {
            _inquiryRepo = inquiryRepo;
            _offerRepo = offerRepo;
        }

        public async Task HandleAsync(InquiryAccepted @event)
        {
            var inquiry = await _inquiryRepo.GetByIdAsync(@event.inquiryId); // TODO if empty
            var offer = Core.Offer.Offer.CreateOffer(inquiry);
            await _offerRepo.AddAsync(offer);
        }
    }
}
