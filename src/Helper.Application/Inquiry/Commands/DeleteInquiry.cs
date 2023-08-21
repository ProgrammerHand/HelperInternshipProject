using Helper.Application.Abstraction.Commands;

namespace Helper.Application.Inquiry.Commands
{
    public sealed record DeleteInquiry(Guid InquiryId) : ICommand;
}
