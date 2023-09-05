using Helper.Application.Abstraction.Events;
using Helper.Core.Offer;

namespace Helper.Application.Offer.Events
{
    public sealed record OfferPaid(OfferId Id) : IEvent;
}
