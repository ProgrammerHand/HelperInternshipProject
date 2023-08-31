using Helper.Application.Abstraction.Events;

namespace Helper.Application.Inquiry.Events
{
    public sealed record InquiryAccepted(Guid inquiryId) : IEvent;
}
