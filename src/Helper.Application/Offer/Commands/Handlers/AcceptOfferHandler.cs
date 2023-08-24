using Helper.Application.Abstraction.Commands;
using Helper.Application.Exceptions;
using Helper.Core.Inquiry.ValueObjects;
using Helper.Core.Offer;

namespace Helper.Application.Offer.Commands.Handlers
{
    public sealed class AcceptOfferHandler : ICommandHandler<AcceptOffer>
    {
        public readonly IOfferRepository _offerRepo;
        public AcceptOfferHandler(IOfferRepository offerRepo)
        {
            _offerRepo = offerRepo;
        }

        public async Task HandleAsync(AcceptOffer command)
        {
            var entity = await _offerRepo.GetByIdAsync(command.OfferId);
            entity.Accept();
            await _offerRepo.UpdateAsync(entity);
        }
    }
}
