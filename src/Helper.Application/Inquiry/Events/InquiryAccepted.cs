using Helper.Application.Abstraction.Events;
using Helper.Core.Inquiry;

namespace Helper.Application.Inquiry.Events
{
    public sealed record InquiryAccepted(InquiryId inquiryId) : IEvent;
}
