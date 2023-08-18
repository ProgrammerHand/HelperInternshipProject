using Helper.Application.Abstraction.Commands;

namespace Helper.Application.Inquiry.Commands
{
    public sealed record SetFeasibilityNote(Guid InquiriId, string Value, Byte[] RowVersion) : ICommand;
}
