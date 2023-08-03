using Helper.Application.Abstractions;

namespace Helper.Application.Commands
{
    public sealed record CreateInquiry(string Description, DateOnly RequestedCompletionDate, Enum SolutionVariant) : ICommand;
}
