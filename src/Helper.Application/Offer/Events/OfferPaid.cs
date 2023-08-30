using Helper.Application.Abstraction.Events;
using Helper.Core.Offer.ValueObjects;

namespace Helper.Application.Offer.Events
{
    public sealed record OfferPaid(OfferId Id) : IEvent;
}
