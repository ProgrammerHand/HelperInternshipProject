using Helper.Application.Abstraction.Commands;
using System.Text.Json.Serialization;

namespace Helper.Application.Inquiry.Commands
{
    public sealed record SetFeasibilityNote([property:JsonIgnore]Guid InquiriId, string Value, Byte[] RowVersion) : ICommand;
}
