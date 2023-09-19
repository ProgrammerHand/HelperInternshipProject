using Helper.Application.Abstraction.Commands;

namespace Helper.Application.Solution.Commands
{
    public sealed record AssignConsultant (Guid SolutionId, Guid UserId) : ICommand;
}
