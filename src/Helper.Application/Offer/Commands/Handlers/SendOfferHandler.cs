using Helper.Core.Offer;

namespace Helper.Application.Offer.Commands.Handlers
{
    public class SendOfferHandler
    {
        public readonly IOfferRepository _offerRepo;
        public SendOfferHandler(IOfferRepository offerRepo)
        {
            _offerRepo = offerRepo;
        }

        public async Task HandleAsync(SendOffer command) 
        {
            var entity = await _offerRepo.GetByIdAsync(command.Offerid);
            entity.FinalizeDraft();
            //TODO: mailsending
        }
        
    }
}
