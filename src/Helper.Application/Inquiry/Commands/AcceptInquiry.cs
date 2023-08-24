using Helper.Application.Abstraction.Commands;
using Helper.Core.Inquiry.ValueObjects;
using System.Text.Json.Serialization;

namespace Helper.Application.Inquiry.Commands
{
    public sealed record AcceptInquiry([property: JsonIgnore] Guid InquiriId, Byte[] RowVersion) : ICommand;
}
