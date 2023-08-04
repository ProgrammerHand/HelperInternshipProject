using Helper.Application.Abstractions;

namespace Helper.Application.Commands
{
    public sealed record SetFeasibilityNote(Guid InquiriId, string Body) : ICommand;
}
