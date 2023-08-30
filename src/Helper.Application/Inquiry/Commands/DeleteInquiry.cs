using Helper.Application.Abstraction.Commands;
using System.Text.Json.Serialization;

namespace Helper.Application.Inquiry.Commands
{
    public sealed record DeleteInquiry([property: JsonIgnore] Guid InquiryId, Byte[] RowVersion) : ICommand;
}
