using Helper.Application.Abstraction.Events;

namespace Helper.Application.Offer.Events
{
    public sealed record IquiryAccepted(Guid precursorId) : IEvent;
}
