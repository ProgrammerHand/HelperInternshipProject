using Helper.Application.Abstraction.Events;
using Helper.Core.Inquiry;
using Helper.Core.Offer;

namespace Helper.Application.Offer.Events.Handlers
{
    public class IquiryAcceptedHandler : IEventHandler<IquiryAccepted>
    {
        private readonly IInquiryRepository _inquiryRepo;
        private readonly IOfferRepository _offerRepo;

        public IquiryAcceptedHandler(IInquiryRepository inquiryRepo, IOfferRepository offerRepo)
        {
            _inquiryRepo = inquiryRepo;
            _offerRepo = offerRepo;
        }

        public async Task HandleAsync(IquiryAccepted @event)
        {
            var inquiry = await _inquiryRepo.GetByIdAsync(@event.precursorId);
            var offer = Core.Offer.Offer.CreateOffer(inquiry);
            await _offerRepo.AddAsync(offer);
        }
    }
}
