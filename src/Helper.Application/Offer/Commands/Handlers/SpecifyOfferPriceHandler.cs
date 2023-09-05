using Helper.Application.Abstraction;
using Helper.Application.Abstraction.Commands;
using Helper.Core.Inquiry;
using Helper.Core.Offer;
using Helper.Core.Offer.Policies;
using Helper.Core.Utility;

namespace Helper.Application.Offer.Commands.Handlers
{
    public sealed class SpecifyOfferPriceHandler : ICommandHandler<SpecifyOfferPrice>
    {
        private readonly IOfferRepository _offerRepo;
        private readonly IClockCustom _clock;

        public SpecifyOfferPriceHandler(IOfferRepository offerRepo, IClockCustom clock)
        {
            _offerRepo = offerRepo;
            _clock = clock;
        }
        public async Task HandleAsync(SpecifyOfferPrice command)
        {
            var offer = await _offerRepo.GetByIdAsync(command.OfferId);
            var finalPrice = DiscountFactory.CreateDiscount(_clock).CalculateDiscount(command.price);
            offer.SpecifyPrice(finalPrice);
            await _offerRepo.UpdateAsync(offer);
        }
    }
}
