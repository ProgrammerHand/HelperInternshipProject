using Helper.Application.Abstractions;

namespace Helper.Application.Commands
{
    public sealed record ChangeAuthor(Guid InquiryId, Guid NewAuthorId) : ICommand;
}
