using Helper.Application.Abstraction.Commands;

namespace Helper.Application.Offer.Commands
{
    public sealed record SendOffer(Guid OfferId) : ICommand;
}
