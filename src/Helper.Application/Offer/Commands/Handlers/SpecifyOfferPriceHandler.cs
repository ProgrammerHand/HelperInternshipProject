using Helper.Application.Abstraction;
using Helper.Application.Abstraction.Commands;
using Helper.Core.Inquiry;
using Helper.Core.Offer;

namespace Helper.Application.Offer.Commands.Handlers
{
    public sealed class SpecifyOfferPriceHandler : ICommandHandler<SpecifyOfferPrice>
    {
        private readonly IOfferRepository _offerRepo;
        private readonly IInquiryRepository _inquiryRepo;
        private readonly IDiscounter _discounter;

        public SpecifyOfferPriceHandler(IOfferRepository offerRepo, IInquiryRepository inquiryRepo, IDiscounter discounter)
        {
            _offerRepo = offerRepo;
            _inquiryRepo = inquiryRepo;
            _discounter = discounter;
        }
        public async Task HandleAsync(SpecifyOfferPrice command)
        {
            var entity = await _offerRepo.GetByIdAsync(command.OfferId);
            var userCreationTime = (await _inquiryRepo.GetByIdAsync(entity.InquiryId)).Author.CreatedAt;
            var finalPrice = _discounter.CalculateDiscount(command.price, userCreationTime);
            entity.SpecifyPrice(finalPrice);
        }
    }
}
