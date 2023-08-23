using Helper.Application.Abstraction.Commands;

namespace Helper.Application.Offer.Commands.Handlers
{
    public sealed class RejectOfferHandler : ICommandHandler<RejectOffer>
    {
        public async Task HandleAsync(RejectOffer command)
        {
        }
    }
}
