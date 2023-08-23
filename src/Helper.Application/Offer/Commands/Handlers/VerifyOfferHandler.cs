using Helper.Application.Abstraction.Commands;

namespace Helper.Application.Offer.Commands.Handlers
{
    public sealed class VerifyOfferHandler : ICommandHandler<VerifyOffer>
    {
        public async Task HandleAsync(VerifyOffer command)
        {
        }
    }
}
