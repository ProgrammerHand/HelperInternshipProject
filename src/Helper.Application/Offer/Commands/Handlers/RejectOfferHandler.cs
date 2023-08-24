using Helper.Application.Abstraction.Commands;
using Helper.Application.Exceptions;
using Helper.Core.Inquiry.ValueObjects;
using Helper.Core.Offer;

namespace Helper.Application.Offer.Commands.Handlers
{
    public sealed class RejectOfferHandler : ICommandHandler<RejectOffer>
    {
        private readonly IOfferRepository _offerRepo;

        public RejectOfferHandler(IOfferRepository offerRepo)
        {
            _offerRepo = offerRepo;
        }
        public async Task HandleAsync(RejectOffer command)
        {
            var entity = await _offerRepo.GetByIdAsync(command.OfferId);
            entity.Reject(command.ClientsReason);
            await _offerRepo.UpdateAsync(entity);
        }
    }
}
