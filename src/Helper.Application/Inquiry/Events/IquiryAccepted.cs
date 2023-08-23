using Helper.Application.Abstraction.Events;

namespace Helper.Application.Inquiry.Events
{
    public sealed record IquiryAccepted(Guid inquiryId) : IEvent;
}
