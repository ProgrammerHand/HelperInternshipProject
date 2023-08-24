using Helper.Application.Abstraction.Commands;
using Helper.Core.Offer;

namespace Helper.Application.Offer.Commands.Handlers
{
    public sealed class VerifyOfferHandler : ICommandHandler<VerifyOffer>
    {
        private IOfferRepository _offerRepo;

        public VerifyOfferHandler(IOfferRepository offerRepo)
        {
            _offerRepo = offerRepo;
        }
        public async Task HandleAsync(VerifyOffer command)
        {
            var entity = await _offerRepo.GetByIdAsync(command.OfferId);
            entity.Verify();
            await _offerRepo.UpdateAsync(entity);
        }
    }
}
