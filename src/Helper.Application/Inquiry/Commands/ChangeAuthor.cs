using Helper.Application.Abstraction.Commands;

namespace Helper.Application.Inquiry.Commands
{
    public sealed record ChangeAuthor(Guid InquiryId, Guid NewAuthorId) : ICommand;
}
