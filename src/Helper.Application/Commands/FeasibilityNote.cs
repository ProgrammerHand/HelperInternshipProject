using Helper.Application.Abstractions;

namespace Helper.Application.Commands
{
    public sealed record FeasibilityNote(int InquiriId, string Body) : ICommand;
}
