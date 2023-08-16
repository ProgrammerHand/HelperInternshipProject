using Helper.Application.Abstractions;
using Helper.Core.Inquiry.ValueObjects;

namespace Helper.Application.Commands
{
    public sealed record CreateInquiry(string Description, DateTime Start, DateTime? End, Variants SolutionVariant, Guid AuthorId) : ICommand;
}
