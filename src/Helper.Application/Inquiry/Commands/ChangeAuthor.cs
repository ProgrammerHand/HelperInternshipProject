using Helper.Application.Abstraction.Commands;
using System.Text.Json.Serialization;

namespace Helper.Application.Inquiry.Commands
{
    public sealed record ChangeAuthor([property: JsonIgnore] Guid InquiryId, Guid NewAuthorId, Byte[] RowVersion) : ICommand;
}
